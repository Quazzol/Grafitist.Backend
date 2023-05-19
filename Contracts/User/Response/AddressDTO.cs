namespace Grafitist.Contracts.User.Response;

public class AddressDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public CityDTO? City { get; set; }
    public DistrictDTO? District { get; set; }
    public string? OpenAddress { get; set; }
}