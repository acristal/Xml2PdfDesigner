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

            for (var i = 0; i < 15; ++i)
                template.Elements.Add(CreateDummyElement(1));

            template.Name = "1 " + 8.RandomString();
            return template;
        }

        private Element CreateDummyElement(int level)
        {
            var elem = new Element { Name = 10.RandomString() };

            var values = Enum.GetValues(typeof(ElementType));
            elem.Type = (ElementType)values.GetValue(Random.Next(values.Length - 1) + 1);

            if (Random.Next(level * 3) != 0)
                return elem;

            var nChildren = Random.Next(5) + 1;
            for (var i = 0; i < nChildren; ++i)
                elem.Elements.Add(CreateDummyElement(level + 1));
            return elem;
        }
    }
}