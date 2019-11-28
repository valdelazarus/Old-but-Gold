using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    string sceneToLoad;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();    
    }


    public void LoadScene(string sceneName)
    {
        sceneToLoad = sceneName;    
        anim.SetTrigger("loadScene");
    }
    public void LoadSelectedScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
