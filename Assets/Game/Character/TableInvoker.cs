using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elang
{
    public class TableInvoker : MonoBehaviour
    {
        [SerializeField]
        TextProInteger _gold;

        [SerializeField]
        ItemTranslator _translator;

        [SerializeField]
        ReelData _menu;

        List<SmallTable> _tables;
        ObjectPool _pool;
        // Start is called before the first frame update
        void Awake() {
            _tables = GetComponentsInChildren<SmallTable>().ToList();
            _pool = GetComponent<ObjectPool>();
        }

        public void On10Seconds() {
            int cost = 0;
            foreach (var table in _tables) {
                cost += table.UpdateCustomerState(_translator, _menu);
            } _gold.AddValue(cost);
        }

        //public void BuyChef() {
        //    _chefs.Add(_pool.Get().GetComponent<Chef>());
        //}
    }
}