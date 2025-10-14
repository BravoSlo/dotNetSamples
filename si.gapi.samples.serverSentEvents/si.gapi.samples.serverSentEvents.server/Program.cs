using System.Net.ServerSentEvents;
using System.Runtime.CompilerServices;
using si.gapi.samples.serverSentEvents.server;

Console.Title = "si.gapi.samples.serverSentEvents.server";
Console.WriteLine("si.gapi.samples.serverSentEvents.server");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(opt => {
	opt.AddPolicy(
		"AllowAll", 
		policy => policy.AllowAnyOrigin());
});

var app = builder.Build();
app.UseCors("AllowAll");

app.MapGet("/sse", (CancellationToken ct) => {
	
	async IAsyncEnumerable<SseItem<PersonDto>> GetPersons([EnumeratorCancellation] CancellationToken ect) {
		while(!ect.IsCancellationRequested) {
			await Task.Delay(1000, ect);
			yield return new SseItem<PersonDto>(
				PersonGenerator.GetPerson(),
				"person") {
				ReconnectionInterval = TimeSpan.FromSeconds(15),
			};
		}
	}
	
	return TypedResults.ServerSentEvents(GetPersons(ct));
});

Console.WriteLine("Press Ctrl+C to stop the server");
app.Run();



