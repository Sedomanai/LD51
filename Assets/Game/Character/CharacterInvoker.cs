using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Elang
{
    public class CharacterInvoker : MonoBehaviour
    {
        [SerializeField]
        TextProInteger _gold;

        [SerializeField]
        ItemTranslator _translator;

        List<Chef> _chefs;
        List<Waiter> _waiters;
        ObjectPool _pool;
        // Start is called before the first frame update
        void Awake() {
            _chefs = GetComponentsInChildren<Chef>().ToList();
            _waiters = GetComponentsInChildren<Waiter>().ToList();
            _pool = GetComponent<ObjectPool>();
        }

        public void On10Seconds() {
            int cost = 0;
            foreach (var chef in _chefs) {
                chef.On10Seconds(_translator);
                cost += 2;
            }
            foreach (var waiter in _waiters) {
                cost += 1;
            }
            cost += 2;

            _gold.AddValue(-cost);

            if (_gold.Value < 0) {
                _gold.SetValue(0);
                var game = _translator.GetComponent<Game>();
                game.ChangeState(Game.State.GameOver);
            } else if (_gold.Value >= 300) {
                var game = _translator.GetComponent<Game>();
                game.ChangeState(Game.State.Finished);
            }
        }

        public void BuyChef() {
            _chefs.Add(_pool.Get().GetComponent<Chef>());
        }
    }
}