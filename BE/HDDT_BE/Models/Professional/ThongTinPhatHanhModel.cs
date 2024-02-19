using HDDT_BE.Models.Core;
using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.Professional;


public class ThongTinPhatHanhModel : Audit, TEntity<string>
{
    
public string invoiceType {get; set;} // Mã loại hoá đơn

public string templateCode {get; set;} // Ký hiệu MẪU hoá đơn

public string invoiceSeries {get; set;} // Ký hiệu hoá đơn

public string currencyCode {get; set;} // Mã tiền tệ

public string adjustmentType  {get; set;} //Trạng thái điều chỉnh hóa đơn

public bool paymentStatus {get; set;} // 1 - Đã thanh toán - 2 chưa thanh toán

public bool cusGetInvoiceRight {get; set;} // Cho phép người dùng tra cứu hóa đơn hay không. MẶC ĐỊNH BẰNG TRUE
    
}




public class ThongTinPhatHanhModelShort{

    public string invoiceType {get; set;} // Mã loại hoá đơn
    public string templateCode {get; set;} // Ký hiệu MẪU hoá đơn

    public string invoiceSeries {get; set;} // Ký hiệu hoá đơn

    public string currencyCode {get; set;} // Mã tiền tệ
    
    public string adjustmentType  {get; set;} //Trạng thái điều chỉnh hóa đơn

    public bool paymentStatus {get; set;} // 1 - Đã thanh toán - 2 chưa thanh toán

    public bool cusGetInvoiceRight {get; set;} // Cho phép người dùng tra cứu hóa đơn hay không. MẶC ĐỊNH BẰNG TRUE
    
}



public class ThongTinPhatHanhCTModelShort{

    public string invoiceType {get; set;} // Mã loại hoá đơn
    public string templateCode {get; set;} // Ký hiệu MẪU hoá đơn

    public string invoiceSeries {get; set;} // Ký hiệu hoá đơn

    public string currencyCode {get; set;} // Mã tiền tệ

    public double invoiceIssuedDate { get; set; }

    public string transactionUuid { get; set; }
    public string adjustmentType  {get; set;} //Trạng thái điều chỉnh hóa đơn

    public bool paymentStatus {get; set;} // 1 - Đã thanh toán - 2 chưa thanh toán

    public bool cusGetInvoiceRight {get; set;} // Cho phép người dùng tra cứu hóa đơn hay không. MẶC ĐỊNH BẰNG TRUE
    
}



public class ThongTinPhatHanhTTModelShort{

    
    public string transactionUuid { get; set; }
    public string invoiceType {get; set;} // Mã loại hoá đơn
    public string templateCode {get; set;} // Ký hiệu MẪU hoá đơn

    public string invoiceSeries {get; set;} // Ký hiệu hoá đơn
    public string currencyCode {get; set;} // Mã tiền tệ
    public string adjustmentType  {get; set;} //Trạng thái điều chỉnh hóa đơn
    
    public string originalInvoiceId { get; set; }
    
    
     public double invoiceIssuedDate { get; set; }
     /*public DateTime originalInvoiceIssueDate { get; set; }
     
     public DateTime additionalReferenceDate { get; set; }*/
    

    public string additionalReferenceDesc { get; set; } = "VAN BAN";
    
    public bool paymentStatus {get; set;} // 1 - Đã thanh toán - 2 chưa thanh toán

     public bool cusGetInvoiceRight {get; set;} // Cho phép người dùng tra cứu hóa đơn hay không. MẶC ĐỊNH BẰNG TRUE
    
}