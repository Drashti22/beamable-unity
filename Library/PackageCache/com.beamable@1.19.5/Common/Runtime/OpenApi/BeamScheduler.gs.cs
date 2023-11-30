
namespace Beamable.Api.Autogenerated.Scheduler
{
	using Beamable.Api.Autogenerated.Models;
	using Beamable.Common;
	using Beamable.Common.Content;
	using Beamable.Common.Dependencies;
	using IBeamableRequester = Beamable.Common.Api.IBeamableRequester;
	using Method = Beamable.Common.Api.Method;

	public partial interface IBeamSchedulerApi
	{
		/// <param name="gsReq">The <see cref="JobExecutionEvent"/> instance to use for the request</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="JobExecutionResult"/></returns>
		Promise<JobExecutionResult> PostSchedulerJobExecute(JobExecutionEvent gsReq, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
		/// <param name="gsReq">The <see cref="JobDefinitionSaveRequest"/> instance to use for the request</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="JobDefinition"/></returns>
		Promise<JobDefinition> PostJob(JobDefinitionSaveRequest gsReq, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
		/// <param name="gsReq">The <see cref="JobDefinitionSaveRequest"/> instance to use for the request</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="JobDefinition"/></returns>
		Promise<JobDefinition> PostSchedulerJob(JobDefinitionSaveRequest gsReq, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
		/// <param name="limit"></param>
		/// <param name="name"></param>
		/// <param name="source"></param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="ApiSchedulerJobsGetSchedulerResponse"/></returns>
		Promise<ApiSchedulerJobsGetSchedulerResponse> GetJobs([System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<int> limit, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> name, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> source, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
		/// <param name="jobId"></param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="JobDefinition"/></returns>
		Promise<JobDefinition> GetJob(string jobId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
		/// <param name="jobId"></param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="ApiSchedulerJobDeleteSchedulerResponse"/></returns>
		Promise<ApiSchedulerJobDeleteSchedulerResponse> DeleteJob(string jobId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
		/// <param name="jobId"></param>
		/// <param name="limit"></param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="ApiSchedulerJobActivityGetSchedulerResponse"/></returns>
		Promise<ApiSchedulerJobActivityGetSchedulerResponse> GetJobActivity(string jobId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<int> limit, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
		/// <param name="from"></param>
		/// <param name="jobId"></param>
		/// <param name="limit"></param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="ApiSchedulerJobNextExecutionsGetSchedulerResponse"/></returns>
		Promise<ApiSchedulerJobNextExecutionsGetSchedulerResponse> GetJobNextExecutions(string jobId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<System.DateTime> from, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<int> limit, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
		/// <param name="jobId"></param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="ApiSchedulerJobCancelPutSchedulerResponse"/></returns>
		Promise<ApiSchedulerJobCancelPutSchedulerResponse> PutJobCancel(string jobId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
	}
	public partial class BeamSchedulerApi : IBeamSchedulerApi
	{
		/// <param name="gsReq">The <see cref="JobExecutionEvent"/> instance to use for the request</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="JobExecutionResult"/></returns>
		public virtual Promise<JobExecutionResult> PostSchedulerJobExecute(JobExecutionEvent gsReq, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/api/internal/scheduler/job/execute";
			// make the request and return the result
			return _requester.Request<JobExecutionResult>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), includeAuthHeader, this.Serialize<JobExecutionResult>);
		}
		/// <param name="gsReq">The <see cref="JobDefinitionSaveRequest"/> instance to use for the request</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="JobDefinition"/></returns>
		public virtual Promise<JobDefinition> PostJob(JobDefinitionSaveRequest gsReq, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/api/scheduler/job";
			// make the request and return the result
			return _requester.Request<JobDefinition>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), includeAuthHeader, this.Serialize<JobDefinition>);
		}
		/// <param name="gsReq">The <see cref="JobDefinitionSaveRequest"/> instance to use for the request</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="JobDefinition"/></returns>
		public virtual Promise<JobDefinition> PostSchedulerJob(JobDefinitionSaveRequest gsReq, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/api/internal/scheduler/job";
			// make the request and return the result
			return _requester.Request<JobDefinition>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), includeAuthHeader, this.Serialize<JobDefinition>);
		}
		/// <param name="limit"></param>
		/// <param name="name"></param>
		/// <param name="source"></param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="ApiSchedulerJobsGetSchedulerResponse"/></returns>
		public virtual Promise<ApiSchedulerJobsGetSchedulerResponse> GetJobs([System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<int> limit, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> name, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> source, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/api/scheduler/jobs";
			string gsQuery = "?";
			System.Collections.Generic.List<string> gsQueries = new System.Collections.Generic.List<string>();
			if (((source != default(OptionalString))
						&& source.HasValue))
			{
				gsQueries.Add(string.Concat("source=", source.Value.ToString()));
			}
			if (((name != default(OptionalString))
						&& name.HasValue))
			{
				gsQueries.Add(string.Concat("name=", name.Value.ToString()));
			}
			if (((limit != default(OptionalInt))
						&& limit.HasValue))
			{
				gsQueries.Add(string.Concat("limit=", limit.Value.ToString()));
			}
			if ((gsQueries.Count > 0))
			{
				gsQuery = string.Concat(gsQuery, string.Join("&", gsQueries));
				gsUrl = string.Concat(gsUrl, gsQuery);
			}
			// make the request and return the result
			return _requester.Request<ApiSchedulerJobsGetSchedulerResponse>(Method.GET, gsUrl, default(object), includeAuthHeader, this.Serialize<ApiSchedulerJobsGetSchedulerResponse>);
		}
		/// <param name="jobId"></param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="JobDefinition"/></returns>
		public virtual Promise<JobDefinition> GetJob(string jobId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/api/scheduler/job/{jobId}";
			gsUrl = gsUrl.Replace("{jobId}", _requester.EscapeURL(jobId.ToString()));
			// make the request and return the result
			return _requester.Request<JobDefinition>(Method.GET, gsUrl, default(object), includeAuthHeader, this.Serialize<JobDefinition>);
		}
		/// <param name="jobId"></param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="ApiSchedulerJobDeleteSchedulerResponse"/></returns>
		public virtual Promise<ApiSchedulerJobDeleteSchedulerResponse> DeleteJob(string jobId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/api/scheduler/job/{jobId}";
			gsUrl = gsUrl.Replace("{jobId}", _requester.EscapeURL(jobId.ToString()));
			// make the request and return the result
			return _requester.Request<ApiSchedulerJobDeleteSchedulerResponse>(Method.DELETE, gsUrl, default(object), includeAuthHeader, this.Serialize<ApiSchedulerJobDeleteSchedulerResponse>);
		}
		/// <param name="jobId"></param>
		/// <param name="limit"></param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="ApiSchedulerJobActivityGetSchedulerResponse"/></returns>
		public virtual Promise<ApiSchedulerJobActivityGetSchedulerResponse> GetJobActivity(string jobId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<int> limit, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/api/scheduler/job/{jobId}/activity";
			gsUrl = gsUrl.Replace("{jobId}", _requester.EscapeURL(jobId.ToString()));
			string gsQuery = "?";
			System.Collections.Generic.List<string> gsQueries = new System.Collections.Generic.List<string>();
			if (((limit != default(OptionalInt))
						&& limit.HasValue))
			{
				gsQueries.Add(string.Concat("limit=", limit.Value.ToString()));
			}
			if ((gsQueries.Count > 0))
			{
				gsQuery = string.Concat(gsQuery, string.Join("&", gsQueries));
				gsUrl = string.Concat(gsUrl, gsQuery);
			}
			// make the request and return the result
			return _requester.Request<ApiSchedulerJobActivityGetSchedulerResponse>(Method.GET, gsUrl, default(object), includeAuthHeader, this.Serialize<ApiSchedulerJobActivityGetSchedulerResponse>);
		}
		/// <param name="from"></param>
		/// <param name="jobId"></param>
		/// <param name="limit"></param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="ApiSchedulerJobNextExecutionsGetSchedulerResponse"/></returns>
		public virtual Promise<ApiSchedulerJobNextExecutionsGetSchedulerResponse> GetJobNextExecutions(string jobId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<System.DateTime> from, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<int> limit, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/api/scheduler/job/{jobId}/next-executions";
			gsUrl = gsUrl.Replace("{jobId}", _requester.EscapeURL(jobId.ToString()));
			string gsQuery = "?";
			System.Collections.Generic.List<string> gsQueries = new System.Collections.Generic.List<string>();
			if (((from != default(OptionalDateTime))
						&& from.HasValue))
			{
				gsQueries.Add(string.Concat("from=", from.Value.ToString()));
			}
			if (((limit != default(OptionalInt))
						&& limit.HasValue))
			{
				gsQueries.Add(string.Concat("limit=", limit.Value.ToString()));
			}
			if ((gsQueries.Count > 0))
			{
				gsQuery = string.Concat(gsQuery, string.Join("&", gsQueries));
				gsUrl = string.Concat(gsUrl, gsQuery);
			}
			// make the request and return the result
			return _requester.Request<ApiSchedulerJobNextExecutionsGetSchedulerResponse>(Method.GET, gsUrl, default(object), includeAuthHeader, this.Serialize<ApiSchedulerJobNextExecutionsGetSchedulerResponse>);
		}
		/// <param name="jobId"></param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="ApiSchedulerJobCancelPutSchedulerResponse"/></returns>
		public virtual Promise<ApiSchedulerJobCancelPutSchedulerResponse> PutJobCancel(string jobId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/api/scheduler/job/{jobId}/cancel";
			gsUrl = gsUrl.Replace("{jobId}", _requester.EscapeURL(jobId.ToString()));
			// make the request and return the result
			return _requester.Request<ApiSchedulerJobCancelPutSchedulerResponse>(Method.PUT, gsUrl, default(object), includeAuthHeader, this.Serialize<ApiSchedulerJobCancelPutSchedulerResponse>);
		}
	}
}
