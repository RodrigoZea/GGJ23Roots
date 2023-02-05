using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneGame : MonoBehaviour
{
    public Canvas gameCanvas;
    public Text keyShown;
    public GameInfo gameInfo;
    public Image imageResult;
    public Image imageNormal;
    public List<Sprite> sprites = new List<Sprite>();
    public TimerManager timerManager;
    private int timeLimit = 2;
    private float timer=0;
    private int randomKeyIndex;
    private KeyCode keyToPress;
    private bool keyPressed;
    private string keyToShow;
    private int randomX, randomY;
    private float originalX, originalY;
    private KeyCode[] validKeys = new[] {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.I, 
        KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U,
        KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z
    };
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("dispatchKey");
    }

    // Update is called once per frame
    void Update()
    {
        if (timerManager.getCanPlay()) {
            keyShown.gameObject.SetActive(true);
            timer += Time.deltaTime;

            float seconds = timeLimit%60;

            if (timer >= seconds) {
                if (!keyPressed) {
                    onLose();
                } 
            }else {
                if (keyPressed) {
                    timer = 0;
                    keyPressed = false;
                }
            }

            if (Input.GetKey(keyToPress) && !keyPressed) {
                if (keyToPress.ToString() == keyToShow) {
                    keyShown.color = Color.green;
                    keyPressed = true;

                    timer = 0;
                } 
            } else if (Input.anyKey && !keyPressed) {
                onLose();
            }
        }

        if (timerManager.returnTimerOver()) {
            timerManager.setCanPlay(false);

            imageNormal.gameObject.SetActive(false);
            imageResult.sprite = sprites[0];
            imageResult.gameObject.SetActive(true);

            timerManager.stopTimer();
            gameInfo.currentLevel = 4;
            timerManager.startCountdown();
            keyShown.gameObject.SetActive(false);
            StopCoroutine("dispatchKey");
        }
    }

    private void onLose() {
        keyShown.color = Color.red;
        timerManager.setCanPlay(false);
        imageNormal.gameObject.SetActive(false);
        imageResult.sprite = sprites[1];
        imageResult.gameObject.SetActive(true);
        timerManager.stopTimer(); 
        gameInfo.gameLose();
        timerManager.startCountdown();
        StopCoroutine("dispatchKey");
    }

    private IEnumerator dispatchKey() {
        while (true) {
            randomX = Random.Range(-200, 200);
            randomY = Random.Range(-200, 200);
            Vector3 currentPosition = new Vector3(randomX, randomY, 0);
            keyShown.rectTransform.localPosition = currentPosition;

            keyShown.color = Color.white;
            randomKeyIndex = Random.Range(0, validKeys.Length);
            keyToPress = validKeys[randomKeyIndex];

            keyToShow = keyToPress.ToString();
            keyShown.text = keyToShow;
            timeLimit = Random.Range(2, 3);
            yield return new WaitForSecondsRealtime(timeLimit);
        }
    }
}
