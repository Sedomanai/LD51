using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang
{
    public class Waiter : Character, ICharacter
    {

        public new Game.State ActivateUtility(GameObject utilObject, IUtility utility, Reel reel) {
            var ret = ActivateUtilityBody(utilObject, utility, reel);

            if (ret == Game.State.Basic)
                ret = Game.State.Waiter;

            return ret;
        }
        public new void UseTray(Tray tray) {
            if (_activeUtility != null) {
                _activeUtility.Active = false;
            }
            _activeUtility = null;

            transform.parent.position = tray.transform.position;
            transform.parent.position += new Vector3(-2, 0, 0);
            _rend.Direction = FacingDirection.Right;
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