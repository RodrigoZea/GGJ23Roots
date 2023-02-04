using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    //public Image playerImage;
    public Text resultText;
    [SerializeField] private GameInfo gameInfo;
    // Start is called before the first frame update
    void Start()
    {
        if (gameInfo.gameResult == true) {
            winScreen();
        } else {
            loseScreen();
        }
    }

    private void winScreen() {
        resultText.text = "NICE!";
    }

    private void loseScreen() {
        resultText.text = "NOOOOO";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
