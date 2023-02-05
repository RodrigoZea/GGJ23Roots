using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private bool disabledAttack = false;
    private AudioSource sound;
    [SerializeField]
    private AudioSource sound2;
    [SerializeField]
    PlayerGrab player;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Enemies" && !disabledAttack){
            if (player.rockyLife > 0 )
                player.rockyLife--;
                sound.Play();
                sound2.Play();
                StartCoroutine(attackDelay());
            Debug.Log(player.rockyLife);

        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Enemies"){
            Debug.Log("Exit");
        }
    }

     IEnumerator attackDelay(){
        disabledAttack = true;
        yield return new WaitForSeconds(1.0f);
        disabledAttack = false;
    }
}
