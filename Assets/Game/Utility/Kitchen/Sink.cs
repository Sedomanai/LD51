using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Elang
{
    public class Sink : Utility, IUtility
    {
        public new Vector3 Offset { get { return new Vector3(0.0f, 0.2f, 0.0f); } }
        public new FacingDirection Direction { get { return FacingDirection.Down; } }

        public new bool Processable(IItem item, out bool ignoreDupe) { 
            ignoreDupe = false; 
            return item != null; 
        }

        public new bool TryInsertingItem(IItem item, out bool stopMoving) { 
            stopMoving = true; 
            return true; 
        }

        public new Item TryCookingItem(ItemTranslator translator) { return null; }
    }
}