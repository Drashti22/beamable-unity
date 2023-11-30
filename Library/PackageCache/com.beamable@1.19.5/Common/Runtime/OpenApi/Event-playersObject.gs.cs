
namespace Beamable.Api.Autogenerated.EventPlayers
{
	using Beamable.Api.Autogenerated.Models;
	using Beamable.Common;
	using Beamable.Common.Content;
	using Beamable.Common.Dependencies;
	using IBeamableRequester = Beamable.Common.Api.IBeamableRequester;
	using Method = Beamable.Common.Api.Method;

	public partial interface IEventPlayersApi
	{
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="EventPlayerView"/></returns>
		Promise<EventPlayerView> ObjectGet(long objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="EventClaimRequest"/> instance to use for the request</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="EventClaimResponse"/></returns>
		Promise<EventClaimResponse> ObjectPostClaim(long objectId, EventClaimRequest gsReq, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="EventScoreRequest"/> instance to use for the request</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		Promise<CommonResponse> ObjectPutScore(long objectId, EventScoreRequest gsReq, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
	}
	public partial class EventPlayersApi : IEventPlayersApi
	{
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="EventPlayerView"/></returns>
		public virtual Promise<EventPlayerView> ObjectGet(long objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/object/event-players/{objectId}/";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<EventPlayerView>(Method.GET, gsUrl, default(object), includeAuthHeader, this.Serialize<EventPlayerView>);
		}
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="EventClaimRequest"/> instance to use for the request</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="EventClaimResponse"/></returns>
		public virtual Promise<EventClaimResponse> ObjectPostClaim(long objectId, EventClaimRequest gsReq, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/object/event-players/{objectId}/claim";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<EventClaimResponse>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), includeAuthHeader, this.Serialize<EventClaimResponse>);
		}
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="EventScoreRequest"/> instance to use for the request</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		public virtual Promise<CommonResponse> ObjectPutScore(long objectId, EventScoreRequest gsReq, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/object/event-players/{objectId}/score";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<CommonResponse>(Method.PUT, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), includeAuthHeader, this.Serialize<CommonResponse>);
		}
	}
}
