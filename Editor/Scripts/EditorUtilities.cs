using System;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace GrandoUniverse.Editor.Utilities {
	
	public static class EditorUtilities {

		public static string GetAssetPath(string _realPath) { 
			return _realPath.Replace(Application.dataPath, "Assets"); 
		}

		public static T[] LoadAllDataFromFolder<T>(string _folderPath, string _filter = "") where T : UnityEngine.Object {
			return AssetDatabase.FindAssets(_filter, new[] { _folderPath }).Select(val => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(val))).ToArray(); 
		}
		
		public static Texture2D[] LoadAllTexture2DsFromFolder(string _folderPath) {
			return LoadAllDataFromFolder<Texture2D>(_folderPath, "t:Texture2D");
		}
		
		public static Material[] LoadAllMaterialsFromFolder(string _folderPath) {
			return LoadAllDataFromFolder<Material>(_folderPath, "t:Material");
		}
		
		public static GameObject[] LoadAllModelsFromFolder(string _folderPath) {
			return LoadAllDataFromFolder<GameObject>(_folderPath, "t:model");
		}
		
	}

}