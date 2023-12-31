
using AzureMediaServices.Repository;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<HomeRepository>();
builder.Services.AddScoped<AssetRepository>();
builder.Services.AddScoped<EncodeRepository>();
builder.Services.AddScoped<IndexerRepository>();
builder.Services.AddScoped<AnalyzerRepository>();
builder.Services.AddScoped<ReportRepository>();
builder.Services.AddSignalR();
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["AZURE_CONNECTION_STRING:blob"], preferMsi: true);
    clientBuilder.AddQueueServiceClient(builder.Configuration["AZURE_CONNECTION_STRING:queue"], preferMsi: true);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
