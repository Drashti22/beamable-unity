
namespace Beamable.Api.Autogenerated.Announcements
{
	using Beamable.Api.Autogenerated.Models;
	using Beamable.Common;
	using Beamable.Common.Content;
	using Beamable.Common.Dependencies;
	using IBeamableRequester = Beamable.Common.Api.IBeamableRequester;
	using Method = Beamable.Common.Api.Method;

	public partial interface IAnnouncementsApi
	{
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="AnnouncementRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		Promise<CommonResponse> ObjectPutRead(long objectId, AnnouncementRequest gsReq);
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="AnnouncementRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		Promise<CommonResponse> ObjectPostClaim(long objectId, AnnouncementRequest gsReq);
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <returns>A promise containing the <see cref="AnnouncementRawResponse"/></returns>
		Promise<AnnouncementRawResponse> ObjectGetRaw(long objectId);
		/// <param name="include_deleted"></param>
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <returns>A promise containing the <see cref="AnnouncementQueryResponse"/></returns>
		Promise<AnnouncementQueryResponse> ObjectGet(long objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<bool> include_deleted);
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="AnnouncementRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		Promise<CommonResponse> ObjectDelete(long objectId, AnnouncementRequest gsReq);
	}
	public partial class AnnouncementsApi : IAnnouncementsApi
	{
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="AnnouncementRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		public virtual Promise<CommonResponse> ObjectPutRead(long objectId, AnnouncementRequest gsReq)
		{
			string gsUrl = "/object/announcements/{objectId}/read";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<CommonResponse>(Method.PUT, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<CommonResponse>);
		}
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="AnnouncementRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		public virtual Promise<CommonResponse> ObjectPostClaim(long objectId, AnnouncementRequest gsReq)
		{
			string gsUrl = "/object/announcements/{objectId}/claim";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<CommonResponse>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<CommonResponse>);
		}
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <returns>A promise containing the <see cref="AnnouncementRawResponse"/></returns>
		public virtual Promise<AnnouncementRawResponse> ObjectGetRaw(long objectId)
		{
			string gsUrl = "/object/announcements/{objectId}/raw";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<AnnouncementRawResponse>(Method.GET, gsUrl, default(object), true, this.Serialize<AnnouncementRawResponse>);
		}
		/// <param name="include_deleted"></param>
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <returns>A promise containing the <see cref="AnnouncementQueryResponse"/></returns>
		public virtual Promise<AnnouncementQueryResponse> ObjectGet(long objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<bool> include_deleted)
		{
			string gsUrl = "/object/announcements/{objectId}/";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			string gsQuery = "?";
			System.Collections.Generic.List<string> gsQueries = new System.Collections.Generic.List<string>();
			if (((include_deleted != default(OptionalBool))
						&& include_deleted.HasValue))
			{
				gsQueries.Add(string.Concat("include_deleted=", include_deleted.Value.ToString()));
			}
			if ((gsQueries.Count > 0))
			{
				gsQuery = string.Concat(gsQuery, string.Join("&", gsQueries));
				gsUrl = string.Concat(gsUrl, gsQuery);
			}
			// make the request and return the result
			return _requester.Request<AnnouncementQueryResponse>(Method.GET, gsUrl, default(object), true, this.Serialize<AnnouncementQueryResponse>);
		}
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="AnnouncementRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		public virtual Promise<CommonResponse> ObjectDelete(long objectId, AnnouncementRequest gsReq)
		{
			string gsUrl = "/object/announcements/{objectId}/";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<CommonResponse>(Method.DELETE, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<CommonResponse>);
		}
	}
}
