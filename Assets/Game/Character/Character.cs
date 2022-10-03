using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang
{
    interface ICharacter
    {
        Transform SpriteTransform { get; }
        bool Equals(GameObject obj);

        IItem Item { get; set; }
        public IUtility ProcessUtility(GameObject utilObject);
        Game.State ActivateUtility(GameObject utilObject, IUtility utility, Reel reel);

        public bool ProcessTray(Tray tray, out bool ignoreDupe);
        public void UseTray(Tray tray);
    }

    abstract public class Character : MonoBehaviour, ICharacter
    {
        protected IItem _item;
        protected CharacterRenderer _rend;
        protected SpriteRenderer _itemHolder;
        protected IUtility _activeUtility;
        void Awake() {
            _item = null;
            _rend = GetComponent<CharacterRenderer>();
            _itemHolder = GetComponentInChildren<ItemHolder>().GetComponent<SpriteRenderer>();
        }
        public IItem Item {
            get { return _item; }
            set {
                _item = value;
                _itemHolder.sprite = (_item == null) ?
                    null : _item.IRL;
                _rend.Direction = FacingDirection.Down;
            }
        }
        public IUtility ActiveUtility { get { return _activeUtility; } }

        public Transform SpriteTransform { get { return transform; } }


        public Game.State ActivateUtility(GameObject utilObject, IUtility utility, Reel reel) {
            return ActivateUtilityBody(utilObject, utility, reel);
        }

        protected Game.State ActivateUtilityBody(GameObject utilObject, IUtility utility, Reel reel) {
            if (_activeUtility != null) {
                _activeUtility.Active = false;
            }
            _activeUtility = utility;
            _activeUtility.Active = true;

            transform.parent.position = utilObject.transform.position;
            _rend.Direction = utility.Direction;
            transform.localPosition = utility.Offset;

            var reelData = utility.ReelData;
            if (reelData) {
                reel.transform.position = transform.parent.position;
                reel.OpenReel(reelData);
                return Game.State.Reel;
            }

            bool stopMoving;
            if (utility.TryInsertingItem(_item, out stopMoving)) {
                utility.TrySetColor(_item.Tint);
                _itemHolder.sprite = null;
                if (!stopMoving)
                    _item = null;
                return Game.State.Basic;
            }

            if (_item == null || _item.Index > (int)SpecialItems.Begin) {
                var got = utility.TryGetItem;
                if (got != null)
                    Item = got;
            }
            return Game.State.Basic;
        }

        public bool Equals(GameObject obj) {
            return gameObject == obj;
        }

        public bool ProcessTray(Tray tray, out bool ignoreDupe) {
            return tray.Processable(_item, out ignoreDupe);
        }

        public IUtility ProcessUtility(GameObject utilObject) {
            IUtility ret = null;

            var iutil = utilObject.GetComponent<IUtility>();

            bool ignoreDupe = false;
            if (iutil != null && iutil.Processable(_item, out ignoreDupe)) {
                ret = iutil;
            }

            if (ret == _activeUtility && !ignoreDupe)
                ret = null;

            if (ret != null && ret.Active)
                ret = null;

            return ret;
        }

        public void UseTray(Tray tray) {
            if (_activeUtility != null) {
                _activeUtility.Active = false;
            }
            _activeUtility = null;

            transform.parent.position = tray.transform.position;
            _rend.Direction = tray.Direction;
            transform.localPosition = tray.Offset;

            bool stopAction;
            if (tray.TryInsertingItem(_item, out stopAction)) {
                _item = null;
                _itemHolder.sprite = null;
                return;
            }

            if (_item == null || _item.Index > (int)SpecialItems.Begin) {
                var got = tray.TryGetItem;
                if (got != null)
                    Item = got;
            }
        }
    }
}