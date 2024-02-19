import store from "@/state/store";
export default [
    {
        path: "/dang-nhap",
        name: "login",
        component: () => import("../pages/login/login"),
        meta: {
            beforeResolve(routeTo, routeFrom, next) {
                if (store.getters["authStore/loggedIn"]) {
                    next({name: "default"});
                } else {
                    next();
                }
            }
        },
    },

    {
        path: "/tra-cuu",
        name: "default",
        meta: {
            authRequired: true,
        },
        component: () => import("../pages/tracuu"),
    },

    {
        path: "/thong-tin-nguoi-ban",
        name: "default",
        meta: {
            authRequired: true,
        },
        component: () => import("../pages/thongtinnguoiban"),
    },

    {
        path: "/menu",
        name: "TrangThai",
        meta: {},
        component: () => import("../pages/menu"),
    },
    {
        path: "/thuoc",
        name: "Thuốc",
        meta: {},
        component: () => import("../pages/thuoc"),
    },
    {
        path: "/thue-gtgt",
        name: "Thuế GTGT",
        meta: {},
        component: () => import("../pages/thuegtgt"),
    },

    {
        path: "/hinh-thuc-thanh-toan",
        name: "Hình thức thanh toán",
        meta: {},
        component: () => import("../pages/hinhthucthanhtoan"),
    },
    {
        path: "/don-vi-tinh",
        name: "Đơn vị tính",
        meta: {},
        component: () => import("../pages/donvitinh"),
    },
    {
        path: "/trang-thai",
        name: "Trạng thái",
        meta: {},
        component: () => import("../pages/trangThai"),
    },
    {
        path: "/loai-dich-vu-y-te",
        name: "Loại dịch vụ y tế",
        meta: {},
        component: () => import("../pages/loaidichvuyte"),
    },
    {
        path: "/nhom-quyen",
        name: "Quản lý nhóm quyền",
        meta: {can : 'viewpage-modules'},
        component: () => import("../pages/module"),
    },
    {
        path: "/nhom-quyen/action/:id?",
        name: "Hành động",
        // meta: {},
        component: () => import("../pages/module/action"),
    },
    {
        path: "/vai-tro",
        name: "Quyền",
        meta: {can: 'viewpage-roles'},
        component: () => import("../pages/role"),
    },
    {
        path: "/vai-tro/thiet-lap-quyen/:id?",
        name: "Vai trò",
        meta: {can: 'viewpage-roles' },
        component: () => import("../pages/role/addPermissions"),
    },
    {
        path: "/add-permissions",
        name: "Test ",
        component: () => import("../pages/role/addPermissions"),
    },
        {
        path: "/tai-khoan",
        name: "Tài khoản",
        meta: {
            can: 'viewpage-user',
        },
        component: () => import("../pages/user"),
    },
    {
        path: "/tai-khoan/doi-mat-khau",
        name: "Đổi mật khẩu",
        meta: {can: 'viewpage-user', },
        component: () => import("../pages/user/ChangePass"),
    },

    {
        path: "/thong-tin-ca-nhan",
        name: "Thông tin cá nhân",
        // meta: {},
        component: () => import("../pages/login/profile"),
    },
    {
        path: "/khach-hang",
        name: "Thông tin khách hàng",
        // meta: {},
        component: () => import("../pages/khachhang/index.vue"),
    },
    {
        path: "/thong-tin-phat-hanh",
        name: "Thông tin phát hành",
        meta: {},
        component: () => import("../pages/thongtinphathanh/index.vue"),
    },




  {
        path: "/404",
        name: "404",
        component: require("../pages/utility/404").default,
    },
    {
        path: "/unauthorized",
        name: "401",
        component: require("../pages/utility/401").default,
    },
    {
        path: "*",
        redirect: "404",
    },

]
