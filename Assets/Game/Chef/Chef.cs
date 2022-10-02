using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEditor.Progress;


namespace Elang
{
    interface ICharacter
    {
        Transform SpriteTransform { get; }
        bool Equals(GameObject obj);

        Item Item { get; set; }
        public IUtility ProcessUtility(GameObject kitchenObject);
        Game.State ActivateUtility(GameObject kitchenObject, IUtility utility, Reel reel);
    }

    public class Chef : MonoBehaviour, ICharacter
    {
        public Item Item { get { return _item; }
            set { 
                _item = value;
                _itemHolder.sprite = (_item == null) ?
                    null : _item.irl;
                _rend.FacingDirection = ChefRenderer.Direction.Down;
            } 
        }
        Item _item;
        ChefRenderer _rend;
        SpriteRenderer _itemHolder;

        IUtility _activeUtility;

        public IUtility ActiveUtility { get { return _activeUtility; }  }

        public Transform SpriteTransform { get { return _rend.transform; }}

        public bool Equals(GameObject obj) {
            return gameObject == obj;
        }
        void Awake() {
            _item = null;
            _rend = GetComponentInChildren<ChefRenderer>();
            _itemHolder = GetComponentInChildren<ItemHolder>().GetComponent<SpriteRenderer>();
        }

        public IUtility ProcessUtility(GameObject kitchenObject) {
            IUtility ret = null;

            var fridge = kitchenObject.GetComponent<Fridge>();
            if (fridge) {
                if (_item == null)
                    ret = fridge;
            }

            if (ret == _activeUtility)
                ret = null;

            if (ret != null && ret.State.On)
                ret = null;

            return ret;
        }

        public Game.State ActivateUtility(GameObject kitchenUtility, IUtility utility, Reel reel) {
            if (_activeUtility != null) {
                _activeUtility.State.TurnOff();
            }
            _activeUtility = utility;
            _activeUtility.State.TurnOn();

            transform.position = kitchenUtility.transform.position;
            _rend.FacingDirection = ChefRenderer.Direction.Up;
            _rend.transform.localPosition = new Vector3(-0.25f, -0.75f, 0.0f);

            var reelData = utility.ReelData;
            if (reelData) {
                reel.transform.position = transform.position;
                reel.OpenReel(reelData);
                return Game.State.Reel;
            }

            return Game.State.Chef;
        }
    }
}
