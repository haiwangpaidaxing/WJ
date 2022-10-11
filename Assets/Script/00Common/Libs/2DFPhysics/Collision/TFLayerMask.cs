using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TF
{
    [System.Serializable]
    public struct TFLayerMask
    {
        public int mask;

        public static implicit operator int(TFLayerMask mask)
        {
            return mask.mask;
        }

        public static implicit operator TFLayerMask(int intVal)
        {
            TFLayerMask layerMask;
            layerMask.mask = intVal;
            return layerMask;
        }
    }
}
