using RentlystBackEnd.Domain.Entities;

namespace RentlystBackEnd.Presentation.Types;

[ExtendObjectType(typeof(Address))]
public class AddressExtensions
{
    public string GetFullAddress([Parent] Address address)
    {
        var street = address.StreetName ?? string.Empty;
        var number = address.StreetNumber ?? string.Empty;
        var suburb = address.Suburb ?? string.Empty;

        return $"{street} {number}, {suburb}".Trim();
    }
}