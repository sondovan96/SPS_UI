using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Service.Products.Queries.GetFeaturedProduct
{
    public class GetFeaturedProductRequest : IRequest<PageListModel<ProductModel>>
    {
        public int? PageSize { get; set; } = 8;
        public int? PageIndex { get; set; } = 1;
    }
}
