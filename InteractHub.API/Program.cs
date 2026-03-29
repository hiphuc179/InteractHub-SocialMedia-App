using InteractHub.API.Data;
using InteractHub.API.Models;
using Microsoft.EntityFrameworkCore;
using InteractHub.API.Interfaces;
using InteractHub.API.Repositories;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// 1. DATABASE & IDENTITY
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// 2. CẤU HÌNH CORS (GIẤY THÔNG HÀNH)
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReact", policy => {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// 3. MIDDLEWARE
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReact"); 
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// 4. API ĐĂNG KÝ "THẬT" - LƯU VÀO SQL SERVER
app.MapPost("/api/Auth/register", async (RegisterDto model, UserManager<User> userManager) => 
{
    var userExists = await userManager.FindByNameAsync(model.UserName);
    if (userExists != null) return Results.BadRequest(new { message = "Tên đăng nhập đã bị chiếm dụng!" });

    var user = new User { 
        UserName = model.UserName, 
        Email = model.Email, 
        FullName = model.FullName 
    };

    var result = await userManager.CreateAsync(user, model.Password);

    if (result.Succeeded) {
        return Results.Ok(new { message = "Gia nhập Biệt đội Bụi District thành công rực rỡ!" });
    }

    return Results.BadRequest(result.Errors);
})
.WithName("Register")
.WithOpenApi();

app.MapControllers();

app.Run();

// 5. ĐỊNH NGHĨA DỮ LIỆU (PHẢI NẰM DƯỚI CÙNG FILE)
public record RegisterDto(string FullName, string Email, string UserName, string Password);