using Zongband.Utils;

using RLEngine.Yaml.Utils;
using RLEngine.Core.Utils;

using UnityEngine;
using System.Collections.Generic;

namespace Zongband.View.Assets
{
    public class AssetLoader<T1, T2> where T1 : IIdentifiable where T2 : Object
    {
        private readonly string path = SerializationPaths.Get(typeof(T1));
        private readonly Dictionary<string, T2> prefabs = new();
        private T2? defaultPrefab = null;

        public AssetLoader()
        {
            Load();
        }

        private void Load()
        {
            prefabs.Clear();
            foreach (var prefab in Resources.LoadAll<T2>(path))
            {
                if (prefab.name == "Default") defaultPrefab = prefab;
                else prefabs.Add(prefab.name, prefab);
            }
            if (defaultPrefab == null) Debug.LogError(Warnings.NotDefaultAsset(path));
        }

        public T2 Get(T1 identifiable)
        {
            if (prefabs.TryGetValue(identifiable.ID, out var model)) return model;
            Debug.LogWarning(Warnings.AssetNotAvailable(identifiable));
            return defaultPrefab!;
        }
    }
}
