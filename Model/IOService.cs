using Microsoft.Win32;

namespace Xml2PdfDesigner.Model
{
    public class IOService : IIOService
    {
        /// <summary>
        ///     Show an OpenFileDialog with XML filters
        /// </summary>
        /// <param name="defaultPath">Initial directory of the file dialog</param>
        /// <returns>Selected file, null if canceled</returns>
        public string OpenFileDialog(string defaultPath)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = defaultPath,
                Filter = "XML Files (*.xml)|*.xml"
            };

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        /// <summary>
        ///     Show an SaveFileDialog with XML filters
        /// </summary>
        /// <param name="defaultPath">Initial directory of the file dialog</param>
        /// <returns>Selected file, null if canceled</returns>
        public string CreateFileDialog(string defaultPath)
        {
            var dialog = new SaveFileDialog
            {
                InitialDirectory = defaultPath,
                FileName = "1 Template",
                Filter = "XML Files (*.xml)|*.xml"
            };

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
    }
}