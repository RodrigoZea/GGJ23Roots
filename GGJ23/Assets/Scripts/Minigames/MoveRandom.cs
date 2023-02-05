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
    [SerializeField]
    private ParticleSystem confetti;
    // Start is called before the first frame update
    private float startTimeScale;
    private float startFixedDeltaTime;
    [SerializeField]
    private float slowTimeScale;
    [SerializeField]
    private AudioSource sound;
    [SerializeField]
    private AudioSource sound2;
    [SerializeField]
    private AudioClip electricity;
    float lastDirectionChange = 0;
    Vector3 randomDirection;
    private bool moving = true;
    public TimerManager timer;
    void Start()
    {   
        sound2.volume = 0.1f;
        rb = GetComponent<Rigidbody>();
        rbp = player.GetComponent<Rigidbody>();
        rb.AddForce(-transform.up * moveSpeed, ForceMode.Impulse);
        rbp.AddForce(-player.transform.up * 100, ForceMode.Impulse);
        startTimeScale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;

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
        if (moving && timer.getCanPlay())
            rb.AddForce(new Vector3(Mathf.Abs(randomDirection.x), 0, randomDirection.z/5)* moveSpeed, ForceMode.Impulse);

    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player" && timer.getCanPlay()){
            Debug.Log(other.gameObject.name);
            confetti.Play();
            confetti.Play();
            sound.Play();
            sound2.volume = 0.5f;
            sound2.clip = electricity;
            sound2.Stop();
            sound2.Play();
            moving = false;
            Time.timeScale = slowTimeScale;
            Time.fixedDeltaTime = startFixedDeltaTime * slowTimeScale;
            timer.setStatus(true);
            timer.setCanPlay(false);
            timer.stopTimer(); 
            timer.startCountdown();
        }
    }
}
