using UnityEngine;
namespace Maconaria
{
    public sealed class WeaponDisplay : MonoBehaviour
    {
        readonly GameObject[] blades = new GameObject[3];
        void Start()
        {
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
