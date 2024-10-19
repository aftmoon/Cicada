using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;//Display or hide
    public Text dialogueText, nameText;
    public bool dialogeEnd = false;

    [TextArea(1, 3)]
    public string[] dialagueLines;
    [SerializeField] private int currentLine;

    public bool isbreak = true;
    //private bool sleep = false;
    CicadaAnimation cicadaAnimation;

    private void Start()
    {
        dialogueText.text = dialagueLines[currentLine];
        cicadaAnimation = FindAnyObjectByType<CicadaAnimation>();
        dialogueBox.SetActive(false);

    }

    private void Update()
    {
        //Debug.Log(dialogeEnd);
        if (cicadaAnimation != null)
        {
            isbreak = !cicadaAnimation.breakEnd;
        }

        if(!isbreak && !dialogeEnd) {
            //if (!sleep)
            //{
            //    System.Threading.Thread.Sleep(200);
            //    sleep = true;
            //}
            dialogueBox.SetActive(true);
        }


        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space))
            {
                currentLine++;
                if (currentLine < dialagueLines.Length)
                {
                    dialogueText.text = dialagueLines[currentLine];
                }
                else
                {
                    dialogueBox.SetActive(false);//box hide
                    dialogeEnd = true;
                }

            }
        }
        
    }
}
