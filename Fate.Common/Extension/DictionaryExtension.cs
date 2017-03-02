using System.Collections.Generic;

namespace Fate.Common.Extension
{
    public static class DictionaryExtension
    {
        public static TValue GetValueOrDefault<TKey, TValue> (this Dictionary<TKey, TValue> dic, TKey key)
        {
            TValue value;
            if (dic.TryGetValue(key, out value))
            {
                return value;
            }
            return default(TValue);
        }
    }
}
