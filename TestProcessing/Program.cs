
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.RepositoryAnswer;
using Repository.RepositoryCategoryTasks;
using Repository.RepositoryDiscipline;
using Repository.RepositoryGroup;
using Repository.RepositoryQuest;
using Repository.RepositoryResultTest;
using Repository.RepositoryRole;
using Repository.RepositoryTest;
using Repository.RepositoryUser;
using Service.ServiceAnswer;
using Service.ServiceCategoryTasks;
using Service.ServiceDiscipline;
using Service.ServiceGroup;
using Service.ServiceQuest;
using Service.ServiceResultTest;
using Service.ServiceRole;
using Service.ServiceTest;
using Service.ServiceUser;
using Microsoft.AspNetCore.HttpOverrides;
using Repository.RepositoryDirection;
using Service.ServiceDirection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>();
builder.Services
    .AddTransient<IRepositoryAnswer, RepositoryAnswer>()
    .AddTransient<IRepositoryCategoryTasks, RepositoryCategoryTasks>()
    .AddTransient<IRepositoryDiscipline, RepositoryDiscipline>()
    .AddTransient<IRepositoryGroup, RepositoryGroup>()
    .AddTransient<IRepositoryQuest, RepositoryQuest>()
    .AddTransient<IRepositoryResultTest, RepositoryResultTest>()
    .AddTransient<IRepositoryTest, RepositoryTest>()
    .AddTransient<IRepositoryUser, RepositoryUser>()
    .AddTransient<IRepositoryRole, RepositoryRole>()
    .AddTransient<IRepositoryDirection, RepositoryDirection>()
     .AddScoped<IServiceDirection, ServiceDirection>()
    .AddScoped<IServiceAnswer, ServiceAnswer>()
    .AddScoped<IServiceCategoryTasks, ServiceCategoryTasks>()
    .AddScoped<IServiceDiscipline, ServiceDiscipline>()
    .AddScoped<IServiceGroup, ServiceGroup>()
    .AddScoped<IServiceQuest, ServiceQuest>()
    .AddScoped<IServiceResultTest, ServiceResultTest>()
    .AddScoped<IServiceTest, ServiceTest>()
    .AddScoped<IServiceUser, ServiceUser>()
    .AddScoped<IServiceRole, ServiceRole>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var tokenSettings = builder.Configuration.GetSection("Token");
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = tokenSettings["ISSUER"],
            ValidateAudience = true,
            ValidAudience = tokenSettings["AUDIENCE"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings["KEY"])),
            ValidateIssuerSigningKey = true,
        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();


}
var patchSettings = builder.Configuration.GetSection("ConnectionStrings");

app.UseHttpsRedirection();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
}); 
Console.WriteLine(builder.Environment.WebRootPath);
app.UseAuthorization();

//var fileProvider = new PhysicalFileProvider(Path.Combine(patchSettings["FilePatchwwwroot"],"Img"));
//var requestPath = "/Img";
//Console.WriteLine(fileProvider);

//// Enable displaying browser links.
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = fileProvider,
//    RequestPath = requestPath
//});
app.UseStaticFiles();

app.MapControllers();

app.Run();
