using HDDT_BE.Helpers;
using HDDT_BE.Models.Core;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace HDDT_BE.Exceptions
{
    public class ResponseMessageException : Exception
    {
        public int ResultCode { get; set; }
        public string ResultString { get; set; }

        public List<ErrorModel> Error { get; set; } = new List<ErrorModel>();

        public ResponseMessageException()
        {
            
        }
        public ResponseMessageException WithException(int resultCode)
        {
            this.ResultCode = resultCode;
            switch (ResultCode)
            {
                case 10 :
                    this.ResultString = DefaultMessage.CREATE_FAILURE;
                    break;
                case 11 :
                    this.ResultString = DefaultMessage.UPDATE_FAILURE;
                    break;
                case 12 :
                    this.ResultString = DefaultMessage.DELETE_FAILURE;
                    break;
                case 20 :
                    this.ResultString = DefaultMessage.EXCEPTION;
                    break;
                case 21 :
                    this.ResultString = DefaultMessage.DATA_EXISTED;
                    break;
                case 22 :
                    this.ResultString = DefaultMessage.DATA_NOT_FOUND;
                    break;
                case 23 :
                    this.ResultString = DefaultMessage.COMMON_NOT_FOUND;
                    break;
                case 24 :
                    this.ResultString = DefaultMessage.ERRER_STRUCTURE;
                    break;
                case 1 :
                    this.ResultString = DefaultMessage.BEYOND_TIME;
                    break;
            }

            return this;
        }

        
        
        public ResponseMessageException WithCode(int code)
        {
            if (!string.IsNullOrEmpty(code.ToString()))
            {
                this.ResultCode = code;
            }
            return this;
        }
        
        public ResponseMessageException WithMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                this.ResultString = message;
            }
            return this;
        }
        public ResponseMessageException WithValidationResult(ValidationResult message)
        {
            if (message != null)
            {
                foreach (var item in message.Errors)
                {
                    this.Error.Add(new ErrorModel(item.PropertyName , item.ErrorMessage));
                }
            }
            return this;
        }
    }
   
}