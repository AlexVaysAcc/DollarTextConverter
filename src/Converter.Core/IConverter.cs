namespace Converter.Core
{
    public interface IConverter
    {
        string Convert(long dollars, int cents);
    }
}
