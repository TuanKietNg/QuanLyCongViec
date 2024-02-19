using HDDT_BE.Constants;
using HDDT_BE.Interfaces.Core;
using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.Core;

public class ThuocModel : Audit, TEntity<string>
{
    
    public string Code { get; set; } // Code của hệ thống mới của mình 

    public int Sort { get; set; } = 0;
    
    public double DonGia { get; set; }
    
    public CommonModelShort ThueGTGT { get; set; }
    
    public int PhanTramChieuThue { get; set; }
    
    public int ChiecKhau { get; set; }
    
    public CommonModelShort TrangThai { get; set; }
    
    public double GiaMuaVao { get; set; }
    public string GiaMuaVaoShow 
    {
        get
        {
            return GiaMuaVao != null ? FormatMoney.ConvertToMoney(GiaMuaVao) : ""; 
        }
    }
    
    public double GiaLamTron { get; set; }
    public string GiaLamTronShow 
    {
        get
        {
            return GiaLamTron != null ? FormatMoney.ConvertToMoney(GiaLamTron) : ""; 
        }
    }
    
    public double PhiTuVan { get; set; }
    public string PhiTuVanShow 
    {
        get
        {
            return PhiTuVan != null ? FormatMoney.ConvertToMoney(PhiTuVan) : ""; 
        }
    }
    
    public double PhiTiem { get; set; }
    
    public string PhiTiemShow 
    {
        get
        {
            return PhiTiem != null ? FormatMoney.ConvertToMoney(PhiTiem) : ""; 
        }
    }
    
    public double DonGiaBanRa { get; set; }
    
    public string DonGiaBanRaShow 
    {
        get
        {
            return DonGiaBanRa != null ? FormatMoney.ConvertToMoney(DonGiaBanRa) : ""; 
        }
    }
    public List<CommonModelShort> DonViTinhs { get; set; }
    
    public CommonModelShort LoaiDichVuYTe { get; set; }
    
    public bool DuocGiamGia { get; set; } = false;

    public double SoTienGiamLanHai { get; set; }

}





public class ThuocModelShort
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }
    
    public string UnsignedName { get; set; }
    
    public string Code { get; set; } // Code của hệ thống mới của mình
    
    public int Sort { get; set; }
    
    public double DonGia { get; set; }
    
    public CommonModelShort ThueGTGT { get; set; }
    
    public int PhanTramChieuThue { get; set; }
    
    public int ChiecKhau { get; set; }
    
    public CommonModelShort TrangThai { get; set; }
    
    public CommonModelShort LoaiHangHoa { get; set; }
    
    public double GiaMuaVao { get; set; }
    
    public double GiaLamTron { get; set; }
    
    public double PhiTuVan { get; set; }
    
    public double PhiTiem { get; set; }
    
    public double DonGiaBanRa { get; set; }
    
    public List<CommonModelShort> DonViTinhs { get; set; }
    public CommonModelShort LoaiDichVuYTe { get; set; }
    
    public bool DuocGiamGia { get; set; } = false;

    public double SoTienGiamLanHai { get; set; }
    
}