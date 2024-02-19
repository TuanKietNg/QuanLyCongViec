using DocumentFormat.OpenXml.Office.PowerPoint.Y2021.M06.Main;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using HDDT_BE.Constants;
using HDDT_BE.Models.Professional;

namespace HDDT_BE.ViewModels;

public class ChiTietDanhThuVM
{
    public string TenThuoc { get; set; }

    public double CongTuVan
    {
        get
        {
            try
            {
                double count = 0;
                if (Items.Count > 0)
                {
                    return Items.Sum(x => x.CongTuVan * x.SoLuong );
                }

                return count;
            }
            catch (Exception e)
            {
                return 0;
            }

            return 0;
        }
    }






    public string CongTuVanShow => CongTuVan != null ? FormatMoney.ConvertToMoney(CongTuVan) : "";



    
    public double CongTuVanChiTiet
    {
        get
        {
            try
            {
                double count = 0;
                if (Items.Count > 0)
                {
                    return  Items.Sum(x => x.CongTuVanItem * x.SoLuong );
                }

                return count;
            }
            catch (Exception e)
            {
                return 0;
            }

            return 0;
        }
    }






    public string CongTuVanChiTietShow => CongTuVanChiTiet != null ? FormatMoney.ConvertToMoney(CongTuVanChiTiet) : "";
    
    
    
    
    public double ChenhLech
    {
        get
        {
            try
            {
                double count = 0;
                if (Items.Count > 0)
                {
                    return Items.Sum(x => x.ChenhLechItem * x.SoLuong);
                    /*foreach (var item in Items)
                    {
                        count = count + item.ChenhLech;
                    }*/
                }
                return count;
            }
            catch (Exception e)
            {
                return 0;
            }
            return 0;
        }
    }
    public string ChenhLechShow => ChenhLech != null ? FormatMoney.ConvertToMoney(ChenhLech) : "";

    public string SoHD
    {
        get { return Items.Count.ToString(); }
    }
    public double SoLuong
    {
        get
        {
            try
            {
                if (Items.Count > 0)
                {
                    return Items.Sum(x => x.SoLuong);
                }
            }
            catch (Exception e)
            {
                return 0;
            }
            return 0;
        }
    }



    public double TongTienKhongGiam
    {
        get
        {
            double tong = 0;
            if (Items.Count > 0)
            {
                foreach (var item in Items)
                {
                    if (item.DonGia > item.GiaMua + item.PhiTiem + item.CongTuVan)
                    {
                        tong = tong + (item.DonGia * item.SoLuong);
                 //       i1++;
                    }else if (item.DonGia <  item.GiaMua + item.PhiTiem + item.CongTuVan)
                    {
                        tong = tong + (item.DonGia + item.CongTuVan) * item.SoLuong;
                 //       i2++;
                    }else  
                     if ((item.PhiTiem == 0 && item.CongTuVan == 0 ) || (item.DonGia == item.GiaMua + item.PhiTiem + item.CongTuVan))
                    {
                        tong = tong + (item.DonGia * item.SoLuong)  ;
                  //      i3++;
                    }
                }
                
            }
            return tong;
        }
    }


    public double TienVaccine
    {
        get
        {
            try
            {
                if (Items.Count > 0)
                {
                    return Items.Sum(x => x.TienVaccine * x.SoLuong);
                    
                }
            }
            catch (Exception e)
            {
                return 0;
            }
            return 0;
        }
    }
    public string TienVaccineShow => TienVaccine != null ? FormatMoney.ConvertToMoney(TienVaccine) : "";
    
    
    
    
    public double PhiTiem
    {
        get
        {
            try
            {
                if (Items.Count > 0)
                {
                    return Items.Sum(x => x.PhiTiem * x.SoLuong);
                }
            }
            catch (Exception e)
            {
                return 0;
            }
            return 0;
        }
    }
    
    
    public string PhiTiemShow => PhiTiem != null ? FormatMoney.ConvertToMoney(PhiTiem) : "";

    public double ThanhTien
    {
        get
        {
            try
            {
                double count = 0;
                if (Items.Count > 0)
                {
                    count = Items.Sum(x => x.ThanhTien );
                }
                return count;
            }
            catch (Exception e)
            {
                return 0;
            }
            return 0;
        }
    }
    public string ThanhTienShow  => ThanhTien != null ? FormatMoney.ConvertToMoney(ThanhTien) : ""; 
    public List<ItemExcelVM> Items { get; set; } = new List<ItemExcelVM>();


}


public class ItemExcelVM
{
    public string TenKhachHang { get; set; }
    public string TenThuoc { get; set; }
    public double GiaMua { get; set; }
    public string GiaMuaShow  => GiaMua != null ? FormatMoney.ConvertToMoney(GiaMua) : ""; 
    public double GiaLamTron { get; set; }

    public string GiaLamTronShow
    {
        get
        {
            if (PhiTiem == 0 && CongTuVan == 0)
            {
                return GiaMua != null ? FormatMoney.ConvertToMoney(GiaMua) : "" ;
            }
            else
            {
                return (GiaLamTron - GiaMua - PhiTiem - CongTuVan < 0) ? FormatMoney.ConvertToMoney(GiaLamTron + PhiTiem + CongTuVan) : FormatMoney.ConvertToMoney(GiaLamTron);
            }
            return "";
        }
    }
    public double PhiTiem { get; set; }
    public string PhiTiemShow  => PhiTiem != null ? FormatMoney.ConvertToMoney(PhiTiem) : ""; 
    public double CongTuVan { get; set; }
    public string CongTuVanShow  => CongTuVan != null ? FormatMoney.ConvertToMoney(CongTuVan) : "";



    public double CongTuVanItem
    {
        get
        {
            if (DonGia != GiaLamTron && DonGia !=  (GiaLamTron + PhiTiem + CongTuVan) || (DonGia != GiaLamTron && (DonGia == ThanhTien - CongTuVan)))
            {
                return 0; 
            }
            return CongTuVan;
        }
    }

    public string CongTuVanItemShow  => CongTuVanItem != null ? FormatMoney.ConvertToMoney(CongTuVanItem) : ""; 
    public double ChenhLech { get; set; }
    public string ChenhLechShow  => ChenhLech != null ? FormatMoney.ConvertToMoney(ChenhLech) : "";
    
    
    

    
    public string SoHoaDon { get; set; }
    
    
    public double DonGiaChuaLamTron { get; set; }
    public string DonGiaChuaLamTronShow  => DonGiaChuaLamTron != null ? FormatMoney.ConvertToMoney(DonGiaChuaLamTron) : ""; 
    
    public double DonGia { get; set; }
    public string DonGiaShow  => DonGia != null ? FormatMoney.ConvertToMoney(DonGia) : ""; 
    
    
    
    
    
 
    public double SoLuong { get; set; }
    public double TienVaccine { get; set; }



    public double ThanhTienChuaLamTron => GiaMua + PhiTiem + CongTuVan; 
    public string ThanhTienChuaLamTronShow =>  FormatMoney.ConvertToMoney(ThanhTienChuaLamTron);



    public double ChenhLechItem
    {
        get
        {
           
                if (PhiTiem == 0 && CongTuVan == 0)
                {
                    return 0;
                }
                else
                {
                    return (GiaLamTron - GiaMua - PhiTiem - CongTuVan  <  0 ) ?  GiaLamTron  - GiaMua : (GiaLamTron - GiaMua - PhiTiem - CongTuVan) ;
                }
            
        }
    }
    public string ChenhLechItemShow =>  FormatMoney.ConvertToMoney(ChenhLechItem);
    
    
    
    
    public double ThanhTien { get; set; }
    public string ThanhTienShow  => ThanhTien != null ? FormatMoney.ConvertToMoney(ThanhTien) : ""; 
    public string GhiChu { get; set; }
    public string CountCongTV { get; set; }
    
    
    public bool KhongTru { get; set; }
    
    
    public ItemExcelVM(){}
    public ItemExcelVM (ThongTinHangHoaModel model)
    {
        this.TenKhachHang = model.TenNguoiMua;
        this.TenThuoc = model.Thuoc.Name;
        this.GiaMua = model.GiaMuaVao;
        this.GiaLamTron = model.GiaLamTron;
        this.PhiTiem = model.PhiTiem;
        this.CongTuVan = model.PhiTuVan;
        this.ChenhLech = model.ChenhLech;
        this.SoHoaDon = model.InvoiceNo;
        this.DonGia = model.DonGia;
        this.SoLuong = model.SoLuong;
        this.ThanhTien = model.TongTien;
        this.TienVaccine = model.GiaMuaVao;
        this.KhongTru = model.KhongTru;
    }
}