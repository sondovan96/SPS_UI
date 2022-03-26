using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Product;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.Products.Queries.GetNewProduct
{
    public class GetNewProductHandler : IRequestHandler<GetNewProductRequest, PageListModel<ProductModel>>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;

        public GetNewProductHandler(IHttpRequestExtension httpRequestExtension)
        {
            _httpRequestExtension = httpRequestExtension;
        }

        public async Task<PageListModel<ProductModel>> Handle(GetNewProductRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpRequestExtension.GetRequestAsync<Response<PageListModel<ProductModel>>>(Constants.ApiUrl.Product.GetNewProduct, default);
            return response.Data;
        }
    }
}
