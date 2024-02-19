<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import {numeric, required} from "vuelidate/lib/validators";
import appConfig from "@/app.config";
import {notifyModel} from "@/models/notifyModel";
import {pagingModel} from "@/models/pagingModel";
import Multiselect from "vue-multiselect";
import {loaiDichVuYTeModel} from "@/models/loaiDichVuYTeModel";
export default {
  page: {
    title: "LOẠI DỊCH VỤ Y TẾ",
    meta: [{name: "description", content: appConfig.description}],
  },
  components: {Layout, PageHeader },
  data() {
    return {
      title: "LOẠI DỊCH VỤ Y TẾ",
      items: [
        {
          text: "LOẠI DỊCH VỤ Y TẾ",
          href: '/loai-dich-vu-y-te'
        },
        {
          text: "Danh sách",
          active: true,
        }
      ],
      data: [],
      fields: [
        { key: 'STT',
          label: 'STT',
          class: 'td-stt',
          sortable: false,
          thClass: 'hidden-sortable'},
        {
          key: "code",
          label: "Mã",
          class: 'td-username',
          sortable: true,
          thStyle: "text-align:center",
          thClass: 'hidden-sortable'
        },
        {
          key: "name",
          label: "Tên",
          class: 'td-ten',
          sortable: true,
          thStyle: "text-align:center",
          thClass: 'hidden-sortable'
        },
        {
          key: 'process',
          label: 'Xử lý',
          class: 'td-xuly',
          sortable: false,
          thClass: 'hidden-sortable'
        }
      ],
      currentPage: 1,
      perPage: 10,
      pageOptions: [5, 10, 25, 50, 100],
      showModal: false,
      showDeleteModal: false,
      submitted: false,
      sortBy: "age",
      filter: null,
      sortDesc: false,
      filterOn: [],
      numberOfElement: 1,
      totalRows: 1,
      list : [],
      model: loaiDichVuYTeModel.baseJson(),
      pagination: pagingModel.baseJson()
    };
  },
  created() {
    this.getThuoc();
  },
  computed: {
    //Validations
    rules(){
      return{
        code: {required},
        name: {required}
      }
    }
  },
  validations: {
    model: {
      code: {required},
      name: {required}
    },
  },
  methods: {
    normalizer(node){
      if(node.children == null || node.children == 'null'){
        delete node.children;
      }
    },
    async fnGetList() {
      this.$refs.tblList?.refresh()
    },
    async getThuoc() {
      await this.$store.dispatch("thuocStore/getAll").then((res) => {
        this.list = res.data || [];
      })
    },
    async handleDelete() {
      if (this.model.id != 0 && this.model.id != null && this.model.id) {
        await this.$store.dispatch("loaiDichVuYTeStore/delete", loaiDichVuYTeModel.toJson(this.model,"DM_LOAIDICHVUYTE")).then((res) => {
          if (res.code===0) {
            this.fnGetList();
            this.showDeleteModal = false;
          }
          var a = {
            message: res.message,
            code: res.code
          };
          console.log("LOG A : " ,a)
          this.$store.dispatch("snackBarStore/addNotify", {
            message: res.message,
            code: res.code
          });
        });
      }
    },
    handleShowDeleteModal(value) {
      this.model = value;
      this.showDeleteModal = true;
    },
    async handleSubmit(e) {
      e.preventDefault();
      this.submitted = true;
      this.$v.$touch();
      if (this.$v.$invalid) {
        return;
      } else {
        let loader = this.$loading.show({
          container: this.$refs.formContainer,
        });
        if (
            this.model.id != 0 &&
            this.model.id != null &&
            this.model.id
        ) {
          await this.$store.dispatch("loaiDichVuYTeStore/update", loaiDichVuYTeModel.toJson(this.model)).then((res) => {
            if (res.code === 0) {
              this.model = loaiDichVuYTeModel.baseJson();
              this.showModal = false;
              this.$refs.tblList.refresh();
            }
            this.$store.dispatch("snackBarStore/addNotify", {
              message: res.message,
              code: res.code,
            });
          });
        } else {
          await this.$store.dispatch("loaiDichVuYTeStore/create", loaiDichVuYTeModel.toJson(this.model)).then((res) => {
            if (res.code === 0) {
              this.fnGetList();
              this.showModal = false;
              this.model = loaiDichVuYTeModel.baseJson();
            }
            this.$store.dispatch("snackBarStore/addNotify", {
              message: res.message,
              code: res.code,
            });
          });
        }
        loader.hide();
      }

      this.submitted = false;
    },
    async handleGetValue(values) {
      await this.$store.dispatch("loaiDichVuYTeStore/getById", values).then((res) => {
        if (res.code===0) {
          console.log(res)
          this.model = loaiDichVuYTeModel.toJson(res.data);
          this.showModal = true;
        } else {
          this.$store.dispatch("snackBarStore/addNotify", {
            message: res.message,
            code: res.code,
          });
        }
      });
    },
    myProvider (ctx) {
      const params = {
        start: ctx.currentPage,
        limit: ctx.perPage,
        content: this.filter,
        sortBy: ctx.sortBy,
        sortDesc: ctx.sortDesc
      }
      this.loading = true
      try {
        let promise =  this.$store.dispatch("loaiDichVuYTeStore/getPagingParams", params)
        return promise.then(resp => {
          let items = resp.data.data
          this.totalRows = resp.data.totalRows
          this.numberOfElement = resp.data.data.length
          this.loading = false
          return items || []
        })
      } finally {
        this.loading = false
      }
    },
  },
  mounted() {
  },
  watch: {
    model: {
      deep: true,
      handler(val) {
      }
    },
    showModal(status) {
      if (status == false) this.model = loaiDichVuYTeModel.baseJson();
    },
    showDeleteModal(val) {
      if (val == false) {
        this.model.id = null;
      }
    }
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items"/>
    <div class="row">
      <div class="col-12">
        <div class="card">
          <div class="card-body">
            <div class="row mb-2">
              <div class="col-sm-4">
                <div class="search-box me-2 mb-2 d-inline-block">
                  <div class="position-relative">
                    <input
                        v-model="filter"
                        type="text"
                        class="form-control"
                        placeholder="Tìm kiếm ..."
                    />
                    <i class="bx bx-search-alt search-icon"></i>
                  </div>
                </div>
              </div>
              <div class="col-sm-8">
                <div class="text-sm-end">
                  <b-button
                      type="button"
                      class="btn cs-btn-primary btn-rounded mb-2 me-2"
                      size="sm"
                      @click="showModal = true"
                  >
                    <i class="mdi mdi-plus me-1"></i> Thêm mới
                  </b-button>
                  <b-modal
                      v-model="showModal"
                      title="Thông tin"
                      title-class="text-black font-18"
                      body-class="p-3"
                      hide-footer
                      centered
                      no-close-on-backdrop
                      size="lg"
                  >
                    <form @submit.prevent="handleSubmit"
                          ref="formContainer"
                    >
                      <div class="row">
                        <div class="col-6">
                          <div class="mb-3">
                            <label class="text-left">Mã Code</label>
                            <span style="color: red">&nbsp;*</span>
                            <input type="hidden" v-model="model.code"/>
                            <input
                                id="userName"
                                v-model.trim="model.code"
                                type="text"
                                class="form-control"
                                placeholder="Nhập mã code"
                                :class="{
                                'is-invalid':
                                  submitted && $v.model.code.$error,
                              }"
                            />
                            <div
                                v-if="submitted && !$v.model.code.required"
                                class="invalid-feedback"
                            >
                              Mã code không được trống.
                            </div>
                          </div>
                        </div>

                        <div class="col-6">
                          <div class="mb-3">
                            <label class="text-left">Tên</label>
                            <span style="color: red">&nbsp;*</span>
                            <input type="hidden" v-model="model.name"/>
                            <input
                                id="lastName"
                                v-model="model.name"
                                type="text"
                                class="form-control"
                                placeholder="Nhập tên"
                                :class="{
                                'is-invalid':
                                  submitted && $v.model.name.$error,
                              }"
                            />
                            <div
                                v-if="submitted && !$v.model.name.required"
                                class="invalid-feedback"
                            >
                              Tên không được trống.
                            </div>
                          </div>
                        </div>
                      </div>
<!--                      <div class="col-12">-->
<!--                        <div class="mb-3">-->
<!--                          <label class="text-left">Thuốc</label>-->
<!--                          <span style="color: red">&nbsp;*</span>-->
<!--                          <multiselect-->
<!--                              v-model="model.thuoc"-->
<!--                              :options="list"-->
<!--                              :multiple="true"-->
<!--                              track-by="id"-->
<!--                              selectLabel="Nhấn vào để chọn"-->
<!--                              deselectLabel="Nhấn vào để xóa"-->
<!--                              label="name"-->
<!--                              placeholder="Chọn thuốc"-->
<!--                              :class="{-->
<!--                                'is-invalid':-->
<!--                                  submitted && $v.model.name.$error,-->
<!--                              }"-->
<!--                            ></multiselect>-->
<!--                            <div-->
<!--                                v-if="submitted && !$v.model.name.required"-->
<!--                                class="invalid-feedback"-->
<!--                            >-->
<!--                              Tên thuốc không được trống.-->
<!--                            </div>-->
<!--                        </div>-->
<!--                      </div>-->
                      <div class="text-end pt-2 mt-3">
                        <b-button variant="light" @click="showModal = false" class="border-0">
                          Đóng
                        </b-button>
                        <b-button  type="submit" variant="success" class="ms-1 cs-btn-primary">Lưu
                        </b-button>
                      </div>
                    </form>
                  </b-modal>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-12">
                <div class="row mt-4">
                  <div class="col-sm-12 col-md-6">
                    <div
                        class="col-sm-12 d-flex justify-content-left align-items-center"
                    >
                      <div
                          id="tickets-table_length"
                          class="dataTables_length m-1"
                          style="
                        display: flex;
                        justify-content: left;
                        align-items: center;
                      "
                      >
                        <div class="me-1" >Hiển thị </div>
                        <b-form-select
                            class="form-select form-select-sm"
                            v-model="perPage"
                            size="sm"
                            :options="pageOptions"
                            style="width: 70px"
                        ></b-form-select
                        >&nbsp;
                        <div style="width: 100px"> dòng </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="table-responsive mb-0">
                  <b-table
                      class="datatables"
                      :items="myProvider"
                      :fields="fields"
                      striped
                      bordered
                      responsive="sm"
                      :per-page="perPage"
                      :current-page="currentPage"
                      :sort-by.sync="sortBy"
                      :sort-desc.sync="sortDesc"
                      :filter="filter"
                      :filter-included-fields="filterOn"
                      ref="tblList"
                      primary-key="id"
                  >
                    <template v-slot:cell(STT)="data">
                      {{ data.index + ((currentPage-1)*perPage) + 1  }}
                    </template>
                    <template v-slot:cell(thuoc)="row">
                      <div v-for="(item,index) in row.value" :key="index" class="pb-2">
                              <span >
                                {{item.name}}
                              </span>
                      </div>
                    </template>
                    <template v-slot:cell(process)="data">
                      <button
                          type="button"
                          size="sm"
                          class="btn btn-outline btn-sm"
                          v-on:click="handleGetValue(data.item.id)">
                        <i class="fas fa-pencil-alt text-success me-1"></i>
                      </button>
                      <button
                          type="button"
                          size="sm"
                          class="btn btn-outline btn-sm"
                          v-on:click="handleShowDeleteModal(data.item)">
                        <i class="fas fa-trash-alt text-danger me-1"></i>
                      </button>
                    </template>
                  </b-table>
                </div>
                <div class="row">
                  <b-col>
                    <div>Hiển thị {{numberOfElement}} trên tổng số {{totalRows}} dòng</div>
                  </b-col>
                  <div class="col">
                    <div
                        class="dataTables_paginate paging_simple_numbers float-end">
                      <ul class="pagination pagination-rounded mb-0">
                        <!-- pagination -->
                        <b-pagination
                            v-model="currentPage"
                            :total-rows="totalRows"
                            :per-page="perPage"
                        ></b-pagination>
                      </ul>
                    </div>
                  </div>
                </div>

              </div>
            </div>
            <b-modal
                v-model="showDeleteModal"
                centered
                title="Xóa dữ liệu"
                title-class="font-18"
                no-close-on-backdrop
            >
              <p>
                Dữ liệu xóa sẽ không được phục hồi!
              </p>
              <template #modal-footer>
                <button v-b-modal.modal-close_visit
                        class="btn btn-outline-info m-1"
                        v-on:click="showDeleteModal = false">
                  Đóng
                </button>
                <button v-b-modal.modal-close_visit
                        class="btn btn-danger btn m-1"
                        v-on:click="handleDelete">
                  Xóa
                </button>
              </template>
            </b-modal>
          </div>
        </div>
      </div>
    </div>
  </Layout>
</template>
<style>
.td-stt {
  text-align: center;
  width: 55px;
}
.td-username {
  width: 50px;
}
.td-ten {
  width: 140px;
}
.td-email {
  width: 120px;
}
.td-donVi{
  width: 180px;
}
.td-xuly {
  text-align: center;
  width: 80px;
}
.td-sodienthoai {
  text-align: center;
  width: 100px;
}
.hidden-sortable:after, .hidden-sortable:before {
  display: none !important;
}
</style>
