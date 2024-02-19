<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import {numeric, required} from "vuelidate/lib/validators";
import appConfig from "@/app.config";
import {pagingModel} from "@/models/pagingModel";
import Multiselect from "vue-multiselect";
import {commonModel} from "@/models/commonModel";
import {congViecModel} from "@/models/congViecModel";
import {VMoney} from "v-money";
import list from "echarts/src/data/List";
export default {
  page: {
    title: "CÔNG VIỆC",
    meta: [{name: "description", content: appConfig.description}],
  },
  // eslint-disable-next-line vue/no-unused-components
  components: {Layout, PageHeader, Multiselect,VMoney },
  directives: {money: VMoney},
  data() {
    return {
      title: "CÔNG VIỆC",
      items: [
        {
          text: "CÔNG VIỆC",
          href: '/congviec'
        },
        {
          text: "Danh sách",
          active: true,
        }
      ],
      data: [],
      list : [],
      listTrangThai : [],
      listThue : [],
      listLoaiCongViec : [], /////
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
          key: "loaiCongViec",
          label: "Loại công việc",
          class: 'td-ten',
          sortable: true,
          thStyle: "text-align:center",
          thClass: 'hidden-sortable'
        },
        {
          key: "thuocTinhs",
          label: "Thuộc tính",
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
      money: {
        decimal: ",",
        thousands: ".",
        prefix: "",
        suffix: "",
        precision: 0,
        masked: true,
      },
      filterOn: [],
      numberOfElement: 1,
      totalRows: 1,
      model: thuocModel.baseJson(),
      pagination: pagingModel.baseJson(),
    };
  },
  created() {
    this.getThuocTinh(); ////
    this.getTrangThai();
    this.getCongViec();
  },
  validations: {
    model: {
      name: {required},
      thuocTinhs: {required},
      trangThai: {required},
      loaiCongViec: {required},
    },
  },
  methods: {
    normalizer(node){
      if(node.children == null || node.children == 'null'){
        delete node.children;
      }
    },
    async getThuocTinh() {
      await this.$store.dispatch("commonStore/getAll", "DM_THUOCTINH").then((res) => {
        if (res.code == 401)
        {
          this.$store.dispatch("snackBarStore/addNotify", {
            message: res.message,
            code: res.code
          });
        }
        this.list = res.data || [];
      })
    },
    async getTrangThai() {
      await this.$store.dispatch("commonStore/getAll", "DM_TRANGTHAI").then((res) => {
        if (res.code == 401)
        {
          this.$store.dispatch("snackBarStore/addNotify", {
            message: res.message,
            code: res.code
          });
        }
        this.listTrangThai = res.data || [];
      })
    },

    async getCongViec() {
      await this.$store.dispatch("loaiCongViecStore/getAll").then((res) => {
        if (res.code == 401)
        {
          this.$store.dispatch("snackBarStore/addNotify", {
            message: res.message,
            code: res.code
          });
        }
        this.listCongViec = res.data || [];
      })
    },
    async fnGetList() {
         this.$refs.tblList?.refresh()
    },
    async handleDelete() {
      if (this.model.id != 0 && this.model.id != null && this.model.id) {
        await this.$store.dispatch("congViecStore/delete", congViecModel.fromJson(this.model)).then((res) => {
          if (res.code===0) {
            this.fnGetList();
            this.showDeleteModal = false;
          }
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
          await this.$store.dispatch("congViecStore/update", congViecModel.toJson(this.model)).then((res) => {
            if (res.code === 0) {
              this.showModal = false;
              this.$refs.tblList.refresh();
            }
            this.$store.dispatch("snackBarStore/addNotify", {
              message: res.message,
              code: res.code,
            });
          });
        } else {
          // Create model
          await this.$store.dispatch("congViecStore/create", congViecModel.toJson(this.model)).then((res) => {
            if (res.code === 0) {
              this.fnGetList();
              this.showModal = false;
              this.model={}
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
      await this.$store.dispatch("congViecStore/getById", values).then((res) => {
        if (res.code===0) {
          this.model = thuocModel.fromJson(res.data);
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
        let promise =  this.$store.dispatch("congViecStore/getPagingParams", params)
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
      if (status == false) this.model = commonModel.baseJson();
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
                        placeholder="Tìm kiếm "
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

                        <div class="col-4">
                          <div class="mb-3">
                            <label class="text-left">Tên công việc</label>
                            <span style="color: red">&nbsp;*</span>
                            <input type="hidden" v-model="model.name"/>
                            <input
                                id="lastName"
                                v-model.trim="model.name"
                                type="text"
                                class="form-control"
                                placeholder="Nhập tên công việc"
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
                        
                        <div class="col-4">
                          <div class="mb-3">
                            <label class="text-left">Thuộc tính</label>
                            <span style="color: red">&nbsp;*</span>
                            <multiselect
                                v-model="model.thuocTinhs"
                                :options="list"
                                :multiple="true"
                                track-by="id"
                                class="helo"
                                selectLabel="Nhấn vào để chọn"
                                deselectLabel="Nhấn vào để xóa"
                                label="name"
                                placeholder="Chọn thuộc tính"
                                :class="{
                                'is-invalid':
                                  submitted && $v.model.thuocTinhs.$error,
                              }"
                            ></multiselect>
                            <div
                                v-if="submitted && !$v.model.thuocTinhs.required"
                                class="invalid-feedback"
                            >
                              Thuộc tính không được trống.
                            </div>
                          </div>
                        </div>
                                            
                        <div class="col-6">
                          <div class="mb-3">
                            <label class="text-left">Trạng thái</label>
                            <span style="color: red">&nbsp;*</span>
                            <multiselect
                                v-model="model.trangThai"
                                :options="listTrangThai"
                                class="helo"
                                selectLabel="Nhấn vào để chọn"
                                deselectLabel="Nhấn vào để xóa"
                                label="name"
                                placeholder="Chọn đơn vị tính"
                                :class="{
                                'is-invalid':
                                  submitted && $v.model.trangThai.$error,
                              }"
                            ></multiselect>
                            <div
                                v-if="submitted && !$v.model.trangThai.required"
                                class="invalid-feedback"
                            >
                              Trạng thái không được trống.
                            </div>
                          </div>
                        </div>
                        <div class="col-6">
                          <div class="mb-3">
                            <label class="text-left">Loại công việc</label>
                            <span style="color: red">&nbsp;*</span>
                            <multiselect
                                v-model="model.loaiCongViec"
                                :options="listLoaiCongViec"
                                class="helo"
                                selectLabel="Nhấn vào để chọn"
                                deselectLabel="Nhấn vào để xóa"
                                label="name"
                                placeholder="Chọn loại hàng hoá"
                                :class="{
                                'is-invalid':
                                  submitted && $v.model.loaiCongViec.$error,
                              }"
                            ></multiselect>
                            <div
                                v-if="submitted && !$v.model.loaiCongViec.required"
                                class="invalid-feedback"
                            >
                              Loại công việc không được bỏ trống.
                            </div>
                          </div>
                        </div>

                      </div>

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
                    <template v-slot:cell(thuocTinhs)="row">
                      <div v-for="(item,index) in row.value" :key="index" class="mb-2">
                              <span>
                                {{item.name}}
                              </span>
                      </div>
                    </template>
                    <template v-slot:cell(loaiCongViec)="data">
                      <span v-if="data.item.loaiCongViec">
                         {{data.item.loaiCongViec.name}}
                      </span>

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
  width: 160px;
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
