using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Data.Core
{
    public class ViewInfo
    {
        public string ViewName { get; set; }
        public IRequest<ViewInfo> RequestModel { get; set; }
        public IDictionary<string, IResponse<object>> ViewData { get; set; }
    }
}
