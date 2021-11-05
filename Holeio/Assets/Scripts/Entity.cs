using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vasik
{
    public class Entity : MonoBehaviour
    {
        public int score = 10;
        public AudioClip soundFX;

        private Vector3 initialPosition;
        private Quaternion initialRotation;
        private WaitForSeconds waitForSeconds;

        private void Awake()
        {
            waitForSeconds = new WaitForSeconds(10f);
        }

        private void OnEnable()
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }

        public IEnumerator Respawn()
        {
            yield return waitForSeconds;

            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }
    }
}
