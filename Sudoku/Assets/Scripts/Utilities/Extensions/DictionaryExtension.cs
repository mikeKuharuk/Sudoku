using System.Collections.Generic;

namespace Utilities.Extensions
{
    public static class DictionaryExtension
    {
        public static void AddSafely<TKey, TValue>(this Dictionary<TKey, List<TValue>> targetDictionary, TKey key, TValue value)
        {
            if (!targetDictionary.ContainsKey(key))
            {
                targetDictionary.Add(key, new List<TValue>());
            }
            
            targetDictionary[key].Add(value);
        }
    }
}