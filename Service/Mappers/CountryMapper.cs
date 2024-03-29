﻿using Entities.Models;
using Shared;
using Shared.DataTransferObjects;
using System.Runtime.CompilerServices;

namespace CountriesApp.Mappers
{
    public static class CountryMapper
    {
        public static Country ToEntity(this CountryDto countryDto)
        {
            var entity = new Country()
            {
                CommonName = countryDto.CommonName,
            };
            if (countryDto.Capitals != null && countryDto.Capitals.Count > 0)
            {
                entity.Capitals = new List<Capital>();
                foreach (var capital in countryDto.Capitals)
                {
                    entity.Capitals.Add(capital.ToEntity());
                }
            }
            if (countryDto.Borders != null && countryDto.Borders.Count > 0)
            {
                entity.Borders = new List<Border>();
                foreach (var border in countryDto.Borders)
                {
                    entity.Borders.Add(border.ToEntity());
                }
            }
            return entity;
        }

        public static CountryDto ToDto(this Country country)
        {
            var dto = new CountryDto()
            {
                CommonName = country.CommonName,
            };
            if (country.Capitals != null && country.Capitals.Count > 0)
            {
                dto.Capitals = new List<CapitalDto>();
                foreach (var capital in country.Capitals)
                {
                    dto.Capitals.Add(capital.ToDto());
                }
            }
            if (country.Borders != null && country.Borders.Count > 0)
            {
                dto.Borders = new List<BorderDto>();
                foreach (var border in country.Borders)
                {
                    dto.Borders.Add(border.ToDto());
                }
            }
            return dto;
        }

        public static Country ToEntity(this ResponseCountryObject countryObject)
        {
            var entity = new Country()
            {
                CommonName = countryObject.Name.Common
            };

            if(countryObject.Capital is not null && countryObject.Capital.Count > 0)
            {
                entity.Capitals = new List<Capital>();
                foreach (var capital in countryObject.Capital)
                {
                    entity.Capitals.Add(new Capital()
                    {
                        Name = capital
                    });
                }
            }

            if (countryObject.Borders is not null && countryObject.Borders.Count > 0)
            {
                entity.Borders = new List<Border>();
                foreach (var border in countryObject.Borders)
                {
                    entity.Borders.Add(new Border()
                    {
                        Name = border
                    });
                }
            }
            return entity;
        }
        public static IEnumerable<CountryDto> ToDtos(this IEnumerable<Country> countries)
        {
            var countryDtos = new List<CountryDto>();
            foreach (var country in countries)
            {
                countryDtos.Add(country.ToDto());
            }
            return countryDtos; 
        }

    }
}

