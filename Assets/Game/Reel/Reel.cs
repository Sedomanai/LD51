using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang
{
    public class Reel : MonoBehaviour
    {
        [SerializeField]
        ObjectPool _cardPool;
        List<GameObject> _cards = new();

        [SerializeField]
        bool _vertical = false;

        [Header("Materials")]
        [SerializeField]
        Material _selected;
        [SerializeField]
        Material _deselected;


        public void DeHighlight() {
            foreach (var card in _cards) {
                var rend = card.GetComponent<SpriteRenderer>();
                rend.material = _deselected;
            }
        }

        public void Highlight(GameObject curr) {
            foreach(var card in _cards) {
                var rend = card.GetComponent<SpriteRenderer>();
                rend.material = (card && card == curr) ? 
                    _selected : _deselected;
            }
        }

        public void OpenReel(ReelData data) {
            if (data && data.sprites.Count > 0) {
                Vector3 startPos = transform.position;
                var s = (data.sprites.Count - 1) / 2.0f;
                startPos.x -= s;

                foreach (var spr in data.sprites) {
                    var card = _cardPool.Get();
                    var rend = card.GetComponentsInChildren<SpriteRenderer>()[1];
                    rend.sprite = spr;

                    //tween or reposition here
                    card.transform.position = startPos;
                    startPos.x += 1;

                    _cards.Add(card);
                }
            }

#if UNITY_EDITOR
            else {
                Debug.LogError("Elang Error: Reel data should not be null or empty.");
            }
#endif
        }

        public void CloseReel() {
            foreach (var card in _cards) {
                card.SetActive(false);
            }
            _cards.Clear();
        }
    }
}