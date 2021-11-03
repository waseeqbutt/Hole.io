using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vasik
{
    public class CameraBehavior : MonoBehaviour
    {
        public Transform target;
        public float speed = 5f;
        public Vector2 offset;

        void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, 
                new Vector3(target.position.x + offset.x, transform.position.y, target.position.z + offset.y), 
                speed * Time.deltaTime);
        }
    }

}