using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Data.Core
{
    public static class Constants
    {
        public static class AppUrl
        {
            
        }
        public static class ApiUrl
        {
            public const string Root = "/";
            public static class Category
            {
                public const string Root = "Category";
            }
        }
        public static class SessionKey
        {
            public const string Token = "_Token";
            public const string TokenScheme = "_TokenScheme";
            public const string Roles = "_Roles";
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
