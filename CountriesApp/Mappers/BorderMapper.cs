using Entities.Models;
using Shared.DataTransferObjects;

namespace CountriesApp.Mappers
{
    public static class BorderMapper
    {
        public static Border ToEntity(this BorderDto borderDto)
        {
            return new Border() { Name = borderDto.Name };
        }

        public static BorderDto ToDto(this Border border)
        {
            return new BorderDto() { Name = border.Name };
        }
    }
}
