using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace si.gapi.samples.jsonDb.context;
public interface IJsonDbCtxFactory :
	IDbContextFactory<JsonDbCtx>,
	IDesignTimeDbContextFactory<JsonDbCtx> {
}
public class JsonDbCtxFactory : IJsonDbCtxFactory {
	#region -- ctor --
	public JsonDbCtxFactory() { }
	#endregion
	#region -- factory --
	public JsonDbCtx CreateDbContext() {
		AssemblyName assemblyName = typeof(JsonDbCtx).GetTypeInfo().Assembly.GetName();
		string migrationsAssembly = assemblyName.Name ?? "";
		var optionsBuilder = new DbContextOptionsBuilder<JsonDbCtx>();
		optionsBuilder.UseSqlite(
			"Data Source=dbstore/jsondb.db",
			opts => opts.MigrationsAssembly(migrationsAssembly));
		return new JsonDbCtx(optionsBuilder.Options);
	}
	public JsonDbCtx CreateDbContext(string[] args) {
		return CreateDbContext();
	}
	#endregion
}