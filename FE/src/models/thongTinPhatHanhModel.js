const toJson = (item) => {
    return {
        id: item.id,
        invoiceType: item.invoiceType,
        templateCode: item.templateCode,
        invoiceSeries: item.invoiceSeries,
        currencyCode : item.currencyCode,
        adjustmentType : item.adjustmentType,
        paymentStatus : item.paymentStatus ,
        cusGetInvoiceRight : item.cusGetInvoiceRight
    }
}

const fromJson = (item) => {
    return {
        id: item.id,
        invoiceType: item.invoiceType,
        templateCode: item.templateCode,
        invoiceSeries: item.invoiceSeries,
        currencyCode : item.currencyCode,
        adjustmentType : item.adjustmentType,
        paymentStatus : item.paymentStatus ,
        cusGetInvoiceRight : item.cusGetInvoiceRight
    }
}
const baseJson = () => {
    return {
        id: null,
        invoiceType: null,
        templateCode: null,
        invoiceSeries: null,
        currencyCode : null,
        adjustmentType : null,
        paymentStatus : true ,
        cusGetInvoiceRight : true
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
export const thongTinPhatHanhModel = {
    toJson, fromJson, baseJson, toListModel
}
