using UnityEngine;

namespace Elang
{
    enum SpecialIndex {
        Cutlet = 2958304
    }

    //public class Cutlet : MonoBehaviour, IItem
    //{
    //    public Sprite Card { get { return null; } set { } }
    //    public Sprite IRL { get { return null; } set { } }

    //    public bool PanFriable { get { return false; } set { } }
    //    public Sprite PanFried { 
    //        get { return null; } 
    //        set { } 
    //    }
    //    public bool Choppable { get { return false; } set { } }

    //    public Color Tint { get { return Color.white; } set { } }
    //    public int Index { get { return (int)SpecialIndex.Cutlet; } set { } }

    //    [SerializeField]
    //    Sprite _cutboard;

    //    SpriteRenderer _board, _cut1, _cut2, _cut3;

    //    void Awake() {
    //        var rend = GetComponentsInChildren<SpriteRenderer>();

    //        _board = rend[0];
    //        _cut1 = rend[1];
    //        _cut2 = rend[2];
    //        _cut3 = rend[3];
    //    }
    //}

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

            return translator.nameToItem["trash"];
        }

        public new bool TryInsertingItem(IItem item, out bool stopMoving) {
            if (item.Choppable) {
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
