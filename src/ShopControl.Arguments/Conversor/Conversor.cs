using System.Reflection;

namespace ShopControl.Arguments.Conversor;

public static class Conversor
{
    #region Conversor Reflection
    public static TDestination? ConverterReflection<TSource, TDestination>(this TSource source)
        where TSource : class
        where TDestination : class
    {
        var destination = Activator.CreateInstance<TDestination>();
        Type type = destination.GetType();

        if (source == null) return null;

        (from i in source.GetType().GetProperties()
         let property = type.GetProperty(i.Name)
         where property != null
         let typeDestination = property.PropertyType
         let value = i.GetValue(source)
         let typeSource = i.PropertyType
         select value.TryConvert(destination, typeDestination, typeSource, property)).ToList();

        return destination;
    }

    public static bool TryConvert<TDestination, TSource, TTypeDestination, TTypeSource>(this TSource? sourceValue, TDestination destination, TTypeDestination typeDestination, TTypeSource typeSource, PropertyInfo property)
        where TDestination : class
        where TSource : class
        where TTypeDestination : Type
        where TTypeSource : Type
    {
        if (typeSource.IsGenericType && typeSource.GetGenericTypeDefinition() == typeof(List<>))
        {
            var verificateType = typeSource.GenericTypeArguments[0];
            if (verificateType.IsClass && !verificateType.IsPrimitive && verificateType != typeof(string))
            {
                if (sourceValue is System.Collections.IEnumerable enumerable && enumerable != null)
                {
                    var listDestinationType = typeDestination.GenericTypeArguments[0];
                    var listEntity = Activator.CreateInstance(typeof(List<>).MakeGenericType(listDestinationType));
                    var conversorMethod = typeof(Conversor).GetMethod(nameof(ConverterReflection)).MakeGenericMethod(verificateType, listDestinationType);
                    foreach (var valueClass in enumerable)
                    {
                        var destinationInstace = Activator.CreateInstance(listDestinationType);
                        var conversor = conversorMethod.Invoke(null, new[] { valueClass });
                        listEntity!.GetType().GetMethod("Add")!.Invoke(listEntity, new[] { conversor });
                    }
                    property!.SetValue(destination, listEntity);
                    return true;
                }
            }
        }
        else if (typeSource.IsClass && !typeSource.IsPrimitive && typeSource != typeof(string))
        {
            var convertMethod = typeof(Conversor).GetMethod(nameof(ConverterReflection)).MakeGenericMethod(typeSource, typeDestination);
            var convert = convertMethod.Invoke(null, new[] { sourceValue });
            property!.SetValue(destination, convert);
            return true;
        }
        property!.SetValue(destination, sourceValue);
        return true;
    }

    public static List<TDestination> ConverterReflectionList<TSource, TDestination>(this List<TSource> listTSource)
    where TSource : class
    where TDestination : class
    {
        return (from i in listTSource
                select i.ConverterReflection<TSource, TDestination>()).ToList();
    }
    #endregion

    #region Implicit Conversor List
    public static List<TDestination> ConverterList<TSource, TDestination>(this List<TSource> listSource)
    {
        return listSource.Select(i => (TDestination)(dynamic)i).ToList();
    }
    #endregion
}