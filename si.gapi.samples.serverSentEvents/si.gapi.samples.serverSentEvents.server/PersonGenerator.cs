using Bogus;

namespace si.gapi.samples.serverSentEvents.server;
public class PersonGenerator {
	#region -- locals --
	private static readonly Faker _bogus = new();
	#endregion
	#region -- public --
	public static PersonDto GetPerson() {
		PersonDto dto = new PersonDto();
		dto.time = DateTime.Now;
		dto.name = _bogus.Internet.UserName();
		dto.email = _bogus.Internet.Email();
		dto.location = _bogus.Address.City();
		return dto;
	}
	#endregion
}