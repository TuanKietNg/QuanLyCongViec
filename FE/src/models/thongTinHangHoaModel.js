import {formatNumber} from "@/models/formatNumberModel";

const sendJson = (item) => {
  //  console.log("LOG SEND JSON : ",  formatNumber.FormatNumber(item.donGia),)
    return {
        ma: item.ma,
        loaiDichVuYTe : item.loaiDichVuYTe,
        thuoc : item.thuoc,
        donViTinh : item.donViTinh,
        tenHangHoa : item.tenHangHoa,
        soLuong : formatNumber.FormatNumber(item.soLuong),
        donGia :  formatNumber.FormatNumber(item.donGia),
        phiTuVan :  formatNumber.FormatNumber(item.phiTuVan),
        phiTiem : formatNumber.FormatNumber(item.phiTiem),
        khongTru : item.khongTru
    }
}

const sendJsonFirst = (item,  ma ,soLuong , donGia, khongTru) => {
    console.log("LOG SENDJSON FIRSE : ", khongTru)
    return {
        ma: ma,
        loaiDichVuYTe : item.loaiDichVuYTe,
        thuoc : item.thuoc,
        donViTinh : item.donViTinh,
        tenHangHoa : item.tenHangHoa,
        soLuong : formatNumber.FormatNumber(soLuong),
        donGia :  formatNumber.FormatNumber(donGia),
        phiTuVan :  formatNumber.FormatNumber(item.phiTuVan),
        phiTiem : formatNumber.FormatNumber(item.phiTiem),
        khongTru : khongTru
    }
}

const sendJsonLeftSection = (item) => {
    console.log("LOG DON GIA : ", item.donGia)
    return {
        ma: item.ma,
        loaiDichVuYTe : item.loaiDichVuYTe,
        thuoc : item.thuoc,
        donViTinh : item.donViTinh,
        tenHangHoa : item.tenHangHoa,
        soLuong : formatNumber.FormatNumber(item.soLuong) - 1,
        donGia :  formatNumber.FormatNumber(item.donGia),
        phiTuVan :  formatNumber.FormatNumber(item.phiTuVan),
        phiTiem : formatNumber.FormatNumber(item.phiTiem),
        khongTru : item.khongTru
    }
}




const baseJson = () => {
    return {
        ma: null,
        thuoc : null ,
        loaiDichVuYTe : null,
        donViTinh : null ,
        tenHangHoa : null,
        soLuong : 1,
        donGia : null,
        tongTien : null,
        phiTuVan : null,
        phiTiem : null,
        khongTru : true,
    }
}

export const thongTinHangHoaModel = {
    baseJson , sendJson,sendJsonFirst ,sendJsonLeftSection
}
