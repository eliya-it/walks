namespace NZWalks.API.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class NZWalksAuthDbContext : IdentityDbContext
{
    public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        string readerRoleId = "1cc5f2bc-ff4b-47c0-a475-1add56c6497d";
        string writerRoleId = "1cc5f2bc-ff4b-47c0-a423-1add56c6497e";
        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = readerRoleId,
                ConcurrencyStamp = readerRoleId,
                Name = "Reader",
                NormalizedName = "READER"
            },
            new IdentityRole
            {
                Id = writerRoleId,
                ConcurrencyStamp = writerRoleId,
                Name = "Writer",
                NormalizedName = "WRITER"

            },
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }
}
