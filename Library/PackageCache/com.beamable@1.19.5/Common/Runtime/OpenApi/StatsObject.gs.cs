
namespace Beamable.Api.Autogenerated.Stats
{
	using Beamable.Api.Autogenerated.Models;
	using Beamable.Common;
	using Beamable.Common.Content;
	using Beamable.Common.Dependencies;
	using IBeamableRequester = Beamable.Common.Api.IBeamableRequester;
	using Method = Beamable.Common.Api.Method;

	public partial interface IStatsApi
	{
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="gsReq">The <see cref="StatUpdateRequestStringListFormat"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		Promise<EmptyResponse> ObjectPostClientStringlist(string objectId, StatUpdateRequestStringListFormat gsReq);
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="stats"></param>
		/// <returns>A promise containing the <see cref="StatsResponse"/></returns>
		Promise<StatsResponse> ObjectGet(string objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> stats);
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="gsReq">The <see cref="StatUpdateRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		Promise<EmptyResponse> ObjectPost(string objectId, StatUpdateRequest gsReq);
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="gsReq">The <see cref="StatRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		Promise<EmptyResponse> ObjectDelete(string objectId, StatRequest gsReq);
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <returns>A promise containing the <see cref="StatsClientSubscriptionsResponse"/></returns>
		Promise<StatsClientSubscriptionsResponse> ObjectGetClientSubscriptions(string objectId);
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="gsReq">The <see cref="StatClientSubscriptionRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		Promise<CommonResponse> ObjectPostClientSubscriptions(string objectId, StatClientSubscriptionRequest gsReq);
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="gsReq">The <see cref="StatClientSubscriptionDeleteRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		Promise<CommonResponse> ObjectDeleteClientSubscriptions(string objectId, StatClientSubscriptionDeleteRequest gsReq);
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="stats"></param>
		/// <returns>A promise containing the <see cref="StatsResponse"/></returns>
		Promise<StatsResponse> ObjectGetClient(string objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> stats);
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="gsReq">The <see cref="StatUpdateRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		Promise<EmptyResponse> ObjectPostClient(string objectId, StatUpdateRequest gsReq);
	}
	public partial class StatsApi : IStatsApi
	{
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="gsReq">The <see cref="StatUpdateRequestStringListFormat"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		public virtual Promise<EmptyResponse> ObjectPostClientStringlist(string objectId, StatUpdateRequestStringListFormat gsReq)
		{
			string gsUrl = "/object/stats/{objectId}/client/stringlist";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<EmptyResponse>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<EmptyResponse>);
		}
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="stats"></param>
		/// <returns>A promise containing the <see cref="StatsResponse"/></returns>
		public virtual Promise<StatsResponse> ObjectGet(string objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> stats)
		{
			string gsUrl = "/object/stats/{objectId}/";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			string gsQuery = "?";
			System.Collections.Generic.List<string> gsQueries = new System.Collections.Generic.List<string>();
			if (((stats != default(OptionalString))
						&& stats.HasValue))
			{
				gsQueries.Add(string.Concat("stats=", stats.Value.ToString()));
			}
			if ((gsQueries.Count > 0))
			{
				gsQuery = string.Concat(gsQuery, string.Join("&", gsQueries));
				gsUrl = string.Concat(gsUrl, gsQuery);
			}
			// make the request and return the result
			return _requester.Request<StatsResponse>(Method.GET, gsUrl, default(object), true, this.Serialize<StatsResponse>);
		}
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="gsReq">The <see cref="StatUpdateRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		public virtual Promise<EmptyResponse> ObjectPost(string objectId, StatUpdateRequest gsReq)
		{
			string gsUrl = "/object/stats/{objectId}/";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<EmptyResponse>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<EmptyResponse>);
		}
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="gsReq">The <see cref="StatRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		public virtual Promise<EmptyResponse> ObjectDelete(string objectId, StatRequest gsReq)
		{
			string gsUrl = "/object/stats/{objectId}/";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<EmptyResponse>(Method.DELETE, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<EmptyResponse>);
		}
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <returns>A promise containing the <see cref="StatsClientSubscriptionsResponse"/></returns>
		public virtual Promise<StatsClientSubscriptionsResponse> ObjectGetClientSubscriptions(string objectId)
		{
			string gsUrl = "/object/stats/{objectId}/client/subscriptions";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<StatsClientSubscriptionsResponse>(Method.GET, gsUrl, default(object), true, this.Serialize<StatsClientSubscriptionsResponse>);
		}
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="gsReq">The <see cref="StatClientSubscriptionRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		public virtual Promise<CommonResponse> ObjectPostClientSubscriptions(string objectId, StatClientSubscriptionRequest gsReq)
		{
			string gsUrl = "/object/stats/{objectId}/client/subscriptions";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<CommonResponse>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<CommonResponse>);
		}
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="gsReq">The <see cref="StatClientSubscriptionDeleteRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		public virtual Promise<CommonResponse> ObjectDeleteClientSubscriptions(string objectId, StatClientSubscriptionDeleteRequest gsReq)
		{
			string gsUrl = "/object/stats/{objectId}/client/subscriptions";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<CommonResponse>(Method.DELETE, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<CommonResponse>);
		}
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="stats"></param>
		/// <returns>A promise containing the <see cref="StatsResponse"/></returns>
		public virtual Promise<StatsResponse> ObjectGetClient(string objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> stats)
		{
			string gsUrl = "/object/stats/{objectId}/client";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			string gsQuery = "?";
			System.Collections.Generic.List<string> gsQueries = new System.Collections.Generic.List<string>();
			if (((stats != default(OptionalString))
						&& stats.HasValue))
			{
				gsQueries.Add(string.Concat("stats=", stats.Value.ToString()));
			}
			if ((gsQueries.Count > 0))
			{
				gsQuery = string.Concat(gsQuery, string.Join("&", gsQueries));
				gsUrl = string.Concat(gsUrl, gsQuery);
			}
			// make the request and return the result
			return _requester.Request<StatsResponse>(Method.GET, gsUrl, default(object), true, this.Serialize<StatsResponse>);
		}
		/// <param name="objectId">Format: domain.visibility.type.gamerTag</param>
		/// <param name="gsReq">The <see cref="StatUpdateRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		public virtual Promise<EmptyResponse> ObjectPostClient(string objectId, StatUpdateRequest gsReq)
		{
			string gsUrl = "/object/stats/{objectId}/client";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<EmptyResponse>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<EmptyResponse>);
		}
	}
}
