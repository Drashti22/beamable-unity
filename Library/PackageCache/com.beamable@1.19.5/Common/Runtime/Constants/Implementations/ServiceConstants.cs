﻿using UnityEngine;

namespace Beamable.Common
{
	public static partial class Constants
	{
		public static partial class Features
		{
			public static partial class Services
			{
				public const string COMPONENTS_PATH = Directories.BEAMABLE_SERVER_PACKAGE_EDITOR_UI + "/Components";
				public const string AUTOGENERATED_CLIENT_PATH = "Assets/Beamable/Autogenerated/Microservices";
				public const string DEPENDENT_SERVICES_WINDOW_TITLE = "Dependent services";

				public const string PUBLISH = "Publish";
				public const string STOP = "Stop";
				public const string BUILD_DEBUG_PREFIX = "[DEBUG]";
				public const string BUILD_ENABLE_DEBUG = "Enable Debug Tools";
				public const string BUILD_DISABLE_DEBUG = "Disable Debug Tools";

				public const string PROMPT_STARTED_FAILURE = "MICROSERVICE HASN'T STARTED...";
				public const string PROMPT_STOPPED_FAILURE = "MICROSERVICE HASN'T STOPPED...";

				public const string REMOTE_ONLY = "Remote Only";

				public const string CONTENT_UPDATE_EVENT = "content.manifest";
				public const string REALM_CONFIG_UPDATE_EVENT = "realm-config.refresh";

				public const string REALM_CONFIG_SERVICE_LOG_NAMESPACE = "service_logs";

				public const int HEALTH_PORT = 6565;
				public const int DISCOVERY_PORT = 8624;

				public const int DISCOVERY_BROADCAST_PERIOD_MS = 250;
				public const int DISCOVERY_RECEIVE_PERIOD_MS = 350;

				public const string UPLOAD_CONTAINER_MESSAGE = "Uploaded container service=[{0}]";
				public const string CONTAINER_ALREADY_UPLOADED_MESSAGE = "Service [{0}] is already deployed at imageId";
				public const string CANT_UPLOAD_CONTAINER_MESSAGE = "Can't upload container service=[{0}]";
				public const string USING_REMOTE_SERVICE_MESSAGE = "Using remote service";
				public const string BROKEN_REMOTE_SERVICES_MESSAGE = "Looks like there are microservices with blank image IDs. This may have occurred due to a past publish with archived microservices, but will make it impossible to publish now. You need to recreate these services: \n\t-{0}\n\nAfter publishing, you may archive any unused microservices. However, they must be published at least once to ensure a correct ServerManifest.";


				public const string MICROSERVICE_IMAGE_CLASS = "microserviceImage";
				public const string STORAGE_IMAGE_CLASS = "storageImage";
				public const string CHECKBOX_TOOLTIP = "Enable/disable the service";
				public const string CHECKBOX_TOOLTIP_ARCHIVED_STORAGE = "At least one dependent StorageObject is archived.";
				public const string CHECKBOX_TOOLTIP_DEPENDENCY_ON_SERVICE = "Depends on at least one Microservice.";
				public const string CHECKBOX_TOOLTIP_NO_DEP_ENABLED = "No enabled services have dependencies to this storage.";

				public const int CMD_RESULT_CODE_DOCKER_NOT_RUNNING = 500;
				public const int CMD_RESULT_CODE_CONTAINER_NOT_RUNNING = 501;
				public const int CMD_RESULT_CODE_SOLUTION_NOT_FOUND = 502;

				public static readonly Vector2 MIN_SIZE = new Vector2(900, 560);
				public const int MAX_ROW = 4;
				public const float ROW_HEIGHT = 50;

				public const string MICROSERVICE_FEDERATED_COMPONENTS_KEY = "x-federated-components";

				public static string GetBuildButtonString(bool includeDebugTools, string text) => includeDebugTools
					? $"{BUILD_DEBUG_PREFIX} {text}"
					: text;

				public const string GENERATOR_SUFFIX = "_generator";
				public const string DOTNET_COMPILE_ERROR_SYMBOL = "error CS";

				public static class Logs
				{
					public const string READY_FOR_TRAFFIC_PREFIX = "Service ready for traffic.";
					public const string STARTING_PREFIX = "Starting..";
					public const string SCANNING_CLIENT_PREFIX = "Scanning client methods for ";
					public const string REGISTERING_STANDARD_SERVICES = "Registering standard services";
					public const string REGISTERING_CUSTOM_SERVICES = "Registering custom services";
					public const string SERVICE_PROVIDER_INITIALIZED = "Service provider initialized";
					public const string EVENT_PROVIDER_INITIALIZED = "Event provider initialized";
					public const string STORAGE_READY = "Waiting for connections";
					public const string GENERATED_CLIENT_PREFIX = "Generated Client Code.";
				}

				public static class Dialogs
				{
					public static class RealmSwitchDialog
					{
						public const string TITLE = "Microservice Manager";
						public const string MESSAGE = "Due to the realm change, all running services are stopped";
						public const string OK = "Ok";
					}
				}

				public static class PublishWindow
				{
					public const string ON_OFF_HEADER_TOOLTIP = "A service can either be off or on. If the service is off, no ClientCallable methods can execute";
					public const string NAME_HEADER_TOOLTIP = "The name of the service";
					public const string KNOWN_LOCATION_HEADER_TOOLTIP = "A service can exist on your machine, or in a remote realm, or both. A 'Remote' value means that the service has been deployed previously, and only exists on the realm. A 'Local' value means that the service has not been deployed yet, and only exists on your machine. A 'Local & Remote' value means that the service exists on the realm and on your machine";
					public const string DEPENDENCIES_HEADER_TOOLTIP = "The storage objects that the service requires to run";
					public const string COMMENTS_HEADER_TOOLTIP = "You may enter in specific comments per service that are viewable from portal for this deployment";
					public const string STATUS_HEADER_TOOLTIP = "Indicates the current phase of the publication for the service";
				}
			}
		}
	}
}
