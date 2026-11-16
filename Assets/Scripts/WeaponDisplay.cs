using UnityEngine;
namespace Maconaria
{
    public sealed class WeaponDisplay : MonoBehaviour
    {
        GameObject censer;
        readonly GameObject[] blades = new GameObject[3];
        void Start()
        {
            censer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            censer.name = "Incensário";
            Destroy(censer.GetComponent<Collider>());
            censer.GetComponent<Renderer>().sharedMaterial = Arena.Current.boltPrefab.GetComponent<Renderer>().sharedMaterial;
            censer.transform.SetParent(transform, false);
            censer.transform.localPosition = new Vector3(-.65f, -.25f, .3f);
            censer.transform.localScale = new Vector3(.3f,.45f,.3f);
            for (int i = 0; i < blades.Length; i++)
            {
                blades[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                blades[i].name = "Lâmina ritual";
                Destroy(blades[i].GetComponent<Collider>());
                blades[i].GetComponent<Renderer>().sharedMaterial = Arena.Current.boltPrefab.GetComponent<Renderer>().sharedMaterial;
                blades[i].transform.SetParent(transform, false);
            }
        }
        void Update()
        {
            if (Arena.Current == null || !Arena.Current.Playing) return;
            censer.SetActive(Arena.Current.weapons[1] > 0);
            censer.transform.localPosition = new Vector3(-.65f, -.25f, .3f + Mathf.Sin(Time.time * 3) * .15f);
            censer.transform.localScale = new Vector3(.3f,.45f,.3f) * (Arena.Current.weapons[1] >= 4 ? 1.3f : 1);
            int level = Arena.Current.weapons[2];
            for (int i = 0; i < blades.Length; i++)
            {
                bool active = level > 0 && i < 1 + level / 2;
                blades[i].SetActive(active);
                if (!active) continue;
                float angle = Time.time * 2.6f + i * Mathf.PI * 2 / (1 + level / 2);
                blades[i].transform.position = transform.position + new Vector3(Mathf.Cos(angle),0,Mathf.Sin(angle)) * 2.8f;
                blades[i].transform.rotation = Quaternion.Euler(0, -angle * Mathf.Rad2Deg, 25);
                blades[i].transform.localScale = new Vector3(level >= 4 ? 1.1f : .7f,.12f,.22f);
            }
        }
    }
}
