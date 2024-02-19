<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import {numeric, required} from "vuelidate/lib/validators";
import appConfig from "@/app.config";
import {notifyModel} from "@/models/notifyModel";
import vue2Dropzone from "vue2-dropzone";
import {thongTinNguoiBanModel} from "@/models/thongTinNguoiBanModel";

export default {
  page: {
    title: "THÔNG TIN NGƯỜI BÁN",
    meta: [{name: "description", content: appConfig.description}],
  },
  components: {Layout, PageHeader },
  data() {
    return {
      data: [],
      title: "THÔNG TIN NGƯỜI BÁN",
      items: [
        {
          text: "THÔNG TIN NGƯỜI BÁN",
          active: true,
        }
      ],
      model: thongTinNguoiBanModel.baseJson(),
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
        sellerLegalName:{required},
        sellerTaxCode: {required},
        sellerAddressLine: {required},
        sellerPhoneNumber: {required},
        sellerBankAccount: {required}
      }
    }
  },
  validations: {
    model: {
      sellerLegalName: {required},
      sellerTaxCode: {required},
      sellerAddressLine: {required},
      sellerPhoneNumber: {required},
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

        // await this.$store.dispatch("hoaDonStore/getFile").then((res) => {
        //   var blob = new Blob([this.base64ToArrayBuffer(res.data)], {type: "application/pdf"});
        //   var link = document.createElement('a');
        //   link.href = window.URL.createObjectURL(blob);
        //   var fileName = "hoadon";
        //   link.download = fileName;
        //   link.click();
        // })
      await this.$store.dispatch("thongTinNguoiBanStore/create", this.model).then((res) => {
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
      let promise =  this.$store.dispatch("thongTinNguoiBanStore/getAll")
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
              <label for="">Tên đăng kí kinh doanh </label><span class="text-danger">*</span>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    v-model="model.sellerLegalName"
                    type="text"
                    class="form-control"
                    placeholder="Nhập tên đăng kí kinh doanh"
                    :class="{
                      'is-invalid':
                        submitted && $v.model.sellerLegalName.$error,
                    }"
                >
                <div
                  v-if="submitted && !$v.model.sellerLegalName.required"
                    class="invalid-feedback"
                >
                  Tên đăng ký kinh doanh không được trống.
                </div>
              </div>
            </div>
          </div>
          <!--Is Ative -->



          <div class="row">
            <div class="col-md-3">
              <label for="">Mã số thuế </label><span class="text-danger">*</span>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    v-model="model.sellerTaxCode"
                    type="text"
                    class="form-control "
                    placeholder="Nhập mã số thuế"
                    :class="{
                      'is-invalid':
                        submitted && $v.model.sellerTaxCode.$error,
                    }"
                  />
                  <div
                      v-if="submitted && !$v.model.sellerTaxCode.required"
                      class="invalid-feedback"
                  >
                    Mã số thuế không được trống.
                  </div>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3">
              <label for="">Địa chỉ của bên bán  </label><span class="text-danger">*</span>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    type="text"
                    class="form-control "
                    v-model="model.sellerAddressLine"
                    placeholder="Nhập địa chỉ của bên bán"
                    :class="{
                      'is-invalid':
                        submitted && $v.model.sellerAddressLine.$error,
                    }"
                  />
                  <div
                      v-if="submitted && !$v.model.sellerAddressLine.required"
                      class="invalid-feedback"
                  >
                    Địa chỉ của bên bán không được trống.
                  </div>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3">
              <label for="">Số điện thoại người bán </label><span class="text-danger">*</span>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    type="text"
                    class="form-control "
                    v-model="model.sellerPhoneNumber"
                    placeholder="Nhập số điện thoại người bán"
                    :class="{
                      'is-invalid':
                        submitted && $v.model.sellerPhoneNumber.$error,
                    }"
                  />
                  <div
                      v-if="submitted && !$v.model.sellerPhoneNumber.required"
                      class="invalid-feedback"
                  >
                    Số điện thoại người bán không được trống.
                  </div>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3">
              <label for="">Số fax người bán</label>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    type="text"
                    class="form-control "
                    placeholder="Nhập số fax người bán"
                    v-model="model.sellerFaxNumber"
                >
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3">
              <label for="">Địa chỉ thư điện tử người bán</label>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    type="text"
                    class="form-control "
                    placeholder="Nhập địa chỉ thư điện tử người bán"
                    v-model="model.sellerEmail"
                >
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3">
              <label for="">Tên ngân hàng </label>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    type="text"
                    class="form-control  "
                    placeholder="Nhập tên ngân hàng"
                    v-model="model.sellerBankName"
                >
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3">
              <label for="">Tên tài khoản ngân hàng </label>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    type="text"
                    class="form-control  "
                    v-model="model.sellerBankAccount"
                    placeholder="Nhập tên tài khoản ngân hàng"

                  />

              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3">
              <label for="">Tên Quận Huyện </label>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    type="text"
                    class="form-control  "
                    placeholder="Nhập tên quận huyện"
                    v-model="model.sellerDistrictName"
                >
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3">
              <label for="">Tên Tỉnh/Thành phố </label>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    type="text"
                    class="form-control  "
                    placeholder="Nhập tên tỉnh/thành phố"
                    v-model="model.sellerCityName"
                >
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3">
              <label for="">Mã quốc gia</label>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    type="text"
                    class="form-control  "
                    placeholder="Nhập mã quốc gia"
                    v-model="model.sellerCountryCode"
                >
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3">
              <label for="">Website của người bán</label>
            </div>
            <div class="col-md-9">
              <div class="form-group mb-3">
                <input
                    type="text"
                    class="form-control  "
                    placeholder="Nhập website của người bán"
                    v-model="model.sellerWebsite"
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
