using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
            Waiter,
            Reel,
            GameOver,
            Finished
        }

        [Header("UI")]
        [SerializeField]
        GameArrows _arrows;
        [SerializeField]
        GameObject _gameOverScreen;
        [SerializeField]
        GameObject _youWinScreen;

        [Header("Input")]
        [SerializeField]
        InputActionAsset _input;

        [SerializeField]
        InputActionReference _selectAction;

        [Header("Logic")]
        [SerializeField]
        State _state = State.Basic;
        State _prevState;

        [SerializeField]
        TimerTextScript _timer;

        ICharacter _activePlayer;

        bool _trySelect = false;
        bool _selectCharacter, _selectKitchen, _selectDining, _selectReel;
        int _characterLayer, _kitchenLayer, _diningLayer, _trayLayer, _reelLayer;
        public Vector3 CursorOrigin { get { return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); } }

        Reel _reel;

        ItemTranslator _translator;

        void Awake() {
            _reel = GetComponentInChildren<Reel>();
            _kitchenLayer = 1 << LayerMask.NameToLayer("Kitchen");
            _diningLayer = 1 << LayerMask.NameToLayer("Dining");
            _characterLayer = 1 << LayerMask.NameToLayer("Player");
            _reelLayer = 1 << LayerMask.NameToLayer("Card");
            _trayLayer = 1 << LayerMask.NameToLayer("Tray");

            _arrows.Init();

            _input.FindActionMap("Play").Enable();
            _selectAction.action.performed += PerformTrySelect;
            ChangeState(_state);

            _translator = GetComponent<ItemTranslator>();

            SoundMgr.Instance.PlayBGM("untitled");
            SoundMgr.Instance.sfxMax = 500;
        }

        void OnDestroy() {
            _selectAction.action.performed -= PerformTrySelect;
        }

        void PerformTrySelect(InputAction.CallbackContext ctx) {
            _trySelect = true;
        }

        public void StartGame() {
            ChangeState(State.Basic);
            SoundMgr.Instance.PlaySFX("start");
        }

        bool _winFanfare = false;
        public void ChangeState(State state) {
            _prevState = _state;
            _selectReel = _selectCharacter = _selectKitchen = _selectDining = false;
            _state = state;
            switch (_state) {
            case State.Basic:
                _selectCharacter = true;
                _activePlayer = null;
                break;
            case State.Chef:
                _selectCharacter = _selectKitchen = true;
                break;
            case State.Waiter:
                _selectCharacter = _selectDining = true;
                break;
            case State.Reel:
                _selectReel = true;
                break;
            case State.GameOver:
                _activePlayer = null;
                _gameOverScreen.SetActive(true);
                _timer.StopTime();
                StartCoroutine(RestartCO());
                break;
            case State.Finished:
                _activePlayer = null;
                _youWinScreen.SetActive(true);
                _timer.StopTime();
                if (!_winFanfare) {
                    SoundMgr.Instance.PlaySFX("win");
                    _winFanfare = true;
                }
                break;
            }
            _trySelect = false;
        }

        void Update() {
            _arrows.DisactivateHighlights();

            CharacterSelect();
            Select(_selectKitchen, _kitchenLayer);
            Select(_selectDining, _diningLayer);
            ReelSelect();
            RestartClick();

            _arrows.ProcessState(_state);
            _trySelect = false;
        }

        IEnumerator RestartCO() {
            SoundMgr.Instance.PlaySFX("gameover");
            yield return new WaitForSeconds(2.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        void RestartClick() {
            if (_state == State.Finished) {
                if (_trySelect) {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
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
                            _arrows.ActivateSmallArrow(_activePlayer.SpriteTransform);
                            ChangeState(State.Chef);
                            SoundMgr.Instance.PlaySFX("char");
                        } else {
                            var waiter = curr.GetComponent<Waiter>();
                            if (waiter) {
                                _activePlayer = waiter;
                                _arrows.ActivateSmallArrow(_activePlayer.SpriteTransform);
                                ChangeState(State.Waiter);
                                SoundMgr.Instance.PlaySFX("char");
                            }
                        }
                    }

                    if (!(_activePlayer != null && _activePlayer.Equals(curr))) {
                        _arrows.ActivateCirclet(curr.GetComponentInChildren<SpriteRenderer>().transform);
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
                if (_activePlayer != null) {
                    SoundMgr.Instance.PlaySFX("getitem");
                    var spr = target.GetComponentsInChildren<SpriteRenderer>()[1];
                    var item = _translator.cardToItem[spr.sprite];
                    _activePlayer.Item = item;
                }

                _reel.CloseReel();
                ChangeState(_prevState);
            }
        }

        void Select(bool flag, int layer) {
            if (flag) {
                var hit = Physics2D.Raycast(CursorOrigin, Vector2.zero, 0, layer);
                if (hit) {
                    var target = hit.collider.gameObject;
                    var utility = _activePlayer.ProcessUtility(target);
                    if (utility != null) {
                        _arrows.ActivateArrow(target.transform);
                        if (_trySelect) {
                            var nextState = _activePlayer.ActivateUtility(target, utility, _reel);
                            ChangeState(nextState);
                        }
                    } 
                } else
                    ProcessTrays();
            }
        }

        void ProcessTrays() {
            var hit = Physics2D.Raycast(CursorOrigin, Vector2.zero, 0, _trayLayer);
            if (hit) {
                var target = hit.collider.gameObject.GetComponent<Tray>();
                bool ignoreDupe;
                if (target && _activePlayer.ProcessTray(target, out ignoreDupe)) {
                    _arrows.ActivateArrow(target.transform);
                    if (_trySelect) {
                        _activePlayer.UseTray(target);
                        _trySelect = false;
                    }
                }
            }
        }
    }
}