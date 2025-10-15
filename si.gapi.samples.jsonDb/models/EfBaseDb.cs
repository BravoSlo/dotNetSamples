using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace si.gapi.samples.jsonDb.models;
public class EfBaseDb<T> where T : EfBaseDb<T> {
	#region -- properties --
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int id  { get; set; }
	[Required]
	public DateTime createdDate { get; set; }
	[Required]
	public DateTime modifiedDate { get; set; }
	#endregion
	#region -- model builder --
	public virtual void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<T>().HasIndex(i => i.id);
		modelBuilder.Entity<T>().Property(i => i.createdDate).HasDefaultValueSql("current_timestamp");
		modelBuilder.Entity<T>().Property(i => i.modifiedDate).HasDefaultValueSql("current_timestamp");
	}
	#endregion
}
