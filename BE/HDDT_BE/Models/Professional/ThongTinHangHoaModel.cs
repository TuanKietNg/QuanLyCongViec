using HDDT_BE.Constants;
using HDDT_BE.Models.Core;
using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.Professional;


// Thong tin hang hoa
public class ThongTinHangHoaModel : Audit, TEntity<string>
{
    public string ma { get; set; }

    public LoaiDichVuYTeModelShort LoaiDichVuYTe{
        get;
        set;
    }
    
    public ThuocModelShort Thuoc{
        get;
        set;
    }
    
    public CommonModelShort DonViTinh{
        get;
        set;
    }
    public string TenHangHoa { get; set; }
    
    
    

    public double DonGia { get; set; } = 0; 
    
    [BsonIgnore]
    public string DonGiaShow 
    {
        get
        {
            return DonGia != null ? FormatMoney.ConvertToMoney(DonGia) : ""; 
        }
    }


    public double ChenhLech
    {
        get
        {
            if (KhongTru)
            {
                return DonGia - GiaMuaVao - PhiTiem - PhiTuVan;
            }
            else
            {
                return DonGia - GiaMuaVao - PhiTiem;
            }
        }
        set{}
    }
    
    
    
    [BsonIgnore]
    public string ChenhLechShow 
    {
        get
        {
            return ChenhLech != null ? FormatMoney.ConvertToMoney(ChenhLech) : ""; 
        }
    }



    
    
    
    public double GiaMuaVao { get; set; }
    
    [BsonIgnore]
    public string GiaMuaVaoShow 
    {
        get
        {
            return GiaMuaVao != null ? FormatMoney.ConvertToMoney(GiaMuaVao) : ""; 
        }
    }
    
    public double GiaLamTron { get; set; }
    
    
    [BsonIgnore]
    public string GiaLamTronShow 
    {
        get
        {
            return GiaLamTron != null ? FormatMoney.ConvertToMoney(GiaLamTron) : ""; 
        }
    }
    
    public double PhiTuVan { get; set; }
    
    [BsonIgnore]
    public string PhiTuVanShow 
    {
        get
        {
            return PhiTuVan != null ? FormatMoney.ConvertToMoney(PhiTuVan) : ""; 
        }
    }
    
    public double PhiTiem { get; set; }
    
    
    [BsonIgnore]
    public string PhiTiemShow 
    {
        get
        {
            return PhiTiem != null ? FormatMoney.ConvertToMoney(PhiTiem) : ""; 
        }
    }
    
    public double SoLuong { get; set; } = 0;

    
    public double TongTien
    {
        get
        {
            return  SoLuong * DonGia;
        }
        set
        {
        }
    }
    
    [BsonIgnore]
    public string TongTienShow 
    {
        get
        {
            return TongTien != null ? FormatMoney.ConvertToMoney(TongTien) : ""; 
        }
    }

    
    public string HoaDonId { get; set; }
    
    
    public string FullName { get; set; }
    
    
    public string UnsignedFullName
    {
        get { return FullName != null ? FormatterString.ConvertToTenKhongDau(FullName) : ""; }
        set { }
    }

    
    public string InvoiceNo { get; set; }
    
    
    public string StatusCode { get; set; }
    
    
    public string TenNguoiMua { get; set; }

    
    public string StatusName { get; set; }
    
    public int? CreatedAtString
    {
        get { return CreatedAt.HasValue ? FormatDate.ConvertDatetimeQG(CreatedAt) : null; }
        set {}
    }
    
    public bool KhongTru { get; set; } = false;

}

public class ThongTinHangHoaShortModel
{
    public string ma { get; set; }

    public LoaiDichVuYTeModelShort LoaiDichVuYTe{
        get;
        set;
    }
    
    public ThuocModelShort Thuoc{
        get;
        set;
    }
    
    public CommonModelShort DonViTinh{
        get;
        set;
    }
    public string TenHangHoa { get; set; }

    public double DonGia { get; set; } 


    public double SoLuong { get; set; } 


    public double TongTien { get; set; } 

    
    public string HoaDonId { get; set; }
    
    
    
    public string UserName { get; set; }
    
    


}