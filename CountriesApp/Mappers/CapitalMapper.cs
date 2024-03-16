using Entities.Models;
using Shared.DataTransferObjects;

namespace CountriesApp.Mappers
{
    public static class CapitalMapper
    {
        public static Capital ToEntity(this CapitalDto capitalDto)
        {
            return new Capital() { Name = capitalDto.Name };
        }

        public static CapitalDto ToDto(this Capital capital)
        {
            return new CapitalDto() { Name = capital.Name };
        }
    }
}
