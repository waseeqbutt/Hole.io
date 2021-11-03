using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace vasik
{
    public class EntityCollision : MonoBehaviour
    {
        public CollisionEvent collisionEvent;

        private Entity entity;
        private AudioSource audioSource;

        [System.Serializable]
        public class CollisionEvent : UnityEvent<int> { }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            entity = other.GetComponent<Entity>();
            audioSource.PlayOneShot(entity.soundFX, 1.0f);
            ShowScore(entity.score);
        }

        private void ShowScore(int score)
        {
            collisionEvent.Invoke(score);
        }
    }
}