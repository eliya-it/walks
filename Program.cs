using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Serilog;
using NZWalks.API.Middlewares;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File("Logs/walk_log.txt", rollingInterval: RollingInterval.Minute).MinimumLevel.Warning().CreateLogger();
builder.Logging.ClearProviders();

builder.Logging.AddSerilog(logger);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NZWalksDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddDbContext<NZWalksAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnection")));
builder.Services.AddIdentityCore<IdentityUser>()
.AddRoles<IdentityRole>()
.AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
.AddEntityFrameworkStores<NZWalksAuthDbContext>()
.AddDefaultTokenProviders();
// builder.Services.Configure<IdentityOptions>(options =>
// {
//     options.User.RequireUniqueEmail = true;
//     options.Password.RequireDigit = false;
//     options.Password.RequiredLength = 6;
//     options.Password.RequireLowercase = false;
//     options.Password.RequireNonAlphanumeric = false;
//     options.Password.RequireUppercase = false;
// });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opts => opts.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/uploads"
});
app.MapControllers();

app.Run();
