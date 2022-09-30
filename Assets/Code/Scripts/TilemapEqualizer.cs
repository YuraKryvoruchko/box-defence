using UnityEngine;
using UnityEngine.Tilemaps;

namespace BoxDefence
{
#if UNITY_EDITOR
    public class TilemapEqualizer : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;

        [EditorButtonAttribute("Compress Bounds")]
        public void CompressBounds()
        {
            _tilemap.CompressBounds();
        }
    }
#endif
}
