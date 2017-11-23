using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

using GGL;

namespace MOBA
{
    [RequireComponent(typeof(Camera))]
    public class AIAgentDirector : MonoBehaviour
    {
        public LayerMask hitLayers;
        public float rayDistance = 1000f;
        public AIAgent[] agentsToDirect;

        private Camera cam;
        private Transform selectionPoint;

        void Awake()
        {
            cam = GetComponent<Camera>();
        }

        void Start()
        {
            GameObject g = new GameObject("Target Location");
            selectionPoint = g.transform;
        }

        void Update()
        {
            // Is mouse button down?
            if (Input.GetMouseButtonDown(0))
            {
                Ray camRay = cam.ScreenPointToRay(Input.mousePosition); // Calculate ray from camera
                RaycastHit rayHit;
                // Perform raycast
                if (Physics.Raycast(camRay, out rayHit, rayDistance, hitLayers))
                {
                    NavMeshHit navHit;
                    // Perform nav mesh sampling (detects if ray is on nav mesh)
                    if (NavMesh.SamplePosition(rayHit.point, out navHit, rayDistance, -1))
                    {
                        // Set the new position to the hit one on the navmesh
                        selectionPoint.position = navHit.position;
                        // Assign the target to all the agents
                        AssignTargetToAllAgents(selectionPoint);
                    }
                }
            }
        }

        void AssignTargetToAllAgents(Transform target)
        {
            foreach (var agent in agentsToDirect)
            {
                // Seek
                Seek s = agent.GetComponent<Seek>();
                // Is there a seek component on the agent?
                if (s != null)
                {
                    s.target = target; // Assign target
                }

                // Path Following
                PathFollowing p = agent.GetComponent<PathFollowing>();
                // Is PathFollowing attached to agent?
                if (p != null)
                {
                    p.target = target; // Assign target to PathFollowing component on agent
                }
            }
        }
    }
}