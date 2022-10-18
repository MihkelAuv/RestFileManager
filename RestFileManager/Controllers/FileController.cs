using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFileManager.Models;
using RestFileManager.Services;

namespace RestFileManager.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("Upload")]
        [MapToApiVersion("1.0")]
        public ActionResult LegacyUpload([FromForm] MultipartFile filerequest)
        {
            Metadata? metadata = null;
            try
            {
                metadata = JsonSerializer.Deserialize<Metadata>(filerequest.MetaData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.InnerException?.ToString());
            }

            var file = filerequest.File;
            if (metadata is not null)
                metadata.ID = Guid.NewGuid().ToString();
            else
                metadata = new Metadata(file.FileName, file.ContentType, string.Empty);
            try
            {
                FileService.SaveFile(file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.InnerException?.ToString());
            }
            
            return Ok(JsonSerializer.Serialize(metadata));
        }

        [HttpPost("Upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [MapToApiVersion("2.0")]
        public ActionResult<Metadata> Upload([ModelBinder(BinderType = typeof(MetadataModelProvider))] Metadata metadata, [Required] IFormFile file)
        {
            var newMetadata = new Metadata(metadata.Name, file.ContentType, metadata.Description);

            try
            {
                FileService.SaveFile(file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.InnerException?.ToString());
            }

            return Ok(newMetadata);
        }
    }
}
