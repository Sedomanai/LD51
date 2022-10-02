using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang
{
    public interface IUtility {
        public BinaryStateRenderer State { get; set; }
        public ReelData ReelData { get;}
    }

    public class Fridge : MonoBehaviour, IUtility
    {
        [SerializeField]
        ReelData _reelData;

        public ReelData ReelData { get { return _reelData; } }
        public BinaryStateRenderer State { get; set; }

        void Awake() {
            State = GetComponentInChildren<BinaryStateRenderer>();
        }
    }
}