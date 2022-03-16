using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Data.Core
{
    public class ErrorDetail
    {
        public string ErrorMessageId { get; set; }
        public string ErrorMessage { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
        public IList<ErrorDetail> Errors { get; set;}
    }
}
