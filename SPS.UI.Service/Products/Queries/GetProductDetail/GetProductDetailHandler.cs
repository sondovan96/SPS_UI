using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Product;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.Products.Queries.GetProductDetail
{
    public class GetProductDetailHandler : IRequestHandler<GetProductDetailRequest, ProductModel>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;

        public GetProductDetailHandler(IHttpRequestExtension httpRequestExtension)
        {
            _httpRequestExtension = httpRequestExtension;
        }
        public async Task<ProductModel> Handle(GetProductDetailRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpRequestExtension.GetRequestAsync<Response<ProductModel>>($"{Constants.ApiUrl.Product.GetProduct}/{request.Id}");
            return response.Data;
        }
    }
}
