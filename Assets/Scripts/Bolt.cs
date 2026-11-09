using UnityEngine;

namespace Maconaria
{
    public sealed class Bolt : MonoBehaviour
    {
        public Vector3 direction;
        bool spent;
        void Start() { Destroy(gameObject, 3); }
        void FixedUpdate()
        {
            var body = GetComponent<Rigidbody>();
            body.velocity = Arena.Current.Playing ? direction * 16 : Vector3.zero;
        }
        void OnTriggerEnter(Collider other)
        {
            if (spent || !Arena.Current.Playing) return;
            var actor = other.GetComponent<Actor>();
            if (actor != null && actor.player) return;
            if (other.isTrigger) return;
            spent = true;
            if (actor != null) actor.Hit(1);
            Destroy(gameObject);
        }
    }
}
