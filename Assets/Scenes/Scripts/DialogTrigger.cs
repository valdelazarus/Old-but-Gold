using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    private GameObject dialogManager;
    public string[] sentences;

    void OnTriggerEnter(Collider other)
    {
        GameObject.Find("DialogManager").GetComponent<Dialog>().AddSentences(sentences);
    }
}
