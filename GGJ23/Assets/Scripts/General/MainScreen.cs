using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameInfo gameInfo;
    public float speed = 3;

    public void loadGame() {
        gameInfo.sumScene();
        SceneManager.LoadScene("CutsceneTemplate");
    }

    public void closeGame() {
        Application.Quit();
    }


}
