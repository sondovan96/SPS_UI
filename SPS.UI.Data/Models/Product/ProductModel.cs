using SPS.UI.Data.Models.Category;
using SPS.UI.Data.Models.ProductImage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SPS.UI.Data.Models.Product
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string MetaTitle { get; set; }
        public bool IsDeleted { get; set; }
        public bool HotProduct { get; set; }
        public bool FeaturedProduct { get; set; }
        public long Stock { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public int ViewCount { get; set; }
        public string Image { get; set; }
        public ICollection<ProductImageModel> ProductImages { get; set; }
        //public ICollection<Data.Models.Entities.FavoriteProduct> FavoriteProduct { get; set; }
        public CategoryModel Category { get; set; }
        public Guid IdCategory { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
