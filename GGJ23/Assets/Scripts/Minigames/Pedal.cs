using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedal : MonoBehaviour
{
    // Start is called before the first frame update
    private bool started = false;
    private int times = 1;
    void Start()
    {
        StartCoroutine(Initiate());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.hasChanged && started)
        {
            if (times == 0){
            print("Pedal");
            } else {times --;}
            transform.hasChanged = false;
        }
    }

    IEnumerator Initiate(){
        yield return new WaitForSeconds(1.5f);
        started = true;
    }


}
