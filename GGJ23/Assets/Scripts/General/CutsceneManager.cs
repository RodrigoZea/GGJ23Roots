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
                loreText.text = "How did this happen? How did this city even begin to burn? How did I end in such a mess? Oh well, guess I'll try to remember what happened until I get to the beginning of it all..";
            break;
            case 2:
                loreImage.sprite = loreImages[1];
                loreText.text = "Right. I remember. I just tried doing my best fixing this city's electrical power but I messed up completely and ended up causing a fire. Definitely not my brightest move, if I have to admit. Well I guess simple mistakes happen to everyone right?";
                break;
            case 4:
                loreImage.sprite = loreImages[2];
                loreText.text = "Now, this, erm, issue with the electrical power. It wasn't entirely my fault. I was being chased by some cops for some dumb reason I can't quite remember, one of those dumb and useless cops had the bright idea to crash directly into the city's main power plant";
                break;
            case 6:
                loreImage.sprite = loreImages[3];
                loreText.text = "Oh right! Now that I remember, it MIGHT have been my fault. I was trying to find my driver's ID because a cop asked me for it. I ran into some trouble while I was searching it in my car though..";
                break;
            case 8:
                loreImage.sprite = loreImages[4];
                loreText.text = "I think I'm starting to remember the events a bit better. I was asked for the cops for my ID because I was talking on the phone with my dumb boss and I was going way too fast going to work. Oh well, as if that even went right, my boss said 'you're fired, loser', and I answered right back at him and said 'no! you're the loser and you stink!'";
                break;
            case 10:
                loreImage.sprite = loreImages[5];
                loreText.text = "Now, why was I going too fast on the highway? Of course, it was that damn rock. We had quite a showdown but he kept chasing after me even after I defeated him. Wait a minute, why was I even fighting a buff rock?";
                break;
            case 11:
                loreImage.sprite = loreImages[6];
                loreText.text = "I REMEMBER! IT ALL STARTED HERE! I was accused of murdering someone because a rock made him trip, and knocked him unconscious. Of course, this rock happened to fall out of nowhere just to make my life a living hell. Oh well, that's how I got into this Deep Rooted Issue. Sorry for being cheesy, I just wanted to say the line.";
                break;
        }
    }

    public void loadScreen() {
        gameInfo.sumScene();
        string sceneName = gameInfo.returnScene();
        SceneManager.LoadScene(sceneName);
    }


}
