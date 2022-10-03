using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

namespace Elang
{

    public class Fridge : Utility, IUtility
    {
        [SerializeField]
        ReelData _reelData;
        public new ReelData ReelData { get { return _reelData; } }
        public new Vector3 Offset { get { return new Vector3(-0.25f, -0.75f, 0.0f); } }

        public new FacingDirection Direction { get { return FacingDirection.Up; } }

        public new bool Processable(IItem item, out bool ignoreDupe) {
            ignoreDupe = false;
            return (item == null);
        }
    }
}