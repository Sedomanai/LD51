using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang
{

    enum SpecialItems {
        Begin = 1000000,
        Customer,
        Cutlet
    }


    public delegate void CustomerSeated();

    public class CustomerItem : ItemBase, IItem
    {
        public new int Index { get { return (int)SpecialItems.Customer; } set { } }

        public int NumCount = 0;

        public List<GameObject> customers = new();

        public CustomerSeated seated;
    }

    //public class Cutlet : ItemBase, IItem
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
}