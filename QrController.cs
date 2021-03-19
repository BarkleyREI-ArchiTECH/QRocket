using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using QRCoder;

namespace QRBuddy
{
    [Route("/api/[controller]")]
    [ApiController]
    public class QrController : ControllerBase
    {
        private readonly IHostEnvironment _env;

        public QrController(IHostEnvironment env) => _env = env;

        [HttpGet("url")]
        public ActionResult Url([FromQuery] string url)
        {
            var root = _env.ContentRootPath;

            var dark  = ColorTranslator.FromHtml("#131313");
            var light = ColorTranslator.FromHtml("#e5dfdc");

            var icon = (Bitmap) Image.FromFile(Path.Combine(root, "Barkley_Logo_64.png"));

            var       generator = new QRCodeGenerator();
            var       data      = generator.CreateQrCode(new PayloadGenerator.Url(url), QRCodeGenerator.ECCLevel.Q);
            var       qr        = new QRCode(data);
            using var graphic   = qr.GetGraphic(20, dark, light, icon);

            using var stream = new MemoryStream();
            graphic.Save(stream, ImageFormat.Png);
            var result = stream.ToArray();
            return File(result, "image/png");
        }
    }
}