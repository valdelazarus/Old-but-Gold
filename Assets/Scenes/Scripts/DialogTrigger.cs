using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public string[] sentences;

    void OnTriggerEnter(Collider other)
    {
        GameObject.Find("DialogManager").GetComponent<DialogManager>().AddSentences(sentences);
        Destroy(gameObject);
    }
}
