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

        #region"LogFile"
        public const string PathLogFile = "c:\\logFile";
        public const string timeformat = "HH:mm:ss";
        public const string fileformat = "yyyyMMdd";
        public const string spit = "-----";
        public const string fileType = ".txt";
        #endregion

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

      

        public static string GetFieldValueString(object Value)
        {
            if (Value == null) return string.Empty;
            if (DBNull.Equals(Value, System.DBNull.Value)) return string.Empty;
            return Convert.ToString(Value);
        }
        public static int GetFieldValueInteger(object Value)
        {
            string valuestring = GetFieldValueString(Value);
            int result;
            if (valuestring == string.Empty) return result = 0;
            else
            {
                try
                {
                    result = Convert.ToInt32(valuestring, 10);
                }
                catch (Exception)
                {
                    result = 0;
                }
            }
            return result;
        }
        public static decimal GetFieldValueDecimal(object Value)
        {
            string valuestring = GetFieldValueString(Value);
            decimal result;
            if (valuestring == string.Empty) return result = 0;
            else
            {
                try
                {
                    result = Convert.ToDecimal(valuestring);
                }
                catch (Exception)
                {
                    result = 0;
                }
            }
            return result;
        }
        public static double GetFieldValueDouble(object Value)
        {
            string valuestring = GetFieldValueString(Value);
            double result;
            if (valuestring == string.Empty) return result = 0;
            else
            {
                try
                {
                    result = Convert.ToDouble(valuestring);
                }
                catch (Exception)
                {
                    result = 0;
                }
            }
            return result;
        }
        public static bool GetFieldValueBoolean(object Value)
        {
            if (Value == null) return false;
            if (DBNull.Equals(Value, System.DBNull.Value)) return true;
            return (bool)Value;
        }
        public static DateTime GetFieldValueDateTime(object Value)
        {
            DateTime tmpDT = new DateTime();
            if (Value == null) return tmpDT;
            if (DBNull.Equals(Value, System.DBNull.Value)) return tmpDT;
            return (DateTime)Value;
        }

        public static object SetFieldValueDateTime(DateTime Value)
        {
            if (Value == DateTime.MinValue) return System.DBNull.Value;
            return (DateTime)Value;
        }
        public static object SetFieldValueObject(object Value)
        {
            if (Value == null) return System.DBNull.Value;
            return Value;
        }
        public static string GetSiteRoot()
        {
            string Port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if ((Port == null) || Port == "80" || Port == "443")
            {
                Port = "";
            }
            else
            {
                Port = ":" + Port;
            }
            string Protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if ((Protocol == null) || Protocol == "0")
            {
                Protocol = "http://";
            }
            else
            {
                Protocol = "https://";
            }
            string strOut = Protocol + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + Port + System.Web.HttpContext.Current.Request.ApplicationPath;
            return strOut;
        }
    }
}