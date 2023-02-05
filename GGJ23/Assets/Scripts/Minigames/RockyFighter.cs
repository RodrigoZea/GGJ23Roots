using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockyFighter : MonoBehaviour
{
    private float timer, punchTimer;
    private bool punched, isPunching;
    private Animator rockyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        rockyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2f && !punched) {
            rockyAnimator.SetBool("punchLeft", true);
            isPunching = true; 
            if (isPunching) {
                punchTimer += Time.deltaTime;
                if (punchTimer >= 1.2f) {
                    timer = 0;
                    punchTimer = 0;
                    punched = true;
                    isPunching = false;
                    rockyAnimator.SetBool("punchLeft", false);
                }
            }
        }else if (timer >= 2f && punched) {
            rockyAnimator.SetBool("punchRight", true);
            isPunching = true;
            if (isPunching) {
                punchTimer += Time.deltaTime;
                if (punchTimer >= 1.2f) {
                    timer = 0;
                    punchTimer = 0;
                    punched = false;
                    isPunching = false;
                    rockyAnimator.SetBool("punchRight", false);
                }
            }
        }
    }
}
