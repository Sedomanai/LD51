using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang
{

    public interface IItem {
        Sprite Card { get; set; }
        Sprite IRL { get; set; }
        bool PanFriable { get; }
        bool Choppable { get; }
        bool Preparable { get; }
        Color Tint { get; set; }
        int Index { get; set; }

        int Cost { get; set; }

        public Sprite PanFried { get; }
        public Sprite Chopped { get; }
        public Sprite Prepared { get; }
    }

    public abstract class ItemBase : IItem
    {
        public Sprite Card { get { return null; } set { } }
        public Sprite IRL { get { return null; } set { } }

        public bool PanFriable { get { return false; } }

        public bool Choppable { get { return false; } }
        public bool Preparable { get { return false; } }

        public Color Tint { get; set; }
        public int Index { get; set; }

        public int Cost { get { return 0; } set { } }

        public Sprite PanFried { get { return null; } }
        public Sprite Chopped { get { return null; } }
        public Sprite Prepared { get { return null; } }
    }

    [System.Serializable]
    public class Item : IItem
    {
        [SerializeField]
        Sprite _card, _irl;
        [SerializeField]
        Sprite _panFried = null;
        [SerializeField]
        Sprite _chopped = null;
        [SerializeField]
        Sprite _prepared = null;
        [SerializeField]
        int _cost = 25;
        [SerializeField]
        Color _tint;
        public Sprite PanFried { get { return _panFried; } }
        public Sprite Chopped { get { return _chopped; } }
        public Sprite Prepared { get { return _prepared; } }
        public int Cost { get { return _cost; } set { } }
        public Sprite Card { get { return _card; } set { _card = value; } }
        public Sprite IRL {
            get { return _irl; }
            set { _irl = value; }
        }
        public bool PanFriable {
            get { return _panFried != null; }
        }
        public bool Choppable {
            get { return _chopped != null; }
        }
        public bool Preparable {
            get { return _prepared != null; }
        }
        public Color Tint {
            get { return _tint; }
            set { _tint = value; }
        }

        public int Index { get; set; }

        [HideInInspector]
        public int index;

    }
    public class ItemTranslator : MonoBehaviour
    {
        [SerializeField]
        List<Item> items;

        public Dictionary<Sprite, Item> cardToItem = new();
        public Dictionary<Sprite, Item> irlToItem = new();
        public Dictionary<string, Item> nameToItem = new();

        void Awake() {
            for(var i = 0; i < items.Count; i++) {
                var item = items[i];
                item.index = i;
                if (item.Card)
                    cardToItem.Add(item.Card, item);
                if (item.IRL) { 
                    irlToItem.Add(item.IRL, item);
                    nameToItem.Add(item.IRL.name, item);
                }
            }
        }
    }

}