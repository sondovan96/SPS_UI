using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Data.Models
{
    public class BodyParamsModel
    {
        public IDictionary<string, object> Params { get; set; }
        public IDictionary<string, IFormFile> Files { get; set; }
    }
}
