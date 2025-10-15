using Microsoft.EntityFrameworkCore;
using si.gapi.samples.jsonDb.context;
using si.gapi.samples.jsonDb.models;

namespace si.gapi.samples.jsonDb.services;
public interface IPersonSvc {
	Task<bool> SetupDatabase();
	Task<bool> RunTest();
}
public sealed class PersonSvc : IPersonSvc {
	#region -- locals --
	private readonly IJsonDbCtxFactory _ctxFactory;
	#endregion
	#region -- ctor --
	public PersonSvc(IJsonDbCtxFactory ctxFactory) {
		_ctxFactory = ctxFactory;
	}
	#endregion
	#region -- public --
	public async Task<bool> SetupDatabase() {
		await using JsonDbCtx ctx = _ctxFactory.CreateDbContext();
		if(!ctx.persons.Any()) {
			PersonDb p1 = new() { firstName = "Janez", lastName = "Novak", age = 45,
				addressPrimary = new Address { type = "dom", street = "Pot 1", city = "Litija", zip = "1270" },
				addressListOne = {
						new() { type = "vikend", street = "Cesta 1", city = "Ljubljana", zip = "1000" },
						new() { type = "sluzba", street = "Ulica 2", city = "Maribor", zip = "2000" }
				},
				addressListTwo = {
					new() { type = "vikend", street = "Ulica 10", city = "Bled", zip = "4260" },
					new() { type = "sluzba", street = "Cesta 11", city = "Koper", zip = "6000" }
				}
			};
			PersonDb p2 = new() { firstName = "Maja", lastName = "Horvat", age = 38,
				addressPrimary = new Address { type = "dom", street = "Cesta 3", city = "Kranj", zip = "4000" },
				addressListOne = {
						new() { type = "vikend", street = "Pot 4", city = "Bled", zip = "4260" },
						new() { type = "sluzba", street = "Ulica 5", city = "Ljubljana", zip = "3000" }
				},
				addressListTwo = {
					new() { type = "vikend", street = "Cesta 1", city = "Ljubljana", zip = "1000" },
					new() { type = "sluzba", street = "Ulica 2", city = "Maribor", zip = "2000" }
				}
			};
			PersonDb p3 = new() { firstName = "Ana", lastName = "Kralj", age = 29,
				addressPrimary = new Address { type = "dom", street = "Ulica 6", city = "Celje", zip = "3000" },
				addressListOne = {
						new() { type = "vikend", street = "Cesta 7", city = "Portoroz", zip = "6320" },
						new() { type = "sluzba", street = "Pot 8", city = "Ljubljana", zip = "1000" }
				}
			};
			PersonDb p4 = new() { firstName = "Marko", lastName = "Zupanc", age = 52,
				addressPrimary = new Address { type = "dom", street = "Pot 9", city = "Litija", zip = "1270" },
				addressListOne = {
						new() { type = "vikend", street = "Ulica 10", city = "Bled", zip = "4260" },
						new() { type = "sluzba", street = "Cesta 11", city = "Koper", zip = "6000" }
				}
			};
			ctx.persons.AddRange(p1, p2, p3 ,p4);
			await ctx.SaveChangesAsync();
		}
		return true;
	}
	public async Task<bool> RunTest() {
		Console.WriteLine("Get Person by first name 'Maja'");
		await using JsonDbCtx ctx = _ctxFactory.CreateDbContext();
		List<PersonDb> lst = await ctx.persons
		                         .Where(x => x.firstName == "Maja")
		                         .ToListAsync();
		foreach(PersonDb p in lst) 
			printPerson(p);
		
		Console.WriteLine("Get persons from address primary city = litija");
		lst = await ctx.persons
		               .Where(x => x.addressPrimary.city.ToLower() == "litija")
		               .ToListAsync();		
		foreach(PersonDb p in lst) 
			printPerson(p);

		Console.WriteLine("Get persons from address list city = ljubljana");
		lst = await ctx.persons
		               .Where(x => x.addressListOne.Any(a => a.city.ToLower() == "ljubljana"))
		               .ToListAsync();		
		foreach(PersonDb p in lst) 
			printPerson(p);

		return true;
	}
	#endregion
	#region -- private --
	private void printPerson(PersonDb p) {
		Console.WriteLine($"  {p.id}: {p.firstName} {p.lastName}, age: {p.age}");
		Console.WriteLine($"    primary address: {p.addressPrimary.type}, {p.addressPrimary.street}, {p.addressPrimary.city}, {p.addressPrimary.zip}");
		foreach(Address a in p.addressListOne)
				Console.WriteLine($"    addressOne: {a.type}, {a.street}, {a.city}, {a.zip}");
		foreach(Address a in p.addressListTwo)
			Console.WriteLine($"    addressTwo: {a.type}, {a.street}, {a.city}, {a.zip}");
	}
	#endregion
}