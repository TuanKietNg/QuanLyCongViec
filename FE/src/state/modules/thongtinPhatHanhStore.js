import {apiClient} from "@/state/modules/apiClient";
const controller = "ThongTinPhatHanh";

export const state = {
    reloadAuthUser: false,
}

export const mutations = {
    SET_RELOADAUTHUSER(state, newValue) {
        state.reloadAuthUser = newValue
    }
}
export const actions = {
    async getAll({commit}, collectionName) {
        return apiClient.get(controller + "/get-all-core" );
    },
    async create({commit}, values) {
        return apiClient.post(controller +"/create", values);
    },
    async update({commit, dispatch}, values) {
        return apiClient.post(controller +"/update", values);
    }
};
