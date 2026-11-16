using UnityEngine;
namespace Maconaria
{
    public sealed class RitualFade : MonoBehaviour
    {
        public float duration = .3f;
        float elapsed;
        LineRenderer line;
        Color color;
        void Start() { line = GetComponent<LineRenderer>(); color = line.startColor; }
        void Update()
        {
            elapsed += Time.unscaledDeltaTime;
            var value = color; value.a = Mathf.Clamp01(1 - elapsed / duration) * .6f;
            line.startColor = line.endColor = value;
            if (elapsed >= duration) Destroy(gameObject);
        }
    }
}
