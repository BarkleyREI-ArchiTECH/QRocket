using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QRCoder;

namespace QRocket
{
    [Route("/api/[controller]")]
    [ApiController]
    public class QrController : ControllerBase
    {
        private readonly dynamic _barkleyBrand = new
        {
            DarkColor  = "#131313",
            LightColor = "#E6E7E8",
            RedColor   = "#EB052C",
            Icon       = "Barkley_Logo_64.png"
        };

        private readonly IWebHostEnvironment _env;

        private readonly dynamic _tacoJohnBrand = new
        {
            DarkColor  = "#76232F",
            LightColor = "#E5DFDC",
            Icon       = "TacoJohns_small.png"
        };

        public QrController(IWebHostEnvironment env) => _env = env;

        [HttpGet("gen-url")]
        public ActionResult GenUrl([FromQuery] string val)
        {
            Bitmap icon = null;
            var    www  = _env.WebRootPath;

            var model = Decode(val);

            if (string.IsNullOrEmpty(model.DarkColor)) model.DarkColor   = _barkleyBrand.DarkColor;
            if (string.IsNullOrEmpty(model.LightColor)) model.LightColor = _barkleyBrand.LightColor;
            model.Icon = null; //_tacoJohnBrand.Icon;

            var dark  = ColorTranslator.FromHtml(model.DarkColor);
            var light = ColorTranslator.FromHtml(model.LightColor);

            if (!string.IsNullOrEmpty(model.Icon)) icon = (Bitmap) Image.FromFile(Path.Combine(www, "media", model.Icon));

            var       generator = new QRCodeGenerator();
            var       data      = generator.CreateQrCode(new PayloadGenerator.Url(model.Url), QRCodeGenerator.ECCLevel.M);
            var       qr        = new QRCode(data);
            using var graphic   = qr.GetGraphic(20, dark, light, icon);

            using var stream = new MemoryStream();
            graphic.Save(stream, ImageFormat.Png);
            var result = stream.ToArray();
            return File(result, "image/png");
        }

        [HttpGet("gen-ics")]
        public ActionResult GenIcs([FromQuery] string val)
        {
            Bitmap icon = null;
            var    www  = _env.WebRootPath;

            var model = Decode(val);

            if (string.IsNullOrEmpty(model.DarkColor)) model.DarkColor   = _barkleyBrand.DarkColor;
            if (string.IsNullOrEmpty(model.LightColor)) model.LightColor = _barkleyBrand.LightColor;
            model.Icon = null; //_tacoJohnBrand.Icon;

            var dark  = ColorTranslator.FromHtml(model.DarkColor);
            var light = ColorTranslator.FromHtml(model.LightColor);

            if (!string.IsNullOrEmpty(model.Icon)) icon = (Bitmap) Image.FromFile(Path.Combine(www, "media", model.Icon));

            var allDay = model.EndDate is null || model.EndDate.Value == model.StartDate.Value;

            var icsPayload = new PayloadGenerator.CalendarEvent(model.Title, model.Notes, model.Location, model.StartDate.Value, model.EndDate.Value, allDay, PayloadGenerator.CalendarEvent.EventEncoding.iCalComplete);

            var       generator = new QRCodeGenerator();
            var       data      = generator.CreateQrCode(icsPayload, QRCodeGenerator.ECCLevel.M);
            var       qr        = new QRCode(data);
            using var graphic   = qr.GetGraphic(20, dark, light, icon);

            using var stream = new MemoryStream();
            graphic.Save(stream, ImageFormat.Png);
            var result = stream.ToArray();
            return File(result, "image/png");
        }

        private static PayloadModel Decode(string base64Json)
        {
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(base64Json));
            return JsonConvert.DeserializeObject<PayloadModel>(json);
        }
    }
}