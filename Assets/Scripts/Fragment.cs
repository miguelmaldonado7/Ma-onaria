using UnityEngine;

namespace Maconaria
{
    public sealed class Fragment : MonoBehaviour
    {
        bool collected;
        void Update() { if (Arena.Current.Playing) transform.Rotate(0, 70 * Time.deltaTime, 0); }
        void OnTriggerEnter(Collider other)
        {
            var actor = other.GetComponent<Actor>();
            if (collected || !Arena.Current.Playing || actor == null || !actor.player) return;
            collected = true;
            Arena.Current.Collect();
            Destroy(gameObject);
        }
    }
}
