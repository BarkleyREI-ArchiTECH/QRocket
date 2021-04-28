$("#start-date").datetimepicker();
$("#end-date").datetimepicker();

var colorDark;
var colorLight;

$("#color-dark")
  .colorpicker({
    inline: true,
    container: true,
    format: "hex",
    customClass: "colorpicker-2x",
    sliders: {
      saturation: {
        maxLeft: 200,
        maxTop: 200,
      },
      hue: {
        maxTop: 200,
      },
      alpha: {
        maxTop: 200,
      },
    },
  })
  .on("colorpickerChange colorpickerCreate", function (e) {
    colorDark = e.color.string();
  });

$("#color-light")
  .colorpicker({
    inline: true,
    container: true,
    customClass: "colorpicker-2x",
    format: "hex",
    sliders: {
      saturation: {
        maxLeft: 200,
        maxTop: 200,
      },
      hue: {
        maxTop: 200,
      },
      alpha: {
        maxTop: 200,
      },
    },
  })
  .on("colorpickerChange colorpickerCreate", function (e) {
    colorLight = e.color.string();
  });

$("#genIcs").click(() => {
  var value = {
    url: $("#url").val(),
    title: $("#title").val(),
    location: $("#loc").val(),
    startDate: $("#start-date").val(),
    endDate: $("#end-date").val(),
    notes: $("#note").val(),
    darkColor: colorDark,
    lightColor: colorLight,
  };
  var json = JSON.stringify(value);
  var encode = btoa(json);
  $("#placeholder").attr("src", "/api/qr/gen-ics?val=" + encode);
});

$("#genUrl").click(() => {
  const base64Canvas = $("#imgCanvas")[0]
    .toDataURL("image/jpeg")
    .split(";base64,")[1];

  var value = {
    url: $("#url").val(),
    darkColor: colorDark,
    lightColor: colorLight,
  };
  var json = JSON.stringify(value);
  var encode = btoa(json);
  $("#placeholder").attr("src", "/api/qr/gen-url?val=" + encode);
});

function previewFile() {
  var maxWidth = 250;
  var input = $("#imgIcon")[0];
  var canvas = $("#imgCanvas")[0];

  var file = input.files[0];
  var context = canvas.getContext("2d");

  if (file) {
    var reader = new FileReader();
    reader.onload = function (e) {
      var img = new Image();
      img.onload = function () {
        canvas.width = maxWidth;
        canvas.height = (img.height * maxWidth) / img.width;
        context.drawImage(img, 0, 0, canvas.width, canvas.height);
      };
      img.src = e.target.result;
    };
    reader.readAsDataURL(file);
  }
}
