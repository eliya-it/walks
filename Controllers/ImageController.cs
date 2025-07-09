using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

[Route("/api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IImageRepository imageRepository;

    public ImageController(IImageRepository imageRepository)
    {
        this.imageRepository = imageRepository;
    }

    [HttpPost("upload")]

    public async Task<IActionResult> Upload([FromForm] UploadImageDto uploadImage)
    {
        ValidateFileupload(uploadImage);
        if (ModelState.IsValid)
        {
            var imageDomainModel = new Image
            {
                File = uploadImage.File,
                Name = Guid.NewGuid().ToString(),
                Extension = Path.GetExtension(uploadImage.File.FileName),
                Size = uploadImage.File.Length,
                Description = uploadImage.Description,

            };
            await imageRepository.Upload(imageDomainModel);
            return Ok(imageDomainModel);
        }

        return BadRequest(ModelState);

    }
    private void ValidateFileupload(UploadImageDto request)
    {
        string[] allowedFiles = [".jpg", ".jpeg", ".png"];
        if (allowedFiles.Contains(Path.GetExtension(request.File.FileName)) == false)
            ModelState.AddModelError("file", "This file is not allowed");

        if (request.File.Length > 10485760)
            ModelState.AddModelError("file", "File size is more than 10MB, Please upload a smaller files");

    }
}