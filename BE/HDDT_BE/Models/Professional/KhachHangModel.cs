using HDDT_BE.Constants;
using HDDT_BE.Models.Core;
using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.Professional;


public class KhachHangModel : Audit, TEntity<string>
{
    
    public string UnsignedTenKhachHang
    {
        get { return TenKhachHang != null ? FormatterString.ConvertToTenKhongDau(TenKhachHang) : ""; }
        set { }
    }
public string  TenKhachHang {get; set;} // Mã loại hoá đơn


public string TenDonVi {get; set;} // Ký hiệu MẪU hoá đơn


public string UnsignedTenDonVi
{
    get { return TenDonVi != null ? FormatterString.ConvertToTenKhongDau(TenDonVi) : ""; }
    set { }
}

public string MaSoThue {get; set;} // Ký hiệu hoá đơn

public string TaiKhoanNganHang {get; set;} // Mã tiền tệ

public string DiaChi  {get; set;} //Trạng thái điều chỉnh hóa đơn


[BsonIgnore]
public string TenKhachHangShow => TenKhachHang + " - " + MaSoThue;

}
