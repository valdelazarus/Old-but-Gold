using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryDialog : MonoBehaviour
{
    string[] sentences; //set in code only

    DialogManager dialogManager;
    bool dialogTriggered;
    Animator anim;

    float initialSpeed;

    private void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        anim = GetComponent<Animator>();
        initialSpeed = FindObjectOfType<PlayerController>().speed;
    }
    
    private void Update()
    {
        if (dialogTriggered && !dialogManager.dialogPanel.activeInHierarchy)
        {
            //make traveler disappear once done with dialogs
            anim.SetTrigger("Idle");
        }
    }
    public void SetDialogueSentences(string[] sentences)
    {
        this.sentences = sentences;
    }

    //animation event - activate dialog at the end of appearing animation
    public void TriggerDialog()
    {
        SetPlayerSpeed(0);

        dialogManager.ShowDialog(sentences);
        dialogTriggered = true;
    }

    public void Resume()
    {
        
        SetPlayerSpeed(initialSpeed);
        anim.SetBool("Talk", false);

    }

    void SetPlayerSpeed(float speed)
    {
        FindObjectOfType<PlayerController>().speed = speed;
    }
}
