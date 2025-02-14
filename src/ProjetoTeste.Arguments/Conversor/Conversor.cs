using System.Reflection;

namespace ProjetoTeste.Arguments.Conversor;

public static class Conversor
{
    public static TDestination Converter<TSource, TDestination>(this TSource source)
        where TSource : class
        where TDestination : class
    {
        Type type = typeof(TDestination);
        var destination = Activator.CreateInstance(type);

        (from i in source.GetType().GetProperties()
         let property = type.GetProperty(i.Name)
         select property != null ? property.setProperty(destination, i.GetValue(source)) : default).ToList();

        return destination as TDestination;
    }

    public static bool setProperty<TDestination>(this PropertyInfo propertyInfo, TDestination destination, object value)
    {
        propertyInfo.SetValue(destination, value);
        return true;
    }

    public static List<TDestination> ConverterList<TSource, TDestination>(this List<TSource> listTSource)
    where TSource : class
    where TDestination : class
    {
        return (from i in listTSource
                select i.Converter<TSource, TDestination>()).ToList();
    }

    public static List<TDestination> Convert<TSource, TDestination>(this List<TSource> listSource)
    {
        return listSource.Select(i => (TDestination)(dynamic)i).ToList();
    }

    public static List<TDestinationFinal> Convert<TSource, TDestinationInitial, TDestinationFinal>(this List<TSource> listSource)
    {
        return listSource.Select(i => (TDestinationFinal)(dynamic)(TDestinationInitial)(dynamic)i).ToList();
    }
}