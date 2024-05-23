using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrandoUniverse.Utilities {

	[Serializable]
	public class TransformTree : TreeStructure<string, Transform> {

		public TransformTree(Transform _rootTransform) {
			SetupTransformTree(_rootTransform);
		}

		public void SetupTransformTree(Transform _rootTransform) {
			Clear();
			rootKey = _rootTransform.name;
			AddTransformRelationship(_rootTransform);
			InternalUpload();
		}
		
		void AddTransformRelationship(Transform _childTransform, string _parentKey = "") {
			TreeNode newNode = new TreeNode(_childTransform.name, _childTransform) { parentKey = _parentKey };
			if (_parentKey != "") {
				this[_parentKey].childKeys.Add(newNode.key);
			}
			Add(newNode);
			for (int i = 0; i < _childTransform.childCount; i++) {
				AddTransformRelationship(_childTransform.GetChild(i), newNode.key);
			}
		}

	}

}