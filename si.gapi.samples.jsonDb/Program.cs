using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using si.gapi.samples.jsonDb.context;
using si.gapi.samples.jsonDb.services;

Console.Title = "si.gapi.samples.jsonDb";
Console.WriteLine("starting si.gapi.samples.jsonDb");

ServiceCollection services = new();
services.AddScoped<IJsonDbCtxFactory, JsonDbCtxFactory>();
services.AddScoped<IPersonSvc, PersonSvc>();

ServiceProvider provider = services.BuildServiceProvider();

IJsonDbCtxFactory? factory = provider.GetService<IJsonDbCtxFactory>();
if(factory == null)
	throw new InvalidProgramException("No JsonDbCtxFactory has been created");
using(JsonDbCtx ctx = factory.CreateDbContext()) {		
	ctx.Database.Migrate();
}

IPersonSvc? personSvc = provider.GetService<IPersonSvc>();
if(personSvc == null)
	throw new InvalidProgramException("No PersonSvc has been created");
await personSvc.SetupDatabase();
await personSvc.RunTest();

Console.WriteLine("done");
Console.ReadLine();