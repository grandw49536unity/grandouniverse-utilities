using System;
using System.Linq;
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
		[SerializeField] private List<KeyValuePair> m_data;
		
		public DictionaryList() {
			m_data = new List<KeyValuePair>();
		}
		
		public void Initialize() {
			Clear();
			foreach (KeyValuePair data in m_data) {
				Add(data.key, data.value);
			}
			m_isInit = true;
		}
		
		public void InternalUpload() {
			m_data = new List<KeyValuePair>();
			foreach (KeyValuePair<TKey, TValue> data in this) {
				m_data.Add(new KeyValuePair{key = data.Key, value = data.Value});
			}
		}

	}

}