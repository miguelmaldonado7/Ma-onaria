using System.IO;
using UnityEditor;
using UnityEngine;

public static class PresentationMaterials
{
    [MenuItem("Maçonaria/Atualizar materiais e texturas")]
    public static void Refresh()
    {
        if (UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline != null)
        {
            Debug.LogError("Este projeto usa Built-in. Remova o Render Pipeline Asset em Graphics e Quality antes de atualizar os materiais.");
            return;
        }
        Directory.CreateDirectory("Assets/Textures");
        AssetDatabase.Refresh();
        foreach (string name in new[] { "Pedra", "Vinho", "Tecido", "Musgo", "Ferro" })
        {
            var material = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/" + name + ".mat");
            if (material == null) continue;
            var texture = new Texture2D(256, 256, TextureFormat.RGBA32, false);
            Color baseColor = name == "Vinho" ? new Color(.38f,.08f,.12f) : name == "Musgo" ? new Color(.25f,.32f,.18f) : name == "Tecido" ? new Color(.5f,.44f,.32f) : new Color(.4f,.4f,.42f);
            for (int y = 0; y < 256; y++) for (int x = 0; x < 256; x++)
            {
                float noise = Mathf.PerlinNoise(x * .13f,y * .13f);
                bool mortar = name == "Pedra" && (y % 64 < 3 || (x + (y / 64 % 2) * 64) % 128 < 3);
                float factor = mortar ? .4f : .8f + noise * .4f;
                if (name == "Tecido" || name == "Vinho") factor *= ((x + y) % 3 == 0 ? .85f : 1);
                texture.SetPixel(x,y,baseColor * factor);
            }
            texture.Apply();
            string path = "Assets/Textures/" + name + "Detalhe.png";
            File.WriteAllBytes(path, texture.EncodeToPNG()); Object.DestroyImmediate(texture);
            AssetDatabase.ImportAsset(path);
            var importer = (TextureImporter)AssetImporter.GetAtPath(path);
            importer.wrapMode = TextureWrapMode.Repeat;
            importer.filterMode = FilterMode.Trilinear;
            importer.anisoLevel = 4; importer.mipmapEnabled = true; importer.SaveAndReimport();
            material.shader = Shader.Find("Standard");
            material.color = Color.white;
            material.mainTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            material.mainTextureScale = name == "Pedra" ? new Vector2(8,8) : new Vector2(2,2);
            material.SetFloat("_Glossiness", name == "Ferro" ? .4f : .08f);
            EditorUtility.SetDirty(material);
        }
        AssetDatabase.SaveAssets();
    }
}
