using UnityEngine;
using UnityEditor;
using System.IO;

public static class RitualModels
{
    [MenuItem("Maçonaria/Atualizar vestes dos personagens")]
    public static void UpdateModels()
    {
        Directory.CreateDirectory("Assets/Models"); AssetDatabase.Refresh();
        foreach (string name in new[] { "Penitente", "Devoto" })
        {
            string path = "Assets/Prefabs/" + name + ".prefab";
            if (AssetDatabase.LoadAssetAtPath<GameObject>(path) == null) continue;
            var root = PrefabUtility.LoadPrefabContents(path);
            try
            {
                var torso = root.transform.Find("Vestes");
                if (torso == null) continue;
                string meshPath = "Assets/Models/" + name + "Manto.asset";
                var mesh = AssetDatabase.LoadAssetAtPath<Mesh>(meshPath);
                if (mesh == null) { mesh = new Mesh(); AssetDatabase.CreateAsset(mesh, meshPath); }
                mesh.Clear();
                const int segments = 32;
                float[] height = { -1.1f, -.85f, -.3f, .25f, .55f };
                float[] radius = { .68f, .62f, .4f, .48f, .22f };
                var vertices = new Vector3[5 * (segments + 1)];
                var uv = new Vector2[vertices.Length];
                var triangles = new int[4 * segments * 6];
                int index = 0;
                for (int y = 0; y < 5; y++) for (int x = 0; x <= segments; x++)
                {
                    float angle = x * Mathf.PI * 2 / segments;
                    float fold = 1 + Mathf.Cos(angle * 8) * .09f;
                    int v = y * (segments + 1) + x;
                    vertices[v] = new Vector3(Mathf.Cos(angle) * radius[y] * fold, height[y] + (y == 0 ? Mathf.Sin(angle * 7) * .05f : 0), Mathf.Sin(angle) * radius[y] * fold);
                    uv[v] = new Vector2((float)x / segments,(float)y / 4);
                    if (y < 4 && x < segments)
                    {
                        triangles[index++] = v; triangles[index++] = v + segments + 1; triangles[index++] = v + 1;
                        triangles[index++] = v + 1; triangles[index++] = v + segments + 1; triangles[index++] = v + segments + 2;
                    }
                }
                mesh.vertices = vertices; mesh.uv = uv; mesh.triangles = triangles; mesh.RecalculateNormals(); mesh.RecalculateBounds();
                EditorUtility.SetDirty(mesh);
                torso.GetComponent<MeshFilter>().sharedMesh = mesh;
                PrefabUtility.SaveAsPrefabAsset(root,path);
            }
            finally { PrefabUtility.UnloadPrefabContents(root); }
        }
        AssetDatabase.SaveAssets();
    }
}
