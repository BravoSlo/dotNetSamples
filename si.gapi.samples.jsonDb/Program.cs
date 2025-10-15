using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using si.gapi.samples.jsonDb.context;
using si.gapi.samples.jsonDb.models;
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
//await personSvc.ClearDatabase();
await personSvc.SetupDatabase();
//await personSvc.RunSelect();
//await personSvc.RunUpdate();
//await personSvc.RunSelect();


PersonDb? ana = await personSvc.AnaGet();
personSvc.PrintPerson(ana!);
ana.age = 69;
ana.addressPrimary = new Address { type = "dom", street = "Cesta 100", city = "Hotic", zip = "1000" };
ana.addressListOne.Add(new Address { type = "sluzba", street = "Ulica 200", city = "Vace", zip = "2000" });
ana.addressListTwo.Add(new Address { type = "sluzba", street = "Ulica 200", city = "Vace", zip = "2000" });
ana = await personSvc.AnaUpdate(ana!);
personSvc.PrintPerson(ana!);
ana = await personSvc.AnaGet();
personSvc.PrintPerson(ana!);

Console.WriteLine("done");
//Console.ReadLine();