using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBA
{
    [RequireComponent(typeof(PathFollowing))]
    public class Patrol : MonoBehaviour
    {
        public AIAgentPatrolSelector patrolSelector;

        private int currentPoint = 0; // The current patrol point the agent is PathFollowing to
        private PathFollowing pathFollowing; // Reference to the attached PathFollowing script
        private List<Transform> patrolPoints; // List of patrol point (referring to the one in the patrolSelector)


        // Use this for initialization
        void Start ()
        {
            pathFollowing = GetComponent<PathFollowing>();
	    }
	
	    // Update is called once per frame
	    void Update ()
        {
            // Is there currently a patrol selector?
            if (patrolSelector != null)
            {
                patrolPoints = patrolSelector.patrolPoints; // Grab patrol points list from selector
                if (patrolPoints.Count > 0)
                {
                    // Is the agent at the target?
                    if (pathFollowing.isAtTarget)
                    {
                        // Reset the currentNode the agent seeks to
                        pathFollowing.currentNode = 0;
                        // Move to next patrol point
                        currentPoint++;
                    }
                    // If currentPoint goes outside to patrolPoints
                    if (currentPoint >= patrolPoints.Count)
                    {
                        currentPoint = 0; // Loop back at start
                    }

                    Transform point = patrolPoints[currentPoint];
                    pathFollowing.target = point;
                }
            }
        }
    }
}
