
namespace Beamable.Api.Autogenerated.Auth
{
	using Beamable.Api.Autogenerated.Models;
	using Beamable.Common;
	using Beamable.Common.Content;
	using Beamable.Common.Dependencies;
	using IBeamableRequester = Beamable.Common.Api.IBeamableRequester;
	using Method = Beamable.Common.Api.Method;

	public partial interface IAuthApi
	{
		/// <param name="cid"></param>
		/// <param name="gamerTagOrAccountId"></param>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <param name="pid"></param>
		/// <returns>A promise containing the <see cref="ListTokenResponse"/></returns>
		Promise<ListTokenResponse> GetTokenList(long gamerTagOrAccountId, int page, int pageSize, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<long> cid, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> pid);
		/// <param name="token"></param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="Token"/></returns>
		Promise<Token> GetToken(string token, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
		/// <param name="gsReq">The <see cref="TokenRequestWrapper"/> instance to use for the request</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="TokenResponse"/></returns>
		Promise<TokenResponse> PostToken(TokenRequestWrapper gsReq, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
		/// <param name="gsReq">The <see cref="RevokeTokenRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		Promise<CommonResponse> PutTokenRevoke(RevokeTokenRequest gsReq);
	}
	public partial class AuthApi : IAuthApi
	{
		/// <param name="cid"></param>
		/// <param name="gamerTagOrAccountId"></param>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <param name="pid"></param>
		/// <returns>A promise containing the <see cref="ListTokenResponse"/></returns>
		public virtual Promise<ListTokenResponse> GetTokenList(long gamerTagOrAccountId, int page, int pageSize, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<long> cid, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> pid)
		{
			string gsUrl = "/basic/auth/token/list";
			string gsQuery = "?";
			System.Collections.Generic.List<string> gsQueries = new System.Collections.Generic.List<string>();
			gsQueries.Add(string.Concat("pageSize=", _requester.EscapeURL(pageSize.ToString())));
			gsQueries.Add(string.Concat("page=", _requester.EscapeURL(page.ToString())));
			if (((cid != default(OptionalLong))
						&& cid.HasValue))
			{
				gsQueries.Add(string.Concat("cid=", cid.Value.ToString()));
			}
			if (((pid != default(OptionalString))
						&& pid.HasValue))
			{
				gsQueries.Add(string.Concat("pid=", pid.Value.ToString()));
			}
			gsQueries.Add(string.Concat("gamerTagOrAccountId=", _requester.EscapeURL(gamerTagOrAccountId.ToString())));
			if ((gsQueries.Count > 0))
			{
				gsQuery = string.Concat(gsQuery, string.Join("&", gsQueries));
				gsUrl = string.Concat(gsUrl, gsQuery);
			}
			// make the request and return the result
			return _requester.Request<ListTokenResponse>(Method.GET, gsUrl, default(object), true, this.Serialize<ListTokenResponse>);
		}
		/// <param name="token"></param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="Token"/></returns>
		public virtual Promise<Token> GetToken(string token, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/basic/auth/token";
			string gsQuery = "?";
			System.Collections.Generic.List<string> gsQueries = new System.Collections.Generic.List<string>();
			gsQueries.Add(string.Concat("token=", _requester.EscapeURL(token.ToString())));
			if ((gsQueries.Count > 0))
			{
				gsQuery = string.Concat(gsQuery, string.Join("&", gsQueries));
				gsUrl = string.Concat(gsUrl, gsQuery);
			}
			// make the request and return the result
			return _requester.Request<Token>(Method.GET, gsUrl, default(object), includeAuthHeader, this.Serialize<Token>);
		}
		/// <param name="gsReq">The <see cref="TokenRequestWrapper"/> instance to use for the request</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="TokenResponse"/></returns>
		public virtual Promise<TokenResponse> PostToken(TokenRequestWrapper gsReq, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/basic/auth/token";
			// make the request and return the result
			return _requester.Request<TokenResponse>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), includeAuthHeader, this.Serialize<TokenResponse>);
		}
		/// <param name="gsReq">The <see cref="RevokeTokenRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		public virtual Promise<CommonResponse> PutTokenRevoke(RevokeTokenRequest gsReq)
		{
			string gsUrl = "/basic/auth/token/revoke";
			// make the request and return the result
			return _requester.Request<CommonResponse>(Method.PUT, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<CommonResponse>);
		}
	}
}
