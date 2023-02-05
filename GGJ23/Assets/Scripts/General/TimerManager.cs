using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    [Header("Minigame UI")]
    public Text minigameInstruction;
    public Text minigameTimerText;
    public Image minigameProgressBar;
    public Image minigameTimerImage;
    public Image playerExpression;
    public List<Sprite> spriteList = new List<Sprite>();
    [Header("Minigame Parameters")]
    [SerializeField] 
    private int timer = 10;
    private int originalTimer;
    [SerializeField]
    private string instruction = "Do something!";
    private bool canPlay = false;
    private bool timerOver = false;
    private bool hasLost;
    private bool timerStopped = false;
    private bool startTransitionTimer = false;
    private float tranisitionTimer = 0f;
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
        showTimer(false);
        // First we show the minigame instructions
        StartCoroutine("instructionAnimation");
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0) {
            timerOver = true;
        }
        if (startTransitionTimer) {
            tranisitionTimer += Time.deltaTime;
        }

        if (tranisitionTimer >= 2f) {
            loadTransitionScreen();
        }
    }

    // Game related
        // Timer
    public void startCountdown() {
        startTransitionTimer = true;
    }

    public bool returnTimerOver() {
        return timerOver;
    }

    void showTimer(bool show) {
        minigameTimerImage.gameObject.SetActive(show);
        minigameTimerText.gameObject.SetActive(show);
        minigameProgressBar.gameObject.SetActive(show);
        playerExpression.gameObject.SetActive(show);
    }

    public void loadTransitionScreen() {
        SceneManager.LoadScene("TransitionScreen");
    }

    IEnumerator startTimer() {
        while (timer >= 1) {
            timer -= 1;
            minigameTimerText.text = timer.ToString() + "s";
            float progress = Mathf.InverseLerp(0, originalTimer, timer);
            minigameProgressBar.fillAmount = progress;
            changeSprite(progress);
            yield return new WaitForSecondsRealtime(1f);   
        }
        canPlay = false;
        setStatus(true);
        Time.timeScale = 1;
    }

    private void changeSprite(float progress) {
        if (progress <= 1 && progress >= 0.7) {
            playerExpression.sprite = spriteList[0];
        } else if (progress <= 0.6 && progress >= 0.4) {
            playerExpression.sprite = spriteList[1];
        } else if (progress <= 0.3 && progress >= 0) {
            playerExpression.sprite = spriteList[2];
        }
    }

    public bool getCanPlay() {
        return canPlay;
    }

    public void setCanPlay(bool canPlayP) {
        canPlay = canPlayP;
    }

    public void stopTimer () {
        StopCoroutine("startTimer");
        StartCoroutine("loseTimer");
    }

    private IEnumerator loseTimer() {
        showTimer(false);

        //Play lose sfx
        yield return new WaitForSecondsRealtime(2f);

        // Send to lose screen
    }

    private IEnumerable winTimer() {
        showTimer(false);

        // Play win sfx
        yield return new WaitForSecondsRealtime(2f);

        // Send to win screen
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
        showTimer(true);
        StartCoroutine("startTimer");
    }

    public void setStatus(bool status) {
        hasLost = status;
    }

    
    // When either the game win condition or the timer reaches 0, transition to screen win or lose
    // Transition screen should be its own separate scene
}
