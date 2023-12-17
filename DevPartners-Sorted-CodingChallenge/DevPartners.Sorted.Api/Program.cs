using DevPartners.Sorted.Api.Configurations;
using DevPartners.Sorted.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient(RainfallServiceSettings.ServiceName);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new()
        {
            Title = "Rainfall Api",
            Description = "An API which provides rainfall reading data",
            Version = "1.0",
            Contact = new() { Name = "Sorted", Url = new Uri($"https://www.sorted.com") }
        });
    c.AddServer(new() { Url = $"http://localhost:3000", Description = "Rainfall Api" });
    c.EnableAnnotations();
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .Build());
});

builder.Services
        .Configure<RainfallApiEndpointSettings>
            (builder.Configuration.GetSection("RainfallApiEndpointSettings"));

builder.Services.AddScopedAndTransientServices();

var app = builder.Build();

app.UseExceptionHandler(_ => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors();

app.Run();
