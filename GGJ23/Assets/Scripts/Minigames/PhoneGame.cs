using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class PhoneGame : MonoBehaviour
{
    public Canvas gameCanvas;
    public Text keyShown;
    public TimerManager timerManager;
    private int timeLimit = 2;
    private float timer;
    private int randomKeyIndex;
    private KeyCode keyToPress;
    private bool keyPressed;
    private string keyToShow;
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
            timer += Time.deltaTime;

            if (timer >= timeLimit) {
                timer = 0;
                keyPressed = false;
                keyShown.color = Color.black;
            }

            if (Input.GetKey(keyToPress) && !keyPressed) {
                if (keyToPress.ToString() == keyToShow) {
                    Debug.Log("good");
                    keyShown.color = Color.green;
                    keyPressed = true;
                } else  {
                    Debug.Log("bad");
                    keyShown.color = Color.red;
                    keyPressed = true;
                }
            } 
        }


       /* if (timer >= timeLimit) {
            // Dispatch a key

            }

            int randomX = Random.Range(0, Screen.width);
            int randomY = Random.Range(0, Screen.height);
            Vector2 position = new Vector2(randomX, randomY);

            if (randomX < 0){
                int xToMove = keyShown.gameObject.transform.x;
                keyShown.rectTransform.position = xToMove;
                if ()
            }
        }*/
    }

    private IEnumerator dispatchKey() {
        for (float f = 1f; f >= 0; f -= 0.1f) {
            randomKeyIndex = Random.Range(0, validKeys.Length);
            keyToPress = validKeys[randomKeyIndex];

            keyToShow = keyToPress.ToString();
            keyShown.text = keyToShow;
            yield return new WaitForSecondsRealtime(Random.Range(1, 2));
        }
    }
}
