using Microsoft.AspNetCore.Mvc;
using Example.Api.Models;
using Newtonsoft.Json;
using System.Net;

namespace Example.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly CRepositorioEstudioExampleSolutionDbExerciseDbMdfContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public ProductsController(CRepositorioEstudioExampleSolutionDbExerciseDbMdfContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }

        // GET: Products/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromForm] string product, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product _product = JsonConvert.DeserializeObject<Product>(product);

            try
            {
                if (string.IsNullOrEmpty(_product.ProductName))
                    throw new ArgumentException("El producto deben contener un nombre");
                if (_context.Products.Any(x => x.ProductName == _product.ProductName))
                    throw new ArgumentException("Este producto ya existe");
                if (image.Length <= 0)
                    throw new ArgumentException("No se ha seleccionado ninguna imagen");
                if (image.Length / 1000000 > 2)
                    throw new ArgumentException("El peso de la imagen no debe ser mayor a 2mb");
                if (image.ContentType != "image/jpeg")
                    throw new ArgumentException("Solo se admiten imagenes en formato jpg");

                _product.Image = Guid.NewGuid() + image.FileName;

                _context.Products.Add(_product);
                await _context.SaveChangesAsync();

                var prodBD = _context.Products.FirstOrDefault(x => x.ProductName == _product.ProductName);
                var Ruta = Path.Combine(_hostingEnvironment.WebRootPath, "user_files");

                if (!Directory.Exists(Ruta))
                    Directory.CreateDirectory(Ruta);

                using (var stream = new FileStream(Path.Combine(Ruta, _product.Image), FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                return CreatedAtAction("GetProduct", new { id = prodBD.IdProduct }, prodBD);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, " Message: " + ex.Message + " Source: " + ex.Source + " InnerException: " + ex.InnerException);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        } 
    }
}
