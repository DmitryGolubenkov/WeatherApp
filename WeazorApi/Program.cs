var builder = WebApplication.CreateBuilder(args);

// Add services to the container
//Setup CORS
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WeazorApi", Version = "v1" });
});

//Add caching
builder.Services.AddMemoryCache();



var app = builder.Build();
app.UseCors(options => options.AllowAnyOrigin());
// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeazorApi v1"));
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();