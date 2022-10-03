using UnityEngine;

namespace Elang
{
    public class SmallTable : Utility, IUtility
    {
        public new Vector3 Offset { get { return new Vector3(-0.75f, 0.0f, 0.0f); } }

        public new FacingDirection Direction { get { return FacingDirection.Right; } }

        public new bool Processable(IItem item, out bool ignoreDupe) {
            ignoreDupe = true;
            if (item != null) {
                switch (_state) {
                case State.None:
                    if (item.Index > (int)SpecialItems.Begin) {
                        var customer = item as CustomerItem;
                        if (customer != null && customer.NumCount < 3)
                            return true;
                    }
                    break;

                case State.Ordering:
                    if (item.Index < (int)SpecialItems.Begin && DeliverableChair(item) != null)
                        return true;
                    break;
                case State.Dirty:
                    if (item.Index > (int)SpecialItems.Begin)
                        return true;
                    break;
                }
            } else {
                if (_state == State.Dirty)
                    return true;
            }

            return false;
        }

        enum State
        {
            None,
            Seated,
            Ordering,
            Eating,
            Dirty
        }

        [SerializeField]
        ItemTranslator _translator;

        State _state = State.None;
        Chair[] _chairs;

        CustomerItem _customer;

        int _eatTime = 2;

        void Awake() {
            _chairs = GetComponentsInChildren<Chair>();
        }

        Chair DeliverableChair(IItem item) {
            foreach (var chair in _chairs) {
                if (chair.IsOpen && chair.item == item)
                    return chair;
            }
            return null;
        }

        public new IItem TryGetItem { 
            get {
                IItem ret = null;
                if (_state == State.Dirty) {
                    foreach (var chair in _chairs) {
                        if (chair.item != null) {
                            ret = chair.item;
                            chair.Clean();
                            break;
                        }
                    }

                    bool allClear = true;
                    foreach (var chair in _chairs) {
                        if (chair.item != null)
                            allClear = false;
                    }

                    if (allClear)
                        _state = State.None;
                }
                return ret; 
            } 
        }

        public new bool TryInsertingItem(IItem item, out bool nullItem) {
            nullItem = false;
            switch (_state) {
            case State.None:
                var customer = item as CustomerItem;
                if (customer != null && customer.NumCount < 3) {
                    _customer = customer;
                    SeatCustomers();
                    return true;
                }
                break;
            case State.Ordering:
                DeliverableChair(item).GiveItem();
                if (CheckAllDelivered())
                    _state = State.Eating;

                nullItem = false;
                return true;
            }

            return false;
        }

        public bool CheckAllDelivered() {
            foreach (var chair in _chairs) {
                if (chair.item != null) {
                    return false;
                }
            }
            return true;
        }

        public void SeatCustomers() {
            _customer.seated.Invoke();
            var cs = _customer.customers;
            for (int i = 0; i < cs.Count; i++) {
                _chairs[i].Open(cs[i]);
            }
            _state = State.Seated;
        }

        public new int UpdateCustomerState(ItemTranslator translator, ReelData menu) {
            switch (_state) {
            case State.Seated:
                foreach (var chair in _chairs) {
                    var sprites = menu.sprites;
                    var index = Random.Range(0, sprites.Count - 1);
                    var item = translator.irlToItem[sprites[index]];
                    chair.AskItem(item);
                }
                _state = State.Ordering;
                break;
            case State.Eating:
                if (_eatTime > 0) {
                    _eatTime--;
                    if (_eatTime == 0) {
                        int cost = 0;
                        foreach (var chair in _chairs) {
                            cost += chair.Close(translator);
                        }

                        _eatTime = 2;
                        _state = State.Dirty;
                        return cost;
                    }
                }

                break;
            }

            return 0;
        }
    }
}