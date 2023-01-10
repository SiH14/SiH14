let convertimage;
let loadFile = function (event) {
  let output = document.getElementById("output");
  output.src = URL.createObjectURL(event.target.files[0]);
  let reader = new FileReader();
  output.onload = function () {
    convertimage = reader.result;
    URL.revokeObjectURL(output.src); // free memory
  };
  reader.readAsDataURL(event.target.files[0]);
};
