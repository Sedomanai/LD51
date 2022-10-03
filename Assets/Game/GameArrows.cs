using UnityEngine;

namespace Elang
{
    [System.Serializable]
    public class GameArrows
    {
        [SerializeField]
        GameObject _arrow, _smallArrow, _circlet;

        SpriteRenderer _arrowRenderer, _smallArrowRenderer, _circletRenderer;
        
        public void Init() {
            _circletRenderer = _circlet.GetComponentInChildren<SpriteRenderer>();
            _arrowRenderer = _arrow.GetComponentInChildren<SpriteRenderer>();
            _smallArrowRenderer = _smallArrow.GetComponentInChildren<SpriteRenderer>();
        }

        public void DisactivateHighlights() {
            _arrowRenderer.enabled = false;
            _circletRenderer.enabled = false;
        }

        public void ProcessState(Game.State state) {
            bool flag = (state == Game.State.Chef || state == Game.State.Waiter);
            _smallArrow.SetActive(flag);
        }

        void Activate(Transform parent, SpriteRenderer rend, GameObject arrow) {
            rend.enabled = true;
            arrow.transform.parent = parent;
            arrow.transform.localPosition = Vector3.zero;
        }

        public void ActivateCirclet(Transform parent) {
            Activate(parent, _circletRenderer, _circlet);
        }

        public void ActivateSmallArrow(Transform parent) {
            Activate(parent, _smallArrowRenderer, _smallArrow);
        }

        public void ActivateArrow(Transform parent) {
            Activate(parent, _arrowRenderer, _arrow);
        }
    }

}