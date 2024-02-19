using HDDT_BE.Models.Professional;

namespace HDDT_BE.ViewModels
{
    public class ExportVM
    {
        public string NgayTao { get; set; }
        
        public string TenHangHoa { get; set; }
        
        public string DonGia { get; set; }
        
        public double SoLuong { get; set; }
        
        public string TongTien { get; set; }
        
        public string GiaMuaVao { get; set; }
    
        public string GiaLamTron { get; set; }
        
        public string PhiTuVan { get; set; }
    
        public string PhiTiem { get; set; }
        
        public string LoaiDichVuYTe { get; set; }
        
        public string FullName { get; set; }
        public string TrangThai { get; set; }
        public string ChenhLech { get; set; }
        public string SoHoaDon { get; set; }
        
        
        
        public ExportVM(string NgayTao , string TenHangHoa, string DonGia,
            double SoLuong, string TongTien, string GiaMuaVao, string GiaLamTron, string PhiTuVan,
            string PhiTiem, string FullName ,  LoaiDichVuYTeModelShort  LoaiDichVuYTe,string SoHoaDon, string TrangThai)
        {
            this.NgayTao = NgayTao;
            this.TenHangHoa = TenHangHoa;
            this.DonGia = DonGia;
            this.SoLuong = SoLuong;
            this.TongTien = TongTien;
            this.GiaLamTron = GiaLamTron;
            this.GiaMuaVao = GiaMuaVao;
            this.PhiTuVan = PhiTuVan;
            this.PhiTiem = PhiTiem;
            this.FullName = FullName;
            this.LoaiDichVuYTe = LoaiDichVuYTe != null ? LoaiDichVuYTe.Name : "";
            this.SoHoaDon = SoHoaDon;
            this.TrangThai = TrangThai; 
        }
     
    }
}