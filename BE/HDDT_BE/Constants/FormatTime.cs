namespace HDDT_BE.Constants
{
    public class FormatTime
    {
        
        public const string FORMAT_DATE_CORE = "dd/MM/yyyy";
        public const string FORMAT_DATE = "dd/MM/yyyy HH:mm:ss";
        
        
        public const string FORMAT_SUBMIT_DATE = "yyyy/MM/dd HH:mm:ss";
        
        public static string ConvertDate(DateTime? dateTime)
      {
          return dateTime != null ? dateTime?.AddDays(1).ToLocalTime().ToString(FormatTime.FORMAT_DATE_CORE) : ""; 
      }
    }
}