
namespace Beamable.Api.Autogenerated.Inventory
{
	using Beamable.Api.Autogenerated.Models;
	using Beamable.Common;
	using Beamable.Common.Content;
	using Beamable.Common.Dependencies;
	using IBeamableRequester = Beamable.Common.Api.IBeamableRequester;
	using Method = Beamable.Common.Api.Method;

	public partial interface IInventoryApi
	{
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="ItemContentResponse"/></returns>
		Promise<ItemContentResponse> GetItems([System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="CurrencyContentResponse"/></returns>
		Promise<CurrencyContentResponse> GetCurrency([System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader);
	}
	public partial class InventoryApi : IInventoryApi
	{
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="ItemContentResponse"/></returns>
		public virtual Promise<ItemContentResponse> GetItems([System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/basic/inventory/items";
			// make the request and return the result
			return _requester.Request<ItemContentResponse>(Method.GET, gsUrl, default(object), includeAuthHeader, this.Serialize<ItemContentResponse>);
		}
		/// <param name="includeAuthHeader">By default, every request will include an authorization header so that the request acts on behalf of the current user. When the includeAuthHeader argument is false, the request will not include the authorization header for the current user.</param>
		/// <returns>A promise containing the <see cref="CurrencyContentResponse"/></returns>
		public virtual Promise<CurrencyContentResponse> GetCurrency([System.Runtime.InteropServices.DefaultParameterValueAttribute(true)][System.Runtime.InteropServices.OptionalAttribute()] bool includeAuthHeader)
		{
			string gsUrl = "/basic/inventory/currency";
			// make the request and return the result
			return _requester.Request<CurrencyContentResponse>(Method.GET, gsUrl, default(object), includeAuthHeader, this.Serialize<CurrencyContentResponse>);
		}
	}
}
