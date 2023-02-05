using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public float forceAmount = 500;

    Rigidbody selectedRigidbody;
    Camera targetCamera;
    Vector3 originalScreenTargetPosition;
    Vector3 originalRigidbodyPos;
    float selectionDistance;
    [SerializeField]
    private AudioSource sound1;
    [SerializeField]
    private AudioSource sound2;
    [SerializeField]
    private AudioClip party, ah;
    public TimerManager timer;
    private bool flag = true;
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
            if (Input.GetMouseButtonUp(0) && selectedRigidbody)
            {
                //Release selected Rigidbody if there any
                selectedRigidbody = null;
            }
        }
    }

    void FixedUpdate()
    {
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
            if (hitInfo.collider.gameObject.GetComponent<Rigidbody>())
            {
                selectionDistance = Vector3.Distance(ray.origin, hitInfo.point);
                originalScreenTargetPosition = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));
                originalRigidbodyPos = hitInfo.collider.transform.position;
                if (hitInfo.collider.gameObject.tag == "WinTag"){
                    Debug.Log("Winner");
                    gameInfo.gameWin();
                    timer.stopTimer();
                    timer.setCanPlay(false);
                    sound2.volume = 1.0f;
                    sound1.clip = ah;
                    sound2.clip = party;
                    sound1.Play();
                    sound2.Play();
                    StartCoroutine(soundDelay(3.0f));
                }
                return hitInfo.collider.gameObject.GetComponent<Rigidbody>();
            }
        }

        return null;
    }

    IEnumerator soundDelay(float t){
        yield return new WaitForSeconds(t);
        timer.startCountdown();
    }
}
