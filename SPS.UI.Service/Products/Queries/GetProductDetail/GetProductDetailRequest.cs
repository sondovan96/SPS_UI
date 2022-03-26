using MediatR;
using SPS.UI.Data.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Service.Products.Queries.GetProductDetail
{
    public class GetProductDetailRequest:IRequest<ProductModel>
    {
        public Guid Id { get; set; }
    }
}
