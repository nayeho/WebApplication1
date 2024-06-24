var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddHttpClient(); // HttpClient를 등록합니다.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:7077")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
});



// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers(); // API Controller 사용을 위해 추가

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(); // CORS 정책을 위해 추가

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers(); // API Controller 사용을 위해 추가

// 엔드포인트 설정
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

app.Run();
