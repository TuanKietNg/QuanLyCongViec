
using HDDT_BE.Models.Core;
using HDDT_BE.Models.Professional;
using MongoDB.Driver;


namespace HDDT_BE.Constants;

public class DefaultStatus
{
    public const string NHAP_CODE = "NHAP";
    public const string NHAP_NAME = "Hóa đơn nháp";
    
    public const string THAYTHE_CODE = "THAYTHE";
    public const string THAYTHE_NAME = "Hóa đơn thay thế";
    
    public const string DAPHATHANH_CODE = "DAPHATHANH";
    public const string DAPHATHANH_NAME = "Hóa đơn đã phát hành";
    
    
    public const string DAXOA_CODE = "DAXOA";
    public const string DAXOA_NAME = "Hóa đơn đã xóa";
    
    
    
}