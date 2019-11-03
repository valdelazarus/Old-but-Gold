using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject coinBar;
    private static Slider healthBarSlider;
    private static Text coinBarText;

    // Start is called before the first frame update
    void Start()
    {
        healthBarSlider = healthBar.GetComponent<Slider>();
        coinBarText = coinBar.GetComponentInChildren<Text>();
    }

    public static void RemoveHealth(float amount)
    {
        healthBarSlider.value -= amount;

        if (healthBarSlider.value == 0)
        {
            PlayerController.anim.SetBool("isDead",true);
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
}
