using UnityEngine;

[CreateAssetMenu]
public class GameInfo : ScriptableObject
{
    public bool gameResult = false;
    public int lives = 3;

    public void gameLose() {
        gameResult = false;
        lives -= 1;
    }
    public void gameWin() {
        gameResult = true;
    }
}
