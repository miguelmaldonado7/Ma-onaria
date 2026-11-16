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
            if (dead || game == null || !game.Playing) { Body.velocity = Vector3.zero; return; }
            Vector3 direction;
            if (player)
            {
                float horizontal = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ? 1 : 0)
                    - (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ? 1 : 0);
                float vertical = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ? 1 : 0)
                    - (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ? 1 : 0);
                direction = new Vector3(horizontal, 0, vertical).normalized;
            }
            else
                direction = (game.Player.transform.position - transform.position).normalized;
            direction.y = 0;
            if (!player)
            {
                for (int attempt = 0; attempt < 6; attempt++)
                {
                    float offset = attempt == 0 ? 0 : ((attempt + 1) / 2) * 35 * (attempt % 2 == 0 ? -1 : 1);
                    Vector3 candidate = Quaternion.Euler(0, offset, 0) * direction;
                    if (!Physics.SphereCast(transform.position + candidate * .6f, .3f, candidate, out RaycastHit obstacle, 1.2f, ~0, QueryTriggerInteraction.Ignore)
                        || obstacle.collider.GetComponent<Actor>() != null)
                    { direction = candidate; break; }
                }
            }
            Body.velocity = direction * (player ? 6f : 2.1f);
            if (direction.sqrMagnitude > .01f) transform.forward = direction;
        }
        public void Hit(int damage)
        {
            if (dead || !Arena.Current.Playing || Time.time < protectedUntil) return;
            health -= damage;
            var motion = GetComponent<FigureMotion>();
            if (motion != null) motion.enabled = false;
            StopAllCoroutines();
            StartCoroutine(React(health <= 0));
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
                GetComponent<Collider>().enabled = false;
                Body.velocity = Vector3.zero;
            }
        }
        System.Collections.IEnumerator React(bool dying)
        {
            float t = 0;
            Quaternion initial = transform.rotation;
            while (t < (dying ? .45f : .15f))
            {
                t += Time.deltaTime;
                transform.rotation = initial * Quaternion.Euler(dying ? Mathf.Min(85, t * 190) : Mathf.Sin(t * 20) * -12, 0, 0);
                yield return null;
            }
            if (dying && !player) Destroy(gameObject);
            else
            {
                transform.rotation = initial;
                var motion = GetComponent<FigureMotion>();
                if (motion != null) motion.enabled = true;
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
