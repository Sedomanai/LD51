using UnityEngine;

namespace Elang
{
    public class Chef : Character, ICharacter
    {
        public void On10Seconds(ItemTranslator translator) {
            if (_activeUtility != null) {
                var item = _activeUtility.TryCookingItem(translator);
                if (item != null || _activeUtility is Sink) {
                    Item = item;
                    _activeUtility.Active = false;
                    _activeUtility = null;
                }
            }
        }

        public new Game.State ActivateUtility(GameObject utilObject, IUtility utility, Reel reel) {
            var ret = ActivateUtilityBody(utilObject, utility, reel);

            if (ret == Game.State.Basic)
                ret = Game.State.Chef;

            return ret;
        }
    }
}
