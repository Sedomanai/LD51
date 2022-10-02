using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang
{
    [System.Serializable]
    public class Item
    {
        public Sprite card, irl;

        [HideInInspector]
        public int index;
    }
    public class ItemTranslator : MonoBehaviour
    {
        [SerializeField]
        List<Item> items;

        public Dictionary<Sprite, Item> cardToItem = new();
        public Dictionary<Sprite, Item> irlToItem = new();

        void Awake() {
            for(var i = 0; i < items.Count; i++) {
                var item = items[i];
                item.index = i;
                cardToItem.Add(item.card, item);
                irlToItem.Add(item.irl, item);
            }
        }
    }

}