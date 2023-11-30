using Beamable.Common;
using Beamable.Config;
using Beamable.Editor.Environment;
using Beamable.Editor.UI.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Beamable.Server.Editor.DockerCommands
{
	public class RunStorageCommand : RunImageCommand
	{
		private readonly StorageObjectDescriptor _storage;

		public const string ENV_MONGO_INITDB_ROOT_USERNAME = "MONGO_INITDB_ROOT_USERNAME";
		public const string ENV_MONGO_INITDB_ROOT_PASSWORD = "MONGO_INITDB_ROOT_PASSWORD";

		public RunStorageCommand(StorageObjectDescriptor storage)
		   : base(storage.ImageName, storage.ContainerName, storage)
		{
			_storage = storage;
			var config = MicroserviceConfiguration.Instance.GetStorageEntry(storage.Name);
			Environment = new Dictionary<string, string>
			{
				[ENV_MONGO_INITDB_ROOT_USERNAME] = config.LocalInitUser,
				[ENV_MONGO_INITDB_ROOT_PASSWORD] = config.LocalInitPass,
			};

			Ports = new Dictionary<uint, uint>
			{
				[config.LocalDataPort] = 27017
			};

			NamedVolumes = new Dictionary<string, string>
			{
				[storage.DataVolume] = "/data/db", // the actual data of the database.
				[storage.FilesVolume] = "/beamable" //
			};
		}

		protected override void HandleStandardOut(string data)
		{
			if (!MicroserviceLogHelper.HandleMongoLog(_storage, data))
			{
				base.HandleStandardOut(data);
			}
			OnStandardOut?.Invoke(data);
		}
		protected override void HandleStandardErr(string data)
		{
			if (!MicroserviceLogHelper.HandleMongoLog(_storage, data, LogLevel.INFO, true))
			{
				base.HandleStandardErr(data);
			}
			OnStandardErr?.Invoke(data);
		}
	}

	public class RunStorageToolCommand : RunImageCommand
	{
		public const string ENV_CODE_THEME = "ME_CONFIG_OPTIONS_EDITORTHEME";
		public const string ENV_MONGO_SERVER = "ME_CONFIG_MONGODB_URL";
		public const string ENV_ME_CONFIG_MONGODB_ENABLE_ADMIN = "ME_CONFIG_MONGODB_ENABLE_ADMIN";
		public const string ENV_ME_CONFIG_SITE_COOKIESECRET = "ME_CONFIG_SITE_COOKIESECRET";
		public const string ENV_ME_CONFIG_SITE_SESSIONSECRET = "ME_CONFIG_SITE_SESSIONSECRET";
		public const string RUNNING_STRING = "Mongo Express server listening at http://0.0.0.0:8081";

		public readonly string[] FAILURE_STRINGS = new string[]
		{
		 "AuthenticationFailed",
		 "UnhandledPromiseRejectionWarning",
		 "process with a non-zero exit code"
		};

		public Promise<bool> IsAvailable { get; private set; } = new Promise<bool>();

		public RunStorageToolCommand(StorageObjectDescriptor storage)
		: base(storage.ToolImageName, storage.LocalToolContainerName, storage)
		{
			var config = MicroserviceConfiguration.Instance.GetStorageEntry(storage.Name);
			Environment = new Dictionary<string, string>
			{
				[ENV_CODE_THEME] = "rubyblue",
				[ENV_ME_CONFIG_MONGODB_ENABLE_ADMIN] = "true",
				[ENV_ME_CONFIG_SITE_COOKIESECRET] = Guid.NewGuid().ToString(),
				[ENV_ME_CONFIG_SITE_SESSIONSECRET] = Guid.NewGuid().ToString(),
				[ENV_MONGO_SERVER] = $"mongodb://{config.LocalInitUser}:{config.LocalInitPass}@gateway.docker.internal:{config.LocalDataPort}"
			};
			Ports = new Dictionary<uint, uint>
			{
				[config.LocalUIPort] = 8081
			};

			OnStandardErr += OnMessage;
			OnStandardOut += OnMessage;
		}

		protected override void HandleOnExit()
		{
			if (_exitCode != 0)
			{
				IsAvailable?.CompleteSuccess(false);
			}
			base.HandleOnExit();
		}

		private void OnMessage(string message)
		{
			if (message == null) return;
			if (message.Contains(RUNNING_STRING))
			{
				IsAvailable?.CompleteSuccess(true);
			}
			else if (FAILURE_STRINGS.Any(message.Contains))
			{
				IsAvailable?.CompleteSuccess(false);
			}
		}
	}

	public class RunClientGenerationCommand : RunImageCommand
	{
		private readonly MicroserviceDescriptor _service;

		public RunClientGenerationCommand(MicroserviceDescriptor service)
		: base(service.ImageName, service.ContainerName, service)
		{
			_service = service;
			if (!PackageUtil.DoesFileExistLocally(service.AttributePath))
			{
				Debug.LogWarning($"Cannot autogenerate client code for service=[{service.Name}], because the service code does not exist locally.");
			}
			var buildPath = _service.BuildPath;
			var fullBuildPath = Path.GetFullPath(buildPath);
			var clientPath = Path.GetFullPath(service.AutoGeneratedClientPath);
			var rawSourcePath = Path.GetDirectoryName(Path.GetFullPath(_service.AttributePath));
			var asmName = _service.ConvertToInfo().Name;

			PruneAfterStartup = true;
			BindMounts = new List<BindMount>
			{
				new BindMount {isReadOnly = false, src = clientPath, dst = "/client-output"},
				new BindMount {isReadOnly = true, src = fullBuildPath, dst = $"/subapp/{_service.ImageName}"},
				new BindMount
				{
					isReadOnly = true, src = rawSourcePath, dst = $"/subapp/{_service.ImageName}/{asmName}"
				},
			};
		}

		protected override string GetCustomDockerFlags()
		{
			return $"--entrypoint dotnet";
		}

		protected override string GetArgString()
		{
			return $"watch -- run generate-client /client-output";
		}
	}

	public class RunServiceCommand : RunImageCommand
	{
		private readonly MicroserviceDescriptor _service;
		private readonly bool _watch;
		public const string ENV_CID = "CID";
		public const string ENV_PID = "PID";
		public const string ENV_SECRET = "SECRET";
		public const string ENV_HOST = "HOST";
		public const string ENV_LOG_LEVEL = "LOG_LEVEL";
		public const string ENV_NAME_PREFIX = "NAME_PREFIX";
		public const string ENV_WATCH_TOKEN = "WATCH_TOKEN";
		public const string ENV_DISABLE_RUN_CUSTOM_HOOK = "DISABLE_CUSTOM_INITIALIZATION_HOOKS";
		public const string ENV_DISABLE_EMOJI = "DOTNET_WATCH_SUPPRESS_EMOJIS";
		public const string ENV_DISABLE_LOG_TRUNCATE = "DISABLE_LOG_TRUNCATE";
		public const string ENV_CPU_RATE_LIMIT_LOW = "WS_RATE_LIMIT_CPU_MULT_LOW";
		public const string ENV_CPU_RATE_LIMIT_HIGH = "WS_RATE_LIMIT_CPU_MULT_HIGH";
		public const string ENV_BEAM_INSTANCE_COUNT = "BEAM_INSTANCE_COUNT";

		protected override bool CaptureStandardBuffers => true;


		public RunServiceCommand(MicroserviceDescriptor service,
								 string cid,
								 string pid,
								 string secret,
								 Dictionary<string, string> env,
								 bool watch = true,
								 bool shouldRunCustomHooks = true) : base(service.ImageName, service.ContainerName, service)
		{
			if (watch && !service.IsSourceCodeAvailableLocally())
			{
				watch = false;
			}
			_service = service;
			_watch = watch;
			PruneAfterStartup = true;
			Environment = new Dictionary<string, string>()
			{
				[ENV_CID] = cid,
				[ENV_PID] = pid,
				[ENV_SECRET] = secret,
				[ENV_HOST] = BeamableEnvironment.SocketUrl,
				[ENV_LOG_LEVEL] = "Debug",
				[ENV_NAME_PREFIX] = MicroserviceIndividualization.Prefix,
				[ENV_WATCH_TOKEN] = watch.ToString(),
				[ENV_DISABLE_RUN_CUSTOM_HOOK] = (!shouldRunCustomHooks).ToString(),
				[ENV_DISABLE_EMOJI] = "1",
				[ENV_DISABLE_LOG_TRUNCATE] = "true",
				[ENV_CPU_RATE_LIMIT_LOW] = "0",
				[ENV_CPU_RATE_LIMIT_HIGH] = "0",
				[ENV_BEAM_INSTANCE_COUNT] = "1"
			};


			if (_watch)
			{
				var dependencies = DependencyResolver.GetDependencies(service);
				BindMounts = new List<BindMount>();
				var buildPath = service.BuildPath;
				var fullBuildPath = Path.GetFullPath(buildPath);
				BindMounts.Add(
					new BindMount { isReadOnly = true, dst = $"/subapp/{service.ImageName}", src = fullBuildPath }
				);
				foreach (var assemblyToCopy in dependencies.Assemblies.ToCopy)
				{
					var mount = new BindMount
					{
						isReadOnly = true,
						dst = $"/subapp/{service.ImageName}/{FileUtils.GetBuildContextPath(assemblyToCopy)}",
						src = FileUtils.GetFullSourcePath(assemblyToCopy)
					};
					BindMounts.Add(mount);
				}

				NamedVolumes = new Dictionary<string, string>
				{
					[service.NugetVolume] = "/root/.nuget/packages", // save the nuget data between builds
				};
			}

			if (env != null)
			{
				foreach (var kvp in env)
				{
					Environment[kvp.Key] = kvp.Value;
				}
			}

			var config = MicroserviceConfiguration.Instance.GetEntry(service.Name);
			if (config.IncludeDebugTools)
			{
				Ports = new Dictionary<uint, uint>
				{
					[(uint)config.DebugData.SshPort] = 2222
				};
			}
			MapDotnetCompileErrors();
		}

	}

	public class RunImageCommand : DockerCommand
	{

		public struct BindMount
		{
			public bool isReadOnly;
			public string src;
			public string dst;

			public string ToArgString()
			{
				/*
				 * The quotes around this command matter a lot.
				 * On either platform we MUST have the quotes around the src directory, so that paths with spaces are supported.
				 * On mac, that means we need to wrap the entire command in single quotes, or it'll mount the folder incorrectly.
				 * On windows, the surrounding quotes aren't required, and in fact, cause it to fail.
				 */

				var includeOuterQuotes = false;
#if BEAMABLE_ENABLE_MOUNT_SINGLE_QUOTE
				includeOuterQuotes = true;
#endif
				var quoteStr = includeOuterQuotes ? "'" : "";
				var optionStr = $"\"{src}\":{dst}{(isReadOnly ? ":ro" : "")}";
				return $"-v {quoteStr}{optionStr}{quoteStr}";
			}
		}

		private readonly IDescriptor _descriptor;
		private bool hasReceivedNonNullStandardOut;
		public string ImageName { get; set; }
		public string ContainerName { get; set; }
		public bool PruneAfterStartup { get; set; }
		public Dictionary<string, string> Environment { get; protected set; }
		public Dictionary<uint, uint> Ports { get; protected set; }

		public Dictionary<string, string> NamedVolumes { get; protected set; }

		public List<BindMount> BindMounts { get; protected set; }

		public RunImageCommand(string imageName, string containerName,
							   IDescriptor descriptor,
							   Dictionary<string, string> env = null,
							   Dictionary<uint, uint> ports = null,
							   Dictionary<string, string> namedVolumes = null,
							   List<BindMount> bindMounts = null)
		{
			_descriptor = descriptor;
			ImageName = imageName;
			ContainerName = containerName;
			UnityLogLabel = null;
			Environment = env ?? new Dictionary<string, string>();
			Ports = ports ?? new Dictionary<uint, uint>();
			NamedVolumes = namedVolumes ?? new Dictionary<string, string>();
			BindMounts = bindMounts ?? new List<BindMount>();
		}

		protected override void HandleStandardOut(string data)
		{
			HandleAutoPrune(data);
			if (_descriptor == null || data == null || !MicroserviceLogHelper.HandleLog(_descriptor, UnityLogLabel, data, logProcessor: _standardOutProcessors))
			{
				base.HandleStandardOut(data);
			}
			OnStandardOut?.Invoke(data);
		}

		protected virtual void HandleAutoPrune(string data)
		{
			if (hasReceivedNonNullStandardOut) return;

			if (PruneAfterStartup)
			{
				if (!MicroserviceConfiguration.Instance.EnableAutoPrune)
				{
					return;
				}

				BeamEditorContext.Default.Dispatcher.Schedule(() =>
				{
					var prune = new PruneImageCommand(_descriptor);
					var _ = prune.StartAsync().Then(__ => { });
				});
			}

			if (!string.IsNullOrEmpty(data))
			{
				hasReceivedNonNullStandardOut = true;
			}
		}

		protected override void HandleStandardErr(string data)
		{
			if (_descriptor == null || data == null || !MicroserviceLogHelper.HandleLog(_descriptor, UnityLogLabel, data, logProcessor: _standardErrProcessors))
			{
				base.HandleStandardErr(data);
			}
			OnStandardErr?.Invoke(data);
		}

		public string GetEnvironmentString()
		{
			var keys = Environment.Select(kvp => $"--env {kvp.Key}=\"{kvp.Value}\"");
			var envString = string.Join(" ", keys);
			return envString;
		}

		string GetPortString()
		{
			var keys = Ports.Select(kvp => $"-p {kvp.Key}:{kvp.Value}");
			var portString = string.Join(" ", keys);
			return portString;
		}

		string GetBindMountsString()
		{
			return string.Join(" ", BindMounts.Select(b => b.ToArgString()));
		}

		string GetNamedVolumeString()
		{
			var volumes = NamedVolumes.Select(kvp => $"-v {kvp.Key}:{kvp.Value}");
			var volumeString = string.Join(" ", volumes);
			return volumeString;
		}

		protected virtual string GetCustomDockerFlags()
		{
			return "";
		}

		protected virtual string GetArgString()
		{
			return "";
		}

		public override string GetCommandString()
		{
			var command = $"{DockerCmd} run --rm " +
						  $"-P " +
						  $"{GetNamedVolumeString()} " +
						  $"{GetPortString()} " +
						  $"{GetEnvironmentString()} " +
						  $"{GetCustomDockerFlags()} " +
						  $"{GetBindMountsString()} " +
						  $"--name {ContainerName} {ImageName} " +
						  $"{GetArgString()}";
			return command;
		}

	}
}
