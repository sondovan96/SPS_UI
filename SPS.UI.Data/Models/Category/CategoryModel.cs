using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Data.Models.Category
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Title { set; get; }
        public string MetaTitle { set; get; }
        public string ParentId { set; get; }
        public CategoryModel ParentCategory { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
