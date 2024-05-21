using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrandoUniverse.Utilities {

	[Serializable]
	public class TreeStruct<TKey, TValue> {

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

		private bool m_isInit;
		public TKey rootKey;
		public List<TreeNode> nodes;
#if UNITY_EDITOR
		private Dictionary<TKey, TreeNode> m_nodeDict;
		public Dictionary<TKey, TreeNode> nodeDict { 
			private set {
				m_nodeDict = value; 
			} get {
				if (!m_isInit) {
					Initialize();
				}
				return m_nodeDict; 
			} 
		}
#else
		public Dictionary<TKey, TreeNode> nodeDict { private set; get; }
#endif
		
		public TreeStruct() {
			nodes = new List<TreeNode>();
		}

		public void Initialize() {
			nodeDict = nodes.ToDictionary((TreeNode val) => val.key, (TreeNode val) => val);
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
		
		public void DefineParentChild(TKey _parentKey, TKey _childKey) {
			nodeDict[_childKey].parentKey = _parentKey;
			nodeDict[_parentKey].childKeys.Add(_childKey);
		}
		
		public void InsertTreeAt(TreeStruct<TKey, TValue> _tree, TKey _insertKey, TKey _parentKey) {
			Dictionary<TKey, TreeNode> insertTreeDict = _tree.nodeDict;
			SearchAndAdd(insertTreeDict[_insertKey]);
			nodeDict[_insertKey].parentKey = _parentKey;
			nodeDict[_parentKey].childKeys.Add(_insertKey);
			void SearchAndAdd(TreeNode _node) {
				Add(_node);
				foreach (TKey i in _node.childKeys) {
					SearchAndAdd(insertTreeDict[i]);
				}
			}
		}
		
		public void RemoveTreeAt(TKey _removeKey) {
			TreeNode removeTreeNode = nodeDict[_removeKey];
			SearchAndRemove(nodeDict[_removeKey]);
			void SearchAndRemove(TreeNode _node) {
				Remove(_node.key);
				foreach (TKey i in _node.childKeys) {
					SearchAndRemove(nodeDict[i]);
				}
			}
			TreeNode parentTreeNode = nodeDict[removeTreeNode.parentKey];
			parentTreeNode.childKeys.Remove(_removeKey);
		}
		
		public TreeStruct<TKey, TValue> SeparateTreeAt(TKey _separateKey) {
			TreeStruct<TKey, TValue> separateTree = new TreeStruct<TKey, TValue>();
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
		
		public TreeNode this[TKey _key] {
			get { return nodeDict[_key]; }
			set { nodeDict[_key] = value; }
		}
		
		public void InternalUpload() {
			nodes = nodeDict.Select((KeyValuePair<TKey, TreeNode> val) => val.Value).ToList();
		}

	}

}