using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TheNextBigThing.Domain.Responses;

namespace TheNextBigThing.Domain.Utilities;

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
}
