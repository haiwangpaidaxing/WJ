using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WMAStar
{
    public class ASNodeBase
    {
        public ASNodeBase Connction { get; private set; }
        public float F { get; private set; }
        public float G { get; private set; }
        public float H { get; private set; }

        public void SetConnction(ASNodeBase nodeBase) =>
            Connction = nodeBase;

        public void SetG(float f) =>
            this.F = f;
        public void SetH(float h) =>
            this.H = h;
    }
    [System.Serializable]
    public class ASNode
    {
        public ASNode parent;
        public float F { get; private set; }
        public float G { get; private set; }
        public float H { get; private set; }
        public Vector2Int pos;
        public GameObject gameObject;
        public void SetF(Vector2 setF)
        {
            //
         //   G = pos - setF;
        }
        public void SetG(float f) =>
            this.F = f;
        public void SetH(float h) =>
            this.H = h;
    }



}
