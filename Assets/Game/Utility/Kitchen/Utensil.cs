using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Elang
{

    public class Utensil : Utility, IUtility
    {
        [SerializeField]
        ItemTranslator _translator;

        public new Vector3 Offset { get { return new Vector3(0.0f, 0.8f, 0.0f); } }
        public new FacingDirection Direction { get { return FacingDirection.Down; } }
        public new bool Processable(IItem item, out bool ignoreDupe) {
            ignoreDupe = false;
            return item != null && item.Preparable;
        }
        public new IItem TryMixItem(IItem item) {
            SoundMgr.Instance.PlaySFX("prepare");
            return _translator.irlToItem[item.Prepared];
        }
    }

}