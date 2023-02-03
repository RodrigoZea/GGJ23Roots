using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [Header("Minigame UI")]
    public Text minigameInstruction;
    public Text minigameTimerText;
    public Image minigameProgressBar;
    public Image minigameTimerImage;
    [Header("Minigame Parameters")]
    [SerializeField] 
    private int timer = 10;
    private int originalTimer;
    [SerializeField]
    private string instruction = "Do something!";
    private bool canPlay = false;
    private bool timerOver = false;
    // Start is called before the first frame update
    void Start()
    {
        // Setup values if there's any need for it
        minigameTimerText.text = timer.ToString();
        minigameInstruction.text = instruction;
        originalTimer = timer;
        // Setup for animation
        //      Make text big so then we can scale it down to animate it
        minigameInstruction.transform.localScale = new Vector3(7, 7, 0);
        //      Hide other UI elements meanwhile the text is being animated
        minigameTimerImage.gameObject.SetActive(false);
        minigameTimerText.gameObject.SetActive(false);
        minigameProgressBar.gameObject.SetActive(false);

        // First we show the minigame instructions
        StartCoroutine("instructionAnimation");
    }

    // Update is called once per frame
    void Update()
    {
        // If we can start the game, then do so. Timer has already been started.
        if (canPlay) {
            
        }
    }

    // Game related
        // Timer
    void showTimer() {
        minigameTimerImage.gameObject.SetActive(true);
        minigameTimerText.gameObject.SetActive(true);
        minigameProgressBar.gameObject.SetActive(true);
    }

    IEnumerator startTimer() {
        while (timer >= 1) {
            timer -= 1;
            minigameTimerText.text = timer.ToString();
            minigameProgressBar.fillAmount = Mathf.InverseLerp(0, originalTimer, timer);
            yield return new WaitForSecondsRealtime(1f);           
        }
        canPlay = false;
        
    }

    // Instruction animations
    IEnumerator instructionAnimation() {
        Vector3 instructionScale = minigameInstruction.transform.localScale;
        while (instructionScale.x >= 1 && instructionScale.y >= 1) {
            instructionScale.x -= Time.deltaTime*6;
            instructionScale.y -= Time.deltaTime*6;
            minigameInstruction.transform.localScale = instructionScale;
            yield return new WaitForSeconds(.01f);
        }
        StartCoroutine("instructionFadingAnimation");
    }

    IEnumerator instructionFadingAnimation() {
        for (float f = 1f; f >= 0; f -= 0.1f) {
            Color c = minigameInstruction.color;
            c.a = f;
            minigameInstruction.color = c;
            yield return new WaitForSeconds(.1f);
        }
        canPlay = true;
        minigameInstruction.gameObject.SetActive(false);
        showTimer();
        StartCoroutine("startTimer");
    }

    
    // When either the game win condition or the timer reaches 0, transition to screen win or lose
    // Transition screen should be its own separate scene
}
