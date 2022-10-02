using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Elang
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class BinaryStateRenderer : MonoBehaviour
    {
        [SerializeField]
        Sprite _on, _off;
        [SerializeField]
        Animation _onAnim, _offAnim;
        int _onHash, _offHash;

        SpriteRenderer _rend;
        Animator _anim;

        public bool On { get; set; }

        void Awake() {
            _rend = GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();

            _onHash = _onAnim ? 
                Animator.StringToHash(_onAnim.name) : -1;
            _offHash = _offAnim ? 
                Animator.StringToHash(_offAnim.name) : -1;

            On = true;
            TurnOff();
        }

        public void TurnOn() {
            if (!On) {
                if (_onHash != -1) {
                    _anim.enabled = true;
                    _anim.Play(_onHash);
                } else if (_on) {
                    _anim.enabled = false;
                    _rend.sprite = _on;
                } else {
                    _anim.enabled = false;
                    _rend.sprite = null;
                }
                On = true;
            }
        }

        public void TurnOff() {
            if (On) {
                if (_offHash != -1) {
                    _anim.enabled = true;
                    _anim.Play(_offHash);
                } else if (_off) {
                    _anim.enabled = false;
                    _rend.sprite = _off;
                } else {
                    _anim.enabled = false;
                    _rend.sprite = null;
                }
                On = false;
            }
        }
    }
}
