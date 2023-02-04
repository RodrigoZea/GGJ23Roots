using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Rigidbody sphere;
    public Transform kartModel;
    public TimerManager timer;
    public ParticleSystem particles;
    //public Transform kartNormal;
    float speed, currentSpeed;
    float rotate, currentRotate;
    public float gravity = 10f;
    public float steering = 80f;
    public float acceleration = 30f;

    [SerializeField] private GameInfo gameInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update() {
        if (timer.getCanPlay()) {
            //Follow Collider
            transform.position = sphere.transform.position - new Vector3(0, 0.4f, 0);

            //Accelerate
            if (Input.GetKey(KeyCode.W))
            speed = acceleration;

            //Steer
            if (Input.GetAxis("Horizontal") != 0)
            {
                int dir = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
                float amount = Mathf.Abs((Input.GetAxis("Horizontal")));
                Steer(dir, amount);
            }

            currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f); speed = 0f;
            currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f); rotate = 0f;
        }

    }

    private void FixedUpdate() {
        if (timer.getCanPlay()) {
            //Forward acceleration
            sphere.AddForce(kartModel.transform.forward * currentSpeed, ForceMode.Acceleration);
            // Gravity
            sphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
            // Steering
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);

            RaycastHit hitOn;
            RaycastHit hitNear;

            Physics.Raycast(transform.position, Vector3.down, out hitOn, 1.1f);
            Physics.Raycast(transform.position, Vector3.down, out hitNear, 2.0f);

            // Normal Rotation
            kartModel.parent.up = Vector3.Lerp(kartModel.parent.up, hitNear.normal, Time.deltaTime * 8.0f);
            kartModel.parent.Rotate(0, transform.eulerAngles.y, 0);
        }
    }

    public void Steer(int direction, float amount){
        rotate = (steering * direction) * amount;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemies"))
        {
            timer.setStatus(true);
            timer.setCanPlay(false);
            kartModel.gameObject.SetActive(false);
            particles.gameObject.SetActive(true);
            timer.stopTimer(); 
            particles.Play();
            gameInfo.gameLose();        
            timer.startCountdown();
        } else if (other.gameObject.CompareTag("WinTag")) {
            timer.setStatus(false);
            timer.setCanPlay(false);
            timer.stopTimer();
            gameInfo.gameWin();
            timer.startCountdown();
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
