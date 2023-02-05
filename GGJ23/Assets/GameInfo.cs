using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class GameInfo : ScriptableObject
{
    private List<string> levelNames = new List<string>() {"Cables", "CarGame", "PapersPlease", "PhoneGame", "FightTest"};
    public bool gameResult = false;
    public int currentLevel = 0;
    public int lives = 3;
    public int currentScene = 0;
    public string previousScene = "";

    public void gameLose() {
        gameResult = false;
        lives -= 1;
    }
    public void gameWin() {
        gameResult = true;
        currentLevel++;

    }
    public void sumScene() {
        currentScene += 1;
    }

    public string returnScene() {
        string scene = "";
        int randomScene = 0;

        if (currentScene == 1 || currentScene == 2 || currentScene == 4) {
            scene = "CutsceneTemplate";
        } else {
            scene = levelNames[currentLevel];
        }
        Debug.Log(scene);
        return scene;
    }


}
