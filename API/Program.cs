using API.Extensions;
using API.Middlewares;
using Aplication.Helpers.MappingProfiles;
using Aplication.Interfaces;
using Aplication.Posts;
using Aplication.Users;
using Doiman;
using FluentValidation.AspNetCore;
using Infrastruture.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
// AddApplicationServices();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

//usamos para definir servico de DB
builder.Services.AddDbContext<DataContext>(optionsAction =>
{
    optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

//So fazemos uma vez
builder.Services.AddMediatR(typeof(CreatePost.CreatePostCommand).Assembly); //configuracao do mediatr

builder.Services.AddIdentity<User, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddAutoMapper(typeof(MappingProfiles));

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(cfg =>
{
    cfg.RegisterValidatorsFromAssemblyContaining<CreatePost.CreatePostCommand>();
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();