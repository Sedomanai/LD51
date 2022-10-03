using UnityEngine;

namespace Elang
{
    public class Cutboard : Utility, IUtility
    {
        IItem _item = null;
        public new Vector3 Offset { get { return new Vector3(0.0f, 0.6f, 0.0f); } }

        public new FacingDirection Direction { get { return FacingDirection.Down; } }

        public new bool Processable(IItem item, out bool ignoreDupe) {
            ignoreDupe = false;
            return item != null && item.Choppable;
        }
        public new bool TrySetColor(Color color) {
            _states[1].GetComponent<SpriteRenderer>().color = color;
            return true;
        }

        public new Item TryCookingItem(ItemTranslator translator) {
            if (_item.Choppable) {
                return translator.irlToItem[_item.Chopped];
            }

            return translator.nameToItem["trash"];
        }

        public new bool TryInsertingItem(ref IItem item, out bool stopMoving) {
            if (item.Choppable) {
                SoundMgr.Instance.PlaySFX("cut");
                _item = item;
                stopMoving = true;
                return true;
            }

            // This shouldn't happen though.
            stopMoving = false;
            return false;
        }
    }
}
