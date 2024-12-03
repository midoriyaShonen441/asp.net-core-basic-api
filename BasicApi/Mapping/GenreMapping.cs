using BasicApi.Dtos;
using BasicApi.Entities;

namespace BasicApi.Mapping;

public static class GenreMapping
{
    public static Genre ToEnitiy(this GenreDto dto)
    {
        return new Genre()
            {
                Id = dto.Id,
                Name = dto.Name
            };
    }

    public static GenreDto ToDto(this Genre entity)
    {
        return new GenreDto(
            entity.Id,
            entity.Name
        );
    }
}