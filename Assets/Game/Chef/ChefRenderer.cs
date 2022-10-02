using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Elang
{
    public class ChefRenderer : MonoBehaviour
    {
        public enum Direction
        {
            Left,
            Right,
            Down,
            Up
        }

        [SerializeField]
        Sprite _upSprite;
        [SerializeField]
        Sprite _downSprite;
        [SerializeField]
        Sprite _sideSprite;

        [SerializeField]
        Direction _direction = Direction.Left;

        public Direction FacingDirection { get { return _direction; } set { _direction = value; ChangeDirectionLogic(); } }

        SpriteRenderer _sprite;

        // Start is called before the first frame update
        void Awake() {
            _sprite = GetComponent<SpriteRenderer>();
            ChangeDirectionLogic();
        }

        void ChangeDirectionLogic() {
            switch (_direction) {
            case Direction.Up:
                _sprite.flipX = false;
                _sprite.sprite = _upSprite;
                break;
            case Direction.Down:
                _sprite.flipX = false;
                _sprite.sprite = _downSprite;
                break;
            case Direction.Left:
                _sprite.flipX = true;
                _sprite.sprite = _sideSprite;
                break;
            case Direction.Right:
                _sprite.flipX = false;
                _sprite.sprite = _sideSprite;
                break;
            }
        }

        // Update is called once per frame
        void Update() {

        }
    }

}
