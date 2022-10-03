using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang
{
    public class Pan : Utility, IUtility
    {
        IItem _item = null;

        public new Vector3 Offset { get { return new Vector3(-0.8f, 0.2f, 0.0f); } }
        public new FacingDirection Direction { get { return FacingDirection.Right; } }
        public new bool Processable(IItem item, out bool ignoreDupe) {
            ignoreDupe = false;
            return item != null && item.PanFriable;
        }
        public new bool TrySetColor(Color color) {
            _states[1].GetComponent<SpriteRenderer>().color = color;
            return true;
        }
        public new Item TryCookingItem(ItemTranslator translator) {
            var item = _item as Item;
            if (item != null) {
                var newItem = translator.irlToItem[item.PanFried];
                return newItem;
            } else { //multi item

            }

            return translator.nameToItem["trash"];
        }
        public new bool TryInsertingItem(ref IItem item, out bool stopMoving) { 
            if (item.PanFriable) {
                _item = item;
                stopMoving = true;
                SoundMgr.Instance.PlaySFX("pan");
                return true;
            }

            stopMoving = false;
            // This shouldn't happen though.
            return false;
        }
    }
}
