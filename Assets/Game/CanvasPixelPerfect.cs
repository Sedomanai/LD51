using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


namespace Elang
{
    public class CanvasPixelPerfect : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake() {
            var canvas = GetComponent<Canvas>();
            canvas.pixelPerfect = true;
        }

    }


}