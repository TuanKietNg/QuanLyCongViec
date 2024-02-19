<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import DatePicker from "vue2-datepicker";
import appConfig from "@/app.config";
import {pagingModel} from "@/models/pagingModel";
import Multiselect from "vue-multiselect";
import {VMoney} from 'v-money'
import {hoaDonModel} from "@/models/hoaDonModel";
import {thongTinHangHoaModel} from "@/models/thongTinHangHoaModel";
import {required} from "vuelidate/lib/validators";
import {deleteModel} from "@/models/deleteModel";
import {formatNumber} from "@/models/formatNumberModel";
import {khachHangModel} from "@/models/khachHangModel";
import Switches from "vue-switches";
export default {
  computed: {
    formatNumber() {
      return formatNumber
    }
  },
  page: {
    title: "TRA CỨU HOÁ ĐƠN",
    meta: [{name: "description", content: appConfig.description}],
  },
  // eslint-disable-next-line vue/no-unused-components
  components: {Multiselect, Layout, PageHeader, DatePicker,VMoney},
  directives: {money: VMoney},
  data() {
    return {
      title: "TRA CỨU HOÁ ĐƠN",
      items: [
        {
          text: "TRA CỨU HOÁ ĐƠN",
          href: '/tra-cuu',
        },
        {
          text: "Danh sách",
          active: true,
        }
      ],
      data: [],
      fields: [
        {
          key: 'STT',
          label: 'STT',
          class: 'td-5',
          thClass: 'hidden-sortable'
        },
        {
          key: "createdAtShow",
          label: "Ngày phát hành",
          class: 'td-10',
          thClass: 'hidden-sortable'
        },
        {
          key: "fullName",
          label: "Nhân viên phát hành",
          class: 'td-20',
          thClass: 'hidden-sortable'
        },
        {
          key: "tenKhachHang",
          label: "Tên khách hàng",
          class: 'td-20',
          thClass: 'hidden-sortable'
        },
        {
          key: "invoiceNo",
          label: "Số hoá đơn",
          class: 'td-15',
          thClass: 'hidden-sortable'
        },
        {
          key: "maSoThue",
          label: "Mã số thuế",
          class: 'td-15',
          thClass: 'hidden-sortable'
        },
        {
          key: "chuyenDoi",
          label: "Chuyển đổi",
          class: 'td-8',
          thClass: 'hidden-sortable'
        },
        {
          key: "tongTienShow",
          label: "Tổng tiền",
          class: 'td-10',
          thClass: 'hidden-sortable'
        },
        {
          key: "statusName",
          label: "Trạng thái",
          class: 'td-15',
          thClass: 'hidden-sortable'
        },
        {
          key: 'process',
          label: '',
          class: 'td-15',
          thClass: 'hidden-sortable'
        }
      ],
      fieldsModal: [
        {
          key: "tenHangHoa",
          label: "Tên hàng hoá",
          class: 'td-username',
          thStyle: "text-align:center",
          thClass: 'hidden-sortable'
        },
        {
          key: "donGia",
          label: "Đơn giá",
          class: 'td-15',
          thClass: 'hidden-sortable'
        },
        {
          key: "soLuong",
          label: "Số lượng",
          class: 'td-15',
          thClass: 'hidden-sortable'
        },
        {
          key: "donViTinh",
          label: "Đơn vị tính",
          class: 'td-20',
          thClass: 'hidden-sortable'
        },

        {
          key: 'process',
          label: '',
          class: "td-5",
          thClass: 'hidden-sortable'
        }
      ],
      money: {
        decimal: ",",
        thousands: ".",
        prefix: "",
        suffix: "",
        precision: 0,
        masked: false,
      },
      currentPage: 1,
      pageOptions: [5, 10, 25, 50, 100],
      showModal: false,
      showDeleteModal: false,
      showDeleteModalNhap: false,
      showDeleteTableModal: false,
      submitted: false,
      isUpdate : false,
      isUpdateList : false,
      submittedPH: false,
      showPhatHanhModal: false,
      submittedHuy: false,
      sortBy: "age",
      filter: null,
      sortDesc: false,
      isShow : false,
      filterOn: [],
      isDelete : true,
      numberOfElement: 1,
      totalRows: 1,
      perPage : 10,
      stt: 20,
      valueLoaiDichVuYTe: null,
      valueThuoc : null,
      valueDonViTinh : null,
      model: hoaDonModel.baseJson(),
      huyModel : deleteModel.baseJson(),
      modelThongTin: thongTinHangHoaModel.baseJson(),
      list: [],
      listKhachHang:[],
      tongTien : 0 ,
      giamGia : 0 ,
      listHinhThuc: [],
      listLoaiDichVu: [],
      isSend : true,
      listThuoc : [],
      listDonViTinh : [],
      valueKhachHang:null,
      pagination: pagingModel.baseJson(),
      itemFilter: {
        hinhThucThanhToan : null,
        ngayBatDau:new Date(this.getCurrentDateFormatted()),
        ngayKetThuc:new Date(this.getCurrentDateFormatted()),
        content:null,
        userName:null,
        trangThai:"ALL"
      },
      isCollapseSearch: false,
      listNguoiPhatHanh:[],
      listTrangThai:[
        {
          code:"ALL",
          name:"CHỌN TẤT CẢ"
        },
        {
          code:"NHAP",
          name:"Hóa đơn nháp"
        },
        {
          code:"DAPHATHANH",
          name:"Hóa đơn đã phát hành"
        },
        {
          code:"THAYTHE",
          name:"Hóa đơn thay thế"
        },
        {
          code:"DAXOA",
          name:"Hóa đơn đã xóa"
        }
      ]
    };
  },
  created() {
    console.log("LOG : ", )
    this.getListHinhThuc();
    this.getListLoaiDichVu();
    this.getListNguoiPhatHanh()
  },
  validations: {
    huyModel : {
      vanban: {required},
      lydo: {required}
    },
    model: {
      hinhThucThanhToan : {required},
      tenKhachHang: {required},
      diaChi: {required}
    },
    modelThongTin : {
      tenHangHoa: {required},
    },
    valueDonViTinh : {required},
    valueThuoc : {required},
    valueLoaiDichVuYTe : {required}


  },
  methods: {
    normalizer(node) {
      if (node.children == null || node.children == 'null') {
        delete node.children;
      }
    },
    async getListDonViTinh() {
      await this.$store.dispatch("commonStore/getAll", "DM_DONVITINH").then((res) => {
        this.listDonViTinh = res.data || [];

      })
    },
    async getListKhachHang() {
      await this.$store.dispatch("khachHangStore/getAll").then((res) => {
        console.log("LOG khach hang : ", res.data)
        this.listKhachHang = res.data || [];
      })
    },
    async getListNguoiPhatHanh() {
      await this.$store.dispatch("userStore/getAll").then((res) => {
        this.listNguoiPhatHanh = res.data || [];
        this.itemFilter.userName = this.listNguoiPhatHanh.at(0);
      })
    },
    async getListHinhThuc() {
      await this.$store.dispatch("commonStore/getAll", "DM_HINHTHUCTHANHTOAN").then((res) => {
        this.listHinhThuc = res.data || [];
      })
    },
    async getListLoaiDichVu() {
      await this.$store.dispatch("loaiDichVuYTeStore/getAll").then((res) => {
        console.log("LOG LOAI DICH VU : ", res.data)
        this.listLoaiDichVu = res.data || [];
      })
    },
    async fnGetList() {
      this.$refs.tblList?.refresh()
    },
    async handleClose() {
      this.showDeleteTableModal = false ;
      this.valueDonViTinh = null;
      this.valueThuoc = null;
      this.valueLoaiDichVuYTe = null;
      this.modelThongTin = thongTinHangHoaModel.baseJson();
    },
    handleSearch() {
      this.itemFilter.isSearch = true
      this.$refs.tblList.refresh();
    },
    handleClear(){
      this.itemFilter.hinhThucThanhToan=null,
          this.itemFilter.ngayBatDau=new Date(this.getCurrentDateFormatted()),
          this.itemFilter.ngayKetThuc=new Date(this.getCurrentDateFormatted()),
          this.itemFilter.content=null,
          this.itemFilter.fill=null,
          this.itemFilter.trangThai="ALL",
          this.itemFilter.userName=null

    },
    async handleModalClose() {
      this.showModal =false;
      this.isUpdate =false;
      this.handleBaseValue();
    },




    async handleUpdateList(item) {
      this.isUpdateList = true;
      this.valueLoaiDichVuYTe = item.loaiDichVuYTe;
      console.log("LOG BEFORE : ", this.isUpdateList)
      this.modelThongTin = item ;
      this.valueDonViTinh = item.donViTinh ;
      this.valueThuoc = item.thuoc;


    },
    async handleUpdate(id) {
      this.getListDonViTinh();
      this.getListKhachHang();
      await this.$store.dispatch("hoaDonStore/getById", id).then((res) => {
        if (res.code===0) {
          console.log(res)
          this.model = hoaDonModel.toJson(res.data);
          this.list = this.model.list;
          this.tongTien = this.model.tongTien;
          this.$refs.tblList?.refresh()
          this.isUpdate = true;
          this.showModal = true;
        } else {
          this.$store.dispatch("snackBarStore/addNotify", {
            message: res.message,
            code: res.code,
          });
        }
      });
    },


    async handleConversionInvoice(id) {
      await this.$store.dispatch("hoaDonStore/SendConversionInvoice", id).then((res) => {
          if (res.data != null)
          {
            var blob = new Blob([this.base64ToArrayBuffer(res.data)], {type: "application/pdf"});
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            var name = "hoadon";
            link.download = name;
            link.click();
            this.$refs.tblList.refresh();
          }
          this.$store.dispatch("snackBarStore/addNotify", {
            message: res.message,
            code: res.code,
          });
      });
    },
    async handleSendInvoiceFromData(value) {
        this.model = value;
        this.showPhatHanhModal = true;
    },
    async handleClosePhatHanh() {
      this.showPhatHanhModal = false ;
      this.model = hoaDonModel.baseJson();
    },

    async handleSendInvoiceFromDataAPI() {
      if (
          this.model.id != null
      ) {
        let loader = this.$loading.show({
          container: this.$refs.formContainer,
        });
        this.isSend = false;
        await this.$store.dispatch("hoaDonStore/SendInvoiceFromData",this.model).then((res) => {
          if (res.data != null)
          {
              this.showModal= false;
              this.showPhatHanhModal = false;
              this.$refs.tblList?.refresh()
          }
          this.$store.dispatch("snackBarStore/addNotify", {
            message: res.message,
            code: res.code,
          });
        })
        loader.hide();
        this.isSend = true;

      }
    },

    async handleDeleteModal() {
   //   console.log("LOG XÓA HAND DELETED MODAL : " , this.modelThongTin)
      if (this.modelThongTin.ma != 0 && this.modelThongTin.ma != null) {
        this.model.list = this.model.list.filter(x => {
          if (x.ma != this.modelThongTin.ma)
            return x;
        })
        if (this.modelThongTin.khongTru) {
          this.isDeleteTinhPhi = true;
        }
        if (this.modelThongTin.ma != 0 && this.modelThongTin.ma != null) {
          this.model.list = this.model.list.filter(x => {
            if (x.ma != this.modelThongTin.ma) {
              return x;
            }
          })
          this.listThongTin = [];
          this.tongTien = 0;
          this.model.list.forEach((element) => {
            //  console.log("LOG NGUYE NTUAN KIET ", this.model.list, element);
            if (this.isDeleteTinhPhi && element.thuoc.duocGiamGia
                && element.loaiDichVuYTe.code === "09") {
              if (element.soLuong > 1) {
    //            console.log("LOG SO LUONG > 1  ", this.isDeleteTinhPhi, formatNumber.FormatNumber(this.tongTien), element);
                this.tongTien = formatNumber.FormatNumber(this.tongTien) + formatNumber.FormatNumber(element.donGia) + element.thuoc.soTienGiamLanHai;
                this.listThongTin.push(thongTinHangHoaModel.sendJsonFirst(element, element.ma + 1, 1, formatNumber.FormatNumber(element.donGia) + element.thuoc.soTienGiamLanHai, true));
                element.donGia = formatNumber.FormatNumber(element.donGia);
                // console.log("LOG MODEL THONG TIN : ", this.tongTien,formatNumber.FormatNumber(this.modelThongTin.donGia) , this.modelThongTin.thuoc.soTienGiamLanHai)
                this.tongTien = formatNumber.FormatNumber(this.tongTien) + (formatNumber.FormatNumber(element.donGia) * (element.soLuong - 1));
                //      console.log("LOG MODEL THONG TIN 2: ",this.tongTien, formatNumber.FormatNumber(this.modelThongTin.donGia) , this.modelThongTin.soLuong-1)
                this.listThongTin.push(thongTinHangHoaModel.sendJsonLeftSection(element));
              } else {
       //         console.log("LOG SO LUONG < 1 ", this.isDeleteTinhPhi, element.soLuong);
                this.tongTien = formatNumber.FormatNumber(this.tongTien) + (formatNumber.FormatNumber(element.donGia) + element.thuoc.soTienGiamLanHai);
                element.donGia = formatNumber.FormatNumber(element.donGia) + element.thuoc.soTienGiamLanHai;
                element.khongTru = true;
                this.listThongTin.push(thongTinHangHoaModel.sendJson(element));
              }
              this.isDeleteTinhPhi = false;
            } else {
         //     console.log("LOG BINH THUONG < 1 ", this.isDeleteTinhPhi, element.soLuong);
              this.tongTien = formatNumber.FormatNumber(this.tongTien) + (formatNumber.FormatNumber(element.donGia));
              element.donGia = formatNumber.FormatNumber(element.donGia);
              this.listThongTin.push(thongTinHangHoaModel.sendJson(element));
            }
          });
          this.model.list = this.listThongTin;
          this.stt++;

          this.showDeleteTableModal = false;
          /*        this.tongTien = formatNumber.FormatNumber(this.tongTien) - (formatNumber.FormatNumber(this.modelThongTin.donGia)* this.modelThongTin.soLuong) ;
        this.modelThongTin = thongTinHangHoaModel.baseJson();
        this.showDeleteTableModal = false;*/
        }
      }
    },

    async handleSendReplacementInvoiceFromDataAPI() {
      if (
          this.model.id != null
      ) {
        let loader = this.$loading.show({
          container: this.$refs.formContainer,
        });
        this.isSend = false;
        await this.$store.dispatch("hoaDonStore/SendReplacementInvoiceFromData",this.model).then((res) => {
          if (res.data != null)
          {
            this.showPhatHanhModal = false;
            this.showModal = false;
            this.$refs.tblList?.refresh()
          }
          this.$store.dispatch("snackBarStore/addNotify", {
            message: res.message,
            code: res.code,
          });
        })
        loader.hide();
        this.isSend = true;

      }
    },

    async handleDelete(value) {
      if (value.statusCode ==="NHAP")
      {
         this.showDeleteModalNhap = true;
      }
      else
      {
        this.showDeleteModal = true;
      }

      this.huyModel.id = value.id;
    },
    handleShowDeleteModalTable(value) {
    /*  this.modelThongTin.ma = value.ma;
      this.modelThongTin.soLuong = value.soLuong;
      this.modelThongTin.donGia = value.donGia;
      this.showDeleteTableModal = true;*/
      this.isDeleteTinhPhi = false;
      this.modelThongTin.ma = value.ma;
      this.modelThongTin.soLuong = value.soLuong;
      this.modelThongTin.donGia = value.donGia;
      this.modelThongTin.khongTru = value.khongTru;
      this.showDeleteTableModal = true;
    },
    async handleDeleteAPI() {
      this.submittedHuy = true;
 //     console.log("LOG SUBMIT LIST  : ",this.model)
      this.$v.$touch();
      if (this.$v.huyModel.$invalid ) {
  //      console.log("LOG INVALID  : ", this.$v)
        return;
      } else {
        this.isDelete = false;
        await this.$store.dispatch("hoaDonStore/delete", this.huyModel).then((res) => {
          if (res.data.code === 0) {
            this.$refs.tblList?.refresh()
            this.showDeleteModal = false;
          }
          this.$store.dispatch("snackBarStore/addNotify", {
            message: res.data.message,
            code: res.data.code,
          });
        });
      }
      this.isDelete = true;
    },

    async handleDeleteNhapAPI() {
      //  e.preventDefault();
        this.isDelete = false;
        await this.$store.dispatch("hoaDonStore/delete", this.huyModel).then((res) => {
    //      console.log("LOG RES ", res.data)
          if (res.data.code === 0) {
            // this.model = hoaDonModel.toJson(res.data);
            // this.list = this.model.list;
            this.$refs.tblList?.refresh()
            // this.isUpdate = true;
            // this.showModal = true;
            this.showDeleteModalNhap = false;
          }
          this.$store.dispatch("snackBarStore/addNotify", {
            message: res.data.message,
            code: res.data.code,
          });
        });
      this.isDelete = true;
    },
    async handleBaseThongTin() {
      this.modelThongTin = thongTinHangHoaModel.baseJson();
      this.valueDonViTinh = null;
      this.valueThuoc = null;
      this.valueLoaiDichVuYTe = null;
    },

    formatPrice(value) {
      return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")
    },
    async handleInvoiceModal() {
      if (
          this.model.id != null
      ) {
        let loader = this.$loading.show({
          container: this.$refs.formContainer,
        });
        await this.$store.dispatch("hoaDonStore/GetInvoiceDraftFromData", this.model.id).then((res) => {
          if (res.data != null)
          {
            var blob = new Blob([this.base64ToArrayBuffer(res.data)], {type: "application/pdf"});
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            var name = "hoadon";
            link.download = name;
            link.click();
          }
          this.$store.dispatch("snackBarStore/addNotify", {
            message: res.message,
            code: res.code,
          });
        })
        loader.hide();
      }
    },
    async handleInvoiceFromData(id) {
      let loader = this.$loading.show({
        container: this.$refs.formContainer,
      });
   //   console.log("LOG INVOICE : ", id);

      try {
        const response = await this.$store.dispatch("hoaDonStore/GetInvoiceDraftFromData", id);
        if (response.data != null) {

          await this.$store.dispatch("hoaDonStore/getFileViettel",response.data).then((res) => {
            if (res  == null || (res != null && res.fileToBytes == null))
            {
              this.$store.dispatch("snackBarStore/addNotify", {
                message: "Tải file thất bại",
                code: 400,
              });
            }
            var blob = new Blob([this.base64ToArrayBuffer(res.fileToBytes)], { type: "application/pdf" });
            this.openAndPrintPDF(blob, "hoadon.pdf"); // Set the desired file name here
          })


      //     var blob = new Blob([this.base64ToArrayBuffer(response.data)], { type: "application/pdf" });
      // //    var blob = new Blob([this.base64ToArrayBuffer(res.data)], {type: "application/pdf"});
      //     var link = document.createElement('a');
      //     link.href = window.URL.createObjectURL(blob);
      //     var name = "hoadon";
      //     link.download = name;
      //     link.click();
        }
        this.$store.dispatch("snackBarStore/addNotify", {
          message: response.message,
          code: response.code,
        });
      } catch (error) {
        // Handle error here
        console.error("Error fetching invoice data:", error);
      }

      loader.hide();
    },


    async handleShowInvoiceFromData(id) {
  //    console.log("LOG SHOW INVOICE : ", id);
      let loader = this.$loading.show({
        container: this.$refs.formContainer,
      });
//     console.log("LOG INVOICE : ", id);

      try {
        const response = await this.$store.dispatch("hoaDonStore/GetInvoiceDraftFromData", id);
        if (response.data != null) {
          var blob = new Blob([this.base64ToArrayBuffer(response.data)], { type: "application/pdf" });
          this.openAndPrintPDF(blob, "hoadon.pdf"); // Set the desired file name here
        }
        this.$store.dispatch("snackBarStore/addNotify", {
          message: response.message,
          code: response.code,
        });
      } catch (error) {
        // Handle error here
        console.error("Error fetching invoice data:", error);
      }
      loader.hide();
    },

    openAndPrintPDF(blob, fileName) {
      // Create a new URL for the Blob object
      const url = window.URL.createObjectURL(blob);

      // Open a new window to display the PDF
      const newWindow = window.open(url, "_blank");

      // Check if the new window was opened successfully
      if (newWindow) {
        // Wait for the new window to finish loading the PDF
        newWindow.onload = () => {
          // Print the PDF
          newWindow.print();
        };
      } else {
        // Handle error if the new window couldn't be opened
        console.error("Failed to open the PDF in a new window.");
      }

      // Set the download attribute of a temporary anchor element to trigger the download
      // const link = document.createElement("a");
      // link.href = url;
      // link.download = fileName;
      // link.style.display = "none";
      // document.body.appendChild(link);
      // link.click();
      // document.body.removeChild(link);
    },


    base64ToArrayBuffer(base64)
    {
      var binaryString = window.atob(base64);
      var binaryLen = binaryString.length;
      var bytes = new Uint8Array(binaryLen);
      for (var i = 0; i < binaryLen; i++) {
        var ascii = binaryString.charCodeAt(i);
        bytes[i] = ascii;
      }
      return bytes;
    },
    myProvider(ctx) {
      const params = {
        start: ctx.currentPage,
        limit: ctx.perPage,
        content: this.filter,
        sortBy: ctx.sortBy,
        sortDesc: ctx.sortDesc,
        hinhThucThanhToan:this.itemFilter.hinhThucThanhToan,
        startDate: this.itemFilter.ngayBatDau
            ? new Date(this.itemFilter.ngayBatDau)
            : null,
        endDate:this.itemFilter.ngayKetThuc
            ? new Date(this.itemFilter.ngayKetThuc)
            : null,
        contentFill:this.itemFilter.content,
        userName:this.itemFilter.userName,
        fill:this.itemFilter.fill,
        trangThai:this.itemFilter.trangThai
      }
      this.loading = true
      try {
        let promise = this.$store.dispatch("hoaDonStore/getSearchPagingParams", params)
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
    getCurrentDateFormatted() {
      const currentDate = new Date();
      const day = String(currentDate.getDate()).padStart(2, '0');
      const month = String(currentDate.getMonth() + 1).padStart(2, '0');
      const year = currentDate.getFullYear();
      return `${month},${day},${year}`;
    },
    async handleBaseValue() {
      this.list = [];
      this.model = hoaDonModel.baseJson();
      this.model.hinhThucThanhToan =  this.listHinhThuc.at(0);
      this.valueKhachHang = null;
      this.valueDonViTinh = null;
      this.valueThuoc = null;
      this.valueLoaiDichVuYTe = null
    },
    async handleSubmitList(e) {
    //  console.log("LOG HANDLED SUBMITE LIST ")
      e.preventDefault();
      this.submitted = true;
      this.$v.$touch();
        if (this.$v.modelThongTin.$invalid || this.model.list == 0)
        {
          this.$store.dispatch("snackBarStore/addNotify", {
            message: "Thông tin khách hàng và danh sách hàng hóa không được bỏ trống",
            code: 20,
          });
        return;
      } else {
  //           console.log("LOG ELE BASE : ", this.modelThongTin.ma)
          this.modelThongTin.ma = this.stt;
          this.modelThongTin.loaiDichVuYTe = this.valueLoaiDichVuYTe;
          this.modelThongTin.thuoc =  this.valueThuoc;
          this.modelThongTin.donViTinh = this.valueDonViTinh;
          if (this.model.list.length == 0 && this.modelThongTin.thuoc.duocGiamGia
              && this.modelThongTin.loaiDichVuYTe.code ==="09")
          {
            //   console.log("LOG 1 : ")
            if (this.modelThongTin.soLuong > 1)
            {
//              console.log("LOG THONG TIN SO LUONG: ", this.modelThongTin.ma)
              this.tongTien = formatNumber.FormatNumber(this.tongTien) + (formatNumber.FormatNumber(this.modelThongTin.donGia)) ;
              this.model.list.push(thongTinHangHoaModel.sendJsonFirst(this.modelThongTin,this.modelThongTin.ma + 20,1,this.modelThongTin.donGia, true));
              this.modelThongTin.donGia = formatNumber.FormatNumber(this.modelThongTin.donGia) - this.modelThongTin.thuoc.soTienGiamLanHai;
              // console.log("LOG MODEL THONG TIN : ", this.tongTien,formatNumber.FormatNumber(this.modelThongTin.donGia) , this.modelThongTin.thuoc.soTienGiamLanHai)
              this.tongTien = formatNumber.FormatNumber(this.tongTien) + (formatNumber.FormatNumber(this.modelThongTin.donGia) * (this.modelThongTin.soLuong-1));
              //      console.log("LOG MODEL THONG TIN 2: ",this.tongTien, formatNumber.FormatNumber(this.modelThongTin.donGia) , this.modelThongTin.soLuong-1)
              this.model.list.push(thongTinHangHoaModel.sendJsonLeftSection(this.modelThongTin));
            }else {
              this.tongTien = formatNumber.FormatNumber(this.tongTien) + (formatNumber.FormatNumber(this.modelThongTin.donGia)) ;
              this.modelThongTin.donGia = formatNumber.FormatNumber(this.modelThongTin.donGia);
              this.model.list.push(thongTinHangHoaModel.sendJson(this.modelThongTin));
            }
          }
          else if (this.model.list.length > 0 && this.modelThongTin.thuoc.duocGiamGia
              && this.modelThongTin.loaiDichVuYTe.code ==="09") {
            //      console.log("LOG 2 :", this.model.list )
            this.model.list.filter(x => {
              if (x.loaiDichVuYTe.code === "09" && x.thuoc.duocGiamGia) {
                this.giamGia = this.modelThongTin.thuoc.soTienGiamLanHai;
              }
            })
            if (this.giamGia === 0) {
   //                 console.log(" GIAM GIA === 0 , " ,this.giamGia)
              this.tongTien = formatNumber.FormatNumber(this.tongTien) + (formatNumber.FormatNumber(this.modelThongTin.donGia));
              this.model.list.push(thongTinHangHoaModel.sendJsonFirst(this.modelThongTin, this.modelThongTin.ma + 20,  1, this.modelThongTin.donGia,true));
              this.modelThongTin.donGia = formatNumber.FormatNumber(this.modelThongTin.donGia) - this.modelThongTin.thuoc.soTienGiamLanHai;
 //                   console.log("LOG MODEL THONG TIN : ", this.tongTien, formatNumber.FormatNumber(this.modelThongTin.donGia), this.modelThongTin.thuoc.soTienGiamLanHai)
              this.tongTien = formatNumber.FormatNumber(this.tongTien) + (formatNumber.FormatNumber(this.modelThongTin.donGia) * (this.modelThongTin.soLuong - 1));
 //                   console.log("LOG MODEL THONG TIN 2: ", this.tongTien, formatNumber.FormatNumber(this.modelThongTin.donGia), this.modelThongTin.soLuong - 1)
              this.model.list.push(thongTinHangHoaModel.sendJsonLeftSection(this.modelThongTin));
            } else {
 //                  console.log(" GIAM CÓ GIAM GIA   , " ,this.tongTien , )
              this.modelThongTin.donGia = formatNumber.FormatNumber(this.modelThongTin.donGia) - this.giamGia;
              this.tongTien = formatNumber.FormatNumber(this.tongTien) + (formatNumber.FormatNumber(this.modelThongTin.donGia) * this.modelThongTin.soLuong);
              //      console.log(" SAU KHI GIAM CÓ GIAM GIA   , " ,this.tongTien ,formatNumber.FormatNumber(this.modelThongTin.donGia) , this.modelThongTin.soLuong )
              this.model.list.push(thongTinHangHoaModel.sendJson(this.modelThongTin));
            }
          }else {
  //                console.log("LOG LIST BREAK ")
            this.modelThongTin.donGia = formatNumber.FormatNumber(this.modelThongTin.donGia);
            this.model.list.push(thongTinHangHoaModel.sendJson(this.modelThongTin));
            this.tongTien = formatNumber.FormatNumber(this.tongTien) + (formatNumber.FormatNumber(this.modelThongTin.donGia)* this.modelThongTin.soLuong) ;
          }
          this.stt++;
          this.valueThuoc = null;
          this.valueLoaiDichVuYTe =null ;
          this.valueDonViTinh = null ;
          this.modelThongTin = thongTinHangHoaModel.baseJson();
          this.giamGia = 0;
          this.loading = false;
          this.$refs.tblListTable?.refresh()
//          console.log("LOG LIST : ", this.model.list)
      }
    },
    disabledEndDate(date) {
      return this.itemFilter.ngayBatDau && date.setHours(0, 0, 0, 0) < new Date(this.itemFilter.ngayBatDau).setHours(0, 0, 0, 0);
    },
    disabledEndTime(date) {
      return this.itemFilter.ngayBatDau && date < this.itemFilter.ngayBatDau;
    },
    filterNonNumeric(event) {
      const inputValue = event.target.value;
      const numericRegex = /^[0-9]+$/;
      if (!numericRegex.test(inputValue)) {
        const filteredValue = inputValue.replace(/[^0-9]/g, '');
        this.model.maSoThue = filteredValue;
      }
    },
  },
  watch: {
    model: {
      deep: true,
      handler(val){
        if (this.model.list != null && this.model.list.length > 0)
        {
          this.isDisabled = true ;
          this.isShow = true;
        }else {
          this.isDisabled = false;
          this.isShow = false
        }
      }
    },
    valueLoaiDichVuYTe: {
      deep: true,
      handler(val) {
        if (!this.isUpdateList)
        {
          this.listThuoc = [];
          this.modelThongTin = thongTinHangHoaModel.baseJson();
          this.valueThuoc = null;
          this.valueDonViTinh = null;

          if (this.valueLoaiDichVuYTe != null && this.valueLoaiDichVuYTe.thuoc.length > 0 )
          {
            this.listThuoc = this.valueLoaiDichVuYTe.thuoc;
          }
        }else {
          this.isUpdateList = false;
        }
      }
    },
    valueThuoc: {
      deep: true,
      handler(val) {
        if ( this.valueThuoc != null && this.valueThuoc.donViTinhs.length  > 0)
        {
          // this.listDonViTinh = this.valueThuoc.donViTinhs;
          this.modelThongTin.tenHangHoa = this.valueThuoc.name;
          this.modelThongTin.donGia = this.valueThuoc.donGiaBanRa;
          this.modelThongTin.phiTiem = this.valueThuoc.phiTiem;
          this.modelThongTin.phiTuVan = this.valueThuoc.phiTuVan;
          if (this.listDonViTinh.length > 0)
          {
            this.valueDonViTinh = this.listDonViTinh.at(0);
          }
        }
      }
    },
    valueKhachHang:{
      deep:true,
      handler(){
        if(this.valueKhachHang!=null){
          this.$store.dispatch("khachHangStore/getById", this.valueKhachHang).then((res) => {
            if (res.code===0) {
        //      console.log(res)
              this.tempModel = khachHangModel.toJson(res.data);
              this.model.tenDonVi=this.tempModel.tenDonVi
              this.model.tenKhachHang=this.tempModel.tenKhachHang
              this.model.maSoThue=this.tempModel.maSoThue
              this.model.diaChi=this.tempModel.diaChi
              this.model.taiKhoanNganHang=this.tempModel.taiKhoanNganHang
              this.model.khachHang=this.tempModel.tenKhachHang
            } else {
              return;
            }
          });
        }
        else {
          this.model.tenDonVi=null
          this.model.tenKhachHang=null
          this.model.maSoThue=null
          this.model.diaChi=null
          this.model.taiKhoanNganHang=null
          this.model.khachHang=null
        }

      }
    },
    showDeleteModalNhap (val)
    {
        this.isDelete = true;
    },
    showModal(status) {
      if (status == false) {
        this.model = hoaDonModel.baseJson();
      }else {
        if (this.model.hinhThucThanhToan == null && this.listHinhThuc.length > 0 );
        this.model.hinhThucThanhToan =  this.listHinhThuc.at(0);
      }
    },
    showDeleteModal(val) {
      if (val == false) {
        this.model.id = null;
        this.isDelete = true;
        this.huyModel = deleteModel.baseJson();
      }
    },
    'itemFilter.trangThai': {
      deep: true,
      handler(newvalue,oldvalue){
        this.itemFilter.trangThai=newvalue
      }
    },
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
            <div class="row">
              <div class="mb-3 col-lg-4">
                <label>Nội dung</label>
                <input
                    v-model="itemFilter.content"
                    size="sm"
                    type="text"
                    name="untyped-input"
                    class="form-control"
                    placeholder="Nhập tên khách hàng/ đơn vị/mã số thuế"
                />
              </div>
              <div class="mb-3 col-lg-4">
                <label>Hình thức thanh toán </label>
                <multiselect v-model="itemFilter.hinhThucThanhToan"
                             label="name"
                             selectLabel="Nhấn vào để chọn"
                             deselectLabel="Nhấn vào để xóa"
                             :options="listHinhThuc.map((type) => type.id)"
                             :custom-label="
                              (opt) => listHinhThuc.find((x) => x.id == opt).name
                              "
                             placeholder="Chọn hình thức thanh toán"
                >
                </multiselect>
              </div>
              <div class="mb-3 col-lg-4">
                <label>Người phát hành </label>
                <multiselect v-model="itemFilter.userName"
                             label="fullName"
                             selectLabel="Nhấn vào để chọn"
                             deselectLabel="Nhấn vào để xóa"
                             :options="listNguoiPhatHanh"
                             placeholder="Chọn người phát hành"
                >
                </multiselect>
              </div>
              <div class="mb-3 col-lg-4">
                <label>Trạng thái hóa đơn</label>
                <multiselect v-model="itemFilter.trangThai"
                             label="name"
                             selectLabel="Nhấn vào để chọn"
                             deselectLabel="Nhấn vào để xóa"
                             :options="listTrangThai.map((type) => type.code)"
                             :custom-label="
                              (opt) => listTrangThai.find((x) => x.code == opt).name
                              "
                             placeholder="Chọn trạng thái hóa đơn"

                >
                </multiselect>
              </div>
              <div class="mb-3 col-lg-4">
                <label>Thời gian bắt đầu</label>
                <date-picker
                    v-model="itemFilter.ngayBatDau"
                    format="DD/MM/YYYY"
                ></date-picker>
              </div>
              <div class="mb-3 col-lg-4">
                <label>Thời gian kết thúc</label>
                <date-picker
                    v-model="itemFilter.ngayKetThuc"
                    format="DD/MM/YYYY"
                    :disabled-date="disabledEndDate"
                    :disabled-time="disabledEndTime"
                ></date-picker>
              </div>
            </div>
            <div class="row">
              <div class="col-12 text-center">
                <b-button variant=""
                          class="w-10 cs-btn-primary"
                          style="margin-right: 10px ; height: 30px ; width: 100px"
                          @click="handleSearch"
                          size="sm"
                >
                  <i
                      class="bx bx-search font-size-16 align-middle me-2"
                  ></i>
                  Tìm kiếm
                </b-button>
                <b-button
                    class="w-10 cs-btn-primary"
                    style="margin-right: 10px; height: 30px ; width: 100px"
                    @click="handleClear"
                    size="sm"
                >
                  <i class="mdi mdi-replay font-size-16 align-middle me-2"></i>
                  Làm mới
                </b-button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <div class="card">
          <div class="card-body">
            <div class="row">
              <div class="col-sm-12">
                <div class="text-sm-end">
                  <b-modal
                      v-model="showModal"
                      title="Thông tin"
                      title-class="text-black font-18"
                      body-class="p-1"
                      hide-footer
                      centered
                      no-close-on-backdrop
                      size="xl"
                      class="modal-xl"
                  >
                    <template #modal-header="{  }">
                      <div class="fw-bolder font-size-18">THÔNG TIN HOÁ ĐƠN</div>
                      <div class="text-end ">
                        <b-button
                            type="submit"
                            variant=""
                            @click="handleSubmitList"
                            class="ms-1 cs-btn-primary"
                        >Thêm
                        </b-button>
                        <b-button
                            type="submit"
                            variant=""
                            @click="handleSendReplacementInvoiceFromDataAPI"
                            class="ms-1 cs-btn-primary"
                        >LẬP HÓA ĐƠN THAY THẾ
                        </b-button>
                        <b-button variant="light"  class="ms-1" @click = "handleModalClose">
                          Đóng
                        </b-button>
                      </div>
                    </template>
                    <form  ref="formContainer">
                      <div class="row md-0">
                        <div class="col-md-6 md-0" style="margin-bottom: -20px">
                          <div class="table-custom-srollbar-dt">
                            <b-table
                                class="datatables"
                                :items="this.model.list"
                                :fields="fieldsModal"
                                striped
                                bordered
                                responsive="sm"
                                :current-page="currentPage"
                                :sort-by.sync="sortBy"
                                :sort-desc.sync="sortDesc"
                                :filter="filter"
                                :filter-included-fields="filterOn"
                                ref="tblListTable"
                                primary-key="id"
                            >
                              <template v-slot:cell(donGia)="data">
                                {{formatPrice(data.item.donGia)}}
                              </template>
                              <template v-slot:cell(donViTinh)="data">
                                {{data.item.donViTinh.name}}
                              </template>
                              <template v-slot:cell(process)="data">
                                <button
                                    type="button"
                                    size="sm"
                                    class="btn btn-outline btn-sm"
                                    v-on:click="handleShowDeleteModalTable(data.item)">
                                  <i class="fas fa-trash-alt text-danger me-1"></i>
                                </button>
                              </template>
                            </b-table>
                          </div>
                          <div class="money" v-if="tongTien > 0">
                            <span class="font-weight-bold font-size-15"> TỔNG TIỀN : </span>
                            <span class="font-weight-bold font-size-15"> {{formatNumber.FormatNumberShow(this.tongTien)}} VND</span>
                          </div>
                        </div>
                        <div class="col-md-6 md-0" style="margin-bottom: -20px">
                          <b-card style="background: #d8e7ff">
                            <div class="row mt-0">
                              <div class="col-12">
                                <div class="mb-2">
                                  <label class="text-left mb-0">Chọn khách hàng</label>
                                  <multiselect
                                      v-model="valueKhachHang"
                                      label="tenKhachHang"
                                      selectLabel="Nhấn vào để chọn"
                                      deselectLabel="Nhấn vào để xóa"
                                      :options="listKhachHang.map((type) => type.id)"
                                      :custom-label="
                              (opt) => listKhachHang.find((x) => x.id == opt).tenKhachHangShow
                              "
                                      placeholder="Chọn khách hàng"
                                  >
                                  </multiselect>
                                </div>
                              </div>
                              <div class="col-6">
                                <div class="mb-2">
                                  <label class="text-left mb-0">Hình thức thanh toán</label>
                                  <multiselect v-model="model.hinhThucThanhToan"
                                               :options="listHinhThuc"
                                               label="name"
                                               selectLabel="Nhấn vào để chọn"
                                               deselectLabel="Nhấn vào để xóa"
                                               track-by="id"
                                               placeholder="Chọn hình thức thanh toán"
                                               :class="{
                                                  'is-invalid':
                                                (submitted  && $v.model.hinhThucThanhToan.$error) ,
                                                }"
                                  >
                                  </multiselect>
                                  <div
                                      v-if="submitted && !$v.model.hinhThucThanhToan.required"
                                      class="invalid-feedback"
                                  >
                                    Vui lòng chọn hình thức thanh toán !
                                  </div>
                                </div>
                              </div>

                              <div class="col-6">
                                <div class="mb-2">
                                  <label class="text-left mb-0">Người mua hàng</label>
                                  <!-- <input type="hidden" v-model="model.id" /> -->
                                  <input
                                      id="percent"
                                      v-model="model.tenKhachHang"
                                      type="text"
                                      class="form-control"
                                      :class="{
                                        'is-invalid':
                                      (submitted && $v.model.tenKhachHang.$error),
                                      }"

                                  />
                                  <div
                                      v-if=" (submitted && !$v.model.tenKhachHang.required)"
                                      class="invalid-feedback"
                                  >
                                    Người mua hàng không được trống !
                                  </div>
                                </div>
                              </div>
                              <div class="col-6">
                                <div class="mb-2">
                                  <label class="text-left mb-0">Tên đơn vị</label>
                                  <input
                                      id="percent"
                                      v-model="model.tenDonVi"
                                      type="text"
                                      class="form-control"
                                  />
                                </div>
                              </div>
                              <div class="col-6">
                                <div class="mb-2">
                                  <label class="text-left mb-0">Mã số thuế</label>
                                  <!-- <input type="hidden" v-model="model.id" /> -->
                                  <input

                                      id="percent"
                                      v-model="model.maSoThue"
                                      type="text"
                                      @input="filterNonNumeric"
                                      class="form-control"

                                  />
                                </div>
                              </div>
                              <div class="col-6">
                                <div class="mb-2">
                                  <label class="text-left mb-0">Tài khoản ngân hàng</label>
                                  <input
                                      id="percent"
                                      v-model="model.taiKhoanNganHang"
                                      type="text"
                                      class="form-control"

                                  />
                                </div>
                              </div>
                              <div class="col-6">
                                <div class="mb-2 ">
                                  <label class="text-left mb-0">Địa chỉ</label>
                                  <input

                                      id="percent"
                                      v-model="model.diaChi"
                                      type="text"
                                      class="form-control"
                                      :class="{
                                        'is-invalid':
                                      submitted && $v.model.diaChi.$error,
                                      }"
                                  />
                                  <div
                                      v-if="submitted && !$v.model.diaChi.required"
                                      class="invalid-feedback"
                                  >
                                    Địa chỉ không được trống !
                                  </div>

                                </div>
                              </div>
                              <div class="col-6">
                                <div class="mb-2">
                                  <label class="text-left mb-0">Loại dịch vụ y tế</label>
                                  <multiselect v-model="valueLoaiDichVuYTe"
                                               :options="listLoaiDichVu"
                                               label="name"
                                               selectLabel="Nhấn vào để chọn"
                                               deselectLabel="Nhấn vào để xóa"
                                               track-by="id"
                                               placeholder="Chọn loại dịch vụ y tế"
                                               :class="{
                                                 'is-invalid':
                                                 submitted && $v.valueLoaiDichVuYTe.$error,
                                         }"

                                  >

                                  </multiselect>
                                  <div
                                      v-if="submitted && !$v.valueLoaiDichVuYTe.required"
                                      class="invalid-feedback"
                                  >
                                    Loại dịch vụ y tế không được để trống.
                                  </div>
                                </div>
                              </div>
                              <div class="col-6">
                                <div class="mb-2 ">
                                  <label class="text-left mb-0">Thuốc - Dịch vụ y tế</label>
                                  <multiselect v-model="valueThuoc"
                                               :options="listThuoc"
                                               label="name"
                                               selectLabel="Nhấn vào để chọn"
                                               deselectLabel="Nhấn vào để xóa"
                                               track-by="id"
                                               placeholder="Chọn thuốc - dịch vụ y tế"
                                               :class="{
                                                 'is-invalid':
                                                 submitted && $v.valueThuoc.$error,
                                         }"

                                  >
                                  </multiselect>
                                  <div
                                      v-if="submitted && !$v.valueThuoc.required"
                                      class="invalid-feedback"
                                  >
                                    Thuốc - Dịch vụ y tế không được để trống.
                                  </div>

                                </div>
                              </div>
                              <div class="col-6">
                                <div class="mb-2 ">
                                  <label class="text-left mb-0">Đơn vị tính</label>
                                  <multiselect v-model="valueDonViTinh"
                                               :options="listDonViTinh"
                                               label="name"
                                               selectLabel="Nhấn vào để chọn"
                                               deselectLabel="Nhấn vào để xóa"
                                               track-by="id"
                                               placeholder="Chọn đơn vị tính"
                                               :class="{
                                                 'is-invalid':
                                                 submitted && $v.valueDonViTinh.$error,
                                         }"
                                  >
                                  </multiselect>
                                  <div
                                      v-if="submitted && !$v.valueDonViTinh.required"
                                      class="invalid-feedback"
                                  >
                                    Đơn vị tính không được để trống.
                                  </div>
                                </div>
                              </div>
                              <div class="col-6">
                                <div class="mb-2 ">
                                  <label class="text-left mb-0">Tên hàng hoá</label>
                                  <input
                                      id="percent"
                                      v-model="modelThongTin.tenHangHoa"
                                      type="text"
                                      class="form-control"
                                      :class="{
                                          'is-invalid':
                                        submitted && $v.modelThongTin.tenHangHoa.$error,
                                        }"
                                  />
                                  <div
                                      v-if="submitted && !$v.modelThongTin.tenHangHoa.required"
                                      class="invalid-feedback"
                                  >
                                    Tên hàng hóa không được trống !
                                  </div>
                                </div>
                              </div>
                              <div class="col-6">
                                <div class="mb-2 ">
                                  <label class="text-left mb-0">Đơn giá</label>
                                  <input
                                      v-model.lazy="modelThongTin.donGia"
                                      v-money="money"
                                      class="form-control"
                                  />
                                </div>
                              </div>
                              <div class="col-6">
                                <div class="mb-2 ">
                                  <label class="text-left mb-0">Số lượng</label>
                                  <input
                                      id="percent"
                                      v-model="modelThongTin.soLuong"
                                      type="number"
                                      class="form-control"
                                  />
                                </div>
                              </div>
                            </div>
                          </b-card>
                        </div>
                      </div>
                    </form>
                  </b-modal>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-12">
                <div class="row">
                  <div class="col-sm-12 col-md-6">
                    <div class="col-sm-12 d-flex justify-content-left align-items-center">
                      <div class="m-1" style="display: flex; justify-content: left; align-items: center;">
                        <div class="me-1">Hiển thị</div>
                        <b-form-select
                            class="form-select form-select-sm"
                            v-model="perPage"
                            size="sm"
                            :options="pageOptions"
                            style="width: 70px"
                        ></b-form-select
                        >&nbsp;
                        <div style="width: 100px"> dòng</div>
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
                      {{ data.index + ((currentPage - 1) * perPage) + 1 }}
                    </template>
                    <template v-slot:cell(chuyenDoi)="data">
                      <div>
                        <input class="form-check-input" type="checkbox" v-model="data.item.chuyenDoi" disabled="true">
                      </div>
                    </template>
                    <template v-slot:cell(statusName)="data">
                      <div >
                         <span
                             class="badge rounded-pill font-size-12 fw-bolder"
                             :class="{
                             'badge-soft-dark': data.item.statusCode === 'DAXOA',
                            'badge-soft-primary': data.item.statusCode === 'NHAP',
                            'badge-soft-success': data.item.statusCode === 'DAPHATHANH',
                             'badge-soft-danger': data.item.statusCode === 'THAYTHE',
                          }"
                         >{{ data.item.statusName }}</span>
                      </div>
                    </template>
                    <template v-slot:cell(process)="data">
                        <button
                            type="button"
                                 size="sm"
                                 class="btn btn-outline btn-sm"
                                v-on:click="handleInvoiceFromData(data.item.id)">
                          <i class="fas fa-cloud-download-alt fa-lg text-sale me-1"></i>
                        </button>
                    </template>

                  </b-table>
                </div>
                <div class="row">
                  <b-col>
                    <div>Hiển thị {{ numberOfElement }} trên tổng số {{ totalRows }} dòng</div>
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
                <b-modal
                    v-model="showPhatHanhModal"
                    centered
                    title="Phát hành hóa đơn"
                    title-class="font-18"
                    no-close-on-backdrop
                    hide-header-close
                    size="lg"
                >
                  <p>
                    Bạn chắc chắn muốn phát hành hóa đơn này?
                  </p>
                  <template #modal-footer>
                    <button v-b-modal.modal-close_visit
                            class="btn btn-outline-info m-1"
                            v-show="isSend"
                            v-on:click="handleClosePhatHanh">
                      Đóng
                    </button>
                    <button v-b-modal.modal-close_visit
                            class="btn btn-warning btn m-1"
                            v-show="isSend"
                            v-on:click="handleSendInvoiceFromDataAPI">
                      Phát hành
                    </button>
                  </template>
                </b-modal>
                <b-modal
                    v-model="showDeleteModal"
                    centered
                    title="Hủy hóa đơn"
                    title-class="font-18"
                    hide-header-close
                    no-close-on-backdrop
                >
                  <div class="col-12">
                    <div class="mb-2 ">
                      <label class="text-left mb-0">Tên văn bản thỏa thuận hủy hóa đơn</label>
                      <input
                          type="text"
                          v-model="huyModel.vanban"
                          class="form-control"
                          :class="{
                                        'is-invalid':
                                      submittedHuy && $v.huyModel.vanban.$error,
                                      }"
                      />
                      <div
                          v-if="submittedHuy && !$v.huyModel.vanban.required"
                          class="invalid-feedback"
                      >
                        Văn bản thỏa thuận hủy hóa đơn không được trống !
                      </div>
                    </div>
                  </div>
                  <div class="col-12">
                    <div class="mb-2 ">
                      <label class="text-left mb-0">Lý do hủy hóa đơn</label>
                      <input
                          id="percent"
                          type="text"
                          v-model="huyModel.lydo"
                          class="form-control"
                          :class="{
                                        'is-invalid':
                                      submittedHuy && $v.huyModel.lydo.$error,
                                      }"
                      />
                      <div
                          v-if="submittedHuy && !$v.huyModel.lydo.required"
                          class="invalid-feedback"
                      >
                        Lý do hủy hóa đơn không được trống !
                      </div>
                    </div>
                  </div>

                  <template #modal-footer>
                    <button v-b-modal.modal-close_visit
                            class="btn btn-outline-info m-1"
                            v-show="isDelete"
                            v-on:click="showDeleteModal = false">
                      Đóng
                    </button>
                    <button
                        v-show="isDelete"
                        v-b-modal.modal-close_visit
                            class="btn btn-danger btn m-1"
                            v-on:click="handleDeleteAPI">
                      Xóa
                    </button>
                  </template>
                </b-modal>
                <b-modal
                    v-model="showDeleteModalNhap"
                    centered
                    title="Xóa hóa đơn nháp"
                    title-class="font-18"
                    no-close-on-backdrop
                >
                  <p>
                  Dữ liệu xóa sẽ không được phục hồi!
                </p>
                  <template #modal-footer>
                    <button v-b-modal.modal-close_visit
                            v-show="isDelete"
                            class="btn btn-outline-info m-1"
                            v-on:click="showDeleteModal = false">
                      Đóng
                    </button>
                    <button
                        v-show="isDelete"
                        v-b-modal.modal-close_visit
                        class="btn btn-danger btn m-1"
                        v-on:click="handleDeleteNhapAPI">
                      Xóa
                    </button>
                  </template>
                </b-modal>
                <b-modal
                    v-model="showDeleteTableModal"
                    centered
                    title="Xóa dữ liệu hóa đơn"
                    title-class="font-18"
                    no-close-on-backdrop
                >
                  <p>
                    Dữ liệu hóa đơn xóa sẽ không được phục hồi!
                  </p>
                  <template #modal-footer>
                    <button v-b-modal.modal-close_visit
                            class="btn btn-outline-info m-1"
                            v-on:click="handleClose">
                      Đóng
                    </button>
                    <button v-b-modal.modal-close_visit
                            class="btn btn-danger btn m-1"
                            v-on:click="handleDeleteModal">
                      Xóa
                    </button>
                  </template>
                </b-modal>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </Layout>
</template>
<style>

.modal-dialog {
  max-width: 97%;
  height: 100vh;
  display: flex;
}

.table-custom-srollbar-dt
{
  height: 460px;
  background: #ffffff;
  width: 100%;
  overflow: auto;
}

</style>
