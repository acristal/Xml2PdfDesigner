using System;
using Xml2PdfDesigner.Model;
using Xml2PdfDesigner.Utils;

namespace Xml2PdfDesigner.Design
{
    public class DesignTemplateService : ITemplateService
    {
        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        ///     Generates a dummy Template object, used at design time
        /// </summary>
        public Template ParseTemplateFrom(string path)
        {
            var template = new Template();

            for (var i = 0; i < 15; ++i)
                template.Elements.Add(CreateDummyElement(1));

            template.Name = "1 Design-" + 5.RandomString();
            return template;
        }

        /// <summary>
        ///     Generates a dummy Element object.
        ///     The deeper we are, the less chance we have to have children elements
        /// </summary>
        private Element CreateDummyElement(int level)
        {
            var elem = new Element { Name = "D-" + 8.RandomString() };

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