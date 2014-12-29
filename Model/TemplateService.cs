using System;
using Xml2PdfDesigner.Utils;

namespace Xml2PdfDesigner.Model
{
    public class TemplateService : ITemplateService
    {
        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        ///     Parse the XML template to create a Template object, containing the object definition of the XML template
        /// </summary>
        /// <param name="path">XML path</param>
        /// <returns>Representation of the XML file</returns>
        public Template ParseTemplateFrom(string path)
        {
            var template = new Template();
            
            template.Elements.Add(CreateDummyElement(null, 0));

            template.Name = "1 " + 8.RandomString();
            return template;
        }

        private Element CreateDummyElement(Element parent, int level)
        {
            var elem = new Element(parent) {Name = 10.RandomString()};

            var values = Enum.GetValues(typeof(ElementType));
            elem.Type = (ElementType)values.GetValue(Random.Next(values.Length - 2) + 2);
            
            if (parent == null)
                elem.Type = ElementType.Page;

            if (Random.Next(level * 3) != 0)
                return elem;

            var nChildren = Random.Next(5) + 1 + (parent == null ? 5 : 0);
            for (var i = 0; i < nChildren; ++i)
                elem.Elements.Add(CreateDummyElement(elem, level + 1));
            return elem;
        }
    }
}