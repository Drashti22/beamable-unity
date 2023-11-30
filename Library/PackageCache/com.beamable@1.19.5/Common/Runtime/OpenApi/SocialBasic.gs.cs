
namespace Beamable.Api.Autogenerated.Social
{
	using Beamable.Api.Autogenerated.Models;
	using Beamable.Common;
	using Beamable.Common.Content;
	using Beamable.Common.Dependencies;
	using IBeamableRequester = Beamable.Common.Api.IBeamableRequester;
	using Method = Beamable.Common.Api.Method;

	public partial interface ISocialApi
	{
		/// <returns>A promise containing the <see cref="Social"/></returns>
		Promise<Social> GetMy();
		/// <param name="gsReq">The <see cref="SendFriendRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		Promise<EmptyResponse> PostFriendsInvite(SendFriendRequest gsReq);
		/// <param name="gsReq">The <see cref="SendFriendRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		Promise<EmptyResponse> DeleteFriendsInvite(SendFriendRequest gsReq);
		/// <param name="gsReq">The <see cref="PlayerIdRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		Promise<EmptyResponse> DeleteFriends(PlayerIdRequest gsReq);
		/// <param name="gsReq">The <see cref="ImportFriendsRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		Promise<EmptyResponse> PostFriendsImport(ImportFriendsRequest gsReq);
		/// <param name="gsReq">The <see cref="MakeFriendshipRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		Promise<CommonResponse> PostFriendsMake(MakeFriendshipRequest gsReq);
		/// <param name="playerIds"></param>
		/// <returns>A promise containing the <see cref="GetSocialStatusesResponse"/></returns>
		Promise<GetSocialStatusesResponse> Get(string[] playerIds);
		/// <param name="gsReq">The <see cref="PlayerIdRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="FriendshipStatus"/></returns>
		Promise<FriendshipStatus> PostBlocked(PlayerIdRequest gsReq);
		/// <param name="gsReq">The <see cref="PlayerIdRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="FriendshipStatus"/></returns>
		Promise<FriendshipStatus> DeleteBlocked(PlayerIdRequest gsReq);
	}
	public partial class SocialApi : ISocialApi
	{
		/// <returns>A promise containing the <see cref="Social"/></returns>
		public virtual Promise<Social> GetMy()
		{
			string gsUrl = "/basic/social/my";
			// make the request and return the result
			return _requester.Request<Social>(Method.GET, gsUrl, default(object), true, this.Serialize<Social>);
		}
		/// <param name="gsReq">The <see cref="SendFriendRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		public virtual Promise<EmptyResponse> PostFriendsInvite(SendFriendRequest gsReq)
		{
			string gsUrl = "/basic/social/friends/invite";
			// make the request and return the result
			return _requester.Request<EmptyResponse>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<EmptyResponse>);
		}
		/// <param name="gsReq">The <see cref="SendFriendRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		public virtual Promise<EmptyResponse> DeleteFriendsInvite(SendFriendRequest gsReq)
		{
			string gsUrl = "/basic/social/friends/invite";
			// make the request and return the result
			return _requester.Request<EmptyResponse>(Method.DELETE, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<EmptyResponse>);
		}
		/// <param name="gsReq">The <see cref="PlayerIdRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		public virtual Promise<EmptyResponse> DeleteFriends(PlayerIdRequest gsReq)
		{
			string gsUrl = "/basic/social/friends";
			// make the request and return the result
			return _requester.Request<EmptyResponse>(Method.DELETE, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<EmptyResponse>);
		}
		/// <param name="gsReq">The <see cref="ImportFriendsRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="EmptyResponse"/></returns>
		public virtual Promise<EmptyResponse> PostFriendsImport(ImportFriendsRequest gsReq)
		{
			string gsUrl = "/basic/social/friends/import";
			// make the request and return the result
			return _requester.Request<EmptyResponse>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<EmptyResponse>);
		}
		/// <param name="gsReq">The <see cref="MakeFriendshipRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		public virtual Promise<CommonResponse> PostFriendsMake(MakeFriendshipRequest gsReq)
		{
			string gsUrl = "/basic/social/friends/make";
			// make the request and return the result
			return _requester.Request<CommonResponse>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<CommonResponse>);
		}
		/// <param name="playerIds"></param>
		/// <returns>A promise containing the <see cref="GetSocialStatusesResponse"/></returns>
		public virtual Promise<GetSocialStatusesResponse> Get(string[] playerIds)
		{
			string gsUrl = "/basic/social/";
			string gsQuery = "?";
			System.Collections.Generic.List<string> gsQueries = new System.Collections.Generic.List<string>();
			gsQueries.Add(string.Concat("playerIds=", _requester.EscapeURL(playerIds.ToString())));
			if ((gsQueries.Count > 0))
			{
				gsQuery = string.Concat(gsQuery, string.Join("&", gsQueries));
				gsUrl = string.Concat(gsUrl, gsQuery);
			}
			// make the request and return the result
			return _requester.Request<GetSocialStatusesResponse>(Method.GET, gsUrl, default(object), true, this.Serialize<GetSocialStatusesResponse>);
		}
		/// <param name="gsReq">The <see cref="PlayerIdRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="FriendshipStatus"/></returns>
		public virtual Promise<FriendshipStatus> PostBlocked(PlayerIdRequest gsReq)
		{
			string gsUrl = "/basic/social/blocked";
			// make the request and return the result
			return _requester.Request<FriendshipStatus>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<FriendshipStatus>);
		}
		/// <param name="gsReq">The <see cref="PlayerIdRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="FriendshipStatus"/></returns>
		public virtual Promise<FriendshipStatus> DeleteBlocked(PlayerIdRequest gsReq)
		{
			string gsUrl = "/basic/social/blocked";
			// make the request and return the result
			return _requester.Request<FriendshipStatus>(Method.DELETE, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<FriendshipStatus>);
		}
	}
}
