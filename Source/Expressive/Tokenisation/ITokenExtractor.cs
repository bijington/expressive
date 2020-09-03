namespace Expressive.Tokenisation
{
    public interface ITokenExtractor
    {
        Token ExtractToken(string expression, int currentIndex, Context context);
    }
}