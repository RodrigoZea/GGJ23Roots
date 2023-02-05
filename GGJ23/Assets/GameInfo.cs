using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class GameInfo : ScriptableObject
{
    private List<string> levelNames = new List<string>() {"Cables", "CarGame", "PapersPlease"};
    public bool gameResult = false;
    public int lives = 3;
    public int currentScene = 0;

    public void gameLose() {
        gameResult = false;
        lives -= 1;
    }
    public void gameWin() {
        gameResult = true;
    }
    public void sumScene() {
        currentScene += 1;
    }

    public string returnScene() {
        string scene = "";
        int randomScene = 0;

        if (currentScene == 1 || currentScene == 2 || currentScene == 4) {
            scene = "CutsceneTemplate";
        } else if (currentScene == 3 || currentScene == 5) {
            randomScene = Random.Range(0, levelNames.Count);
            scene = levelNames[randomScene];
        }

        return scene;
    }


}
