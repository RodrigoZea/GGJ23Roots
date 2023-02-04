using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceMovement : MonoBehaviour
{
    private NavMeshAgent enemy;
    public Transform playerTarget;
    public TimerManager timer;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.getCanPlay()) {
            enemy.isStopped = false;
            enemy.SetDestination(playerTarget.position);
        } else  {
            enemy.isStopped = true;
        }
    }
}
