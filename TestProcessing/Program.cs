
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

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Service.AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = Service.AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = Service.AuthOptions.GetSymmetricSecurityKey(),
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


app.UseHttpsRedirection();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthorization();

app.MapControllers();

app.Run();
