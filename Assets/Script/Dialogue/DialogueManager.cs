using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;//Display or hide
    public Text dialogueText, nameText;
    public static bool dialogeEnd = false;

    [TextArea(1, 3)]
    public string[] dialagueLines;
    [SerializeField] private int currentLine = 0;

    public bool isbreak = true;

    private void OnEnable()
    {
        currentLine = 0;
        dialogueText.text = dialagueLines[currentLine];
        dialogueBox.SetActive(false);
    }

    private void LateUpdate()
    {
        Debug.Log(dialogueBox.active);
        if (dialogueBox.active && (Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            currentLine++;
            dialogueText.text = dialagueLines[currentLine - 2];
            if (currentLine == dialagueLines.Length)
            {
                dialogueBox.SetActive(false);
                dialogeEnd = true;
                Debug.Log("dialogeEnd = true;");

            }

        }
    }

}
