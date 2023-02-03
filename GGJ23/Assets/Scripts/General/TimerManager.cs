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
    private bool instructionFinished = false;
    private bool instructionFaded = false;
    // Start is called before the first frame update
    void Start()
    {
        minigameInstruction.transform.localScale = new Vector3(7, 7, 0);
        minigameTimerImage.gameObject.SetActive(false);
        minigameProgressBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // First we show the minigame instructions
        if (!instructionFinished) {
            instructionAnimation();
        } else {
            if (!instructionFaded) {
                StartCoroutine("instructionFadingAnimation");
            }
            
        }
    }

    void instructionAnimation() {
        Vector3 instructionScale = minigameInstruction.transform.localScale;
        instructionScale.x -= Time.deltaTime*6;
        instructionScale.y -= Time.deltaTime*6;

        // if instruction reaches the appropiate size then end the animation
        if (instructionScale.x <= 1 && instructionScale.y <= 1) {
            instructionFinished = true;
        }
        minigameInstruction.transform.localScale = instructionScale;
    }

    IEnumerator instructionFadingAnimation() {
        for (float f = 1f; f >= 0; f -= 0.1f) 
            {
                Color c = minigameInstruction.color;
                c.a = f;
                minigameInstruction.color = c;
                yield return new WaitForSeconds(.1f);
            }
        instructionFaded = true;
        minigameInstruction.gameObject.SetActive(false);
    }

    // Then the timer starts running
    // When either the game win condition or the timer reaches 0, transition to screen win or lose
    // Transition screen should be its own separate scene
}
