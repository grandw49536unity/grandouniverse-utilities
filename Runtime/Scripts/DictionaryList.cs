using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrandoUniverse.Utilities {

	[Serializable]
	public class DictionaryList<TKey, TValue> : Dictionary<TKey, TValue> {

		[Serializable]
		public class KeyValuePair {
			public TKey key;
			public TValue value;
		}

		private bool m_isInit = false;
		public bool isInit { get { return m_isInit; } }
		
		[SerializeField] private List<KeyValuePair> m_data;
		public List<KeyValuePair> data => m_data;
		
		public DictionaryList() {
			m_data = new List<KeyValuePair>();
			Clear();
		}
		
		public DictionaryList(Dictionary<TKey,TValue> _dict) {
			Clear();
			m_isInit = true;
			InternalUpload();
		}
		
		public new TValue this[TKey _key] {
			get {
#if UNITY_EDITOR
				if (!isInit) {
					Initialize();
				}
#endif
				return base[_key]; 
			}
			set {
#if UNITY_EDITOR
				if (!isInit) {
					Initialize();
				}
#endif
				base[_key] = value; 
			}
		}
		
		public void Initialize() {
			Clear();
			foreach (KeyValuePair i in m_data) {
				Add(i.key, i.value);
			}
			m_isInit = true;
		}

		public void InternalUpload() {
			m_data = new List<KeyValuePair>();
			foreach (KeyValuePair<TKey, TValue> i in this) {
				m_data.Add(new KeyValuePair { key = i.Key, value = i.Value });
			}
		}

	}

}