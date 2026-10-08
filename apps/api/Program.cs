using Ibernia.Assessment.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<INoteExtractionService, NoteExtractionService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Netlify", policy =>
        policy.WithOrigins("https://ibernia-assessment-web.netlify.app")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("Netlify");
app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run();

public partial class Program { }
