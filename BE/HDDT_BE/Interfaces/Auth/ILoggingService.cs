
using HDDT_BE.Models.Core;

namespace HDDT_BE.Interfaces.Auth
{
    public interface ILoggingService
    {
       
        
        Task<dynamic> Create(string content , string code, string casename);
    }
}