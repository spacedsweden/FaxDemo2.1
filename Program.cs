using FaxDemo2._1;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("phaxio",  o=> {
    var apiKey = builder.Configuration["phaxioKey"];
    var apiSecret = builder.Configuration["phaxioSecret"];
    
    o.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:{apiSecret}")));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapPost("/incoming", ([FromForm]FaxEvent event_type, HttpRequest request, IFormFile file) =>
{
    if (event_type == FaxEvent.fax_completed)
    {
        var fax = JsonSerializer.Deserialize<Fax>(request.Form["fax"]);
        Console.WriteLine($"Incoming fax from: {fax.FromNumber}");
        Console.WriteLine($"Number of pages: {file.FileName}");
    }
    return Results.Ok();
})
.WithName("Incoming").DisableAntiforgery();

app.MapPost("/sendFax", async (IHttpClientFactory httpClientFactory, [FromForm]string to, [FromForm] string callerId, [FromForm] string contentUrl) =>
{
    var client = httpClientFactory.CreateClient("phaxio");
    var content = new MultipartFormDataContent();
    content.Add(new StringContent(to), "to");
    content.Add(new StringContent(callerId), "caller_id");
    content.Add(new StringContent(contentUrl), "content_url");
    var response = await client.PostAsync("https://api.phaxio.com/v2.1/faxes", content);
    response.EnsureSuccessStatusCode();
    var responseString = await response.Content.ReadAsStringAsync();
    return responseString;
}).WithName("SendFax").DisableAntiforgery();

app.Run();
