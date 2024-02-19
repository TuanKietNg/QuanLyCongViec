<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import {numeric, required} from "vuelidate/lib/validators";
import appConfig from "@/app.config";
import {notifyModel} from "@/models/notifyModel";
import vue2Dropzone from "vue2-dropzone";
import {nguoiPhuTrachModel} from "@/models/nguoiPhuTrachModel";

export default {
  page: {
    title: "THÔNG TIN NGƯỜI PHỤ TRÁCH",
    meta: [{name: "description", content: appConfig.description}],
  },
  components: {Layout, PageHeader },
  data() {
    return {
      data: [],
      title: "THÔNG TIN NGƯỜI PHỤ TRÁCH",
      items: [
        {
          text: "THÔNG TIN NGƯỜI PHỤ TRÁCH",
          active: true,
        }
      ],
      model: nguoiPhuTrachModel.baseJson(),
      showModal : false,
      dropzoneOptions: {
        url: `${process.env.VUE_APP_API_URL}files/upload`,
        thumbnailWidth: 300,
        thumbnailHeight: 160,
        maxFiles: 4,
        maxFilesize: 30,
        headers: { "My-Awesome-Header": "header value" },
        addRemoveLinks: true,
        acceptedFiles: ".pdf",
      },
      url :  process.env.VUE_APP_API_URL + 'files/view/' ,
      submitted: false,
    };
  },
  computed: {
    //Validations
    rules(){
      return{
        ManagerName:{required},
        ManagerSex: {required},
        ManagerBirth: {required},
        ManagerPosition: {required},
        ManagerPhone: {required},       
        ManagerEmail: {required}
      }
    }
  },
  validations: {
    model: {
        ManagerName:{required},
        ManagerSex: {required},
        ManagerBirth: {required},
        ManagerPosition: {required},
        ManagerPhone: {required},       
        ManagerEmail: {required}
    },
  },
  created() {
    this.getThongTin()
  },
  methods: {
    async handleSubmitThongTin(e) {
      console.log("LOG THONG TIN ", this.model)
      e.preventDefault();
      this.submitted = true;
      this.$v.$touch();
      console.log("LOG INVALID 36165 : ", this.$v);
      if (this.$v.$invalid) {
        console.log("LOG INVALID  : ")
        return;
      } else {
        console.log("LOG ELSE  INVALID  : ")
        let loader = this.$loading.show({
          container: this.$refs.formContainer,
        });
      await this.$store.dispatch("nguoiPhuTrachStore/create", this.model).then((res) => {
        if (res.code === 0) {
          this.model = res.data
          this.showModal = false;
        }
        this.$store.dispatch("snackBarStore/addNotify", notifyModel.addMessage(res))
      });
        loader.hide();
        this.submitted = false;

    }
    this.submitted = false;
},

    async getThongTin(){
      let promise =  this.$store.dispatch("nguoiPhuTrachStore/getAll")
      return promise.then(resp => {
        if(resp.data == null){
          return []
        }else{
          if (resp.data != null && resp.data.length > 0 )
          {
           this.model = resp.data[0]
          }
        }
      })
    },

  },
  watch: {},
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items"/>

    <div class="card">
      <div class="card-body">
        <form ref="formContainer">
          <!-- API Key -->
          <div class="row">
            <div class="col-md-3">
              <label for="">Tên người phụ trách </label><span class="text-danger">*</span>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    v-model="model.ManagerName"
                    type="text"
                    class="form-control"
                    placeholder="Nhập tên người phụ trách"
                    :class="{
                      'is-invalid':
                        submitted && $v.model.ManagerName.$error,
                    }"
                >
                <div
                  v-if="submitted && !$v.model.ManagerName.required"
                    class="invalid-feedback"
                >
                  Tên người phụ trách không được trống.
                </div>
              </div>
            </div>
          </div>
          <!--Is Ative -->



          <div class="row">
            <div class="col-md-3">
              <label for="">Giới tính </label><span class="text-danger">*</span>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    v-model="model.ManagerSex"
                    type="text"
                    class="form-control "
                    placeholder="Nhập giới tính"
                    :class="{
                      'is-invalid':
                        submitted && $v.model.ManagerSex.$error,
                    }"
                  />
                  <div
                      v-if="submitted && !$v.model.ManagerSex.required"
                      class="invalid-feedback"
                  >
                    Giới tính không được trống.
                  </div>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3">
              <label for="">Ngày sinh  </label><span class="text-danger">*</span>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    type="text"
                    class="form-control "
                    v-model="model.ManagerBirth"
                    placeholder="Nhập ngày sinh"
                    :class="{
                      'is-invalid':
                        submitted && $v.model.ManagerBirth.$error,
                    }"
                  />
                  <div
                      v-if="submitted && !$v.model.ManagerBirth.required"
                      class="invalid-feedback"
                  >
                    Ngày sinh không được trống.
                  </div>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3">
              <label for="">Chức vụ người phụ trách </label><span class="text-danger">*</span>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    type="text"
                    class="form-control "
                    v-model="model.ManagerPosition"
                    placeholder="Nhập chức vụ người phụ trách"
                    :class="{
                      'is-invalid':
                        submitted && $v.model.ManagerPosition.$error,
                    }"
                  />
                  <div
                      v-if="submitted && !$v.model.ManagerPosition.required"
                      class="invalid-feedback"
                  >
                    Chức vụ người phụ trách không được trống.
                  </div>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3">
              <label for="">Số điện thoại người phụ trách</label>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    type="text"
                    class="form-control "
                    placeholder="Nhập số điện thoại người phụ trách"
                    v-model="model.ManagerPhone"
                >
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3">
              <label for="">Email người phụ trách</label>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    type="text"
                    class="form-control "
                    placeholder="Nhập email người phụ trách"
                    v-model="model.ManagerEmail"
                >
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-12 text-center" >
              <b-button
                  class="btn cs-btn-primary btn-rounded mb-2 me-2"
                  variant="success"
                  v-on:click="handleSubmitThongTin"
              >
                <i class="bx bx-save "></i>
                Lưu thông tin
              </b-button>
            </div>
          </div>
        </form>
      </div>
    </div>

  </Layout>
</template>
<style>
.cs-edit-file{
  background: rgba(241, 102, 92, 0.1);
  padding: 0px 10px;
  border-radius: 5px;
  width: fit-content;
}
</style>
