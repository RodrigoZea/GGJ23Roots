using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public float forceAmount = 500;
    public int rockyLife = 3;
    public int playerLife = 5;
    Rigidbody selectedRigidbody;
    Camera targetCamera;
    Vector3 originalScreenTargetPosition;
    Vector3 originalRigidbodyPos;
    float selectionDistance;
    public TimerManager timer;
    public GameInfo gameInfo;
    void Start()
    {
        targetCamera = GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.getCanPlay()){
            if (!targetCamera)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                //Check if we are hovering over Rigidbody, if so, select it
                selectedRigidbody = GetRigidbodyFromMouseClick();
            }
            if (rockyLife == 0 || Input.GetKey(KeyCode.A)){
                gameInfo.gameWin();
                gameInfo.currentScene = 11;
                timer.stopTimer();
                timer.setCanPlay(false);
                timer.startCountdown();
                
            }
            if (playerLife == 0){
                timer.stopTimer();
                timer.setCanPlay(false);
                timer.startCountdown();
            }
        }
    }

    private void FixedUpdate() {
        if (selectedRigidbody)
        {
            Vector3 mousePositionOffset = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)) - originalScreenTargetPosition;
            selectedRigidbody.velocity = (originalRigidbodyPos + mousePositionOffset - selectedRigidbody.transform.position) * forceAmount * Time.deltaTime;
        }
    }

    Rigidbody GetRigidbodyFromMouseClick()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out hitInfo);
        if (hit)
        {
            if (hitInfo.collider.gameObject.GetComponent<Rigidbody>() && hitInfo.collider.gameObject.tag == "Player")
            {
                selectionDistance = Vector3.Distance(ray.origin, hitInfo.point);
                originalScreenTargetPosition = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));
                originalRigidbodyPos = hitInfo.collider.transform.position;
                return hitInfo.collider.gameObject.GetComponent<Rigidbody>();
            }
        }

        return null;
    }

    IEnumerator soundDelay(){
        yield return new WaitForSeconds(3.0f);
        timer.startCountdown();
    }
}
