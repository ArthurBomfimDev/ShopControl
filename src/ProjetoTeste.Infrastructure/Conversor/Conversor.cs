using System.Reflection;

namespace ProjetoTeste.Infrastructure.Conversor.BasesConversor;

public static class Conversor
{
    public static TOutput Converter<TInput, TOutput>(this TInput input)
        where TInput : class
        where TOutput : class
    {
        Type type = typeof(TOutput);
        var output = Activator.CreateInstance(type);

        (from i in input.GetType().GetProperties()
         let property = type.GetProperty(i.Name)
         select property != null ? property.setProperty(output, i.GetValue(input)) : default).ToList();

        return output as TOutput;
    }

    public static bool setProperty<TOutput>(this PropertyInfo propertyInfo, TOutput output, object value)
    {
        propertyInfo.SetValue(output, value);
        return true;
    }

    public static List<TOutput> ConverterList<TInput, TOutput>(this List<TInput> listTInput)
    where TInput : class
    where TOutput : class
    {
        return (from i in listTInput
                select i.Converter<TInput, TOutput>()).ToList();
    }
}