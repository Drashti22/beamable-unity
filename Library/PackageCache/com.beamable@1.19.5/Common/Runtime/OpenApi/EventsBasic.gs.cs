
namespace Beamable.Api.Autogenerated.Events
{
	using Beamable.Api.Autogenerated.Models;
	using Beamable.Common;
	using Beamable.Common.Content;
	using Beamable.Common.Dependencies;
	using IBeamableRequester = Beamable.Common.Api.IBeamableRequester;
	using Method = Beamable.Common.Api.Method;

	public partial interface IEventsApi
	{
		/// <returns>A promise containing the <see cref="EventContentResponse"/></returns>
		Promise<EventContentResponse> GetContent();
		/// <param name="from"></param>
		/// <param name="limit"></param>
		/// <param name="query"></param>
		/// <param name="to"></param>
		/// <returns>A promise containing the <see cref="EventsInDateRangeResponse"/></returns>
		Promise<EventsInDateRangeResponse> GetCalendar([System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> from, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<int> limit, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> query, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> to);
		/// <param name="gsReq">The <see cref="EventApplyRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		Promise<CommonResponse> PostApplyContent(EventApplyRequest gsReq);
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="EventQueryResponse"/></returns>
		Promise<EventQueryResponse> GetRunning([System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
	}
	public partial class EventsApi : IEventsApi
	{
		/// <returns>A promise containing the <see cref="EventContentResponse"/></returns>
		public virtual Promise<EventContentResponse> GetContent()
		{
			string gsUrl = "/basic/events/content";
			// make the request and return the result
			return _requester.Request<EventContentResponse>(Method.GET, gsUrl, default(object), true, this.Serialize<EventContentResponse>);
		}
		/// <param name="from"></param>
		/// <param name="limit"></param>
		/// <param name="query"></param>
		/// <param name="to"></param>
		/// <returns>A promise containing the <see cref="EventsInDateRangeResponse"/></returns>
		public virtual Promise<EventsInDateRangeResponse> GetCalendar([System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> from, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<int> limit, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> query, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> to)
		{
			string gsUrl = "/basic/events/calendar";
			string gsQuery = "?";
			System.Collections.Generic.List<string> gsQueries = new System.Collections.Generic.List<string>();
			if (((from != default(OptionalString))
						&& from.HasValue))
			{
				gsQueries.Add(string.Concat("from=", from.Value.ToString()));
			}
			if (((to != default(OptionalString))
						&& to.HasValue))
			{
				gsQueries.Add(string.Concat("to=", to.Value.ToString()));
			}
			if (((query != default(OptionalString))
						&& query.HasValue))
			{
				gsQueries.Add(string.Concat("query=", query.Value.ToString()));
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
			return _requester.Request<EventsInDateRangeResponse>(Method.GET, gsUrl, default(object), true, this.Serialize<EventsInDateRangeResponse>);
		}
		/// <param name="gsReq">The <see cref="EventApplyRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		public virtual Promise<CommonResponse> PostApplyContent(EventApplyRequest gsReq)
		{
			string gsUrl = "/basic/events/applyContent";
			// make the request and return the result
			return _requester.Request<CommonResponse>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<CommonResponse>);
		}
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="EventQueryResponse"/></returns>
		public virtual Promise<EventQueryResponse> GetRunning([System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/basic/events/running";
			// make the request and return the result
			return _requester.Request<EventQueryResponse>(Method.GET, gsUrl, default(object), includeAuthHeader, this.Serialize<EventQueryResponse>);
		}
	}
}
