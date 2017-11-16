using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBA
{
    [RequireComponent(typeof(AIAgent))]
    public class SkeletonArcherAnim : MonoBehaviour
    {
        public Animator anim;

        private AIAgent aiAgent;

        void Start()
        {
            aiAgent = GetComponent<AIAgent>();
            aiAgent.updatePosition = false;
        }

        void Update()
        {
            AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
            if (!state.IsName("spawn"))
            {
                aiAgent.updatePosition = true;                  // Update position
                float moveSpeed = aiAgent.velocity.magnitude;   // Set the MoveSpeed of the anim
                anim.SetFloat("MoveSpeed", moveSpeed);
            }
        }
    }
}