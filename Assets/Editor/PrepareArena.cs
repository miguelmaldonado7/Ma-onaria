using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Maconaria;

[InitializeOnLoad]
public static class PrepareArena
{
    const string ScenePath = "Assets/Scenes/Claustro.unity";
    static PrepareArena() { EditorApplication.delayCall += PrepareIfMissing; }
    static void PrepareIfMissing()
    {
        if (Application.isBatchMode || File.Exists(ScenePath) || EditorApplication.isPlayingOrWillChangePlaymode) return;
        if (EditorApplication.isCompiling || EditorApplication.isUpdating)
        {
            EditorApplication.delayCall += PrepareIfMissing;
            return;
        }
        Create();
    }
    [MenuItem("Maçonaria/Preparar cena inicial")]
    public static void Create()
    {
        if (File.Exists(ScenePath))
        {
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
            if (!Application.isBatchMode && EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                EditorSceneManager.OpenScene(ScenePath);
            return;
        }
        if (!Application.isBatchMode && !EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
        Directory.CreateDirectory("Assets/Scenes");
        Directory.CreateDirectory("Assets/Prefabs");
        Directory.CreateDirectory("Assets/Materials");
        Directory.CreateDirectory("Assets/Textures");
        AssetDatabase.Refresh();
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var stone = Material("Pedra", new Color(.27f, .25f, .25f));
        var dark = Material("Ferro", new Color(.13f, .14f, .17f));
        var robe = Material("Vinho", new Color(.35f, .055f, .09f));
        var bone = Material("Tecido", new Color(.56f, .50f, .4f));
        var gold = Material("Relicário", new Color(.95f, .64f, .18f), true);
        var texture = new Texture2D(64, 64);
        var pixels = new Color[4096];
        for (int y = 0; y < 64; y++) for (int x = 0; x < 64; x++)
        {
            bool seam = y % 16 == 0 || (x + ((y / 16) % 2) * 16) % 32 == 0;
            float shade = seam ? .35f : .78f + ((x * 17 + y * 31) % 19) / 100f;
            pixels[y * 64 + x] = new Color(shade, shade, shade);
        }
        texture.SetPixels(pixels); texture.Apply();
        File.WriteAllBytes("Assets/Textures/Pedra.png", texture.EncodeToPNG());
        Object.DestroyImmediate(texture);
        AssetDatabase.ImportAsset("Assets/Textures/Pedra.png");
        stone.mainTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Textures/Pedra.png");
        stone.mainTextureScale = new Vector2(6, 6);
        Shape("Piso", PrimitiveType.Cube, new Vector3(0, -.5f, 0), new Vector3(40, 1, 40), stone);
        Shape("Muro norte", PrimitiveType.Cube, new Vector3(0, 1, 20), new Vector3(42, 2, 1), stone);
        Shape("Muro sul", PrimitiveType.Cube, new Vector3(0, 1, -20), new Vector3(42, 2, 1), stone);
        Shape("Muro leste", PrimitiveType.Cube, new Vector3(20, 1, 0), new Vector3(1, 2, 40), stone);
        Shape("Muro oeste", PrimitiveType.Cube, new Vector3(-20, 1, 0), new Vector3(1, 2, 40), stone);
        Shape("Altar", PrimitiveType.Cube, new Vector3(0, .75f, 0), new Vector3(3, 1.5f, 2), stone);
        for (int x = -12; x <= 12; x += 8)
        {
            Shape("Pilar", PrimitiveType.Cube, new Vector3(x, 2, 13), new Vector3(1, 4, 1), stone);
            Shape("Arco partido", PrimitiveType.Cube, new Vector3(x + 1, 4, 13), new Vector3(3, .5f, 1), stone).transform.rotation = Quaternion.Euler(0, 0, -20);
            Shape("Lápide", PrimitiveType.Cube, new Vector3(x, .65f, -12), new Vector3(1.1f, 1.3f, .5f), stone);
        }
        var cameraObject = new GameObject("Câmera", typeof(Camera), typeof(AudioListener));
        cameraObject.tag = "MainCamera";
        var camera = cameraObject.GetComponent<Camera>();
        camera.orthographic = true; camera.orthographicSize = 13;
        camera.backgroundColor = new Color(.06f, .035f, .045f);
        camera.clearFlags = CameraClearFlags.SolidColor;
        cameraObject.transform.position = new Vector3(0, 23, -20);
        cameraObject.transform.rotation = Quaternion.Euler(56, 0, 0);
        var light = new GameObject("Luar", typeof(Light)).GetComponent<Light>();
        light.type = LightType.Directional; light.intensity = 1.15f;
        light.color = new Color(.8f, .83f, 1); light.shadows = LightShadows.Soft;
        light.transform.rotation = Quaternion.Euler(55, -30, 0);
        RenderSettings.ambientLight = new Color(.35f, .29f, .34f);
        var arena = new GameObject("Partida", typeof(Arena)).GetComponent<Arena>();
        arena.playerPrefab = Character("Penitente", robe, dark, gold, true);
        arena.enemyPrefab = Character("Devoto", bone, dark, robe, false);
        var bolt = Shape("Projétil", PrimitiveType.Sphere, Vector3.zero, Vector3.one * .25f, gold);
        bolt.GetComponent<Collider>().isTrigger = true;
        var body = bolt.AddComponent<Rigidbody>(); body.useGravity = false;
        body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        bolt.AddComponent<Bolt>();
        var trail = bolt.AddComponent<TrailRenderer>();
        trail.time = .25f; trail.startWidth = .3f; trail.endWidth = 0;
        trail.sharedMaterial = gold;
        arena.boltPrefab = Save(bolt);
        var fragment = Shape("Fragmento", PrimitiveType.Sphere, Vector3.zero, Vector3.one * .55f, gold);
        fragment.GetComponent<Collider>().isTrigger = true;
        fragment.AddComponent<Fragment>();
        var glow = fragment.AddComponent<Light>();
        glow.type = LightType.Point; glow.range = 2; glow.intensity = .7f; glow.color = Color.yellow;
        var halo = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Particle.mat");
        var haloType = typeof(GameObject).Assembly.GetType("UnityEngine.Halo");
        if (haloType != null)
        {
            var component = fragment.AddComponent(haloType);
            var serialized = new SerializedObject(component);
            var size = serialized.FindProperty("m_Size");
            if (size != null) size.floatValue = 1.8f;
            var enabled = serialized.FindProperty("m_Enabled");
            if (enabled != null) enabled.boolValue = true;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }
        arena.fragmentPrefab = Save(fragment);
        var effect = new GameObject("Cinzas");
        var particles = effect.AddComponent<ParticleSystem>();
        particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var main = particles.main; main.loop = false; main.duration = .5f;
        main.startLifetime = .7f; main.startSpeed = 3; main.startSize = .18f; main.playOnAwake = false;
        var emission = particles.emission; emission.rateOverTime = 0;
        emission.SetBursts(new[] { new ParticleSystem.Burst(0, 22) });
        effect.GetComponent<ParticleSystemRenderer>().sharedMaterial = halo;
        arena.effectPrefab = Save(effect);
        EditorSceneManager.SaveScene(scene, ScenePath);
        EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
        PlayerSettings.productName = "Maçonaria";
        PlayerSettings.companyName = "Miguel e Daniel";
        PlayerSettings.defaultScreenWidth = 1280; PlayerSettings.defaultScreenHeight = 720;
        AssetDatabase.SaveAssets();
    }
    static Material Material(string name, Color color, bool emissive = false)
    {
        string path = "Assets/Materials/" + name + ".mat";
        var material = AssetDatabase.LoadAssetAtPath<Material>(path);
        if (material == null)
        {
            material = new Material(Shader.Find("Standard"));
            AssetDatabase.CreateAsset(material, path);
        }
        material.color = color;
        if (emissive) { material.EnableKeyword("_EMISSION"); material.SetColor("_EmissionColor", color * 1.5f); }
        EditorUtility.SetDirty(material);
        return material;
    }
    static GameObject Shape(string name, PrimitiveType type, Vector3 position, Vector3 scale, Material material)
    {
        var obj = GameObject.CreatePrimitive(type); obj.name = name;
        obj.transform.position = position; obj.transform.localScale = scale;
        obj.GetComponent<Renderer>().sharedMaterial = material;
        return obj;
    }
    static GameObject Character(string name, Material clothing, Material iron, Material emblem, bool player)
    {
        var root = new GameObject(name);
        var collider = root.AddComponent<CapsuleCollider>(); collider.height = 2; collider.radius = .45f;
        var body = root.AddComponent<Rigidbody>(); body.useGravity = false;
        body.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        var actor = root.AddComponent<Actor>(); actor.player = player; actor.health = player ? 3 : 2;
        var torso = Shape("Vestes", PrimitiveType.Capsule, Vector3.zero, new Vector3(.8f, .8f, .7f), clothing);
        Object.DestroyImmediate(torso.GetComponent<Collider>()); torso.transform.SetParent(root.transform, false);
        var head = Shape("Máscara", PrimitiveType.Sphere, new Vector3(0, .8f, .12f), new Vector3(.5f, .5f, .5f), iron);
        Object.DestroyImmediate(head.GetComponent<Collider>()); head.transform.SetParent(root.transform, false);
        var relic = Shape("Relicário", PrimitiveType.Cube, new Vector3(0, .2f, .4f), new Vector3(.22f, .3f, .12f), emblem);
        Object.DestroyImmediate(relic.GetComponent<Collider>()); relic.transform.SetParent(root.transform, false);
        if (!player) torso.transform.localRotation = Quaternion.Euler(18, 0, 0);
        return Save(root);
    }
    static GameObject Save(GameObject obj)
    {
        var prefab = PrefabUtility.SaveAsPrefabAsset(obj, "Assets/Prefabs/" + obj.name + ".prefab");
        Object.DestroyImmediate(obj); return prefab;
    }
    public static void BuildWindows()
    {
        Create();
        Directory.CreateDirectory("Builds/Windows");
        var report = BuildPipeline.BuildPlayer(new[] { ScenePath }, "Builds/Windows/Maconaria.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
        if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
            throw new System.Exception("Falha ao gerar versão Windows: " + report.summary.result);
    }
}
