using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Category;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryHandler : IRequestHandler<GetAllCategoryRequest, List<CategoryModel>>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;

        public GetAllCategoryHandler(IHttpRequestExtension httpRequestExtension)
        {
            _httpRequestExtension = httpRequestExtension;
        }

        public async Task<List<CategoryModel>> Handle(GetAllCategoryRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpRequestExtension.GetRequestAsync<Response<List<CategoryModel>>>(Constants.ApiUrl.Category.Root);
            return response.Data;
        }
    }
}
