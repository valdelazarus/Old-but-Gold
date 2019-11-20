using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject coinBar;
    public GameObject speedBar;
    public GameObject strengthBar;
    public GameObject savedPeople;
    public static int numberOfCaptured;
    private GameObject [] captured;
    private static int saved=0;
    private static Image speedImage;
    private static Image strengthImage;
    private static Slider healthBarSlider;
    private static Text coinBarText;
    private static Text savedPeopleText;

    // Start is called before the first frame update
    void Start()
    {
        numberOfCaptured = 0;
        saved = 0;

        //find all npcs
        captured= GameObject.FindGameObjectsWithTag("capturedNPC");
        foreach(GameObject npc in captured)
        {
            numberOfCaptured++;

        }


        healthBarSlider = healthBar.GetComponent<Slider>();
        coinBarText = coinBar.GetComponentInChildren<Text>();
        savedPeopleText = savedPeople.GetComponentInChildren<Text>();
        speedImage = speedBar.GetComponentInChildren<Image>();
        strengthImage = strengthBar.GetComponentInChildren<Image>();

        savedPeopleText.text = (saved + "/" + numberOfCaptured);
    }

    public static void AddSaved()
    {
        saved++;

        savedPeopleText.text = (saved+"/"+numberOfCaptured); 
    }

    public static void RemoveHealth(float amount)
    {
        healthBarSlider.value -= amount;

        if (healthBarSlider.value <= 0)
        {
            PlayerController.anim.SetBool("isDead",true);
            FindObjectOfType<LevelManager>().LoadScene("Game Over");
        }
    }
    public static void AddHealth(float amount)
    {
        healthBarSlider.value += amount;
    }


    public static void AddCoins(int amount)
    {
        MysteryEventTrigger.coinTotal++;
        coinBarText.text = (int.Parse(coinBarText.text) + amount).ToString();
    }

    public static void DisplaySpeedPowerup()
    {
        speedImage.gameObject.SetActive(true);
    }

    public static void HideSpeedPowerup()
    {
        speedImage.gameObject.SetActive(false);
    }

    public static void DisplayStrengthPowerup()
    {
        strengthImage.gameObject.SetActive(true);
    }

    public static void HideStrengthPowerup()
    {
        strengthImage.gameObject.SetActive(false);
    }
}
