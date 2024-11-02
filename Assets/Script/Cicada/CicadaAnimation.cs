using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CicadaAnimation : MonoBehaviour
{
    private Animator anim;
    public GameObject CicadaSprite;
    public GameObject Cicada;
    public GameObject Dialogue;
    public Sprite Cicada02;
    public int FirCount = 4;
    public int SecCount = 3;
    private int clickCount = 0;
    bool DialogeFlag = false;
    bool CicadaFlag = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Cicada.SetActive(false);
        Dialogue.SetActive(false);
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            clickCount++;
            anim.ResetTrigger("IsClick");
            anim.SetTrigger("IsClick");
            anim.SetInteger("clickCount", clickCount);
            //Debug.Log(clickCount);
            if (clickCount > FirCount && !DialogeFlag)
            {
              GetComponent<SpriteRenderer>().sprite = Cicada02;
                
                
            }
            
            if (clickCount > FirCount + SecCount)
            {
                
                if (!DialogeFlag)
                {
                    Cicada.SetActive(true);
                    Dialogue.SetActive(true);
                    DialogeFlag = true;

                }
                    //CicadaSprite.SetActive(false);
                   

            }
            //if(clickCount  SecCount)
        }
        if (DialogueManager.dialogeEnd && !CicadaFlag)
        {
            Cicada.GetComponent<Animator>().SetTrigger("Falling");
            CicadaFlag = true;
            
            //Debug.Log("SetTrigger(\"Falling\");");
        }

    }

}
