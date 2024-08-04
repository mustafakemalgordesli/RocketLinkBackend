namespace RocketLink.Application.Mapping;

public class ObjectMapper
{
    public static TDto MapGeneric<TEntity, TDto>(TEntity entity)
    {
        var dto = Activator.CreateInstance<TDto>();
        var dtoProperties = typeof(TDto).GetProperties();
        var entityProperties = typeof(TEntity).GetProperties();

        foreach (var entityProperty in entityProperties)
        {
            var dtoProperty = dtoProperties.FirstOrDefault(p => p.Name == entityProperty.Name);

            if (dtoProperty is not null)
            {
                dtoProperty.SetValue(dto, entityProperty.GetValue(entity));
            }
        }

        return dto;
    }
}
