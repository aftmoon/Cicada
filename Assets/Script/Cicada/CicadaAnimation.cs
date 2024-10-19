using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicadaAnimation : MonoBehaviour
{
    private Animator anim;
    public bool breakEnd = false;
    public int count = 0;
    private int clickCount = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!breakEnd && (Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space)) && count < 4)
        {
           clickCount++;
           if(clickCount > 3)
            {
                clickCount = 0;
                //SetAnimation();
                anim.SetBool("break", true);
                anim.SetTrigger("click");
                //breakEnd = true;
                count++;
            }
        }
        else
        {
            anim.SetBool("break", false);
            //Debug.Log(count);
            if(count >= 3)
            {
                breakEnd = true;
            }
        }

    }

    //public void SetAnimation()
    //{

    //}
}
