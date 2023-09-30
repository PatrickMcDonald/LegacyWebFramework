using System.Diagnostics;
using LegacyWcfServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IUserService>(provider => new UserServiceClient());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Trace-Id", Activity.Current?.Id ?? context.TraceIdentifier);

    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
