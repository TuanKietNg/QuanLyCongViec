using System.Globalization;
using HDDT_BE.Constants;

namespace HDDT_BE.ViewModels;

public class TongDanhThuVM
{
    public List<ChiTietDanhThuVM> ChiTietDanhThu { get; set; } = new List<ChiTietDanhThuVM>();
    
    public double TongCongTV
    {
    get
        {
            try
            {
                double count = 0;
                if (ChiTietDanhThu.Count > 0)
                {
                    foreach (var item in ChiTietDanhThu)
                    {
                        count = count + item.CongTuVan;
                    }
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
    public string TongCongTVShow => TongCongTV != null ? FormatMoney.ConvertToMoney(TongCongTV) : ""; 
    
    
    
    
    public double TongCongTVItem
    {
        get
        {
            try
            {
                double count = 0;
                if (ChiTietDanhThu.Count > 0)
                {
                    foreach (var item in ChiTietDanhThu)
                    {
                        count = count + item.CongTuVanChiTiet;
                    }
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
    public string TongCongTVItemShow => TongCongTVItem != null ? FormatMoney.ConvertToMoney(TongCongTVItem) : ""; 
    
    
    
    
    public double TongSoHoaDon
    {
        get
        {
            try
            {
                double count = 0;
                if (ChiTietDanhThu.Count > 0)
                {
                    foreach (var item in ChiTietDanhThu)
                    {
                        count = count + item.SoLuong;
                    }
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

    public double TongPhiTiem
    {
        get
        {
            try
            {
                double count = 0;
                if (ChiTietDanhThu.Count > 0)
                {
                    foreach (var item in ChiTietDanhThu)
                    {
                        count = count + item.PhiTiem;
                    }
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
    public string TongPhiTiemShow => TongPhiTiem != null ? FormatMoney.ConvertToMoney(TongPhiTiem) : ""; 
    
    
    
    
         


    public double TongChenhLech
    {
        get
        {
            try
            {
                double count = 0;
                if (ChiTietDanhThu.Count > 0)
                {
                    foreach (var item in ChiTietDanhThu)
                    {
                        count = count + item.ChenhLech;
                    }
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
    public string TongChenhLechShow => TongChenhLech != null ? FormatMoney.ConvertToMoney(TongChenhLech) : ""; 
    
    

    public double TienVaccine
    {
        get
        {
            try
            {
                double count = 0;
                if (ChiTietDanhThu.Count > 0)
                {
                    foreach (var item in ChiTietDanhThu)
                    {
                        count = count + item.TienVaccine;
                    }
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
    
    public string TienVaccineShow => TienVaccine != null ? FormatMoney.ConvertToMoney(TienVaccine) : ""; 
    public double TongThanhTien
    {
        get
        {
            try
            {
                double count = 0;
                if (ChiTietDanhThu.Count > 0)
                {
                    foreach (var item in ChiTietDanhThu)
                    {
                        count = count + item.ThanhTien;
                    }
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
    
    public string TongThanhTienShow => TongThanhTien != null ? FormatMoney.ConvertToMoney(TongThanhTien) : "";




    
    public string TongTienBangChu
    {
        get { return TongThanhTien != 0 ? FormatterString.NumberToText(TongThanhTien) : "Không"; }
    }
}
