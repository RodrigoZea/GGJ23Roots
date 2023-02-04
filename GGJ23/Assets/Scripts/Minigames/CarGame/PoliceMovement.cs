using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceMovement : MonoBehaviour
{
    private NavMeshAgent enemy;
    public Transform playerTarget;
    public TimerManager timer;
    public float enemySpeed;
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
            enemy.speed = Mathf.SmoothStep(enemy.speed, enemySpeed, Time.deltaTime * 12f);
            //enemy.acceleration = enemyAcceleration*Time.deltaTime;
        } else  {
            enemy.isStopped = true;
        }
    }
}
