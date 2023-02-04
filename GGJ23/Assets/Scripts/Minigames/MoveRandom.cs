using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandom : MonoBehaviour
{
    public float timeToDirectionChange = 1; // change direction every second
    public float moveSpeed = 5; // move 5 units per second
    private Rigidbody rb,rbp;
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    float lastDirectionChange = 0;
    Vector3 randomDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rbp = player.GetComponent<Rigidbody>();
        rb.AddForce(-transform.up * moveSpeed, ForceMode.Impulse);
        rbp.AddForce(-player.transform.up * 100, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() {
        if(Time.time - lastDirectionChange > timeToDirectionChange) {
            randomDirection = Random.insideUnitSphere; // generate a new random direction
            lastDirectionChange = Time.time;
        }
 
        // apply the direction every frame to the rigidbody
        //Debug.Log(randomDirection);
        rb.AddForce(new Vector3(Mathf.Abs(randomDirection.x), 0, randomDirection.z/5)* moveSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player"){
            Debug.Log(other.gameObject.name);
        }
    }
}
