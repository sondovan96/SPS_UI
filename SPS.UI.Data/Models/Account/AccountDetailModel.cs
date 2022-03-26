using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Data.Models.Account
{
    public class AccountDetailModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
