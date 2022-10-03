using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elang
{
    public class Receptionist : Utility, IUtility
    {
        public new Vector3 Offset { get { return new Vector3(0.75f, 0.25f, 0.0f); } }

        public new FacingDirection Direction { get { return FacingDirection.Down; } }

        public new bool Processable(IItem item, out bool ignoreDupe) {
            ignoreDupe = false;
            return (item == null && _customers != null);
        }

        public new IItem TryGetItem {  
            get {
                SoundMgr.Instance.PlaySFX("sinklet");
                return _customers; 
            }  
        }

        ObjectPool _pool;
        Transform[] _spawn;
        CustomerItem _customers = null;

        void Awake() {
            var tr = GetComponentsInChildren<Transform>().ToList();
            tr.RemoveAt(0);
            _spawn = tr.ToArray();
            _pool = GetComponent<ObjectPool>();
        }

        public void On10Seconds() {
            if (_customers == null) {
                _customers = new CustomerItem();
                _customers.customers.Add(Spawn(0)); // spawn more here
                _customers.customers.Add(Spawn(1)); // spawn more here
                _customers.NumCount = 2;
                _customers.seated += Seated;
            }
        }

        GameObject Spawn(int index) {
            var obj = _pool.Get();
            obj.transform.parent = _spawn[index];
            obj.transform.localPosition = Vector3.zero;
            return obj;
        }

        public void Seated() {
            _customers.seated -= Seated;
            _customers = null;
        }
    }

}
