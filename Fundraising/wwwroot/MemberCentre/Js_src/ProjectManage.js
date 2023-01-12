$(document).ready(function () {
  $("#table_id").DataTable({ processing: true });
});

const app = {
  data() {
    return {};
  },
};
Vue.createApp(app).mount("#table_id");
