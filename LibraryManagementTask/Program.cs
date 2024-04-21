using LibraryManagementTask.Data;
using LibraryManagementTask.Data.Repositories._UnitOfWork;
using LibraryManagementTask.Extensions;
using LibraryManagementTask.Handlers;
using LibraryManagementTask.Services.JWT;
using LibraryManagementTask.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwagger();

//packages
builder.Services
    .AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Default")))
    .AddAutoMapper(Assembly.GetExecutingAssembly())
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddProblemDetails()
    .AddJWT(builder.Configuration)
    .AddMemoryCache();    

//services
builder.Services
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddScoped<ITokenGenerator, TokenGenerator>()
    .AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>))
    .AddScoped<ApplicationDbContextInitialiser>();

//configs
builder.Services
    .Configure<JWTSettings>(builder.Configuration.GetSection(JWTSettings.SectionName))
    .ConfigureApiBehaviorOptions();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Initialise database if not exists
using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider
        .GetRequiredService<ApplicationDbContextInitialiser>()
        .InitialiseAsync();
}

app.Run();
