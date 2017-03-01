using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


namespace com.PancakeTeam
{
    public abstract class DrawableDictionary
    {

    }

    [System.Serializable()]
    public class SerializableDictionary<TKey, TValue, TKeyValuePair> : 
            DrawableDictionary, IDictionary<TKey, TValue>, UnityEngine.ISerializationCallbackReceiver
            where TKeyValuePair : SerializableKeyValuePair<TKey, TValue>, new()
    {

        #region Fields

        //TODO: Fix the hack
        [System.NonSerialized()]
        private Dictionary<TKey, TValue> _dict = new Dictionary<TKey, TValue>();

        #endregion

        #region IDictionary Interface

        public int Count
        {
            get { return (_dict != null) ? _dict.Count : 0; }
        }

        public void Add(TKey key, TValue value)
        {
            if (_dict == null) _dict = new Dictionary<TKey, TValue>();
            _dict.Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            if (_dict == null) return false;
            return _dict.ContainsKey(key);
        }

        public ICollection<TKey> Keys
        {
            get
            {
                if (_dict == null) _dict = new Dictionary<TKey, TValue>();
                return _dict.Keys;
            }
        }

        public bool Remove(TKey key)
        {
            if (_dict == null) return false;
            return _dict.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_dict == null)
            {
                value = default(TValue);
                return false;
            }
            return _dict.TryGetValue(key, out value);
        }

        public ICollection<TValue> Values
        {
            get
            {
                if (_dict == null) _dict = new Dictionary<TKey, TValue>();
                return _dict.Values;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                if (_dict == null) throw new KeyNotFoundException();
                return _dict[key];
            }
            set
            {
                if (_dict == null) _dict = new Dictionary<TKey, TValue>();
                _dict[key] = value;
            }
        }

        public void Clear()
        {
            if (_dict != null) _dict.Clear();
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            if (_dict == null) _dict = new Dictionary<TKey, TValue>();
            (_dict as ICollection<KeyValuePair<TKey, TValue>>).Add(item);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            if (_dict == null) return false;
            return (_dict as ICollection<KeyValuePair<TKey, TValue>>).Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (_dict == null) return;
            (_dict as ICollection<KeyValuePair<TKey, TValue>>).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            if (_dict == null) return false;
            return (_dict as ICollection<KeyValuePair<TKey, TValue>>).Remove(item);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return false; }
        }

        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            if (_dict == null) return default(Dictionary<TKey, TValue>.Enumerator);
            return _dict.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            if (_dict == null) return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
            return _dict.GetEnumerator();
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            if (_dict == null) return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
            return _dict.GetEnumerator();
        }

        #endregion

        #region ISerializationCallbackReceiver

        [UnityEngine.SerializeField()]
        private TKeyValuePair[] _elements;

        int _count;
        //[UnityEngine.SerializeField()]
        //private TValue[] _values;

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            //if (_keys != null && _values != null)
            //{
            if (_elements != null)
            {
                //if (_dict == null) _dict = new Dictionary<TKey, TValue>();
                //else _dict.Clear();
                _dict.Clear();
                for (int i = 0; i < _elements.Length; i++)
                {
                    if (_elements[i].Key != null) {
                        _dict[_elements[i].Key] = _elements[i].Value;
                    }
                }
                _count = _elements.Length;
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            //if (_dict != null && _dict.Count > 0) {
            if (_dict.Count > 0)
            {
                _elements = new TKeyValuePair[_count];
                Dictionary<TKey, TValue>.Enumerator e = _dict.GetEnumerator();
                int i = 0;
                while (e.MoveNext())
                {
                    TKeyValuePair element = new TKeyValuePair();
                    element.Key = e.Current.Key;
                    element.Value = e.Current.Value;
                    _elements[i] = element;
                    ++i;
                }
                if (i < _count - 1)
                {
                    for (int j = i; j < _count; ++j)
                    {
                        _elements[j] = new TKeyValuePair();
                    }
                }
            }
        }

#endregion

    }

    [System.Serializable()]
    public class SerializableKeyValuePair<TKey, TValue>
    {
        [SerializeField]
        TKey _key;

        [SerializeField]
        TValue _value;

        public SerializableKeyValuePair() { }

        public SerializableKeyValuePair(TKey key, TValue value)
        {
            _key = key;
            _value = value;
        }

        public TKey Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        public TValue Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }
    }
}