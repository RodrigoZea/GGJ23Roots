using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    //public Image playerImage;
    public Text resultText;
    public List<Sprite> spriteList = new List<Sprite>();
    public List<GameObject> livesList = new List<GameObject>();
    public List<GameObject> doorAnimators = new List<GameObject>(); 
    public Image playerImage;
    [SerializeField] private GameInfo gameInfo;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {        
        if (gameInfo.gameResult == true) {
            winScreen();
        } else {
            loseScreen();
        }

        showLives();
    }

    private void hideUI() {
        resultText.gameObject.SetActive(false);
        playerImage.gameObject.SetActive(false);
    }

    private void winScreen() {
        resultText.text = "NICE!";
        playerImage.sprite = spriteList[0];
        // next scene (cutscene/game/whatever)
    }

    private void loseScreen() {
        resultText.text = "NOOOOO";
        playerImage.sprite = spriteList[1];
        // replay game
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2f) {
            hideUI();
            doorAnimators[0].GetComponent<Animator>().SetBool("slideD", true);
            doorAnimators[1].GetComponent<Animator>().SetBool("slideD", true);
            //doorAnimators[1].
        }
    }

    void showLives() {
        switch(gameInfo.lives){
            case 1:
                livesList[1].SetActive(false);
                livesList[2].SetActive(false);
                break;
            case 2:
                livesList[2].SetActive(false);
                break;
        }
    }
}
