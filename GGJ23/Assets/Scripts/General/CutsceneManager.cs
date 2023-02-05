using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public Image loreImage;
    public List<Sprite> loreImages = new List<Sprite>();
    public Text loreText;
    public GameInfo gameInfo;
    // Start is called before the first frame update
    void Start()
    {
        getScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getScene() {
        switch (gameInfo.currentScene){
            case 1:
                loreImage.sprite = loreImages[0];
                loreText.text = "samply samply sample text";
            break;
            case 2:
                loreImage.sprite = loreImages[0];
                loreText.text = "second samply sample text";
                break;
        }
    }

    public void loadScreen() {
        gameInfo.sumScene();
        string sceneName = gameInfo.returnScene();
        SceneManager.LoadScene(sceneName);
    }


}
