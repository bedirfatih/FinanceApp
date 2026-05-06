using System.Text.Json.Serialization;
using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<TransactionsService>();
builder.Services.AddScoped<TransfersService>();

builder.Services.AddControllers().AddJsonOptions(opt =>
    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

var app = builder.Build();

app.UseCors();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
