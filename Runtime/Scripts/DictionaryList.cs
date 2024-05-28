using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrandoUniverse.Utilities {

	[Serializable]
	public class DictionaryList<TKey, TValue> {

		[Serializable]
		public class KeyValuePair {
			public TKey key;
			public TValue value;
		}

		private bool m_isInit = false;
		public bool isInit { get { return m_isInit; } }
		[SerializeField] private List<KeyValuePair> m_data;
		private Dictionary<TKey, TValue> m_dataDict;
		public Dictionary<TKey, TValue> dataDict {
			private set { m_dataDict = value; }
			get {
#if UNITY_EDITOR
				if (!m_isInit) {
					Initialize();
				}
#endif
				return m_dataDict;
			}
		}

		public DictionaryList() {
			Clear();
		}
		
		public DictionaryList(Dictionary<TKey,TValue> _dict) {
			m_dataDict = _dict;
			m_isInit = true;
			InternalUpload();
		}
		
		public void Initialize() {
			dataDict = new Dictionary<TKey, TValue>();
			foreach (KeyValuePair i in m_data) {
				dataDict.Add(i.key, i.value);
			}
			m_isInit = true;
		}
		
		public int Count => dataDict.Count;
		
		public bool ContainsKey(TKey _key) {
			return dataDict.ContainsKey(_key);
		}
		
		public void Add(TKey _key, TValue _value) {
			dataDict.Add(_key, _value);
		}
		
		public void Remove(TKey _removeKey) {
			dataDict.Remove(_removeKey);
		}

		public void Clear() { 
			m_data = new List<KeyValuePair>();
			dataDict = new Dictionary<TKey, TValue>();
		}

		public TValue this[TKey _key] {
			get { return dataDict[_key]; }
			set { dataDict[_key] = value; }
		}

		
		public void InternalUpload() {
			m_data = new List<KeyValuePair>();
			foreach (KeyValuePair<TKey, TValue> i in dataDict) {
				m_data.Add(new KeyValuePair { key = i.Key, value = i.Value });
			}
		}

	}

}