using AutoMapper;
using EG.Walks.Contracts.Requests;
using EG.Walks.Contracts.Responses;
using EG.Walks.Domain.Entities;
using EG.Walks.Infrastructure.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EG.Walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ImagesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm]ImageUploadRequestDto requestDto)
        {
            // Validate the request
            ValidateImageUploadRequest(requestDto);

            // If the model state is valid, proceed with the upload logic
            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = requestDto.File,
                    FileExtention = Path.GetExtension(requestDto.File.FileName),
                    FileSizeInBytes = requestDto.File.Length,
                    FileName = requestDto.FileName,
                    FileDescription = requestDto.FileDescription
                };

                await _unitOfWork.Image.ImageUploadAsync(imageDomainModel);
                // Return a Created response with the uploaded image details
                await _unitOfWork.SaveAsync();

                // Map the uploaded image to a response DTO 
                var imageResponseDto = _mapper.Map<ImageUploadResponseDto>(imageDomainModel);

                return Ok(imageResponseDto);
            }
            // If the model state is invalid, return a BadRequest response with the validation errors
            return BadRequest(ModelState);
        }

        private void ValidateImageUploadRequest(ImageUploadRequestDto requestDto)
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
