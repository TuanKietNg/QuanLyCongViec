using System.Globalization;

namespace HDDT_BE.Constants
{
    public static class FormatMoney
    {
        public static string ConvertToMoney(Double money)
        {
            if (money == 0)
            {
                return "0";
            }
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN"); 
            return money.ToString("#,###", cul.NumberFormat);
        }
    }
}