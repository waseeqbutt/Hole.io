using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vasik
{
    public class ZombieBehavior : CharacterBehavior
    {
        private float next = 0.0f;
        private bool targetFound = false;

        private void Update()
        {
            if(followTarget == null)
            {
                if (Time.time > next)
                {
                    targetFound = SearchTarget();

                    if (targetFound == true)
                        OnMove();

                    next = Time.time + 0.5f;
                }

                return;
            }

            if(state == State.Idle)
            {
                if (followTarget)
                    OnMove();
            }
            else
            if(state == State.Move)
            {
                if(Vector3.Distance(transform.position, followTarget.position) < 0.5f)
                {
                    OnAttack();
                }
            }
        }

        void FixedUpdate()
        {
            if (followTarget == null)
                return;

            if (state == State.Move)
            {
                m_rigidbody.velocity = transform.forward * (walkSpeed * 10f) * Time.fixedDeltaTime;
                transform.LookAt(new Vector3(followTarget.position.x, transform.position.y, followTarget.position.z));
            }
        }

        protected override bool SearchTarget()
        {
            layerMask = LayerMask.GetMask("Character");
            colliders = Physics.OverlapSphere(transform.position, 100f, layerMask);

            foreach(Collider obj in colliders)
            {
                if(obj.gameObject.CompareTag("Human"))
                {
                    followTarget = obj.transform;
                    return true;
                }
            }

            return false;
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
        }

        protected override void OnAttack()
        {
            state = State.Attack;
            animator.SetFloat("Movement", 0.0f);
            coroutine = StartCoroutine(ActionRoutine(0f));
        }

        protected override void OnDead()
        {
            animator.Play("Death");

            if(coroutine != null)
                StopCoroutine(coroutine);

            isAlive = false;
            this.tag = "Untagged";
            this.enabled = false;
        }

        protected override IEnumerator ActionRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            animator.Play("Attack");

            if (followTarget)
                followTarget.GetComponent<ICharacter>().OnDamage();

            followTarget = null;

            yield return new WaitForSeconds(2f);

            OnIdle();
        }

        public override void OnDeathTrigger()
        {
            OnDead();
        }

        public override void OnDamage()
        {
            throw new System.NotImplementedException();
        }
    }
}
