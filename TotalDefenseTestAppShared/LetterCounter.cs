namespace TotalDefenseTestAppShared
{
    public class LetterCounter
    {
        #region Public Methods

        public static string GetLetterCounter(int i)
        {
            return new string((char)(i % 26 + 97), i / 26 + 1);
        }

        #endregion
    }
}