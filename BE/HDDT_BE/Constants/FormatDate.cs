namespace HDDT_BE.Constants
{
    public class FormatDate
    {
        
        public static int ConvertDatetimeQG(DateTime? value)
        {
            try
            {
                string datetime = value?.Year.ToString() + ConvernNumber(value.Value.Month) +
                                  ConvernNumber(value.Value.Day);
                return Int32.Parse(datetime);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        
        
        
        private static  string ConvernNumber(int number)
        {
            if (number > 0 && number < 10)
            {
                return "0" + number;
            }
            return number.ToString();
        }
    }
}