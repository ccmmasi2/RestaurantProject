using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public ImageController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("{URL}")]
        public FileStreamResult GetImage([FromRoute] string URL)
        {
            var ruta = Path.Combine(_hostingEnvironment.WebRootPath, "user_files");
            var stream = System.IO.File.OpenRead(Path.Combine(ruta, URL));
            return File(stream, "image/jpeg");
        }
    }
}
