// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$("#start-date").datetimepicker();
$("#end-date").datetimepicker();

var colorDark;
var colorLight;

$("#color-dark").colorpicker({
  inline: true,
  container: true,
  format: "hex",
  customClass: "colorpicker-2x",
  sliders: {
    saturation: {
      maxLeft: 200,
      maxTop: 200
    },
    hue: {
      maxTop: 200
    },
    alpha: {
      maxTop: 200
    }
  }
}).on("colorpickerChange colorpickerCreate",
  function(e) {
    colorDark = e.color.string();
  });

$("#color-light").colorpicker({
  inline: true,
  container: true,
  customClass: "colorpicker-2x",
  format: "hex",
  sliders: {
    saturation: {
      maxLeft: 200,
      maxTop: 200
    },
    hue: {
      maxTop: 200
    },
    alpha: {
      maxTop: 200
    }
  }
}).on("colorpickerChange colorpickerCreate",
  function(e) {
    colorLight = e.color.string();
  });;

$("#genIcs").click(() => {
  var value = {
    url: $("#url").val(),
    title: $("#title").val(),
    location: $("#loc").val(),
    startDate: $("#start-date").val(),
    endDate: $("#end-date").val(),
    notes: $("#note").val(),
    darkColor: colorDark,
    lightColor: colorLight
  };
  var json = JSON.stringify(value);
  var encode = btoa(json);
  $("#placeholder").attr("src", "/api/qr/gen-ics?val=" + encode);
});

$("#genUrl").click(() => {
  var value = {
    url: $("#url").val(),
    darkColor: colorDark,
    lightColor: colorLight
  };
  var json = JSON.stringify(value);
  var encode = btoa(json);
  $("#placeholder").attr("src", "/api/qr/gen-url?val=" + encode);
});