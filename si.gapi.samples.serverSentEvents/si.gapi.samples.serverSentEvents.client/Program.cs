Console.Title = "si.gapi.samples.serverSentEvents.client";
Console.WriteLine("si.gapi.samples.serverSentEvents.client");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages(options => {
	options.RootDirectory = "/ui";
});

var app = builder.Build();
app.MapRazorPages();
Console.WriteLine("Press Ctrl+C to stop the server");
app.Run();