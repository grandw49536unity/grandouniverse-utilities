using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrandoUniverse.Utilities {
	
	[Serializable]
	public class TransformInfo {
		public Vector3 localPosition;
		public Quaternion localRotation;
		public TransformInfo(Transform _transform) {
			localPosition = _transform.localPosition;
			localRotation = _transform.localRotation;
		}
	}

	[Serializable]
	public class TransformTree : TreeStructure<string, TransformInfo> {

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
			TreeNode<string, TransformInfo> newNode = new TreeNode<string, TransformInfo>(_childTransform.name, new TransformInfo(_childTransform));
			newNode.parentKey = _parentKey;
			if (_parentKey != "") {
				this[_parentKey].childKeys.Add(newNode.key);
			}
			Add(newNode.key, newNode);
			for (int i = 0; i < _childTransform.childCount; i++) {
				AddTransformRelationship(_childTransform.GetChild(i), newNode.key);
			}
		}

	}

}