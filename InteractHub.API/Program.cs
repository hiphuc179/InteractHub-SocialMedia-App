using InteractHub.API.Data;
using InteractHub.API.Models;
using Microsoft.EntityFrameworkCore;
using InteractHub.API.Interfaces;
using InteractHub.API.Repositories;
using InteractHub.API.Middleware;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);
// 1. Đăng ký Database Context (Kết nối SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    // 2. Cấu hình Identity (Bảo mật tài khoản)
// 2. Cấu hình Identity (Bảo mật tài khoản - Nâng cấp)
builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.Password.RequireDigit = false; // Không bắt buộc có số
    options.Password.RequiredLength = 6;   // Độ dài tối thiểu 6 ký tự
    options.Password.RequireNonAlphanumeric = false; // Không bắt buộc ký tự đặc biệt (@, #, !)
    options.Password.RequireUppercase = false; // Không bắt buộc viết hoa
})
.AddEntityFrameworkStores<ApplicationDbContext>();
    // 3. Thêm bộ Controllers (Để sau này viết code trong thư mục Controllers)
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Đăng ký Repository và Unit of Work
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); //hệ thống biết ai đang đăng nhập
app.UseAuthorization(); //Để phân quyền (Admin/User)

app.MapControllers();   

app.Run();