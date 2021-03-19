// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$("#date").datetimepicker();

$("#genUrl").click(() => {
  var value = $("#url").val();
  $("#placeholder").attr("src", "/api/qr/gen-url?url=" + value);
});