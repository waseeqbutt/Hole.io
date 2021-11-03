using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vasik
{
    public abstract class CharacterBehavior : MonoBehaviour, ICharacter
    {
        public enum State { Idle, Move, Attack, Dead }
        public State state;
        public float walkSpeed = 2f;
        public Animator animator;

        protected bool isAlive = true;
        protected Vector3 direction;
        protected Transform followTarget;
        protected Rigidbody m_rigidbody;
        protected Coroutine coroutine;
        protected Collider[] colliders;
        protected LayerMask layerMask;

        void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            state = State.Idle;
        }

        protected abstract void OnIdle();
        protected abstract void OnMove();
        protected abstract void OnDead();
        protected virtual void OnAttack() { }
        protected virtual bool SearchTarget() { return false; }

        protected abstract IEnumerator ActionRoutine(float delay);

        public abstract void OnDeathTrigger();

        public abstract void OnDamage();
    }

}