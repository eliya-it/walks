
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class ImageRepository(IWebHostEnvironment webHost, IHttpContextAccessor httpContext, NZWalksDbContext dbContext) : IImageRepository
{
    private readonly IWebHostEnvironment webHost = webHost;
    private readonly IHttpContextAccessor httpContext = httpContext;
    private readonly NZWalksDbContext dbContext = dbContext;

    public async Task<Image> Upload(Image image)
    {
        var uploadsFolder = Path.Combine(webHost.ContentRootPath, "Uploads");
        Directory.CreateDirectory(uploadsFolder);
        var fileName = image.Name + image.Extension;
        var path = Path.Combine(uploadsFolder, fileName);
        using var stream = new FileStream(path, FileMode.Create);
        await image.File.CopyToAsync(stream);
        if (httpContext.HttpContext == null)
        {
            throw new InvalidOperationException("HttpContext is null.");
        }
        var url = $"{httpContext.HttpContext.Request.Scheme}://{httpContext.HttpContext.Request.Host}{httpContext.HttpContext.Request.PathBase}/uploads/{image.Name}{image.Extension}";
        image.Path = url;

        await dbContext.Images.AddAsync(image);
        await dbContext.SaveChangesAsync();
        return image;
    }
}