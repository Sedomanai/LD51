using UnityEngine;
using UnityEngine.Assertions;

namespace Elang
{
    public class Brewer : Utility, IUtility
    {
        public new Vector3 Offset { get { return new Vector3(0.0f, -1.4f, 0.0f); } }
        public new FacingDirection Direction { get { return FacingDirection.Up; } }
        public new bool Processable(IItem item, out bool ignoreDupe) {
            ignoreDupe = false;
            return item == null;
        }
        public new bool TryInsertingItem(ref IItem item, out bool stopMoving) {
            SoundMgr.Instance.PlaySFX("pan");
            Assert.IsTrue(item == null);
            stopMoving = true;
            return true;
        }
        public new Item TryCookingItem(ItemTranslator translator) {
            return translator.nameToItem["coffee"];
        }
    }
}