using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace CalendarTest
{
    public class SlowDictionary : IDictionary<DateTime, string>
    {
        private IDictionary<DateTime, string> _localDictionary = new Dictionary<DateTime, string>();

        public string this[DateTime key]
        {
            get
            {
                Thread.Sleep(10);
                return _localDictionary[key];
            }
            set
            {
                Thread.Sleep(10);
                _localDictionary[key] = value;
            }
        }

        public ICollection<DateTime> Keys
        {
            get
            {
                Thread.Sleep(10);
                return _localDictionary.Keys;
            }
        }

        public ICollection<string> Values
        {
            get
            {
                Thread.Sleep(10);
                return _localDictionary.Values;
            }
        }

        public int Count
        {
            get
            {
                Thread.Sleep(10);
                return _localDictionary.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                Thread.Sleep(10);
                return _localDictionary.IsReadOnly;
            }
        }

        public void Add(DateTime key, string value)
        {
            Thread.Sleep(10);
            _localDictionary.Add(key, value);
        }

        public void Add(KeyValuePair<DateTime, string> item)
        {
            Thread.Sleep(10);
            _localDictionary.Add(item);
        }

        public void Clear()
        {
            Thread.Sleep(10);
            _localDictionary.Clear();
        }

        public bool Contains(KeyValuePair<DateTime, string> item)
        {
            Thread.Sleep(10);
            return _localDictionary.Contains(item);
        }

        public bool ContainsKey(DateTime key)
        {
            Thread.Sleep(10);
            return _localDictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<DateTime, string>[] array, int arrayIndex)
        {
            Thread.Sleep(10);
            _localDictionary.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<DateTime, string>> GetEnumerator()
        {
            Thread.Sleep(10);
            return _localDictionary.GetEnumerator();
        }

        public bool Remove(DateTime key)
        {
            Thread.Sleep(10);
            return _localDictionary.Remove(key);
        }

        public bool Remove(KeyValuePair<DateTime, string> item)
        {
            Thread.Sleep(10);
            return _localDictionary.Remove(item);
        }

        public bool TryGetValue(DateTime key, out string value)
        {
            Thread.Sleep(10);
            return _localDictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Thread.Sleep(10);
            return _localDictionary.GetEnumerator();
        }
    }
}
