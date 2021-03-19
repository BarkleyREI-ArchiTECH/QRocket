using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using QRCoder;

namespace QRBuddy
{
    [Route("/api/[controller]")]
    [ApiController]
    public class QrController : ControllerBase
    {
        private readonly IHostEnvironment _env;

        public QrController(IHostEnvironment env) => _env = env;

        [HttpGet("gen-url")]
        public ActionResult GenUrl([FromQuery] string val)
        {
            var root = _env.ContentRootPath;

            var dark  = ColorTranslator.FromHtml("#131313");
            var light = ColorTranslator.FromHtml("#e5dfdc");

            var icon = (Bitmap) Image.FromFile(Path.Combine(root, "Barkley_Logo_64.png"));

            var model = Decode(val);

            var       generator = new QRCodeGenerator();
            var       data      = generator.CreateQrCode(new PayloadGenerator.Url(model.Url), QRCodeGenerator.ECCLevel.Q);
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
            var root = _env.ContentRootPath;

            var dark  = ColorTranslator.FromHtml("#131313");
            var light = ColorTranslator.FromHtml("#e5dfdc");

            var icon = (Bitmap) Image.FromFile(Path.Combine(root, "Barkley_Logo_64.png"));

            var model = Decode(val);

            var icsPayload = new PayloadGenerator.CalendarEvent(model.Title, model.Notes, model.Location, model.StartDate.Value, model.EndDate.Value, false);

            var       generator = new QRCodeGenerator();
            var       data      = generator.CreateQrCode(icsPayload, QRCodeGenerator.ECCLevel.Q);
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

    public class PayloadModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Notes { get; set; }
        public string DarkColor { get; set; }
        public string LightColor { get; set; }
        public string Icon { get; set; }
    }
}