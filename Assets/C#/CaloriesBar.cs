using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaloriesBar : MonoBehaviour
{

    private Slider slider;
    public Text CaloriesCounter;

    public GameObject PlayerStatistics;

    private float currentCalories, maxCalories;

    void Awake()
    {
        slider = GetComponents<Slider>()[0];
    }

    void Update()
    {
        currentCalories = PlayerStatistics.GetComponent<PlayerStatistics>().currentCalories;
        maxCalories = PlayerStatistics.GetComponent<PlayerStatistics>().maxCalories;

        if (currentCalories > maxCalories)
        {
            currentCalories = maxCalories;
        }
        else if (currentCalories < 0)
        {
            currentCalories = 0;
        }
        PlayerStatistics.GetComponent<PlayerStatistics>().currentCalories = currentCalories;

        float fillValue = currentCalories / maxCalories;
        slider.value = fillValue;

        CaloriesCounter.text = currentCalories + "/" + maxCalories;
    }
}
