const toJson = (item) => {
    return {
        id: item.id,
        ManagerName: item.ManagerName,
        ManagerSex: item.ManagerSex,
        ManagerBirth: item.ManagerBirth,
        ManagerPosition : item.ManagerPosition,
        ManagerPhone : item.ManagerPhone,
        ManagerEmail : item.ManagerEmail ,
    }
}

const fromJson = (item) => {
    return {
        id: item.id,
        ManagerName: item.ManagerName,
        ManagerSex: item.ManagerSex,
        ManagerBirth: item.ManagerBirth,
        ManagerPosition : item.ManagerPosition,
        ManagerPhone : item.ManagerPhone,
        ManagerEmail : item.ManagerEmail ,
    }
}
const baseJson = () => {
    return {
        id: null,
        ManagerName: null,
        ManagerSex: null,
        ManagerBirth: null,
        ManagerPosition : null,
        ManagerPhone : null,
        ManagerEmail : null ,        
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
export const nguoiPhuTrachModel = {
    toJson, fromJson, baseJson, toListModel
}
