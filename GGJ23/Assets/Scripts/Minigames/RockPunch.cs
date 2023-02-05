using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPunch : MonoBehaviour
{
    // Start is called before the first frame update
    bool disabledAttack = false;
    private AudioSource sound;
    [SerializeField]
    private AudioSource sound2;
    [SerializeField]
    private PlayerGrab player;
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player" && !disabledAttack){
            sound.Play();
            sound2.Play();
            if (player.playerLife > 0)
                player.playerLife--;
            StartCoroutine(attackDelay());
        }
    }

    IEnumerator attackDelay(){
        disabledAttack = true;
        yield return new WaitForSeconds(1.5f);
        disabledAttack = false;
    }
}
