using UnityEngine;
namespace Maconaria
{
    public sealed class FigureMotion : MonoBehaviour
    {
        Actor actor;
        Transform[] parts;
        Quaternion[] rest;
        void Start()
        {
            actor = GetComponent<Actor>();
            parts = new Transform[transform.childCount];
            rest = new Quaternion[parts.Length];
            for (int i = 0; i < parts.Length; i++) { parts[i] = transform.GetChild(i); rest[i] = parts[i].localRotation; }
        }
        void Update()
        {
            if (!Arena.Current.Playing) return;
            float swing = actor.Body.velocity.sqrMagnitude > .1f ? Mathf.Sin(Time.time * 8) * 12 : 0;
            for (int i = 0; i < parts.Length; i++)
                if (parts[i].name == "Manga") parts[i].localRotation = rest[i] * Quaternion.Euler(swing * (i % 2 == 0 ? 1 : -1), 0, 0);
        }
    }
}
