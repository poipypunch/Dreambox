using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace MVCDreambox.App_Code
{
    public class CommonConstant
    {
        public const string DefaultPassword = "password";
        public const string SessionUserID = "UserID";
        public const string SessionRole = "Role";
        public class Role
        {
            public const string Admin = "Admin";
            public const string Dealer = "Dealer";
        }
        public class Status
        {
            public const string Active = "Active";
            public const string InActive = "InActive";
        }

        public class CardStatus
        {
            public const string InUsed = "InUsed";
            public const string New = "New";
        } 
    }      
}