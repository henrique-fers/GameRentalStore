using GameRentalStore.Services;
using GameRentalStore.Services.Interfaces;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.AspNetCore;
using GameRentalStore.Repositories;
using GameRentalStore.Repositories.Interfaces;
using FluentValidation;
using GameRentalStore.Models.Views.Employee;
using GameRentalStore.Models.Validators.Employee;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IPromotionService, PromotionService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IValidator<CreateEmployeeViewModel>, CreateEmployeeViewModelValidator>();
//builder.Services.AddTransient<IValidator<CreateEmployeeViewModel>, CreateEmployeeViewModelValidator>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();


builder.Services.AddTransient<IPromotionRepository, PromotionRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();


builder.Services.AddControllers();
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

var key = Encoding.ASCII.GetBytes(GameRentalStore.Configuration.JwtKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
}); 


var app = builder.Build();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
