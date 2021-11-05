using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vasik
{
    public class RespawnManager : MonoBehaviour
    {
        public static RespawnManager Instance;

        void Awake()
        {
            Instance = null;
        }

        public void AddRespawnObject(GameObject obj, Vector3 startPosition)
        {

        }
    }

}