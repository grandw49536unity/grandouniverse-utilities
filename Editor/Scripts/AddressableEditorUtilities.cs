using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;

namespace GrandoUniverse.Editor.Utilities {

	public static class AddressableEditorUtilities {

		public static void DeleteAddressableGroup(string _groupName) {
			AddressableAssetSettings addressableSetting = AddressableAssetSettingsDefaultObject.Settings;
			AddressableAssetGroup textureAssetGroup = addressableSetting.FindGroup(_groupName);
			if (textureAssetGroup) {
				textureAssetGroup.ClearSchemas(true);
				addressableSetting.RemoveGroup(textureAssetGroup);
			}
		}

		public static AddressableAssetGroup AddAddressableGroup(string _groupName) {
			AddressableAssetSettings addressableSetting = AddressableAssetSettingsDefaultObject.Settings;
			return addressableSetting.FindGroup(_groupName) ?? addressableSetting.CreateGroup(_groupName, false, false, true, null, typeof(ContentUpdateGroupSchema), typeof(BundledAssetGroupSchema));
		}

		public static AddressableAssetGroup ClearAddressableGroup(string _groupName) {
			DeleteAddressableGroup(_groupName);
			return AddAddressableGroup(_groupName);
		}

		public static void AddAssetToAddressableGroup(Object _object, string _assetKey, AddressableAssetGroup _group) {
			AddressableAssetSettings addressableSetting = AddressableAssetSettingsDefaultObject.Settings;
			string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(_object));
			AddressableAssetEntry entry = addressableSetting.CreateOrMoveEntry(guid, _group);
			entry.address = _assetKey;
		}

	}

}