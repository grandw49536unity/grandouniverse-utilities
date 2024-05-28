using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrandoUniverse.Utilities {

	[Serializable]
	public class TreeStructure<TKey, TValue> {
		
		[Serializable]
		public class TreeNode {
			public TKey key;
			public TValue value;
			public TKey parentKey;
			public List<TKey> childKeys;
			public TreeNode(TKey _key, TValue _value) {
				key = _key;
				value = _value;
				childKeys = new List<TKey>();
			}
		}

		private bool m_isInit = false;
		public bool isInit { get { return m_isInit; } }
		public TKey rootKey;
		[SerializeField] private List<TreeNode> m_nodes;
		private Dictionary<TKey, TreeNode> m_nodeDict;
		public Dictionary<TKey, TreeNode> nodeDict { 
			private set {
				m_nodeDict = value; 
			} get {
#if UNITY_EDITOR
				if (!m_isInit) {
					Initialize();
				}
#endif
				return m_nodeDict; 
			} 
		}
		
		public TreeStructure() {
			Clear();
		}

		public void Initialize() {
			nodeDict = new Dictionary<TKey, TreeNode>();
			foreach (TreeNode i in m_nodes) {
				nodeDict.Add(i.key, i);
			}
			m_isInit = true;
		}
		
		public int Count { get { return nodeDict.Count; } }
	
		public bool ContainsKey(TKey _key) {
			return nodeDict.ContainsKey(_key);
		}
		
		public void Add(TreeNode _newTreeNode) {
			nodeDict.Add(_newTreeNode.key, _newTreeNode);
		}
		
		public void Remove(TKey _removeKey) {
			nodeDict.Remove(_removeKey);
		}

		public void Clear() { 
			m_nodes = new List<TreeNode>();
			nodeDict = new Dictionary<TKey, TreeNode>();
		}

		public TreeNode this[TKey _key] {
			get { return nodeDict[_key]; }
			set { nodeDict[_key] = value; }
		}

		public void DefineParentChild(TKey _parentKey, TKey _childKey) {
			nodeDict[_childKey].parentKey = _parentKey;
			nodeDict[_parentKey].childKeys.Add(_childKey);
		}
		
		public void InsertTreeAt(TreeStructure<TKey, TValue> _insertTree, TKey _insertKey, TKey _parentKey) {
			Dictionary<TKey, TreeNode> insertTreeDict = _insertTree.m_nodeDict;
			SearchAndAdd(insertTreeDict[_insertKey]);
			nodeDict[_insertKey].parentKey = _parentKey;
			nodeDict[_parentKey].childKeys.Add(_insertKey);
			void SearchAndAdd(TreeNode _node) {
				Add(_node);
				foreach (TKey k in _node.childKeys) {
					SearchAndAdd(insertTreeDict[k]);
				}
			}
		}
		
		public void RemoveTreeAt(TKey _removeKey) {
			TreeNode removeTreeNode = nodeDict[_removeKey];
			SearchAndRemove(nodeDict[_removeKey]);
			void SearchAndRemove(TreeNode _node) {
				nodeDict.Remove(_node.key);
				foreach (TKey i in _node.childKeys) {
					SearchAndRemove(nodeDict[i]);
				}
			}
			TreeNode parentTreeNode = nodeDict[removeTreeNode.parentKey];
			parentTreeNode.childKeys.Remove(_removeKey);
		}
		
		public TreeStructure<TKey, TValue> SeparateTreeAt(TKey _separateKey) {
			TreeStructure<TKey, TValue> separateTree = new TreeStructure<TKey, TValue>();
			SearchAndAdd(nodeDict[_separateKey]);
			void SearchAndAdd(TreeNode _node) {
				separateTree.Add(_node);
				foreach (TKey i in _node.childKeys) {
					SearchAndAdd(nodeDict[i]);
				}
			}
			RemoveTreeAt(_separateKey);
			separateTree.InternalUpload();
			return separateTree;
		}
		
		public void InternalUpload() {
			m_nodes = new List<TreeNode>();
			foreach (KeyValuePair<TKey, TreeNode> i in nodeDict) {
				m_nodes.Add(i.Value);
			}
		}

	}

}