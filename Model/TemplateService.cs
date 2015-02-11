using System;
using System.Collections.ObjectModel;
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
            var template = new Template
            {
                Name = "1 " + 8.RandomString(),
                Elements = { createRootElement() }
            };

            //template.Elements.Add(CreateDummyElement(null, 0));

            return template;
        }

        private Element CreateDummyElement(Element parent, int level)
        {
            var elem = new Element(parent)
            {
                Name = 10.RandomString(),
                Coordinate =
                {
                    X = Random.Next(100) / 100.0f,
                    Y = Random.Next(100) / 100.0f,
                    Width = Random.Next(20) / 100.0f,
                    Height = Random.Next(20) / 100.0f
                }
            };

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

        private Element createRootElement()
        {
            var root = new Element(null)
            {
                Name = "Root",
                Coordinate =
                {
                    X = 0,
                    Y = 0,
                    Width = (float)(72.0 / 2.54 * 29.7), // A4 paysage
                    Height = (float)(72.0 / 2.54 * 21)
                },
                Type = ElementType.Page
            };

            var a = new Element(root)
            {
                Name = "A",
                Coordinate =
                {
                    X = 10,
                    Y = 10,
                    Width = 20,
                    Height = 20
                },
                Type = ElementType.Text
            };
            var b = new Element(root)
            {
                Name = "B",
                Coordinate =
                {
                    X = 50,
                    Y = 10,
                    Width = 40,
                    Height = 20
                },
                Type = ElementType.Text
            };
            var c = new Element(root)
            {
                Name = "C",
                Coordinate =
                {
                    X = 10,
                    Y = 50,
                    Width = 40,
                    Height = 40
                },
                Type = ElementType.Image
            };
            root.Elements = new ObservableCollection<Element>
            {
                a,
                b,
                c
            };

            return root;
        }
    }
}