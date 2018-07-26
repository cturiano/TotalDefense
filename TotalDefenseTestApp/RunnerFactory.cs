using System;
using TotalDefenseTestAppDictionary;
using TotalDefenseTestAppLucene;
using TotalDefenseTestAppShared;

namespace TotalDefenseTestApp
{
    internal static class RunnerFactory
    {
        #region Internal Methods

        internal static IRunner GetRunner(int type)
        {
            switch (type)
            {
                case 0:
                    return new DictionaryRunner();
                case 1:
                    return new LuceneRunner();
                default:
                    throw new IndexOutOfRangeException(nameof(type));
            }
        }

        #endregion
    }
}