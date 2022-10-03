using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang
{
    public class Tray : Utility, IUtility
    {
        IItem _item;
        SpriteRenderer _rend;

        public new Vector3 Offset { get { return new Vector3(1.0f, 0, 0); } }

        public new FacingDirection Direction { get { return FacingDirection.Left; } }

        public new bool Processable(IItem item, out bool ignoreDupe) {
            ignoreDupe = true;
            bool emptyHand = item == null;

            if (item != null && item.Index > (int)SpecialItems.Begin) {
                emptyHand = true;
            }


            return emptyHand != (_item == null);
        }

        public new IItem TryGetItem {
            get {
                SoundMgr.Instance.PlaySFX("tray");
                var item = _item;
                _item = null;
                _rend.sprite = null;
                return item;
            }
        }

        public new bool TryInsertingItem(ref IItem item, out bool stopAction) {
            stopAction = false;
            if (item != null && _item == null) {
                if (item.Index > (int)SpecialItems.Begin)
                    return false;

                SoundMgr.Instance.PlaySFX("tray");
                _item = item;
                _rend.sprite = item.IRL;
                return true;
            }

            return false;
        }

        void Awake() {
            _rend = GetComponent<SpriteRenderer>();
        }
    }
}