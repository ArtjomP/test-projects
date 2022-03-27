// See https://aka.ms/new-console-template for more information

using System.Text;

public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding => Encoding.UTF8;
}