using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vasik
{
    public class CivilianBehavior : CharacterBehavior
    {
        private void Start()
        {
            coroutine = StartCoroutine(ActionRoutine(Random.Range(0f, 2f)));
        }

        void FixedUpdate()
        {
            if(state == State.Move)
            {
                m_rigidbody.velocity = transform.forward * (walkSpeed * 10f) * Time.fixedDeltaTime;
            }
        }

        private void ChangeDirection()
        {
            transform.Rotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Random.Range(-90f, 90f),
                transform.eulerAngles.z), Space.World);
        }

        protected override void OnIdle()
        {
            state = State.Idle;
            animator.SetFloat("Movement", 0.0f);
        }

        protected override void OnMove()
        {
            state = State.Move;
            animator.SetFloat("Movement", 1.0f);
            ChangeDirection();
        }

        protected override void OnDead()
        {
            animator.Play("Death");
            StopCoroutine(coroutine);
            isAlive = false;
            this.tag = "Untagged";
            this.enabled = false;
        }

        protected override IEnumerator ActionRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            while (isAlive)
            {
                OnIdle();
                yield return new WaitForSeconds(2f);
                OnMove();
                yield return new WaitForSeconds(2f);
            }
        }

        public override void OnDeathTrigger()
        {
            OnDead();
        }

        public override void OnDamage()
        {
            OnDead();
        }
    }

}