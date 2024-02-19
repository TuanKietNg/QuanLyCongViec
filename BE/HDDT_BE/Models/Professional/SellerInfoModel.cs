using HDDT_BE.Models.Core;
using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.Professional;
//THONG TIN NGUOI BAN
public class SellerInfoModel : Audit, TEntity<string>
{
    
public string sellerLegalName {get; set;}
public string sellerTaxCode {get; set;}

public string sellerAddressLine {get; set;}

public string sellerFaxNumber {get; set;}

public string sellerPhoneNumber {get; set;}

public string sellerEmail {get; set;}

public string sellerBankName {get; set;}

public string sellerBankAccount {get; set;}



public string sellerDistrictName {get; set;}

public string sellerCityName {get; set;}

public string sellerCountryCode {get; set;}

public string sellerWebsite {get; set;}

    
}




public class SellerInfoModelShort{
    
    
    public string sellerLegalName {get; set;}
    public string sellerTaxCode {get; set;}

    public string sellerAddressLine {get; set;}

    public string sellerFaxNumber {get; set;}

    public string sellerPhoneNumber {get; set;}

    public string sellerEmail {get; set;}

    public string sellerBankName {get; set;}

    public string sellerBankAccount {get; set;}



    public string sellerDistrictName {get; set;}

    public string sellerCityName {get; set;}

    public string sellerCountryCode {get; set;}

    public string sellerWebsite {get; set;}

    
}