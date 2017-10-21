using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Inheritance
{
    [RequireComponent(typeof(Rigidbody))]
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy")]
        public Transform target;
        public int health = 100;
        public int damage = 20;
        public float attackRate = 5f;
        public float attackDuration = 2f;
        public float attackRadius = 10f;

        protected NavMeshAgent nav;
        protected Rigidbody rigid;

        private float attackTimer = 0f;

        private void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            rigid = GetComponent<Rigidbody>();
        }

        protected virtual void Update()
        {
            if (target == null)
                return;

            // Set navigation to follow target
            nav.SetDestination(target.position);
            attackTimer += Time.deltaTime;

            if(attackTimer >= attackRate)
            {
                float distance = Vector3.Distance(transform.position, target.position);

                if (distance <= attackRadius)
                {
                    // IF distance is within attack range
                    Attack();                                       // call Attack()
                    attackTimer = 0f;                               // Reset attackTimer
                    StartCoroutine(AttackDelay(attackDuration));    // StartCoroutine AttackDelay
                }
            }
        }

        protected virtual void Attack(){ }

        protected virtual void OnAttackEnd() { }

        IEnumerator AttackDelay(float delay)
        {
            // Happens instantly
            nav.Stop();                                   // STOP nav
            yield return new WaitForSeconds(delay);
            nav.Resume();                                 // RESUME nav
            // Happens after delay
            OnAttackEnd();
        }
    }

}
