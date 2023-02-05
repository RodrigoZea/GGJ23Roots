using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class License : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TimerManager timer;
    [SerializeField]
    private AudioSource sound1,sound2;
    [SerializeField]
    private AudioClip policeChatter, uhoh;
    private bool flag = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.returnTimerOver() && flag){
            flag = false;
            sound1.Stop();
            sound2.Stop();
            sound1.volume = 1.0f;
            sound1.clip = policeChatter;
            sound2.volume = 1.0f;
            sound2.clip = uhoh;
            sound1.Play();
            sound2.Play();
            StartCoroutine(soundDelay());
        }
    }

    IEnumerator soundDelay(){
        yield return new WaitForSeconds(1.0f);
        timer.startCountdown();
    }
}
