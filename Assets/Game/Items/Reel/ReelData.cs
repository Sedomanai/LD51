using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elang
{

    [CreateAssetMenu(fileName = "Reel Data", menuName = "Ludum Dare 51/Reel Data", order = 11)]
    public class ReelData : ScriptableObject
    {
        public List<Sprite> sprites;
    }
}
