using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace nowe_Restaurant_API.Controllers
{
    [Route("file")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {

        [HttpGet]
        [ResponseCache (Duration =1000, VaryByQueryKeys = new[] {"filename"})]
        public ActionResult GetFile([FromQuery] string filename)
        {
            var rootpath = Directory.GetCurrentDirectory();
            var currenpath = $"{rootpath}/privatefiles/{filename}";
            var fileexist = System.IO.File.Exists(currenpath);
            if (!fileexist)
            {
                return NotFound();
            }
            var contentprovider = new FileExtensionContentTypeProvider();
            contentprovider.TryGetContentType(filename, out string contentType);

            var filecontent = System.IO.File.ReadAllBytes(currenpath);

            return File(filecontent, contentType, filename);



        }
        [HttpPost]
        public ActionResult UploadFile([FromForm] IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var rootpath = Directory.GetCurrentDirectory();
                var filename = file.FileName;
                var fullpath = $"{rootpath}/privatefiles/{filename}";
                using (var stream = new FileStream(fullpath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok();
            }
            return BadRequest();
        }
    }
}
