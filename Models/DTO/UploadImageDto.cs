using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

public class UploadImageDto
{
    [Required(ErrorMessage = "Please provide a file!")]
    public IFormFile File { get; set; }

    [Required(ErrorMessage = "Please provide a file name!")]

    public string Name { get; set; }

    public string? Description { get; set; }


}