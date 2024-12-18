using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    private Slider slider;
    public Text healthCounter;

    public GameObject PlayerStatistics;

    private float currentHealth, maxHealth;


    void Awake()
    {
        slider = GetComponents<Slider>()[0];
    }

    void Update()
    {
        currentHealth = PlayerStatistics.GetComponent<PlayerStatistics>().currentHealth;
        maxHealth = PlayerStatistics.GetComponent<PlayerStatistics>().maxHealth;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        PlayerStatistics.GetComponent<PlayerStatistics>().currentHealth = currentHealth;

        float fillValue = currentHealth / maxHealth;
        slider.value = fillValue;

        healthCounter.text = currentHealth + "/" + maxHealth;
    }
}
