<script>
import MetisMenu from "metismenujs/dist/metismenujs";

import Vue from "vue";
import {mapState} from "vuex";

/**
 * Sidenav component
 */
export default {
  data() {
    return {
      menuData: null,
      checkMounted: true,
      menu : []
    };
  },
  created() {
   // console.log("LOG ON CREATED : SIDE - NAV ")
    var currentUser = localStorage.getItem("auth-user");
    if (currentUser) {
      let data = JSON.parse(currentUser)
      if (data && data.user.menu)
        this.menuItems = data.user.menu;
    }
  },
  computed: {
    ...mapState('snackBarStore', ['isMenuAction'])
  },
  watch: {
    isMenuAction: {
      deep: true,
      handler(val) {
        var currentMenu = localStorage.getItem("currentMenuId")

        var menuRef = new MetisMenu("#side-menu");
        var links = document.getElementsByClassName("side-nav-link-ref active");
        var chil = links[0]
        if (chil) {
          chil.classList.remove("active")
          var parent = chil.parentElement;
          if (parent)
            parent.classList.remove("mm-active");
        }
        this.handleMenuActive()
      }
    },
  },
  mounted: function () {
    var currentMenu = localStorage.getItem("currentMenuId")

    var menuRef = new MetisMenu("#side-menu");
    var links = document.getElementsByClassName("side-nav-link-ref");

    var matchingMenuItem = null;
    const paths = [];
    for (var i = 0; i < links.length; i++) {
      paths.push({link: links[i].getAttribute("pathname"), menuId: links[i].getAttribute("menuId")});
    }
    var itemIndex = paths.findIndex(x => x.link = window.location.pathname && x.menuId == currentMenu);
    if (itemIndex === -1) {
      const strIndex = window.location.pathname.lastIndexOf("/");
      const item = window.location.pathname.substr(0, strIndex).toString();
      matchingMenuItem = links[paths.findIndex(x => x.link == item)];
    } else {
      matchingMenuItem = links[itemIndex];
    }
    if (matchingMenuItem) {
      matchingMenuItem.classList.add("active");
      var parent = matchingMenuItem.parentElement;

      /**
       * TODO: This is hard coded way of expading/activating parent menu dropdown and working till level 3.
       * We should come up with non hard coded approach
       */
      if (parent) {
        parent.classList.add("mm-active");
        const parent2 = parent.parentElement.closest("ul");
        if (parent2 && parent2.id !== "side-menu") {
          parent2.classList.add("mm-show");

          const parent3 = parent2.parentElement;
          if (parent3) {
            parent3.classList.add("mm-active");
            var childAnchor = parent3.querySelector(".has-arrow");
            var childDropdown = parent3.querySelector(".has-dropdown");
            if (childAnchor) childAnchor.classList.add("mm-active");
            if (childDropdown) childDropdown.classList.add("mm-active");

            const parent4 = parent3.parentElement;
            if (parent4 && parent4.id !== "side-menu") {
              parent4.classList.add("mm-show");
              const parent5 = parent4.parentElement;
              if (parent5 && parent5.id !== "side-menu") {
                parent5.classList.add("mm-active");
                const childanchor = parent5.querySelector(".is-parent");
                if (childanchor && parent5.id !== "side-menu") {
                  childanchor.classList.add("mm-active");
                }
              }
            }
          }
        }
      }
    }
  },
  methods: {
    toggleCollapse(item) {
      item.collapsed = !item.collapsed;
    },
    handleMenuActive() {
      var currentMenu = localStorage.getItem("currentMenuId")

      var menuRef = new MetisMenu("#side-menu");
      var links = document.getElementsByClassName("side-nav-link-ref");

      var matchingMenuItem = null;
      const paths = [];
      for (var i = 0; i < links.length; i++) {
        paths.push({link: links[i].getAttribute("pathname"), menuId: links[i].getAttribute("menuId")});
      }
      var itemIndex = paths.findIndex(x => x.link = window.location.pathname && x.menuId == currentMenu);
      if (itemIndex === -1) {
        const strIndex = window.location.pathname.lastIndexOf("/");
        const item = window.location.pathname.substr(0, strIndex).toString();
        matchingMenuItem = links[paths.findIndex(x => x.link == item)];
      } else {
        matchingMenuItem = links[itemIndex];
      }

      if (matchingMenuItem) {
        matchingMenuItem.classList.add("active");
        var parent = matchingMenuItem.parentElement;

        /**
         * TODO: This is hard coded way of expading/activating parent menu dropdown and working till level 3.
         * We should come up with non hard coded approach
         */
        if (parent) {
          parent.classList.add("mm-active");
          const parent2 = parent.parentElement.closest("ul");
          if (parent2 && parent2.id !== "side-menu") {
            parent2.classList.add("mm-show");

            const parent3 = parent2.parentElement;
            if (parent3) {
              parent3.classList.add("mm-active");
              var childAnchor = parent3.querySelector(".has-arrow");
              var childDropdown = parent3.querySelector(".has-dropdown");
              if (childAnchor) childAnchor.classList.add("mm-active");
              if (childDropdown) childDropdown.classList.add("mm-active");

              const parent4 = parent3.parentElement;
              if (parent4 && parent4.id !== "side-menu") {
                parent4.classList.add("mm-show");
                const parent5 = parent4.parentElement;
                if (parent5 && parent5.id !== "side-menu") {
                  parent5.classList.add("mm-active");
                  const childanchor = parent5.querySelector(".is-parent");
                  if (childanchor && parent5.id !== "side-menu") {
                    childanchor.classList.add("mm-active");
                  }
                }
              }
            }
          }
        }
      }
    },
    /**
     * Returns true or false if given menu item has child or not
     * @param item menuItem
     */
    hasItems(item) {
      return item.subItems !== undefined ? item.subItems?.length > 0 : false;
    },
    toggleMenu(event) {
      event.currentTarget.nextElementSibling.classList.toggle("mm-show");
    },
    handleGetIdMenu(path, id) {
      Vue.prototype.menuId = id;
      localStorage.setItem("currentMenuId", id);
      if (path != window.location.pathname)
        this.$router.push(path);
      this.$store.dispatch("snackBarStore/clickMenu", !this.$store.state.snackBarStore.isMenuAction)
    },
    getCount(id) {
      var resultData = this.menu.filter(x => {
        return x.id == id;
      })
    //  console.log("LOG ID GET COUNT : ", id , resultData)
      if (resultData.length > 0)
      {
        return resultData[0].soLuong;
      }
      return 0 ;
    },
  },
};
</script>

<template>
  <!-- ========== Left Sidebar Start ========== -->

  <!--- Sidemenu -->
<!--  <div id="sidebar-menu">-->
<!--    &lt;!&ndash; Left Menu Start &ndash;&gt;-->
<!--    <ul id="side-menu" class="metismenu list-unstyled">-->
<!--      <template v-for="item in menuItems">-->
<!--        <li class="menu-title" v-if="item.isTitle" :key="item.id" style="color: #5d5858">-->
<!--          {{ item.label}}-->
<!--        </li>-->
<!--        <template v-if="hasItems(item)">-->
<!--          <li v-for="(value) in item.subItems" :key="value.id">-->
<!--            <a :menuId="value.id" :pathname="value.link" style="cursor: pointer"-->
<!--               @click="handleGetIdMenu(value.link, value.id)" class="side-nav-link-ref">-->
<!--              <i :class="`bx ${value.icon}`" v-if="value.icon"></i>-->
<!--              <span>{{ value.label}}</span>-->
<!--              <span v-if="getCount(value.id) > 0" style="font-weight:bold "> ( {{getCount(value.id)}} )</span>-->
<!--&lt;!&ndash;              <span v-else >NHỎ HƠN 0</span>&ndash;&gt;-->
<!--              <span :class="`badge rounded-pill bg-${value.badge.variant} float-end`"-->
<!--                    v-if="value.badge">{{ value.badge.text }}</span>-->
<!--            </a>-->
<!--          </li>-->
<!--        </template>-->
<!--      </template>-->
<!--    </ul>-->
<!--  </div>-->
  <div id="sidebar-menu">
    <!-- Left Menu Start -->
    <ul id="side-menu" class="metismenu list-unstyled">
      <template v-for="item in menuItems">
<!--        <li class="menu-title" v-if="item.isTitle" :key="item.id" style="color: #5d5858">-->
<!--          {{ item.label }}-->
<!--        </li>-->
        <template v-if="hasItems(item)">
          <li :key="item.id">
            <a :menuId="item.id" style="cursor: pointer; text-transform: uppercase" @click="toggleCollapse(item)">
              <i :class="`bx ${item.icon}`" v-if="item.icon"></i>
              <span>{{ item.label }}</span>
              <i class="bx" :class="{'bx-chevron-down': !item.collapsed, 'bx-chevron-up': item.collapsed}"
                 style="float: right;"></i>
            </a>
            <ul v-show="!item.collapsed" class="list-unstyled">
              <li v-for="(value) in item.subItems" :key="value.id">
                <a :menuId="value.id" :pathname="value.link" style="cursor: pointer"
                   @click="handleGetIdMenu(value.link, value.id)" class="side-nav-link-ref">
                  <i :class="`bx ${value.icon}`" v-if="value.icon"></i>
                  <span>{{ value.label }}</span>
                  <span v-if="getCount(value.id) > 0" style="font-weight: bold;"> ({{ getCount(value.id) }})</span>
                  <!--              <span v-else >NHỎ HƠN 0</span>-->
                  <span :class="`badge rounded-pill bg-${value.badge.variant} float-end`"
                        v-if="value.badge">{{ value.badge.text }}</span>
                </a>
              </li>
            </ul>
          </li>
        </template>
      </template>
    </ul>
  </div>
  <!-- Sidebar -->
</template>
<style type="text/css" scoped>
.mm-active > a {
  color: #082957 !important;
}
#sidebar-menu ul li a:hover {
  color: #082957;
}
.mm-active > a i {
  color: #082957 !important;
}
#sidebar-menu > ul > li > a:hover i {
  color: #082957;
}
#sidebar-menu ul li a:hover i {
  color: #082957;
}
</style>

