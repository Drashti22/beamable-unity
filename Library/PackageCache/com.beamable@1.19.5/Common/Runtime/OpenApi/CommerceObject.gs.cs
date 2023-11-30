
namespace Beamable.Api.Autogenerated.Commerce
{
	using Beamable.Api.Autogenerated.Models;
	using Beamable.Common;
	using Beamable.Common.Content;
	using Beamable.Common.Dependencies;
	using IBeamableRequester = Beamable.Common.Api.IBeamableRequester;
	using Method = Beamable.Common.Api.Method;

	public partial interface ICommerceApi
	{
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="scope"></param>
		/// <returns>A promise containing the <see cref="GetActiveOffersResponse"/></returns>
		Promise<GetActiveOffersResponse> ObjectGet(long objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> scope);
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="GetTotalCouponResponse"/></returns>
		Promise<GetTotalCouponResponse> ObjectGetCouponsCount(long objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="CooldownModifierRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		Promise<CommonResponse> ObjectPutListingsCooldown(long objectId, CooldownModifierRequest gsReq);
		/// <param name="language"></param>
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="stores"></param>
		/// <param name="time"></param>
		/// <returns>A promise containing the <see cref="GetActiveOffersResponse"/></returns>
		Promise<GetActiveOffersResponse> ObjectGetOffersAdmin(long objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> language, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> stores, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> time);
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="PurchaseRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		Promise<CommonResponse> ObjectPostPurchase(long objectId, PurchaseRequest gsReq);
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="ReportPurchaseRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="ResultResponse"/></returns>
		Promise<ResultResponse> ObjectPutPurchase(long objectId, ReportPurchaseRequest gsReq);
		/// <param name="listing"></param>
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="store"></param>
		/// <param name="time"></param>
		/// <returns>A promise containing the <see cref="ActiveListingResponse"/></returns>
		Promise<ActiveListingResponse> ObjectGetListings(string listing, long objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> store, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> time);
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="ClearStatusRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		Promise<CommonResponse> ObjectDeleteStatus(long objectId, ClearStatusRequest gsReq);
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="GiveCouponReq"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		Promise<CommonResponse> ObjectPostCoupons(long objectId, GiveCouponReq gsReq);
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="StatSubscriptionNotification"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		Promise<CommonResponse> ObjectPostStatsUpdate(long objectId, StatSubscriptionNotification gsReq);
		/// <param name="language"></param>
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="stores"></param>
		/// <param name="time"></param>
		/// <returns>A promise containing the <see cref="GetActiveOffersResponse"/></returns>
		Promise<GetActiveOffersResponse> ObjectGetOffers(long objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> language, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> stores, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> time);
	}
	public partial class CommerceApi : ICommerceApi
	{
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="scope"></param>
		/// <returns>A promise containing the <see cref="GetActiveOffersResponse"/></returns>
		public virtual Promise<GetActiveOffersResponse> ObjectGet(long objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> scope)
		{
			string gsUrl = "/object/commerce/{objectId}/";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			string gsQuery = "?";
			System.Collections.Generic.List<string> gsQueries = new System.Collections.Generic.List<string>();
			if (((scope != default(OptionalString))
						&& scope.HasValue))
			{
				gsQueries.Add(string.Concat("scope=", scope.Value.ToString()));
			}
			if ((gsQueries.Count > 0))
			{
				gsQuery = string.Concat(gsQuery, string.Join("&", gsQueries));
				gsUrl = string.Concat(gsUrl, gsQuery);
			}
			// make the request and return the result
			return _requester.Request<GetActiveOffersResponse>(Method.GET, gsUrl, default(object), true, this.Serialize<GetActiveOffersResponse>);
		}
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="GetTotalCouponResponse"/></returns>
		public virtual Promise<GetTotalCouponResponse> ObjectGetCouponsCount(long objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/object/commerce/{objectId}/coupons/count";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<GetTotalCouponResponse>(Method.GET, gsUrl, default(object), includeAuthHeader, this.Serialize<GetTotalCouponResponse>);
		}
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="CooldownModifierRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		public virtual Promise<CommonResponse> ObjectPutListingsCooldown(long objectId, CooldownModifierRequest gsReq)
		{
			string gsUrl = "/object/commerce/{objectId}/listings/cooldown";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<CommonResponse>(Method.PUT, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<CommonResponse>);
		}
		/// <param name="language"></param>
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="stores"></param>
		/// <param name="time"></param>
		/// <returns>A promise containing the <see cref="GetActiveOffersResponse"/></returns>
		public virtual Promise<GetActiveOffersResponse> ObjectGetOffersAdmin(long objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> language, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> stores, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> time)
		{
			string gsUrl = "/object/commerce/{objectId}/offersAdmin";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			string gsQuery = "?";
			System.Collections.Generic.List<string> gsQueries = new System.Collections.Generic.List<string>();
			if (((language != default(OptionalString))
						&& language.HasValue))
			{
				gsQueries.Add(string.Concat("language=", language.Value.ToString()));
			}
			if (((time != default(OptionalString))
						&& time.HasValue))
			{
				gsQueries.Add(string.Concat("time=", time.Value.ToString()));
			}
			if (((stores != default(OptionalString))
						&& stores.HasValue))
			{
				gsQueries.Add(string.Concat("stores=", stores.Value.ToString()));
			}
			if ((gsQueries.Count > 0))
			{
				gsQuery = string.Concat(gsQuery, string.Join("&", gsQueries));
				gsUrl = string.Concat(gsUrl, gsQuery);
			}
			// make the request and return the result
			return _requester.Request<GetActiveOffersResponse>(Method.GET, gsUrl, default(object), true, this.Serialize<GetActiveOffersResponse>);
		}
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="PurchaseRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		public virtual Promise<CommonResponse> ObjectPostPurchase(long objectId, PurchaseRequest gsReq)
		{
			string gsUrl = "/object/commerce/{objectId}/purchase";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<CommonResponse>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<CommonResponse>);
		}
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="ReportPurchaseRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="ResultResponse"/></returns>
		public virtual Promise<ResultResponse> ObjectPutPurchase(long objectId, ReportPurchaseRequest gsReq)
		{
			string gsUrl = "/object/commerce/{objectId}/purchase";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<ResultResponse>(Method.PUT, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<ResultResponse>);
		}
		/// <param name="listing"></param>
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="store"></param>
		/// <param name="time"></param>
		/// <returns>A promise containing the <see cref="ActiveListingResponse"/></returns>
		public virtual Promise<ActiveListingResponse> ObjectGetListings(string listing, long objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> store, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> time)
		{
			string gsUrl = "/object/commerce/{objectId}/listings";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			string gsQuery = "?";
			System.Collections.Generic.List<string> gsQueries = new System.Collections.Generic.List<string>();
			gsQueries.Add(string.Concat("listing=", _requester.EscapeURL(listing.ToString())));
			if (((store != default(OptionalString))
						&& store.HasValue))
			{
				gsQueries.Add(string.Concat("store=", store.Value.ToString()));
			}
			if (((time != default(OptionalString))
						&& time.HasValue))
			{
				gsQueries.Add(string.Concat("time=", time.Value.ToString()));
			}
			if ((gsQueries.Count > 0))
			{
				gsQuery = string.Concat(gsQuery, string.Join("&", gsQueries));
				gsUrl = string.Concat(gsUrl, gsQuery);
			}
			// make the request and return the result
			return _requester.Request<ActiveListingResponse>(Method.GET, gsUrl, default(object), true, this.Serialize<ActiveListingResponse>);
		}
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="ClearStatusRequest"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		public virtual Promise<CommonResponse> ObjectDeleteStatus(long objectId, ClearStatusRequest gsReq)
		{
			string gsUrl = "/object/commerce/{objectId}/status";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<CommonResponse>(Method.DELETE, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<CommonResponse>);
		}
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="GiveCouponReq"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		public virtual Promise<CommonResponse> ObjectPostCoupons(long objectId, GiveCouponReq gsReq)
		{
			string gsUrl = "/object/commerce/{objectId}/coupons";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<CommonResponse>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<CommonResponse>);
		}
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="gsReq">The <see cref="StatSubscriptionNotification"/> instance to use for the request</param>
		/// <returns>A promise containing the <see cref="CommonResponse"/></returns>
		public virtual Promise<CommonResponse> ObjectPostStatsUpdate(long objectId, StatSubscriptionNotification gsReq)
		{
			string gsUrl = "/object/commerce/{objectId}/stats/update";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			// make the request and return the result
			return _requester.Request<CommonResponse>(Method.POST, gsUrl, Beamable.Serialization.JsonSerializable.ToJson(gsReq), true, this.Serialize<CommonResponse>);
		}
		/// <param name="language"></param>
		/// <param name="objectId">Gamertag of the player.Underlying objectId type is integer in format int64.</param>
		/// <param name="stores"></param>
		/// <param name="time"></param>
		/// <returns>A promise containing the <see cref="GetActiveOffersResponse"/></returns>
		public virtual Promise<GetActiveOffersResponse> ObjectGetOffers(long objectId, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> language, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> stores, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)][System.Runtime.InteropServices.OptionalAttribute()] Beamable.Common.Content.Optional<string> time)
		{
			string gsUrl = "/object/commerce/{objectId}/offers";
			gsUrl = gsUrl.Replace("{objectId}", _requester.EscapeURL(objectId.ToString()));
			string gsQuery = "?";
			System.Collections.Generic.List<string> gsQueries = new System.Collections.Generic.List<string>();
			if (((language != default(OptionalString))
						&& language.HasValue))
			{
				gsQueries.Add(string.Concat("language=", language.Value.ToString()));
			}
			if (((time != default(OptionalString))
						&& time.HasValue))
			{
				gsQueries.Add(string.Concat("time=", time.Value.ToString()));
			}
			if (((stores != default(OptionalString))
						&& stores.HasValue))
			{
				gsQueries.Add(string.Concat("stores=", stores.Value.ToString()));
			}
			if ((gsQueries.Count > 0))
			{
				gsQuery = string.Concat(gsQuery, string.Join("&", gsQueries));
				gsUrl = string.Concat(gsUrl, gsQuery);
			}
			// make the request and return the result
			return _requester.Request<GetActiveOffersResponse>(Method.GET, gsUrl, default(object), true, this.Serialize<GetActiveOffersResponse>);
		}
	}
}