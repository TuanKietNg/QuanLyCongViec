import {apiClient} from "@/state/modules/apiClient";
const controller = "users";

export const state = {
    reloadAuthUser: false,
}

export const mutations = {
    SET_RELOADAUTHUSER(state, newValue) {
        state.reloadAuthUser = newValue
    }
}
export const actions = {
    async getAll({commit}) {
        return apiClient.get(controller + "/get-all");
    },
    async changePassword({commit}, params) {
        return apiClient.post(controller + "/change-password",params);
    },
    async getPagingParams({commit}, params) {
        return apiClient.post(controller +"/get-paging-params", params);
    },
    async getPagingParamsHandler({commit}, params) {
        return apiClient.post(controller +"/get-paging-params-handler", params);
    },
    async getAllHandler({commit}, params) {
        return apiClient.get(controller +"/get-all-handler");
    },
  async getPagingParamsMembers({commit}, params) {
    return apiClient.post(controller +"/get-paging-params-members", params);
  },
  async getPagingParamsMembersProject({commit}, params) {
    return apiClient.post(controller +"/get-paging-params-members-project", params);
  },
    async create({commit}, values) {
        return apiClient.post(controller +"/create", values);
    },
    async update({commit, dispatch}, values) {
        return apiClient.put(controller +"/update", values);
    },
    async delete({commit}, id) {
        return await apiClient.delete(controller +"/delete/" + id);
    },
    async getById({commit}, id) {
        return apiClient.get(controller +"/get-by-id/" + id);
    },
    async getByIdCongDan({commit}, id) {
        return apiClient.get(controller +"/get-by-id-cong-dan/" + id);
    },
    async changeProfile({commit, dispatch}, values) {
        return apiClient.put(controller + "/change-profile", values);
    },
};
