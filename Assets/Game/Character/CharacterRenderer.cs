using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Elang
{
    public enum FacingDirection
    {
        Left,
        Right,
        Down,
        Up
    }
    public class CharacterRenderer : MonoBehaviour
    {

        [SerializeField]
        Sprite _upSprite;
        [SerializeField]
        Sprite _downSprite;
        [SerializeField]
        Sprite _sideSprite;

        [SerializeField]
        FacingDirection _direction = FacingDirection.Left;

        public FacingDirection Direction { get { return _direction; } set { _direction = value; ChangeDirectionLogic(); } }

        SpriteRenderer _sprite;

        void Awake() {
            _sprite = GetComponent<SpriteRenderer>();
        }

        void OnEnable() {
            ChangeDirectionLogic();
        }

        void ChangeDirectionLogic() {
            switch (_direction) {
            case FacingDirection.Up:
                _sprite.flipX = false;
                _sprite.sprite = _upSprite;
                break;
            case FacingDirection.Down:
                _sprite.flipX = false;
                _sprite.sprite = _downSprite;
                break;
            case FacingDirection.Left:
                _sprite.flipX = true;
                _sprite.sprite = _sideSprite;
                break;
            case FacingDirection.Right:
                _sprite.flipX = false;
                _sprite.sprite = _sideSprite;
                break;
            }
        }
    }

}
