using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elang
{
    public interface IUtility
    {
        public Vector3 Offset { get; }
        public bool Active { get; set; }
        public FacingDirection Direction { get; }
        public ReelData ReelData { get; }
        public IItem TryGetItem { get; }
        public bool Processable(IItem item, out bool ignoreDupe);
        public int UpdateCustomerState(ItemTranslator translator, ReelData menu);
        public Item TryCookingItem(ItemTranslator translator);
        public bool TryInsertingItem(IItem item, out bool stopMoving);
        public bool TrySetColor(Color color);
    }

    public class Utility : MonoBehaviour, IUtility
    {
        public bool Active {
            get { return _states.Count() > 0 ? _states[0].On : false; }
            set {
                foreach (var state in _states) {
                    if (value)
                        state.TurnOn();
                    else state.TurnOff();
                }
            }
        }

        public Vector3 Offset { get { return Vector3.zero; } }
        public FacingDirection Direction { get { return FacingDirection.Left; } }

        public ReelData ReelData { get { return null; } }

        public IItem TryGetItem { get { return null; } }
        public bool Processable(IItem item, out bool ignoreDupe) { ignoreDupe = false; return false; }

        public Item TryCookingItem(ItemTranslator translator) { return null; }
        public int UpdateCustomerState(ItemTranslator translator, ReelData menu) { return 0; }

        public bool TryInsertingItem(IItem item, out bool stopMoving) { stopMoving = false; return false; }

        public bool TrySetColor(Color color) { return false; }


        protected List<BinaryStateRenderer> _states = new();

        void Awake() {
            _states = GetComponentsInChildren<BinaryStateRenderer>().ToList();
        }
    }

}