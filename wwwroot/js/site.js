// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$("#start-date").datetimepicker();
$("#end-date").datetimepicker();

$("#genIcs").click(() => {
  var value = {
    url: $("#url").val(),
    title: $("#title").val(),
    location: $("#loc").val(),
    startDate: $("#start-date").val(),
    endDate: $("#end-date").val(),
    notes: $("#note").val()
  };
  var json = JSON.stringify(value);
  var encode = btoa(json);
  console.log(value, json, encode);
  $("#placeholder").attr("src", "/api/qr/gen-ics?val=" + encode);
});

$("#genUrl").click(() => {
  var value = {
    url: $("#url").val()
  };
  var json = JSON.stringify(value);
  var encode = btoa(json);
  console.log(value, json, encode);
  $("#placeholder").attr("src", "/api/qr/gen-url?val=" + encode);
});