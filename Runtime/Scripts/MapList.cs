using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrandoUniverse.Utilities {

	[Serializable]
	public class MapList<TKey> : HashSet<TKey> {
		
		private bool m_isInit = false;
		public bool isInit { get { return m_isInit; } }
		
		[SerializeField] private List<TKey> m_data;
		public List<TKey> data => m_data;
		
		public MapList() {
			m_data = new List<TKey>();
			Initialize();
		}
		
		public MapList(List<TKey> _data) {
			m_data = _data;
			Initialize();
		}
		
		public MapList(HashSet<TKey> _data) {
			Clear();
			foreach (TKey i in _data) {
				Add(i);
			}
			InternalUpload();
			m_isInit = true;
		}
		
		public void Initialize() {
			Clear();
			foreach (TKey i in m_data) {
				Add(i);
			}
			m_isInit = true;
		}

		public void InternalUpload() {
			m_data = new List<TKey>();
			foreach (TKey i in this) {
				m_data.Add(i);
			}
		}

	}

}