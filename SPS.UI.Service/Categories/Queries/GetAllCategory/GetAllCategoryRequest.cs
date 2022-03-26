using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Service.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryRequest:IRequest<PageListModel<CategoryModel>>
    { 
        public int? PageSize { get; set; } = 10;
        public int? PageIndex { get; set; } = 1;
    }
}
