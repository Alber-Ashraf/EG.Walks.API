using EG.Walks.Contracts.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EG.Walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        public async Task<IActionResult> UploadImageAsync(ImageUploadRequestDto requestDto)
        {
            // Validate the request
            ValidateImageUploadRequest(requestDto);

            // If the model state is valid, proceed with the upload logic
            if (ModelState.IsValid)
            {

            }
            // If the model state is invalid, return a BadRequest response with the validation errors
            return BadRequest(ModelState);
        }

        public void ValidateImageUploadRequest(ImageUploadRequestDto requestDto)
        {
            var acceptedFileTypes = new[] { ".jpg", ".jpeg", ".png" };
            var maxLengthInBytes = 10 * 1024 * 1024; // 10 MB

            if(!acceptedFileTypes.Contains(Path.GetExtension(requestDto.File.FileName).ToLower()))
            {
                ModelState.AddModelError("File", "Invalid file type. Only .jpg, .jpeg, and .png files are allowed.");
            }

            if (requestDto.File.Length > maxLengthInBytes)
            {
                ModelState.AddModelError("File", "File size exceeds the maximum limit of 10 MB.");
            }
        }
    }
}
