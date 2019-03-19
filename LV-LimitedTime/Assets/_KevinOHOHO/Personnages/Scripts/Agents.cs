using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agents : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform spawn;
    public Transform myDestination;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToDestination();
    }

    // Update is called once per frame
    void Update()
    {
        CheckEndDestination();
    }

    private void GoToDestination()
    {
        agent.destination = myDestination.position;
    }

    private void CheckEndDestination()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Debug.Log(gameObject.name + " arrived");
                    agent.destination = spawn.position;
                }
            }
        }
    }
}
