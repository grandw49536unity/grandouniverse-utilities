using System;
using System.Linq;
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
#if UNITY_EDITOR
		private Dictionary<TKey, TValue> m_dataDict;
		public Dictionary<TKey, TValue> dataDict { 
			private set {
				m_dataDict = value; 
			} get {
				if (!m_isInit) {
					Initialize();
				}
				return m_dataDict; 
			} 
		}
#else
		public Dictionary<TKey, TValue> dataDict { private set; get; }
#endif
		
		public DictionaryList() {
			Clear();
		}
		
		public void Initialize() {
			dataDict = m_data.ToDictionary(val => val.key, val => val.value);
			m_isInit = true;
		}
		
		public int Count { get { return dataDict.Count; } }
	
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
			foreach (KeyValuePair<TKey, TValue> data in dataDict) {
				m_data = dataDict.Select((KeyValuePair<TKey, TValue> val) => new KeyValuePair { key = val.Key, value = val.Value }).ToList();
			}
		}

	}

}