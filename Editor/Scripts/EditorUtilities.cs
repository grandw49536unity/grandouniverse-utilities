using System;
using UnityEngine;
using UnityEditor;

namespace GrandoUniverse.Editor.Utilities {
	
	public static class EditorUtilities {
		
		public static void CleanFolder(string _folderPath) {
			if (AssetDatabase.IsValidFolder(_folderPath)) {
				AssetDatabase.DeleteAsset(_folderPath);
			}
			CreateFolder(_folderPath);
		}

		public static void CreateFolder(string _folderPath) {
			string[] subPaths = GetFolderSubPaths(_folderPath);
			string path = subPaths[0];
			for (int i = 1; i < subPaths.Length; i++) {
				path += $"/{subPaths[i]}";
				if (!AssetDatabase.IsValidFolder(path)) {
					string parentFolder = System.IO.Path.GetDirectoryName(path);
					string newFolderName = System.IO.Path.GetFileName(path);
					AssetDatabase.CreateFolder(parentFolder, newFolderName);
					AssetDatabase.Refresh();
				}
			}
		}

		public static string[] GetFolderSubPaths(string _folderPath) {
			return _folderPath.Split("/");
		}

		public static string GetAssetPath(string _realPath) { 
			return _realPath.Replace(Application.dataPath, "Assets"); 
		}

		public static T[] LoadAllDataFromFolder<T>(string _folderPath, string _filter = "") where T : UnityEngine.Object {
			string[] identities = AssetDatabase.FindAssets(_filter, new[] { _folderPath });
			int count = identities.Length;
			for (int i = 0; i < count; i++) identities[i] = AssetDatabase.GUIDToAssetPath(identities[i]);
			T[] assets = new T [count];
			for (int i = 0; i < count; i++) assets[i] = AssetDatabase.LoadAssetAtPath<T>(identities[i]);
			return assets;
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