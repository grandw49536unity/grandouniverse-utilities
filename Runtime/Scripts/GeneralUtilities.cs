using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrandoUniverse.Utilities {

	public static class GeneralUtilities {

		public static void Destroy(UnityEngine.Object _object, bool _allowDestroyingAssets = false) {
#if UNITY_EDITOR
			if (!Application.isPlaying) {
				UnityEngine.Object.DestroyImmediate(_object, _allowDestroyingAssets);
			} else {
#endif
				UnityEngine.Object.Destroy(_object);
#if UNITY_EDITOR
			}
#endif
		}

		public static bool IsDiff(this float _a, float _b) { return Mathf.Abs(_a - _b) >= float.Epsilon; }

		public static bool IsEquals(this float _a, float _b) { return Mathf.Abs(_a - _b) < float.Epsilon; }

		public static Vector2 Clamp01(this Vector2 _vec) { return new Vector2(Mathf.Clamp01(_vec.x), Mathf.Clamp01(_vec.y)); }

		public static Vector3 Clamp01(this Vector3 _vec) { return new Vector3(Mathf.Clamp01(_vec.x), Mathf.Clamp01(_vec.y), Mathf.Clamp01(_vec.z)); }

		public static Vector4 Clamp01(this Vector4 _vec) { return new Vector4(Mathf.Clamp01(_vec.x), Mathf.Clamp01(_vec.y), Mathf.Clamp01(_vec.z), Mathf.Clamp01(_vec.w)); }

		public static Vector3 Multiple(this Vector3 _a, Vector3 _b) { return new Vector3(_a.x * _b.x, _a.y * _b.y, _a.z * _b.z); }
		
		public static Vector3 Divide(this Vector3 _a, Vector3 _b) { return new Vector3(_a.x / _b.x, _a.y / _b.y, _a.z / _b.z); }

		public static GameObject CreateGameObject(string _name, Transform _parent, Vector3 _position, bool _is_global_position, Quaternion _rotation, bool _is_global_rotation) {
			GameObject obj_new = new GameObject(_name);
			Transform node_obj_new = obj_new.transform;
			node_obj_new.SetParent(_parent);
			if (_is_global_position && _is_global_rotation)
				node_obj_new.SetPositionAndRotation(_position, _rotation);
			else if (!_is_global_position && !_is_global_rotation)
				node_obj_new.SetLocalPositionAndRotation(_position, _rotation);
			else {
				if (_is_global_position)
					node_obj_new.position = _position;
				else
					node_obj_new.localPosition = _position;
				if (_is_global_rotation)
					node_obj_new.rotation = _rotation;
				else
					node_obj_new.localRotation = _rotation;
			}
			return obj_new;
		}

		public static GameObject CreateGameObject(string _name, Transform _parent = null) { return CreateGameObject(_name, _parent, Vector3.zero, false, Quaternion.identity, false); }

		public static T CreateGameObjectWithComponent<T>(string _name, Transform _parent, Vector3 _position, bool _is_global_position, Quaternion _rotation, bool _is_global_rotation) where T : UnityEngine.Component {
			GameObject newObj = CreateGameObject(_name, _parent, _position, _is_global_position, _rotation, _is_global_rotation);
			return newObj.AddComponent<T>();
		}

		public static T CreateGameObjectWithComponent<T>(string _name, Transform _parent = null) where T : UnityEngine.Component {
			GameObject newObj = CreateGameObject(_name, _parent, Vector3.zero, false, Quaternion.identity, false);
			return newObj.AddComponent<T>();
		}

	}

}