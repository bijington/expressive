namespace Expressive.Tokenisation
{
    internal interface ITokenExtractor
    {
        Token ExtractToken(string expression, int currentIndex, Context context);
    }
}