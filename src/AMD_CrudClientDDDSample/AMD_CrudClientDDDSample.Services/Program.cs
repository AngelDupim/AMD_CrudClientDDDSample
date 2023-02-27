using AMD_CrudClientDDDSample.Infrastructure.IoC;
using AMD_CrudClientDDDSample.Infrastructure.Shared.JWT;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API_Client_DDD_Sample",
        Description = "API restful",
        Version = "v1"
    });

    var xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
    var filePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(filePath);
});

// Configuration IoC
builder.Services.AddRegisterServices();

// Configuration JWT
builder.Services.addJWT();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "API_Client v1");
});


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();