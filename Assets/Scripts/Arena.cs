using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Maconaria
{
    public sealed class Arena : MonoBehaviour
    {
        public static Arena Current { get; private set; }
        public GameObject playerPrefab, enemyPrefab, boltPrefab, fragmentPrefab, effectPrefab;
        public Actor Player { get; private set; }
        public bool Playing { get; private set; }
        public event Action Changed;
        readonly List<Actor> enemies = new List<Actor>();
        float remaining = 180, spawnAt, fireAt, noticeUntil, animationUntil;
        int score, experience, level = 1;
        Text hud, notice, result;
        Image animationImage;
        Sprite[] frames;
        Camera view;
        Font font;
        void Awake() { Current = this; }
        void Start()
        {
            Player = Instantiate(playerPrefab, new Vector3(0, 1, -5), Quaternion.identity).GetComponent<Actor>();
            view = Camera.main;
            MakeInterface();
            Changed += RenderHud;
            Playing = true;
            notice.text = "WASD / SETAS PARA MOVER • ATAQUE AUTOMÁTICO\nSobreviva à vigília por três minutos";
            noticeUntil = Time.time + 7;
            Refresh();
        }
        void Update()
        {
            if (!Playing)
            {
                if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }
            remaining = Mathf.Max(0, remaining - Time.deltaTime);
            if (Time.time >= spawnAt)
            {
                Spawn();
                spawnAt = Time.time + Mathf.Lerp(1.8f, .45f, 1 - remaining / 180);
            }
            if (Time.time >= fireAt) Fire();
            if (Time.time > noticeUntil) notice.text = "";
            animationImage.enabled = Time.time < animationUntil;
            if (animationImage.enabled) animationImage.sprite = frames[(int)(Time.time * 12) % frames.Length];
            Refresh();
        }
        void LateUpdate()
        {
            if (Player == null) return;
            view.transform.position = Player.transform.position + new Vector3(0, 22, -15);
            view.transform.rotation = Quaternion.Euler(56, 0, 0);
            if (Playing && remaining <= 0) End(Player.health > 0);
        }
        void Spawn()
        {
            enemies.RemoveAll(e => e == null);
            if (enemies.Count >= 30) return;
            Vector3 position;
            int tries = 0;
            do
            {
                float edge = UnityEngine.Random.Range(-17f, 17f);
                switch (UnityEngine.Random.Range(0, 4))
                {
                    case 0: position = new Vector3(edge, 1, 17); break;
                    case 1: position = new Vector3(edge, 1, -17); break;
                    case 2: position = new Vector3(17, 1, edge); break;
                    default: position = new Vector3(-17, 1, edge); break;
                }
                tries++;
            } while ((Vector3.Distance(position, Player.transform.position) < 7 || Physics.CheckSphere(position, .6f)) && tries < 20);
            if (tries >= 20) return;
            enemies.Add(Instantiate(enemyPrefab, position, Quaternion.identity).GetComponent<Actor>());
        }
        void Fire()
        {
            Actor target = null;
            float nearest = 13 * 13;
            foreach (var enemy in enemies)
            {
                if (enemy == null) continue;
                float distance = (enemy.transform.position - Player.transform.position).sqrMagnitude;
                if (distance < nearest) { nearest = distance; target = enemy; }
            }
            if (target == null) return;
            Vector3 direction = target.transform.position - Player.transform.position;
            direction.y = 0;
            direction.Normalize();
            var bolt = Instantiate(boltPrefab, Player.transform.position + direction * .8f, Quaternion.identity).GetComponent<Bolt>();
            bolt.direction = direction;
            fireAt = Time.time + Mathf.Max(.4f, Mathf.Pow(.9f, level - 1));
        }
        public void Defeated(Actor enemy)
        {
            enemies.Remove(enemy);
            score += 10;
            Instantiate(fragmentPrefab, new Vector3(enemy.transform.position.x, .5f, enemy.transform.position.z), Quaternion.identity);
            Burst(enemy.transform.position, new Color(.7f, .45f, .2f));
            Refresh();
        }
        public void Collect()
        {
            experience++;
            if (experience >= 5)
            {
                experience -= 5;
                level++;
                notice.text = "NÍVEL " + level + " • RELICÁRIO FORTALECIDO";
                noticeUntil = Time.time + 2;
                animationUntil = Time.time + 1.5f;
                Burst(Player.transform.position, Color.yellow);
            }
            Refresh();
        }
        public void Burst(Vector3 position, Color color)
        {
            var effect = Instantiate(effectPrefab, position, Quaternion.identity);
            var particles = effect.GetComponent<ParticleSystem>();
            var main = particles.main;
            main.startColor = color;
            particles.Play();
            Destroy(effect, 2);
        }
        public void End(bool victory)
        {
            if (!Playing) return;
            Playing = false;
            notice.text = "";
            animationImage.enabled = false;
            foreach (var body in FindObjectsOfType<Rigidbody>()) body.velocity = Vector3.zero;
            result.text = (victory ? "A VIGÍLIA TERMINOU" : "O CLAUSTRO RECLAMA MAIS UM") + "\n\nPONTUAÇÃO: " + score + "\nNÍVEL: " + level + "\n\nR — TENTAR NOVAMENTE";
        }
        public void Refresh() { Changed?.Invoke(); }
        void RenderHud()
        {
            int seconds = Mathf.CeilToInt(remaining);
            hud.text = "MAÇONARIA\nVIDA " + Mathf.Max(0, Player.health) + "/3     PONTOS " + score + "\nNÍVEL " + level + "     EXPERIÊNCIA " + experience + "/5     TEMPO " + (seconds / 60).ToString("00") + ":" + (seconds % 60).ToString("00");
        }
        void MakeInterface()
        {
            font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            var canvas = new GameObject("Interface", typeof(Canvas), typeof(CanvasScaler));
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = canvas.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1280, 720);
            hud = Label(canvas.transform, "Status", new Vector2(0, 1), new Vector2(28, -22), new Vector2(850, 115), 24, TextAnchor.UpperLeft);
            notice = Label(canvas.transform, "Avisos", new Vector2(.5f, 0), new Vector2(0, 38), new Vector2(1100, 100), 23, TextAnchor.MiddleCenter);
            result = Label(canvas.transform, "Resultado", new Vector2(.5f, .5f), Vector2.zero, new Vector2(1100, 360), 38, TextAnchor.MiddleCenter);
            var graphic = new GameObject("Evolução", typeof(RectTransform), typeof(Image));
            graphic.transform.SetParent(canvas.transform, false);
            animationImage = graphic.GetComponent<Image>();
            var rect = graphic.GetComponent<RectTransform>();
            rect.anchorMin = rect.anchorMax = new Vector2(.5f, .5f);
            rect.anchoredPosition = new Vector2(0, 130);
            rect.sizeDelta = new Vector2(100, 100);
            frames = new Sprite[8];
            for (int f = 0; f < frames.Length; f++)
            {
                var texture = new Texture2D(64, 64, TextureFormat.RGBA32, false);
                var pixels = new Color[64 * 64];
                float radius = 7 + f * 3;
                for (int y = 0; y < 64; y++) for (int x = 0; x < 64; x++)
                {
                    float distance = Vector2.Distance(new Vector2(x, y), new Vector2(31.5f, 31.5f));
                    pixels[y * 64 + x] = Mathf.Abs(distance - radius) < 2 ? new Color(1, .8f, .3f, 1 - f * .09f) : Color.clear;
                }
                texture.SetPixels(pixels); texture.Apply();
                frames[f] = Sprite.Create(texture, new Rect(0, 0, 64, 64), new Vector2(.5f, .5f));
            }
            animationImage.enabled = false;
        }
        Text Label(Transform parent, string name, Vector2 anchor, Vector2 position, Vector2 size, int fontSize, TextAnchor alignment)
        {
            var obj = new GameObject(name, typeof(RectTransform), typeof(Text), typeof(Outline));
            obj.transform.SetParent(parent, false);
            var rect = obj.GetComponent<RectTransform>();
            rect.anchorMin = rect.anchorMax = anchor; rect.pivot = anchor;
            rect.anchoredPosition = position; rect.sizeDelta = size;
            var text = obj.GetComponent<Text>();
            text.font = font; text.fontSize = fontSize; text.alignment = alignment;
            text.color = new Color(.95f, .87f, .7f);
            return text;
        }
        void OnDestroy()
        {
            Changed -= RenderHud;
            if (frames != null) foreach (var frame in frames) { Destroy(frame.texture); Destroy(frame); }
            if (Current == this) Current = null;
        }
    }
}
