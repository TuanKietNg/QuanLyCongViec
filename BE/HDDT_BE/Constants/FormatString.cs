using System.Globalization;

namespace HDDT_BE.Constants
{
    public static class FormatterString
    {
        public static string ConvertToTenKhongDau(string text)
        {
            text = text.ToLower().Trim();
            string[] arr1 = new string[]
            {
                "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                "đ",
                "é", "è", "ẻ", "ẽ", "ẹ", "ê", "ế", "ề", "ể", "ễ", "ệ",
                "í", "ì", "ỉ", "ĩ", "ị",
                "ó", "ò", "ỏ", "õ", "ọ", "ô", "ố", "ồ", "ổ", "ỗ", "ộ", "ơ", "ớ", "ờ", "ở", "ỡ", "ợ",
                "ú", "ù", "ủ", "ũ", "ụ", "ư", "ứ", "ừ", "ử", "ữ", "ự",
                "ý", "ỳ", "ỷ", "ỹ", "ỵ","\t", "\n","," ,"\t" , "\n"
            };
            string[] arr2 = new string[]
            {
                "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                "d",
                "e", "e", "e", "e", "e", "e", "e", "e", "e", "e", "e",
                "i", "i", "i", "i", "i",
                "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o",
                "u", "u", "u", "u", "u", "u", "u", "u", "u", "u", "u",
                "y", "y", "y", "y", "y","","","","",""
            };
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
            }
            return text.ToUpper();
        }
        
        
        
         public static string NumberToText(double inputNumber, bool suffix = true)
    {
        string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
        bool isNegative = false;

        // -12345678.3445435 => "-12345678"
        string sNumber = inputNumber.ToString("#");
        double number = Convert.ToDouble(sNumber);
        if (number < 0)
        {
          number = -number;
          sNumber = number.ToString();
          isNegative = true;
        }


        int ones, tens, hundreds;

        int positionDigit = sNumber.Length;   // last -> first

        string result = " ";


        if (positionDigit == 0)
          result = unitNumbers[0] + result;
        else
        {
          int placeValue = 0;

          while (positionDigit > 0)
          {
            tens = hundreds = -1;
            ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
            positionDigit--;
            if (positionDigit > 0)
            {
              tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
              positionDigit--;
              if (positionDigit > 0)
              {
                hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                positionDigit--;
              }
            }

            if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
              result = placeValues[placeValue] + result;

            placeValue++;
            if (placeValue > 3) placeValue = 1;

            if ((ones == 1) && (tens > 1))
              result = "một " + result;
            else
            {
              if ((ones == 5) && (tens > 0))
                result = "lăm " + result;
              else if (ones > 0)
                result = unitNumbers[ones] + " " + result;
            }
            if (tens < 0)
              break;
            else
            {
              if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
              if (tens == 1) result = "mười " + result;
              if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
            }
            if (hundreds < 0) break;
            else
            {
              if ((hundreds > 0) || (tens > 0) || (ones > 0))
                result = unitNumbers[hundreds] + " trăm " + result;
            }
            result = " " + result;
          }
        }
        result = result.Trim();
        if (isNegative) result = "Âm " + result;
        var Str =  result + (suffix ? " đồng. " : "");
        
        return Str.Substring(0,1).ToUpper() + Str.Substring(1);
    }
         
         
    }
}