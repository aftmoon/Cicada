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
            if (clickCount > FirCount)
            {
              GetComponent<SpriteRenderer>().sprite = Cicada02;
                
            }
            if (clickCount > FirCount + SecCount)
            {
                Cicada.SetActive(true);
                //CicadaSprite.SetActive(false);
                Dialogue.SetActive(true);

            }
        }
        if (DialogueManager.dialogeEnd)
        {
            Cicada.GetComponent<Animator>().SetTrigger("Falling");
            Debug.Log("SetTrigger(\"Falling\");");
        }

    }

}
