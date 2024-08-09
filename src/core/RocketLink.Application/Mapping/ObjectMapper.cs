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

    public static TTarget UpdateProperties<TSource, TTarget>(TSource source, TTarget target)
    {
        if (source == null || target == null)
            throw new ArgumentNullException("Source and target objects must not be null.");

        var sourceType = source.GetType();
        var targetType = target.GetType();

        foreach (var property in sourceType.GetProperties())
        {
            var sourceValue = property.GetValue(source);

            if (sourceValue != null)
            {
                var targetProperty = targetType.GetProperty(property.Name);
                if (targetProperty != null && targetProperty.CanWrite)
                {
                    targetProperty.SetValue(target, sourceValue);
                }
            }
        }

        return target;
    }
}

