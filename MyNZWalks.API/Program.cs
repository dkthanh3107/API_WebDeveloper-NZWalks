using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyNZWalks.API.Data;
using MyNZWalks.API.Middleware;
using MyNZWalks.API.Repositories;
using Serilog;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/NZWalks_Log.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Warning()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = " NZ Walks API ", Version = "v1" });
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type= SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme,
                },
                Scheme = "Ouach2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
            }
        });
    });
    builder.Services.AddDbContext<NZWalkDBContext>(options => 
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddDbContext<NZWalksAuthenDBContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("NZAuthenConnection")));
    builder.Services.AddScoped<IRegionRepository,RegionRepository>();
    builder.Services.AddScoped<IWalksRepository, WalkRepository>();
    builder.Services.AddScoped<ITokenRepository, TokenRepository>();
    builder.Services.AddScoped<IImageRepository, LocalImageRepository>();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddIdentityCore<IdentityUser>()
        .AddRoles<IdentityRole>()
        .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("MyNZWalks")
        .AddEntityFrameworkStores<NZWalksAuthenDBContext>()
        .AddDefaultTokenProviders();

    builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
    });

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        });

    var app = builder.Build();


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
app.UseMiddleware<ExceptionHandlerMiddleware>();
    app.UseHttpsRedirection();

    app.UseAuthentication();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
    //https://localhost1234
});
    app.UseAuthorization(); 

    app.MapControllers();

    app.Run();
