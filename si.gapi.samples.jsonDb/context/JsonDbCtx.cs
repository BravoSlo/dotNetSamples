using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using si.gapi.samples.jsonDb.models;

namespace si.gapi.samples.jsonDb.context;
public interface IJsonDbCtx {
	#region -- tables --
	DbSet<PersonDb> persons { get; set; }
	#endregion
}
public class JsonDbCtx : DbContext, IJsonDbCtx {
	#region -- ctor --
	public JsonDbCtx(DbContextOptions<JsonDbCtx> options) : base(options) { }
	#endregion
	#region -- model builder --
	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		new PersonDb().OnModelCreating(modelBuilder);
	}
	#endregion
	#region -- tables --
	public virtual DbSet<PersonDb> persons { get; set; }
	#endregion
	
}