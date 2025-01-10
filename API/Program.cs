using API.Middleware;
using Core.Entities;
using Core.Interface;
using Infrastructure.Data;
using Infrastructure.Photo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationContext>(opt =>{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", builder =>
    {
        builder.WithOrigins("http://localhost:4200","https://localhost:4200") // Update with your Angular app's domain
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Allows sending cookies
    });
});

builder.Services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();
builder.Services.AddScoped<ICardDetailRepository, CardDetailRepository>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IQRCoreRepository, QRCoreRepository>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

//app.UseHttpsRedirection();

//app.UseAuthorization();
app.UseCors("AllowAngular");

app.MapControllers();

app.Run();
