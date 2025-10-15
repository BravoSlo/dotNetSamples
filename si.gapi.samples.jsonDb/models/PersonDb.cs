using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace si.gapi.samples.jsonDb.models;
[Table("persons")]
public class PersonDb : EfBaseDb<PersonDb> {
	#region -- properties --
	[MaxLength(100)] [Required]
	public string firstName { get; set; } = string.Empty;
	[MaxLength(100)]
	public string lastName { get; set; } = string.Empty;
	public int? age { get; set; }
	
	public Address addressPrimary { get; set; } = new();
	[NotMapped]
	public List<Address> addressListOne { get; set; } = new();
	public List<Address> addressListTwo { get; set; } = new();
	#endregion
	#region -- model builder --
	public override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<PersonDb>().HasIndex(i => new { i.firstName, i.lastName });
		modelBuilder.Entity<PersonDb>()
		            .OwnsOne(p => p.addressPrimary, ap => { ap.ToJson(); })
		            .OwnsMany(p => p.addressListOne, al1 => { al1.ToJson(); })
		            .OwnsMany(p => p.addressListTwo, al2 => { al2.ToJson(); });
	}
	#endregion
	
}