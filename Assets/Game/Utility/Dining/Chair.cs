using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Elang
{
    public class Chair : MonoBehaviour
    {
        [SerializeField]
        FacingDirection _direction;

        [SerializeField]
        float _openOffset, _customerOffset, _plateOffset;

        IItem _item = null;

        public IItem item { get { return _item; } }
        public bool IsOpen { get; private set; }

        GameObject _customer = null;

        SpriteRenderer _bubble, _bubbleObject, _plate;


        private void Awake() {
            var rends = GetComponentsInChildren<SpriteRenderer>();
            _bubble = rends[1];
            _bubbleObject = rends[2];
            _plate = rends[3];
            _plate.sprite = null;
            _bubble.gameObject.SetActive(false);
        }

        public void Open(GameObject customer) {
            transform.localPosition += new Vector3(0, _openOffset, 0);
            customer.transform.parent = transform;
            customer.transform.localPosition = new Vector3(0, _customerOffset, 0);
            _plate.transform.localPosition = new Vector3(0, _plateOffset, 0);

            var rend = customer.GetComponentsInChildren<CharacterRenderer>();
            foreach (var r in rend) {
                r.Direction = _direction;
            }

            _customer = customer;
            IsOpen = true;
        }

        public int Close(ItemTranslator translator) {
            _plate.transform.localPosition += transform.localPosition;
            transform.localPosition = Vector3.zero;
            _customer.SetActive(false);
            _customer = null;

            //TODO: get cost here

            _item = translator.nameToItem["dish"];
            _plate.sprite = _item.IRL;
            IsOpen = false;
            return 10;
        }

        public void Clean() {
            _item = null;
            _plate.sprite = null;
        }

        public void AskItem(Item item) {
            _item = item;
            _bubble.gameObject.SetActive(true);
            _bubbleObject.sprite = _item.IRL;
        }

        public void GiveItem() {
            _plate.sprite = _item.IRL;
            _item = null;
            _bubble.gameObject.SetActive(false);
        }
    }
}