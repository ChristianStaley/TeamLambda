using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    

        public float cycleloop = 5f;
        public float cyclespeed;

        void Start()
        {
            cyclespeed = 0.1f / cycleloop * -1f;
        }

        void Update()
        {
            transform.Rotate(0, 0, cyclespeed, Space.World);
        }
    
}
