using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using TheNextBigThing.API.Middlewares;
using TheNextBigThing.Application;
using TheNextBigThing.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = builder.Configuration.GetValue<string>("SwaggerTitle")
                ?? throw new ArgumentException("Configuration does not have a SwaggerTitle section"),
            Description = builder.Configuration.GetValue<string>("SwaggerDescription") ?? string.Empty
        }
     );

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "TheNextBigThing.API.xml");
    c.IncludeXmlComments(filePath);
});

builder.Services.AddHttpClient();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration.GetValue<string>("DBConnection"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandlingMiddleware();

app.MapControllers();

app.Run();
