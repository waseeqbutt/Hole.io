using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vasik
{
    public class DestroyObject : MonoBehaviour
    {
        public float delay = 1.0f;

        private void OnEnable()
        {
            Destroy(gameObject, delay);
        }
    }

}