using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private bool disabledAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Enemies" && !disabledAttack){
            Debug.Log("Enter");
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Enemies"){
            Debug.Log("Exit");
        }
    }

    IEnumerator soundDelay(){
        disabledAttack = true;
        yield return new WaitForSeconds(3.0f);
    }
}
