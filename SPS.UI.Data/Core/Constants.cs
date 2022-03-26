using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Data.Core
{
    public static class Constants
    {
        public static class AppUrl
        {
            public const string RedirectUrl = "https://localhost:5001/Account/EmailConfirm";
        }
        public static class ApiUrl
        {
            public static class Account
            {
                public const string Root = "Account";
                public const string Login = Root + "/Login";
                public const string Logout = Root + "/Logout";
                public const string ConfirmEmail = Root + "/email-confirmation";
                public const string SendConfirmEmail = Root + "/send-confirmation-email";
                public const string Register = Root + "/Register";
            }
            public static class Role
            {
                public const string Root = "Role";
                public const string GetRoles = Root + "/GetRoles";
            }

            public static class Category
            {
                public const string Root = "Category";
            }
            public static class Product
            {
                public const string Root = "Product";

                public const string GetNewProduct = Product.Root+"/GetNewProduct";
                public const string GetFeaturedProduct = Product.Root + "/GetFeaturedProduct";
                public const string GetHotProduct = Product.Root + "/GetHotProduct";
                public const string GetProduct = Product.Root + "/GetProduct";
            }
            public static class Order
            {
                public const string Root = "/Order";

                public const string AddOrder = Root + "/AddOrder";

            }
            public static class OrderDetail
            {
                public const string Root = "OrderDetail";

            }
        }
        public static class SessionKey
        {
            public const string Token = "_Token";
            public const string TokenScheme = "_TokenScheme";
            public const string Roles = "_Roles";
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string Email = "Email";

            public const string CartSession = "CartSession";
        }
        public static class Request
        {
            public static class Header
            {
                public const string Authorization = "Authorization";
            }
        }
    }
}
