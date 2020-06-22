using System;
using System.Collections.Generic;
using KSP.UI.Screens;
using UnityEngine;

namespace inventory
{
    public class IconLoader
    {
        private const string fallbackIcon = "stockIcon_fallback";
        private static Dictionary<string, RUI.Icons.Selectable.Icon> IconDict = new Dictionary<string, RUI.Icons.Selectable.Icon>();

        public static RUI.Icons.Selectable.Icon GetIcon(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                if (IconDict.TryGetValue(name, out RUI.Icons.Selectable.Icon icon)
                    || PartCategorizer.Instance.iconLoader.iconDictionary.TryGetValue(name, out icon))
                {
                    return icon;
                }
            }
            return PartCategorizer.Instance.iconLoader.iconDictionary[fallbackIcon];
        }

        public static void Load()
        {
            GameDatabase.TextureInfo texInfo = null;
            Texture2D selectedTex = null;
            var texDict = new Dictionary<string, GameDatabase.TextureInfo>();
            for (int i = GameDatabase.Instance.databaseTexture.Count - 1; i >= 0; --i)
            {
                texInfo = GameDatabase.Instance.databaseTexture[i];
                if (texInfo.texture != null && texInfo.texture.width == 32 && texInfo.texture.height == 32)
                {
                    Debug.Log("[INVENTORY] textures");
                    if (!texDict.ContainsKey(texInfo.name)) texDict.Add(texInfo.name, texInfo);
                }
            }

            foreach (KeyValuePair<string, GameDatabase.TextureInfo> kvp in texDict)
            {
                if (kvp.Value.name.Contains("_selected"))
                {
                    continue;
                }

                if (texDict.TryGetValue(kvp.Value.name + "_selected", out texInfo))
                {
                    selectedTex = texInfo.texture;
                }
                else
                {
                    selectedTex = kvp.Value.texture;
                }

                string[] paths = kvp.Value.name.Split(new char[] { '/', '\\' });
                string name = paths[paths.Length - 1];
                var icon = new RUI.Icons.Selectable.Icon(name, kvp.Value.texture, selectedTex, false);
                if (!IconDict.ContainsKey(icon.name)) IconDict.Add(icon.name, icon);
            }

        }

    }


}
