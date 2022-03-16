using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Service.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryRequest:IRequest<List<CategoryModel>>
    {

    }
}
