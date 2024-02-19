const toJson = (item) => {
    return {
        id: item.id,
        sellerLegalName: item.sellerLegalName,
        sellerTaxCode: item.sellerTaxCode,
        sellerAddressLine: item.sellerAddressLine,
        sellerPhoneNumber : item.sellerPhoneNumber,
        sellerFaxNumber : item.sellerFaxNumber,
        sellerEmail : item.sellerEmail ,
        sellerBankName : item.sellerBankName,
        sellerBankAccount : item.sellerBankAccount,
        sellerDistrictName : item.sellerDistrictName,
        sellerCityName : item.sellerCityName,
        sellerCountryCode : item.sellerCountryCode ,
        sellerWebsite : item.sellerWebsite,
    }
}

const fromJson = (item) => {
    return {
        id: item.id,
        sellerLegalName: item.sellerLegalName,
        sellerTaxCode: item.sellerTaxCode,
        sellerAddressLine: item.sellerAddressLine,
        sellerPhoneNumber : item.sellerPhoneNumber,
        sellerFaxNumber : item.sellerFaxNumber,
        sellerEmail : item.sellerEmail ,
        sellerBankName : item.sellerBankName,
        sellerBankAccount : item.sellerBankAccount,
        sellerDistrictName : item.sellerDistrictName,
        sellerCityName : item.sellerCityName,
        sellerCountryCode : item.sellerCountryCode ,
        sellerWebsite : item.sellerWebsite,
    }
}
const baseJson = () => {
    return {
        id: null,
        sellerLegalName: null,
        sellerTaxCode: null,
        sellerAddressLine: null,
        sellerPhoneNumber : null,
        sellerFaxNumber : null,
        sellerEmail : null ,
        sellerBankName : null,
        sellerBankAccount : null,
        sellerDistrictName : null,
        sellerCityName : null,
        sellerCountryCode : null,
        sellerWebsite : null
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
export const thongTinNguoiBanModel = {
    toJson, fromJson, baseJson, toListModel
}
