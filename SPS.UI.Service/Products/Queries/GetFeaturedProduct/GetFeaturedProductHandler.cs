using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Product;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.Products.Queries.GetFeaturedProduct
{
    public class GetFeaturedProductHandler : IRequestHandler<GetFeaturedProductRequest, PageListModel<ProductModel>>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;

        public GetFeaturedProductHandler(IHttpRequestExtension httpRequestExtension)
        {
            _httpRequestExtension = httpRequestExtension;
        }

        public async Task<PageListModel<ProductModel>> Handle(GetFeaturedProductRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpRequestExtension.GetRequestAsync<Response<PageListModel<ProductModel>>>(Constants.ApiUrl.Product.GetFeaturedProduct, default);
            return response.Data;
        }
    }
}
