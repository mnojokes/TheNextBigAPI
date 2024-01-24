using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using TheNextBigThing.Application.Services;

namespace TheNextBigThing.Application.Utilities;

public static class XmlUtility
{
    public static string Serialize<T>(T obj)
    {
        XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
        using (var sww = new StringWriter())
        {
            using (XmlTextWriter writer = new XmlTextWriter(sww) { Formatting = Formatting.Indented })
            {
                xsSubmit.Serialize(writer, obj);
                return sww.ToString();
            }
        }
    }

    public static T? Deserialize<T>(string obj)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));

        // Deserialize the XML data into an ExchangeRates object
        using (TextReader reader = new StringReader(obj))
        {
            return (T?)serializer.Deserialize(reader);
        }
    }
}
