using Beamable.AccountManagement;
using Beamable.Api;
using Beamable.Api.Auth;
using Beamable.Api.Autogenerated;
using Beamable.Api.Caches;
using Beamable.Api.Connectivity;
using Beamable.Avatars;
using Beamable.Common;
using Beamable.Common.Api;
using Beamable.Common.Api.Auth;
using Beamable.Common.Api.Realms;
using Beamable.Common.Assistant;
using Beamable.Common.BeamCli;
using Beamable.Common.Content;
using Beamable.Common.Content.Validation;
using Beamable.Common.Dependencies;
using Beamable.Common.Reflection;
using Beamable.Config;
using Beamable.Console;
using Beamable.Content;
using Beamable.Editor;
using Beamable.Editor.Assistant;
using Beamable.Editor.BeamCli;
using Beamable.Editor.BeamCli.Commands;
using Beamable.Editor.Config;
using Beamable.Editor.Content;
using Beamable.Editor.Environment;
using Beamable.Editor.Modules.Account;
using Beamable.Editor.Modules.EditorConfig;
using Beamable.Editor.Modules.Hubspot;
using Beamable.Editor.Reflection;
using Beamable.Editor.Register;
using Beamable.Editor.ToolbarExtender;
using Beamable.Editor.Toolbox.Models;
using Beamable.Editor.UI;
using Beamable.Inventory.Scripts;
using Beamable.Reflection;
using Beamable.Serialization;
using Beamable.Serialization.SmallerJSON;
using Beamable.Sessions;
using Beamable.Shop;
using Beamable.Sound;
using Beamable.Theme;
using Beamable.Tournaments;
using Beamable.UI.Buss;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.VersionControl;
using UnityEditor.VspAttribution.Beamable;
using UnityEngine;
using static Beamable.Common.Constants;
using Debug = UnityEngine.Debug;
using Logger = Beamable.Common.Spew.Logger;
using Task = System.Threading.Tasks.Task;
#if UNITY_2019_3_OR_NEWER
using UnityEditor.Compilation;
#endif

namespace Beamable
{
	[BeamContextSystem]
	public static class BeamEditorDependencies
	{
		public static IDependencyBuilder DependencyBuilder;

		
		static BeamEditorDependencies()
		{
			DependencyBuilder = new DependencyBuilder();
			DependencyBuilder.AddSingleton(provider => new AccessTokenStorage(provider.GetService<BeamEditorContext>().PlayerCode));
			DependencyBuilder.AddSingleton<IPlatformRequester>(provider => new PlatformRequester(provider)
			{
				RequestTimeoutMs = $"{30 * 1000}",
				ErrorHandler = provider.CanBuildService<IPlatformRequesterErrorHandler>() ? provider.GetService<IPlatformRequesterErrorHandler>() : null
			}.RemoveConnectivityChecks()
			);
			DependencyBuilder.AddSingleton<IPlatformRequesterFactory, BeamableRequestFactory>();
			DependencyBuilder.AddSingleton(provider => provider.GetService<IPlatformRequester>() as IHttpRequester);
			DependencyBuilder.AddSingleton(provider => provider.GetService<IPlatformRequester>() as PlatformRequester);
			DependencyBuilder.AddSingleton(provider => provider.GetService<IPlatformRequester>() as IBeamableRequester);
			DependencyBuilder.AddSingleton<IRequester>(provider => provider.GetService<IPlatformRequester>());
			DependencyBuilder.AddSingleton<IEditorAuthApi>(provider => new EditorAuthService(provider.GetService<IPlatformRequester>()));
			DependencyBuilder.AddSingleton<IAuthApi>(provider => provider.GetService<IEditorAuthApi>());
			DependencyBuilder.AddSingleton<IContentIO>(provider => provider.GetService<ContentIO>());
			DependencyBuilder.AddSingleton<ContentIO>();
			DependencyBuilder.AddSingleton(provider => new ContentPublisher(provider.GetService<IPlatformRequester>(), provider.GetService<ContentIO>()));
			DependencyBuilder.AddScoped<AliasService>();
			DependencyBuilder.AddSingleton(provider => new RealmsService(provider.GetService<PlatformRequester>()));

			DependencyBuilder.AddSingleton<BeamableVsp>();
			DependencyBuilder.AddSingleton<HubspotService>();
			DependencyBuilder.AddSingleton<BeamableDispatcher>();

			DependencyBuilder.AddSingleton<IEditorHttpRequester>(provider => new BeamableEditorWebRequester());

			DependencyBuilder.AddSingleton<IWebsiteHook, WebsiteHook>();
			DependencyBuilder.AddSingleton<IToolboxViewService, ToolboxViewService>();
			DependencyBuilder.AddSingleton<OfflineCache>(p => new OfflineCache(p.GetService<IRuntimeConfigProvider>(), CoreConfiguration.Instance.UseOfflineCache));

			DependencyBuilder.AddSingleton<ServiceStorage>();
			DependencyBuilder.AddSingleton(() => BeamableEnvironment.Data);
			DependencyBuilder.AddSingleton<IPlatformRequesterHostResolver>(() => BeamableEnvironment.Data);
			DependencyBuilder.AddSingleton<EnvironmentService>();
			DependencyBuilder.AddSingleton<IConnectivityService>(() => null); // TODO: We should come up with an actual editor connectivity checker... https://disruptorbeam.atlassian.net/browse/BEAM-3487

			DependencyBuilder.AddSingleton<IValidationContext>(provider => provider.GetService<ValidationContext>());
			DependencyBuilder.AddSingleton<ValidationContext>();
			DependencyBuilder.AddSingleton<ContentDatabase>();

			DependencyBuilder.AddSingleton<ConfigDefaultsService>();

			DependencyBuilder.AddSingleton<IPlatformRequesterErrorHandler, EditorPlatformRequesterErrorHandler>();
			DependencyBuilder.AddSingleton<EditorAuthServiceFactory>();
			DependencyBuilder.AddSingleton<EditorStorageLayer>();
			DependencyBuilder.AddSingleton<IBeamableFilesystemAccessor, EditorFilesystemAccessor>();
			DependencyBuilder.AddGlobalStorage<AccountService, EditorStorageLayer>();
			DependencyBuilder.AddSingleton<IAccountService>(p => p.GetService<AccountService>());

			DependencyBuilder.AddSingleton<BeamCommands>();
			DependencyBuilder.AddGlobalStorage<BeamCommandFactory, EditorStorageLayer>();
			DependencyBuilder.AddSingleton<BeamCli>();

			DependencyBuilder.AddSingleton<SingletonDependencyList<ILoadWithContext>>();

			DependencyBuilder.AddSingleton<IRuntimeConfigProvider, EditorRuntimeConfigProvider>();

			OpenApiRegistration.RegisterOpenApis(DependencyBuilder);
		}

		[RegisterBeamableDependencies(-999, RegistrationOrigin.RUNTIME)]
		public static void RegisterRuntime(IDependencyBuilder builder)
		{
			try
			{
				var editorCtx = BeamEditorContext.Default;
				var accountService = editorCtx.ServiceScope.GetService<AccountService>();
				if (accountService != null && (accountService.Cid?.HasValue ?? false))
				{
					var provider = new EditorRuntimeConfigProvider(accountService);
					Beam.RuntimeConfigProvider ??= new DefaultRuntimeConfigProvider(provider);
					Beam.RuntimeConfigProvider.Fallback = provider;
				
					builder.ReplaceSingleton<IRuntimeConfigProvider>(Beam.RuntimeConfigProvider);
				}
			}
			catch (Exception ex)
			{
				Debug.Log(ex.Message);
			}
		}
	}

	[InitializeOnLoad, BeamContextSystem]
	public static class BeamEditor
	{
		public static CoreConfiguration CoreConfiguration { get; private set; }
		public static ReflectionCache EditorReflectionCache { get; private set; }
		public static IBeamHintGlobalStorage HintGlobalStorage { get; private set; }
		public static IBeamHintPreferencesManager HintPreferencesManager { get; private set; }
		public static bool IsInitialized { get; private set; }

		public static IDependencyBuilder BeamEditorContextDependencies;

		static BeamEditor()
		{
			Initialize();
			AssemblyReloadEvents.beforeAssemblyReload += () =>
			{
				BeamEditorContext.StopAll().Wait();
			};
		}

		static void Initialize()
		{
			if (IsInitialized) return;
			// Attempts to load all Module Configurations --- If they fail, we delay BeamEditor initialization until they don't fail.
			// The ONLY fail case is:
			//   - On first import or "re-import all", Resources and AssetDatabase don't know about the existence of these instances when this code runs for a couple of frames.
			//   - Empirically, we noticed this takes 2~3 attempts (frames) until this is done. So it's an acceptable and unnoticeable wait.
			// Doing this loading in this manner and making our windows delay their initialization until this is initialized (see BeamableAssistantWindow.OnEnable), we can
			// never have to care about this UnityEditor problem in our code that actually does things and we can have a guarantee that these will never throw.
			CoreConfiguration coreConfiguration;
			try
			{
				coreConfiguration = CoreConfiguration = CoreConfiguration.Instance;
				_ = AccountManagementConfiguration.Instance;
				_ = AvatarConfiguration.Instance;
				_ = BussConfiguration.OptionalInstance;
				_ = ConsoleConfiguration.Instance;
				_ = ContentConfiguration.Instance;
				_ = EditorConfiguration.Instance;
				_ = InventoryConfiguration.Instance;
				_ = SessionConfiguration.Instance;
				_ = ShopConfiguration.Instance;
				_ = SoundConfiguration.Instance;
				_ = ThemeConfiguration.Instance;
				_ = TournamentsConfiguration.Instance;
			}
			// Solves a specific issue on first installation of package ---
			catch (ModuleConfigurationNotReadyException)
			{
				EditorApplication.delayCall += Initialize;
				return;
			}

			// Ensures we have the latest assembly definitions and paths are all correctly setup.
			CoreConfiguration.OnValidate();

			// Apply the defined configuration for how users want to uncaught promises (with no .Error callback attached) in Beamable promises.
			if (!Application.isPlaying)
			{
				var promiseHandlerConfig = CoreConfiguration.Instance.DefaultUncaughtPromiseHandlerConfiguration;
				switch (promiseHandlerConfig)
				{
					case CoreConfiguration.EventHandlerConfig.Guarantee:
					{
						if (!PromiseBase.HasUncaughtErrorHandler)
							PromiseExtensions.RegisterBeamableDefaultUncaughtPromiseHandler();

						break;
					}
					case CoreConfiguration.EventHandlerConfig.Replace:
					case CoreConfiguration.EventHandlerConfig.Add:
					{
						PromiseExtensions.RegisterBeamableDefaultUncaughtPromiseHandler(promiseHandlerConfig == CoreConfiguration.EventHandlerConfig.Replace);
						break;
					}
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			// Reload the current environment data
			BeamableEnvironment.ReloadEnvironment();

			// If we ever get to this point, we are guaranteed to run the initialization until the end so we...
			// Initialize Editor instances of Reflection and Assistant services
			EditorReflectionCache = new ReflectionCache();
			HintGlobalStorage = new BeamHintGlobalStorage();
			HintPreferencesManager = new BeamHintPreferencesManager(new List<BeamHintHeader>()
			{
				// insert hints that should auto-block, here. At the moment, there are none!
			});

			// Load up all Asset-based IReflectionSystem (injected via ReflectionSystemObject instances). This was made to solve a cross-package injection problem.
			// It doubles as a no-code way for users to inject their own IReflectionSystem into our pipeline.
			var reflectionCacheSystemGuids = BeamableAssetDatabase.FindAssets<ReflectionSystemObject>(
				coreConfiguration.ReflectionSystemPaths
								 .Where(Directory.Exists)
								 .ToArray());

			// Get ReflectionSystemObjects and sort them
			var reflectionSystemObjects = reflectionCacheSystemGuids.Select(reflectionCacheSystemGuid =>
																	{
																		var assetPath = AssetDatabase.GUIDToAssetPath(reflectionCacheSystemGuid);
																		return AssetDatabase.LoadAssetAtPath<ReflectionSystemObject>(assetPath);
																	})
																	.Union(Resources.LoadAll<ReflectionSystemObject>("ReflectionSystems"))
																	.Where(system => system.Enabled)
																	.ToList();
			if (reflectionSystemObjects.Count < 1)
			{
				EditorApplication.delayCall += Initialize;
				return;
			}
			reflectionSystemObjects.Sort((reflectionSys1, reflectionSys2) => reflectionSys1.Priority.CompareTo(reflectionSys2.Priority));

			// Inject them into the ReflectionCache system in the correct order.
			foreach (var reflectionSystemObject in reflectionSystemObjects)
			{
				EditorReflectionCache.RegisterTypeProvider(reflectionSystemObject.TypeProvider);
				EditorReflectionCache.RegisterReflectionSystem(reflectionSystemObject.System);
			}

			// Add non-ScriptableObject-based Reflection-Cache systems into the pipeline.
			var contentReflectionCache = new ContentTypeReflectionCache();
			EditorReflectionCache.RegisterTypeProvider(contentReflectionCache);
			EditorReflectionCache.RegisterReflectionSystem(contentReflectionCache);

			// Also initializes the Reflection Cache system with it's IBeamHintGlobalStorage instance
			// (that gets propagated down to any IReflectionSystem that also implements IBeamHintProvider).
			// Finally, calls the Generate Reflection cache
			EditorReflectionCache.SetStorage(HintGlobalStorage);
			EditorReflectionCache.GenerateReflectionCache(coreConfiguration.AssembliesToSweep);

			// Hook up editor play-mode-warning feature.
			async void OnPlayModeStateChanged(PlayModeStateChange change)
			{
				if (!coreConfiguration.EnablePlayModeWarning) return;

				if (change == PlayModeStateChange.ExitingEditMode)
				{
					HintPreferencesManager.SplitHintsByPlayModeWarningPreferences(HintGlobalStorage.All, out var toWarnHints, out _);
					var hintsToWarnAbout = toWarnHints.ToList();
					if (hintsToWarnAbout.Count > 0)
					{
						var msg = string.Join("\n", hintsToWarnAbout.Select(hint => $"- {hint.Header.Id}"));

						var res = EditorUtility.DisplayDialogComplex("Beamable Assistant",
																	 "There are pending Beamable Validations.\n" + "These Hints may cause problems during runtime:\n\n" + $"{msg}\n\n" +
																	 "Do you wish to stop entering playmode and see these validations?", "Yes, I want to stop and go see validations.",
																	 "No, I'll take my chances and don't bother me about these specific hints anymore.",
																	 "No, I'll take my chances and don't bother me ever again about any hints.");

						if (res == 0)
						{
							EditorApplication.isPlaying = false;
							await BeamableAssistantWindow.Init();
						}
						else if (res == 1)
						{
							foreach (var hint in hintsToWarnAbout) HintPreferencesManager.SetHintPlayModeWarningPreferences(hint, BeamHintPlayModeWarningPreference.Disabled);
						}
						else if (res == 2)
						{
							coreConfiguration.EnablePlayModeWarning = false;
						}
					}
				}
			}

			EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
			EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

			// Set up Globally Accessible Hint System Dependencies and then call init
			foreach (var hintSystem in GetReflectionSystem<BeamHintReflectionCache.Registry>().GloballyAccessibleHintSystems)
			{
				hintSystem.SetStorage(HintGlobalStorage);
				hintSystem.SetPreferencesManager(HintPreferencesManager);

				hintSystem.OnInitialized();
			}

			// Initialize BeamEditorContext dependencies
			BeamEditorContextDependencies = BeamEditorDependencies.DependencyBuilder.Clone();
			BeamEditorContextDependencies.AddSingleton(_ => EditorReflectionCache);
			BeamEditorContextDependencies.AddSingleton(_ => HintGlobalStorage);
			BeamEditorContextDependencies.AddSingleton(_ => HintPreferencesManager);

			GetReflectionSystem<BeamReflectionCache.Registry>()
				.LoadCustomDependencies(BeamEditorContextDependencies, RegistrationOrigin.EDITOR);

			var hintReflectionSystem = GetReflectionSystem<BeamHintReflectionCache.Registry>();
			foreach (var globallyAccessibleHintSystem in hintReflectionSystem.GloballyAccessibleHintSystems)
				BeamEditorContextDependencies.AddSingleton(globallyAccessibleHintSystem.GetType(), () => globallyAccessibleHintSystem);

			// Set flag of SocialsImporter
			BeamableSocialsImporter.SetFlag();

			async Promise InitDefaultContext()
			{
				await BeamEditorContext.Default.InitializePromise;

#if BEAMABLE_DEVELOPER
				Debug.Log($"Initialized Default Editor Context [{BeamEditorContext.Default.PlayerCode}] - " +
				          $"[{BeamEditorContext.Default.ServiceScope.GetService<PlatformRequester>().Cid}] - " +
				          $"[{BeamEditorContext.Default.ServiceScope.GetService<PlatformRequester>().Pid}]");
#endif
				IsInitialized = true;

#if !DISABLE_BEAMABLE_TOOLBAR_EXTENDER
				// Initialize toolbar
				BeamableToolbarExtender.LoadToolbarExtender();
#endif
				if (SessionState.GetBool(SESSION_STATE_INSTALL_DEPS, false) && !BeamEditorContext.HasDependencies())
				{
					await BeamEditorContext.Default.CreateDependencies();
					SessionState.EraseBool(SESSION_STATE_INSTALL_DEPS);
				}
			}

			InitDefaultContext().Error(Debug.LogError);
		}

		public static T GetReflectionSystem<T>() where T : IReflectionSystem => EditorReflectionCache.GetFirstSystemOfType<T>();

		[Conditional("UNITY_EDITOR")]
		// ReSharper disable once RedundantAssignment
		public static void GetBeamHintSystem<T>(ref T foundProvider) where T : IBeamHintSystem
		{
			var hintReflectionSystem = GetReflectionSystem<BeamHintReflectionCache.Registry>();
			foundProvider = hintReflectionSystem.GloballyAccessibleHintSystems.Where(a => a is T).Cast<T>().FirstOrDefault();
		}

		[RegisterBeamableDependencies(), Conditional("UNITY_EDITOR")]
		public static void ConditionallyRegisterBeamHintsAsServices(IDependencyBuilder builder)
		{
			foreach (var hintSystemConstructor in GetReflectionSystem<BeamHintReflectionCache.Registry>().BeamContextAccessibleHintSystems)
			{
				builder.AddSingleton(hintSystemConstructor.DeclaringType, () =>
				{
					var builtObj = (IBeamHintSystem)hintSystemConstructor.Invoke(null);
					builtObj.SetPreferencesManager(HintPreferencesManager);
					builtObj.SetStorage(HintGlobalStorage);

					builtObj.OnInitialized();
					return builtObj;
				});
			}
		}

		/// <summary>
		/// Utility function to delay an initialization call (from within any of Unity's callbacks) until we have initialized our default <see cref="BeamEditorContext"/>.
		/// This must be used to wrap any logic dependent on <see cref="BeamEditorContext"/> or <see cref="BeamEditor"/> systems that is being executed from within a unity event function that initializes things.
		/// These are: OnEnable, OnValidate, OnAfterDeserialize and others like it. Essentially, this guarantees our initialization has finished running, before the given action runs.
		/// <para/>
		/// This is especially used to handle first-import cases and several other edge-cases that happen when these unity event functions are called with our windows opened. In these cases, if we don't delay
		/// our windows cases, the following issues have arisen in the past:
		/// <list type="bullet">
		/// <item><see cref="BeamEditorContext.Default"/> is null; which should be impossible, but happens (probably has to do with DomainReloads)</item>
		/// <item>The window tries to make calls to a partially initialized <see cref="BeamEditorContext"/> and throws.</item>
		/// </list>
		/// </summary>
		/// <param name="onInitializationFinished">
		/// The that must be scheduled to run from a Unity callback, but is dependent on our initialization being done.
		/// </param>
		/// <param name="forceDelay">
		/// Whether or not we should force the call to be delayed. This is used to guarantee that the callback in <see cref="BeamEditorWindow{TWindow}.OnEnable"/> is
		/// called only after the <see cref="BeamEditorWindow{TWindow}.InitializedConfig"/> was set during the <see cref="BeamEditorWindow{TWindow}.InitBeamEditorWindow"/> flow.
		/// </param>
		public static void DelayedInitializationCall(Action onInitializationFinished, bool forceDelay, BeamEditorInitializedDelayClause customDelay = null)
		{
			var hasCustomDelay = customDelay != null;
			if (!IsInitialized || forceDelay || (hasCustomDelay && customDelay()))
			{
				EditorApplication.delayCall += () => DelayedInitializationCall(onInitializationFinished, false);
				return;
			}

			onInitializationFinished?.Invoke();
		}
	}

	public delegate bool BeamEditorInitializedDelayClause();

	public class BeamEditorContext
	{
		public const string EDITOR_PLAYER_CODE_TEMPLATE = "editor.{0}.";
		private const int LOGIN_RETRY_AMOUNT = 3;

		public static Dictionary<string, BeamEditorContext> EditorContexts = new Dictionary<string, BeamEditorContext>();
		public static List<BeamEditorContext> All => EditorContexts.Values.ToList();
		public static BeamEditorContext Default => Instantiate(string.Format(EDITOR_PLAYER_CODE_TEMPLATE, "0"));
		public static BeamEditorContext ForEditorUser(int idx) => Instantiate(string.Format(EDITOR_PLAYER_CODE_TEMPLATE, idx));
		public static BeamEditorContext ForEditorUser(string code) => Instantiate(code);

		private int _loginRetries;

		public static bool ConfigFileExists { get; private set; }

		/// <summary>
		/// Create or retrieve a <see cref="BeamContext"/> for the given <see cref="PlayerCode"/>. There is only one instance of a context per <see cref="PlayerCode"/>.
		/// A <see cref="BeamableBehaviour"/> is required because the context needs to attach specific Unity components to a GameObject, and the given <see cref="BeamableBehaviour"/>'s GameObject will be used.
		/// If no <see cref="BeamableBehaviour"/> is given, then a new GameObject will be instantiated at the root transform level named, "Beamable (playerCode)"
		/// </summary>
		/// <param name="beamable">A component that will invite other Beamable components to exist on its GameObject</param>
		/// <param name="playerCode">A named code that represents a player slot on the device. The <see cref="Default"/> context uses an empty string. </param>
		/// <returns></returns>
		public static BeamEditorContext Instantiate(string playerCode = null, IDependencyBuilder dependencyBuilder = null)
		{
			dependencyBuilder = dependencyBuilder ?? BeamEditor.BeamEditorContextDependencies;
			playerCode = playerCode ?? string.Format(EDITOR_PLAYER_CODE_TEMPLATE, All.Count.ToString());

			// there should only be one context per playerCode.
			if (EditorContexts.TryGetValue(playerCode, out var existingContext))
			{
				if (existingContext.IsStopped)
				{
					existingContext.Init(playerCode, dependencyBuilder);
				}

				return existingContext;
			}

			var ctx = new BeamEditorContext();
			ctx.Init(playerCode, dependencyBuilder);
			All.Add(ctx);
			EditorContexts[playerCode] = ctx;
			return ctx;
		}

		public string PlayerCode { get; private set; }
		public bool IsStopped { get; private set; }
		public bool IsAuthenticated => ServiceScope.GetService<PlatformRequester>().Token != null;

		public IDependencyProviderScope ServiceScope { get; private set; }
		public Promise InitializePromise { get; private set; }
		public ContentIO ContentIO => ServiceScope.GetService<ContentIO>();
		public ContentDatabase ContentDatabase => ServiceScope.GetService<ContentDatabase>();
		public IPlatformRequester Requester => ServiceScope.GetService<PlatformRequester>();
		public BeamableDispatcher Dispatcher => ServiceScope.GetService<BeamableDispatcher>();
		public IAccountService EditorAccountService => ServiceScope.GetService<IAccountService>();

		public CustomerView CurrentCustomer => EditorAccount?.CustomerView;
		public RealmView CurrentRealm => EditorAccount?.CurrentRealm?.GetOrElse(() => null);
		public RealmView ProductionRealm => EditorAccount?.CurrentGame?.GetOrElse(() => null);
		public EditorUser CurrentUser => EditorAccount?.user;
		public AliasService AliasService => ServiceScope.GetService<AliasService>();
		public IEditorAuthApi AuthService => ServiceScope.GetService<IEditorAuthApi>();

		public EditorAccountInfo EditorAccount => EditorAccountService.Account; // TODO: events and setting?

		/// <summary>
		/// The permissions for the <see cref="CurrentUser"/> in the <see cref="CurrentRealm"/>.
		/// If either the user or realm are null, the <see cref="Permissions"/> will be at the lowest level.
		/// </summary>
		public UserPermissions Permissions =>
			CurrentUser?.GetPermissionsForRealm(CurrentRealm?.Pid) ?? new UserPermissions(null);

		public bool HasToken => Requester.Token != null;
		public bool HasCustomer => CurrentCustomer != null && !string.IsNullOrEmpty(CurrentCustomer.Cid);
		public bool HasRealm => CurrentRealm != null && !string.IsNullOrEmpty(CurrentRealm.Pid);

#pragma warning disable CS0067
		public event Action<RealmView> OnRealmChange;
		public event Action<CustomerView> OnCustomerChange;
		public event Action<EditorUser> OnUserChange;
#pragma warning restore CS0067

		public Action OnServiceDeleteProceed;
		public Action OnServiceArchived;
		public Action OnServiceUnarchived;

		public OptionalString RealmSecret { get; private set; } = new OptionalString();

		public void Init(string playerCode, IDependencyBuilder builder)
		{
			PlayerCode = playerCode;
			IsStopped = false;

			builder = builder.Clone();
			builder.AddSingleton(this);

			var oldScope = ServiceScope;
			ServiceScope = builder.Build();
			oldScope?.Hydrate(ServiceScope);

			ServiceScope.GetService<BeamableDispatcher>();

			// TODO: Handle faulty API
			// TODO: Handle offline?

			async Promise Initialize()
			{
				var configService = ServiceScope.GetService<ConfigDefaultsService>();
				var initResult = await EditorAccountService.TryInit();
				var account = initResult.account;
				if (!initResult.hasCid)
				{
					Requester.DeleteToken(); // not signed in... 
					return;
				}

				var requester = ServiceScope.GetService<PlatformRequester>();
				var cid = requester.Cid = EditorAccountService.Cid.GetOrThrow();

				if (account.realmPid.HasValue)
				{
					requester.Pid = account.realmPid.Value;
				}
				else if (configService.Pid.HasValue)
				{
					account.realmPid.Set(configService.Pid.Value);
					requester.Pid = configService.Pid.Value;
				}

				requester.Host = BeamableEnvironment.ApiUrl;

				ServiceScope.GetService<BeamableVsp>()
				            .TryToEmitAttribution("login"); // this will no-op if the package isn't a VSP package.

				var accessTokenStorage = ServiceScope.GetService<AccessTokenStorage>();
				var accessToken = await accessTokenStorage.LoadTokenForCustomer(cid);
				requester.Token = accessToken;

				// it is possible that the requester cid/pid have been set, but the editor account service hasn't.
				if (accessToken != null && account.HasEmptyCustomerView)
				{
					await account.UpdateRealms(requester);
					account.Refresh();
				}

				await RefreshRealmSecret();

				var _ = ServiceScope.GetService<SingletonDependencyList<ILoadWithContext>>();

			}

			InitializePromise = Initialize().ToPromise();
		}

		/// <summary>
		/// Given an alias, and an email and password, this method will
		/// set the current toolbox authorization state for the new user.
		/// </summary>
		/// <param name="aliasOrCid"></param>
		/// <param name="email"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public async Promise<Unit> LoginCustomer(string aliasOrCid, string email, string password)
		{
			var res = await AliasService.Resolve(aliasOrCid);

			var cid = res.Cid.GetOrThrow();

			var authFactory = ServiceScope.GetService<EditorAuthServiceFactory>();
			var accessTokenStorage = ServiceScope.GetService<AccessTokenStorage>();

			var authService = authFactory.CreateCidScopedAuthService(cid);
			var tokenRes = await authService.Login(email, password, mergeGamerTagToAccount: false, customerScoped: true);
			var token = new AccessToken(accessTokenStorage, cid, null, tokenRes.access_token, tokenRes.refresh_token, tokenRes.expires_in);

			await ApplyToken(cid, token);
			await RefreshRealmSecret();
			return PromiseBase.Unit;
		}


		private async Promise ApplyToken(string cid, AccessToken token)
		{
			var info = await EditorAccountService.Login(cid, token);
			await token.SaveAsCustomerScoped();
			Requester.Token = token;

			OnUserChange?.Invoke(CurrentUser);
		}

		[Obsolete("this method is no longer supported, and will be removed in a future release")]
		public Promise Login(string email, string password, string pid = null)
		{
			Debug.LogWarning("The " + nameof(Login) + " method is no longer supported. It will not do anything.");
			return Promise.Success;
		}

		[Obsolete("this method is no longer supported, and will be removed in a future release")]
		public async Promise Relogin()
		{
			var accessTokenStorage = ServiceScope.GetService<AccessTokenStorage>();
			var currentToken = Requester.Token;
			var expiresIn = (long)(currentToken.ExpiresAt - DateTime.UtcNow).TotalMilliseconds;
			var newToken = new AccessToken(accessTokenStorage, Requester.Cid, null, currentToken.Token,
										   currentToken.RefreshToken, expiresIn);
			await Login(newToken);
		}

		[Obsolete("this method is no longer supported, and will be removed in a future release")]
		public Promise Login(AccessToken token, string pid = null)
		{
			Debug.LogWarning("The " + nameof(Login) + " method is no longer supported. It will not do anything.");
			return Promise.Success;
		}

		/// <summary>
		/// Erase the toolbox's authorization. 
		/// </summary>
		/// <param name="clearRealmPid">
		/// If true, the local PID will be erased. This should not change the config-defaults PID.
		/// </param>
		public void Logout(bool clearRealmPid)
		{
			EditorAccountService.Logout(clearRealmPid);
			OnUserChange?.Invoke(null);
			BeamableEnvironment.ReloadEnvironment();
		}

		public static void WriteConfig(string alias, string pid, string host = null, string cid = "")
		{
			BeamEditorContext.Default.ServiceScope
							 .GetService<ConfigDefaultsService>()
							 .SaveConfig(alias, cid, pid);
		}

		public async Promise LoadConfig()
		{
			var needsLogout = await EditorAccountService.SwitchToConfigDefaults();
			if (needsLogout)
			{
				Logout(false);
			}
		}

		/// <summary>
		/// Save the current toolbox CID/PID data in the config-defaults.txt file.
		/// </summary>
		public void WriteConfig()
		{
			var alias = EditorAccount.CustomerView.Alias;
			var cid = EditorAccount.CustomerView.Cid;
			var pid = EditorAccount.realmPid.Value;
			WriteConfig(alias, pid, cid: cid);
		}

		[Obsolete]
		public void SaveConfig(string alias, string pid, string host = null, string cid = "")
		{
			if (string.IsNullOrEmpty(host))
			{
				host = BeamableEnvironment.ApiUrl;
			}

			if (!string.IsNullOrEmpty(pid))
			{
				ConfigDatabase.SetString(Features.Config.PID_KEY, pid, createField: true);
			}

			WriteConfig(alias, pid, host, cid);
			// Initialize the requester configuration data so we can attempt a login.
			var requester = ServiceScope.GetService<PlatformRequester>();
			requester.Cid = cid;
			requester.Pid = pid;
			requester.Host = host;
		}

		[Obsolete]
		private static string GetCustomContainerPrefix()
		{
			return ConfigDatabase.TryGetString("containerPrefix", out var customPrefix) ? customPrefix : null;
		}

		#region Customer & User Creation and Management

		private async Promise SendHubspotRegistrationEvent(string email, string alias)
		{
			try
			{
				await ServiceScope.GetService<HubspotService>().SubmitRegistrationEvent(email, alias);
			}
			catch (Exception hubspotError)
			{
				Debug.LogWarning(
					$"Hubspot registration event failed. type=[{hubspotError?.GetType()}] message=[{hubspotError?.Message}]");
			}
		}

		public async Promise CreateCustomer(string alias, string gameName, string email, string password)
		{
			var customerResponse = await AuthService.RegisterCustomer(email, password, gameName, alias, alias);
			await SendHubspotRegistrationEvent(email, alias);

			var cid = customerResponse.cid.ToString();
			var pid = customerResponse.pid;
			var tokenResponse = customerResponse.token;
			var accessTokenStorage = ServiceScope.GetService<AccessTokenStorage>();
			var token = new AccessToken(accessTokenStorage, cid, pid, tokenResponse.access_token,
										tokenResponse.refresh_token, tokenResponse.expires_in);

			await ApplyToken(cid, token);
		}

		public async Promise SendPasswordReset(string cidOrAlias, string email)
		{
			var aliasService = ServiceScope.GetService<AliasService>();
			var res = await aliasService.Resolve(cidOrAlias);
			var cid = res.Cid.GetOrThrow();
			Requester.Cid = cid;
			var authService = ServiceScope.GetService<IEditorAuthApi>();
			await authService.IssuePasswordUpdate(email);
		}

		public async Promise SendPasswordResetCode(string code, string newPassword)
		{
			var authService = ServiceScope.GetService<IEditorAuthApi>();
			await authService.ConfirmPasswordUpdate(code, newPassword).ToUnit();
		}

		/// <summary>
		/// Force a publish operation, with no validation, with no UX popups. Log output will occur.
		/// </summary>
		/// <param name="force">Pass true to force all content to publish. Leave as false to only publish changed content.</param>
		/// <returns>A Promise of Unit representing the completion of the publish.</returns>
		private async Promise DoSilentContentPublish(bool force = false)
		{
			var contentPublisher = ServiceScope.GetService<ContentPublisher>();
			var clearPromise = force ? contentPublisher.ClearManifest() : Promise<Unit>.Successful(PromiseBase.Unit);
			await clearPromise;

			var publishSet = await contentPublisher.CreatePublishSet();
			await contentPublisher.Publish(publishSet, progress => { });

			var contentIO = ServiceScope.GetService<ContentIO>();
			await contentIO.FetchManifest();
		}

		#endregion

		#region Game & Realm Switching

		public Promise<string> GetRealmSecret()
		{
			// TODO this will only work if the current user is an admin.
			return Requester.Request<CustomerResponse>(Method.GET, "/basic/realms/admin/customer").Map(resp =>
			{
				var matchingProject = resp.customer.projects.FirstOrDefault(p => p.name.Equals(CurrentRealm.Pid));
				return matchingProject?.secret ?? "";
			});
		}

		public async Promise SetGame(RealmView game)
		{
			if (game == null) throw new Exception("Cannot set game to null");

			string realmPid = EditorAccount.GetRealmPidForGame(game);
			await SwitchRealm(game, realmPid);
			await EditorAccount.UpdateRealms(Requester);
		}

		public Promise SwitchRealm(RealmView realm)
		{
			return SwitchRealm(realm.FindRoot(), realm?.Pid);
		}

		public async Promise<OptionalString> ResetRealmSecret()
		{
			await RefreshRealmSecret();
			return RealmSecret;
		}

		public async Promise RefreshRealmSecret()
		{
			try
			{
				if (CurrentRealm == null) return;
				var secret = await GetRealmSecret();
				RealmSecret.SetValue(secret);
			}
			catch
			{
				// this is expected to fail if the user doesn't have permission to get the realm secret.
				RealmSecret.Clear();
			}
		}


		public async Promise SwitchRealm(RealmView game, string realmPid)
		{
			var oldRealmPid = Requester.Pid;
			if (oldRealmPid == realmPid) return; // bail early; nothing to do.

			EditorAccountService.SetRealm(EditorAccount, game, realmPid);
			Requester.Cid = EditorAccount.cid;
			Requester.Pid = realmPid;

			await ContentIO.FetchManifest();
			ContentDatabase.RecalculateIndex();
			await RefreshRealmSecret();

			EditorAccountService.WriteUnsetConfigValues();
			OnRealmChange?.Invoke(CurrentRealm);
		}


		#endregion

		#region TMP & Addressables Dependencies Check

#if BEAMABLE_DEVELOPER
		[MenuItem(MenuItems.Windows.Paths.MENU_ITEM_PATH_WINDOW_BEAMABLE_UTILITIES_BEAMABLE_DEVELOPER + "/Force Refresh Content (New)")]
		public static void ForceRefreshContent()
		{
			var contentIO = Default.ServiceScope.GetService<ContentIO>();
			// Do these in parallel to simulate startup behavior.
			_ = contentIO.BuildLocalManifest();
			_ = Default.CreateDependencies().GetResult();
		}
#endif

		public static bool HasDependencies()
		{
			var hasAddressables = AddressableAssetSettingsDefaultObject.GetSettings(false) != null;
			var hasTextmeshPro = TextMeshProImporter.EssentialsLoaded;

			return hasAddressables && hasTextmeshPro;
		}

		public async Promise CreateDependencies()
		{
			// import addressables...
			AddressableAssetSettingsDefaultObject.GetSettings(true);

			var contentIO = Default.ServiceScope.GetService<ContentIO>();
			await TextMeshProImporter.ImportEssentials();

			AssetDatabase.Refresh();
			contentIO.EnsureAllDefaultContent();

			ConfigManager.Initialize();

			if (IsAuthenticated)
			{
				var serverManifest = await contentIO.OnManifest;
				var hasNoContent = serverManifest.References.Count == 0;
				if (hasNoContent)
					await DoSilentContentPublish();
				else
					await PromiseBase.SuccessfulUnit;
			}
		}

		#endregion

		public static async Task StopAll()
		{
			foreach (var ctx in All)
			{
				await ctx.Stop();
			}
		}

		private async Promise Stop()
		{
			IsStopped = true;
			await ServiceScope.Dispose();
		}
	}

	[Serializable]
	public class ConfigData
	{
		public string cid;
		public string alias;
		public string pid;

		[Obsolete("This will be removed in a future version of Beamable. Use " + nameof(BeamableEnvironment) + "." + nameof(BeamableEnvironment.ApiUrl) + " instead.")]
		public string platform;
		[Obsolete("This will be removed in a future version of Beamable. Use " + nameof(BeamableEnvironment) + "." + nameof(BeamableEnvironment.SocketUrl) + " instead.")]
		public string socket;
		[Obsolete("This will be removed in a future version of Beamable.")]
		public string containerPrefix;
	}

	[Serializable]
	public class CustomerResponse
	{
		public CustomerDTO customer;
	}

	[Serializable]
	public class CustomerDTO
	{
		public List<ProjectDTO> projects;
	}

	[Serializable]
	public class ProjectDTO
	{
		public string name;
		public string secret;
	}
}
