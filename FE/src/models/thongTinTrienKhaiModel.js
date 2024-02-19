const toJson = (item) => {
    return {
        id: item.id,
        projectType: item.projectType,
        templateCode: item.templateCode,
        projectSeries: item.projectSeries,
        adjustmentType : item.adjustmentType,
        deploymentStatus : item.deploymentStatus ,
        cusGetProjectRight : item.cusGetProjectRight
    }
}

const fromJson = (item) => {
    return {
        id: item.id,
        projectType: item.projectType,
        templateCode: item.templateCode,
        projectSeries: item.projectSeries,
        adjustmentType : item.adjustmentType,
        deploymentStatus : item.deploymentStatus ,
        cusGetProjectRight : item.cusGetProjectRight
    }
}
const baseJson = () => {
    return {
        id: null,
        projectType: null,
        templateCode: null,
        projectSeries: null,
        adjustmentType : null,
        deploymentStatus : true ,
        cusGetProjectRight : true
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
export const thongTinTrienKhaiModel = {
    toJson, fromJson, baseJson, toListModel
}
