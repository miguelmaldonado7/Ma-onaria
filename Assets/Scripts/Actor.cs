using UnityEngine;

namespace Maconaria
{
    public sealed class Actor : MonoBehaviour
    {
        public bool player;
        public int health = 2;
        public Rigidbody Body { get; private set; }
        float protectedUntil;
        bool dead;
        void Awake() { Body = GetComponent<Rigidbody>(); }
        void FixedUpdate()
        {
            var game = Arena.Current;
            if (game == null || !game.Playing) { Body.velocity = Vector3.zero; return; }
            Vector3 direction;
            if (player)
                direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            else
                direction = (game.Player.transform.position - transform.position).normalized;
            direction.y = 0;
            Body.velocity = direction * (player ? 6f : 2.1f);
            if (direction.sqrMagnitude > .01f) transform.forward = direction;
        }
        public void Hit(int damage)
        {
            if (dead || !Arena.Current.Playing || Time.time < protectedUntil) return;
            health -= damage;
            if (player)
            {
                protectedUntil = Time.time + 1;
                Arena.Current.Burst(transform.position, Color.red);
                Arena.Current.Refresh();
                if (health <= 0) { dead = true; Arena.Current.End(false); }
            }
            else if (health <= 0)
            {
                dead = true;
                Arena.Current.Defeated(this);
                Destroy(gameObject);
            }
        }
        void OnCollisionStay(Collision collision)
        {
            if (player) return;
            var actor = collision.gameObject.GetComponent<Actor>();
            if (actor != null && actor.player) actor.Hit(1);
        }
    }
}
