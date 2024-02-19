import { formatNumber } from "./formatNumberModel";
const toJson = (item) => {
    //console.log("LOG DON GIA : ",  formatNumber.FormatNumber(item.donGia));
    return {
        id: item.id,
        code: item.code,
        name: item.name,
        donViTinhs : item.donViTinhs,
        donGia :  formatNumber.FormatNumber(item.donGia),
        thueGTGT  : item.thueGTGT,
        phanTramChieuThue : item.phanTramChieuThue,
        chiecKhau : item.chiecKhau,
        trangThai : item.trangThai,
        loaiDichVuYTe : item.loaiDichVuYTe,
        giaMuaVao : formatNumber.FormatNumber(item.giaMuaVao),
        giaLamTron: formatNumber.FormatNumber(item.giaLamTron),
        phiTuVan : formatNumber.FormatNumber(item.phiTuVan),
        phiTiem : formatNumber.FormatNumber(item.phiTiem),
        donGiaBanRa:formatNumber.FormatNumber(item.donGiaBanRa),
    }
}



const fromJson = (item) => {
    return {
        id: item.id,
        code: item.code,
        name: item.name,
        donViTinhs : item.donViTinhs,
        donGia : item.donGia,
        thueGTGT  : item.thueGTGT,
        phanTramChieuThue : item.phanTramChieuThue,
        chiecKhau : item.chiecKhau,
        trangThai : item.trangThai,
        loaiDichVuYTe : item.loaiDichVuYTe,
        giaMuaVao : item.giaMuaVao,
        giaLamTron: item.giaLamTron,
        phiTuVan : item.phiTuVan,
        phiTiem : item.phiTiem,
        donGiaBanRa:item.donGiaBanRa,
    }
}



const baseJson = () => {
    return {
        id: null,
        code: null,
        name: null,
        donViTinhs : null,
        donGia : null,
        thueGTGT  : null,
        phanTramChieuThue :  0,
        chiecKhau : 0,
        trangThai : null,
        loaiDichVuYTe :null,
        giaMuaVao : 0,
        giaLamTron: 0,
        phiTuVan : 0,
        phiTiem : 0,
        donGiaBanRa: 0
    }
}

const toListModel = (items) =>{
    if(items.length > 0){
        let data = [];
        items.map((value, index) =>{
            data.push(fromJson(value));
        })
        return data??[];
    }
    return [];
}
export const thuocModel = {
    toJson, fromJson, baseJson, toListModel
}
