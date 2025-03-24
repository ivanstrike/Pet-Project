using PetProject.Application;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Infrastructure;
using PetProject.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddInfrastructure()
    .AddApplication();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

app.Run();

