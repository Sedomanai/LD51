using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

namespace Elang
{
    public class Game : MonoBehaviour
    {
        public enum State
        {
            Paused,
            Basic,
            Chef,
            Character,
            Reel
        }

        [Header("UI")]
        [SerializeField]
        GameObject _arrow;
        [SerializeField]
        GameObject _smallArrow, _circlet;

        [Header("Input")]
        [SerializeField]
        InputActionAsset _input;

        [SerializeField]
        InputActionReference _selectAction;

        [Header("Logic")]
        [SerializeField]
        State _state = State.Basic;
        State _prevState;

        SpriteRenderer _circletRenderer;
        SpriteRenderer _arrowRenderer;
        BinaryStateRenderer _binRend;
        ICharacter _activePlayer;

        bool _trySelect = false;
        bool _selectCharacter, _selectKitchen, _selectReel;

        int _characterLayer, _kitchenLayer, _reelLayer;
        public Vector3 CursorOrigin { get { return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); } }

        Reel _reel;

        ItemTranslator _translator;

        void Awake() {
            _reel = GetComponent<Reel>();
            _kitchenLayer = 1 << LayerMask.NameToLayer("Kitchen");
            _characterLayer = 1 << LayerMask.NameToLayer("Player");
            _reelLayer = 1 << LayerMask.NameToLayer("Card");

            _input.FindActionMap("Play").Enable();
            _selectAction.action.performed += PerformTrySelect;
            ChangeState(_state);

            _circletRenderer = _circlet.GetComponentInChildren<SpriteRenderer>();
            _arrowRenderer = _arrow.GetComponentInChildren<SpriteRenderer>();
            _translator = GetComponent<ItemTranslator>();
        }

        void OnDestroy() {
            _selectAction.action.performed -= PerformTrySelect;
        }

        void PerformTrySelect(InputAction.CallbackContext ctx) {
            _trySelect = true;
        }

        public void ChangeState(State state) {
            _prevState = _state;
            _state = state;
            switch (_state) {
            case State.Paused:
                _selectReel = _selectCharacter = _selectKitchen = false;
                break;
            case State.Basic:
                _selectCharacter = true;
                _selectReel = _selectKitchen = false;
                break;
            case State.Chef:
                _selectCharacter = _selectKitchen = true;
                _selectReel = false;
                break;
            case State.Reel:
                _selectReel = true;
                _selectCharacter = _selectKitchen = false;
                break;
            }
        }

        void Update() {
            _circletRenderer.enabled = false;
            _arrowRenderer.enabled = false;
            CharacterSelect();
            KitchenSelect();
            ReelSelect();

            _trySelect = false;
        }

        void CharacterSelect() {
            if (_selectCharacter) {
                var hit = Physics2D.Raycast(CursorOrigin, Vector2.zero, 0, _characterLayer);
                if (hit) {
                    var curr = hit.collider.gameObject;
                    if (_trySelect) {
                        var chef = curr.GetComponent<Chef>();
                        if (chef) {
                            _activePlayer = chef;
                            SetSmallArrowActive();
                            ChangeState(State.Chef);
                        }
                    }

                    if (!(_activePlayer != null && _activePlayer.Equals(curr))) {
                        SetCircletActive(curr.GetComponentInChildren<SpriteRenderer>().transform);
                    }
                }
            }
        }

        void ReelSelect() {
            _reel.DeHighlight();
            if (_selectReel) {
                var hit = Physics2D.Raycast(CursorOrigin, Vector2.zero, 0, _reelLayer);
                if (hit) {
                    ProcessCardAction(hit.collider.gameObject);
                }
            }
        }

        void ProcessCardAction(GameObject target) {
            _reel.Highlight(target);
            if (_trySelect) {
                var spr = target.GetComponentsInChildren<SpriteRenderer>()[1];
                var item = _translator.cardToItem[spr.sprite];
                if (_activePlayer != null) {
                    _activePlayer.Item = item;
                }

                _reel.CloseReel();

                //fridge 
                //if (_binRend) {
                //    _binRend.TurnOff();
                //    _binRend = null;
                //}

                ChangeState(_prevState);
            }
        }

        void SetCircletActive(Transform parent) {
            _circletRenderer.enabled = true;
            _circlet.transform.parent = parent;
            _circlet.transform.localPosition = Vector3.zero;
        }

        void SetArrowActive(Transform parent) {
            _arrowRenderer.enabled = true;
            _arrow.transform.parent = parent;
            _arrow.transform.localPosition = Vector3.zero;
        }

        void SetSmallArrowActive() {
            _smallArrow.SetActive(true);
            _smallArrow.transform.parent = _activePlayer.SpriteTransform;
            _smallArrow.transform.localPosition = Vector3.zero;
        }

        void KitchenSelect() {
            if (_selectKitchen) {
                var hit = Physics2D.Raycast(CursorOrigin, Vector2.zero, 0, _kitchenLayer);
                if (hit) {
                    ProcessActivePlayerAction(hit.collider.gameObject);
                }
            }
        }

        void ProcessActivePlayerAction(GameObject target) {
            var utility = _activePlayer.ProcessUtility(target);
            if (utility != null) {
                SetArrowActive(target.transform);
                if (_trySelect) {
                    var nextState = _activePlayer.ActivateUtility(target, utility, _reel);
                    ChangeState(nextState);
                        
                }
            }
        }

    }
}