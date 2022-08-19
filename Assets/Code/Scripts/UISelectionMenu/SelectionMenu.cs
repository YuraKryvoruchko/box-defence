using UnityEngine;

namespace BoxDefence.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class SelectionMenu : MonoBehaviour
    {
        #region Fields

        private RectTransform _rectTransform;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        #endregion

        #region Public Methods

        public void MoveToCell(Vector2 positon)
        {
            _rectTransform.position = positon;
        }
        public bool GetActivedStatusMenu()
        {
            return gameObject.activeSelf;
        }
        public void EnableMenu()
        {
            gameObject.SetActive(true);
        }
        public void DisableMenu()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}
