using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedal : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioSource sound;
    [SerializeField]
    private AudioSource sound2;
    [SerializeField]
    private AudioClip uhoh, police;
    private bool started = false;
    private int times = 1;
    private int count = 0;
    public TimerManager timer;
    public GameInfo gameInfo;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.hasChanged && timer.getCanPlay())
        {
            if (times == 0){
            count++;
            if (count == 1){
                gameInfo.gameLose();
                timer.stopTimer();
                timer.setCanPlay(false);
                sound.Play();
                sound2.Stop();
                sound2.clip = uhoh;
                sound2.Play();
                StartCoroutine(soundDelay());
            }
            } else {times --;}
            transform.hasChanged = false;
        }
    }

    IEnumerator soundDelay(){
        yield return new WaitForSeconds(4.7f);
        sound2.clip = police;
        sound2.Play();
        yield return new WaitForSeconds(1.0f);
        timer.startCountdown();
    }




}
