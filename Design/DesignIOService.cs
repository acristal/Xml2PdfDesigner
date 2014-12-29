using Xml2PdfDesigner.Model;

namespace Xml2PdfDesigner.Design
{
    public class DesignIOService : IIOService
    {
        /// <summary>
        ///     Returns FileTest.xml
        /// </summary>
        public string OpenFileDialog(string defaultPath)
        {
            return "FileTest.xml";
        }

        /// <summary>
        ///     Returns FileTest_create.xml
        /// </summary>
        public string CreateFileDialog(string defaultPath)
        {
            return "FileTest_create.xml";
        }
    }
}