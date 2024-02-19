import { formatNumber } from "./formatNumberModel";
const toJson = (item) => {
    return {
        id: item.id,
        code: item.code,
        name: item.name,
        thuocTinhs : item.thuocTinhs,
        trangThai : item.trangThai,
        loaiCongViec : item.loaiCongViec,
    }
}



const fromJson = (item) => {
    return {
        id: item.id,
        code: item.code,
        name: item.name,
        thuocTinhs : item.thuocTinhs,
        trangThai : item.trangThai,
        loaiCongViec : item.loaiCongViec,
    }
}



const baseJson = () => {
    return {
        id: null,
        code: null,
        name: null,
        thuocTinhs : null,
        trangThai : null,
        loaiCongViec :null,
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
export const congViecModel = {
    toJson, fromJson, baseJson, toListModel
}
