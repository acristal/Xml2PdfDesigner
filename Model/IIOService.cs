namespace Xml2PdfDesigner.Model
{
    public interface IIOService
    {
        string OpenFileDialog(string defaultPath);
        string CreateFileDialog(string defaultPath);
    }
}