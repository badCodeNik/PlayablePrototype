using System;
using System.Threading.Tasks;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Source.Scripts.ProjectLouders
{
    public static class ProjectLoader
    {
        public const string GameConfigsPath = "Assets/Source/Data/GameConfiguration.asset";
        public const string BuildSetupPath = "Assets/Source/Data/BuildSetup.asset";
        public const string DataPath = "Assets/Source/Data/";
        
        private static readonly ILoader Loader = new ResourceLoader();
        
        public static T LoadResource<T>(string path) where T : Object
        {
            return Loader.ResourceLoad<T>(path);
        }
        
        public static void LoadResourceAsync<T>(string path, Action<T> onSuccess, Action<string> onFalse) where T : Object
        {
            Loader.ResourceLoadAsync(path, onSuccess, onFalse);
        } 
        
        public static async Task<T> LoadResourceAsync<T>(string path) where T : Object
        {
            var task = Loader.ResourceLoadAsync<T>(path);
            await task;
            return task.Result;
        } 
        
        /// <summary>
        ///  Работает ТОЛЬКО в Unity Editor. Загружает дату по пути Assets/Source/Data/ по умолчанию.
        /// </summary>
        public static T GetAssetByTypeOnValidate<T>(string path = DataPath) where T : Object
        {
            T asset = null;
#if UNITY_EDITOR
            asset = AssetDatabase.LoadAssetAtPath<T>($"{path}/{typeof(T).Name}.asset");
            
            if (asset == null)
            {
                var allAssets = AssetDatabase.LoadAllAssetsAtPath(path);
                foreach (var lookingObject in allAssets)
                {
                    if (lookingObject is T foundObject)
                    {
                        asset = foundObject;
                        break;
                    }
                }
            }
            
            if (asset == null)
            {
                string[] assetGuids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
                foreach (var guid in assetGuids)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                    asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                    if (asset != null)
                    {
                        break;
                    }
                }
            }
#endif
            return asset;
        }
    }
        
    public class ResourceLoadResult<T> where T : Object
    {
        public bool Success { get; }
        public T Asset { get; }

        public ResourceLoadResult(bool success, T asset)
        {
            Success = success;
            Asset = asset;
        }
    }  
}