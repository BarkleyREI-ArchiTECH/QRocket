using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace QRBuddy
{
    [Route("/api/[controller]")]
    [ApiController]
    public class QrController : ControllerBase
    {
        [HttpGet("url")]
        public ActionResult Url([FromQuery] string url)
        {
            var       generator = new QRCodeGenerator();
            var       data      = generator.CreateQrCode(new PayloadGenerator.Url(url), QRCodeGenerator.ECCLevel.Q);
            var       qr        = new QRCode(data);
            using var graphic   = qr.GetGraphic(20);

            using var stream = new MemoryStream();
            graphic.Save(stream, ImageFormat.Png);
            var result = stream.ToArray();
            return File(result, "image/png");
        }
    }
}