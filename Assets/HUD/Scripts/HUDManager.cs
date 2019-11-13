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
    private static Image speedImage;
    private static Image strengthImage;
    private static Slider healthBarSlider;
    private static Text coinBarText;

    // Start is called before the first frame update
    void Start()
    {
        healthBarSlider = healthBar.GetComponent<Slider>();
        coinBarText = coinBar.GetComponentInChildren<Text>();
        speedImage = speedBar.GetComponentInChildren<Image>();
        strengthImage = strengthBar.GetComponentInChildren<Image>();
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
