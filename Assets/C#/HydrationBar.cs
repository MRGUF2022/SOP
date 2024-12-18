using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HydrationBar : MonoBehaviour
{
    private Slider slider;
    public Text HydrationCounter;

    public GameObject PlayerStatistics;

    private float currentHydration, maxHydration;


    void Awake()
    {
        slider = GetComponents<Slider>()[0];
    }

    void Update()
    {
        currentHydration = PlayerStatistics.GetComponent<PlayerStatistics>().currentHydrationPercent;
        maxHydration = PlayerStatistics.GetComponent<PlayerStatistics>().maxHydrationPercent;

        if (currentHydration > maxHydration)
        {
            currentHydration = maxHydration;
        }
        else if (currentHydration < 0)
        {
            currentHydration = 0;
        }
        PlayerStatistics.GetComponent<PlayerStatistics>().currentHydrationPercent = currentHydration;

        float fillValue = currentHydration / maxHydration;
        slider.value = fillValue;

        HydrationCounter.text = currentHydration + "/" + maxHydration;
    }
}
