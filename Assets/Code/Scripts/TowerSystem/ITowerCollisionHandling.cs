using UnityEngine;

namespace BoxDefence.Towers
{
    public interface ITowerCollisionHandling
    {
        void OnEnter(Collider2D collision);
        void OnExit(Collider2D collision);
    }
}
