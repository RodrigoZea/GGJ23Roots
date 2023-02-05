using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class PhoneGame : MonoBehaviour
{
    public Canvas gameCanvas;
    public Text keyShown;
    public GameInfo gameInfo;
    public TimerManager timerManager;
    private int timeLimit = 2;
    private float timer;
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
            float seconds = timer%60;
            if (timer >= seconds) {
                timer = 0;
                keyPressed = false;
            }   

            if (Input.GetKey(keyToPress) && !keyPressed) {
                if (keyToPress.ToString() == keyToShow) {
                    keyShown.color = Color.green;
                    keyPressed = true;
                } 
            } else if (Input.anyKey && !keyPressed) {
                keyShown.color = Color.red;
                keyPressed = true;
                
                timerManager.setCanPlay(false);
                timerManager.stopTimer(); 
                gameInfo.gameLose();
                timerManager.startCountdown();
            }
        }

        if (timerManager.returnTimerOver()) {
            timerManager.setCanPlay(false);
            timerManager.stopTimer();
            gameInfo.gameWin();
            timerManager.startCountdown();
        }
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
