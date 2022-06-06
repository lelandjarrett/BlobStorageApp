using BlobStorage.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);



    var builders = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");

    var configuration = builders.Build();

    // Get a connection string to our Azure Storage account.
    var connectionString = configuration.GetConnectionString("StorageAccount");

    // Get a reference to the container client object so you can create the "photos" container
    string containerName = "customerimage";
    BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
    container.CreateIfNotExists();

    // List out all the blobs in the container
    var blobs = container.GetBlobs();
    foreach (var blob in blobs)
    {
        Console.WriteLine($"{blob.Name} --> Created On: {blob.Properties.CreatedOn:yyyy-MM-dd HH:mm:ss}  Size: {blob.Properties.ContentLength}");
    }

// Add services to the container.


builder.Services.AddRazorPages();
builder.Services.AddScoped<ICustomerData, SqlCustomerData>();
builder.Services.AddScoped<ICustomerImage, SqlCustomerImageData>();
builder.Services.AddDbContextPool<CustomerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerDb")));
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["ConnectionStrings:StorageAccount:blob"], preferMsi: true);
    clientBuilder.AddQueueServiceClient(builder.Configuration["ConnectionStrings:StorageAccount:queue"], preferMsi: true);
});
//builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer("DefaultConnection"));


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

app.UseAuthorization();

app.MapRazorPages();

app.Run();