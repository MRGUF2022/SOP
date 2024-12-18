using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatistics : MonoBehaviour
{

    public static PlayerStatistics Instance { get; set; }


    public float currentHealth;
    public float maxHealth;


    public float currentCalories;
    public float maxCalories;

    float distanceTravelled = 0;
    Vector3 lastPosition;

    public GameObject playerBody;

    public float currentHydrationPercent;
    public float maxHydrationPercent;

    public bool isHydrationActive;



    private void Awake()
    {
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydrationPercent = maxHydrationPercent;




        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void start()
    {
        currentHealth = maxHealth;

        StartCoroutine(decreaseHydration()) ;

    }

    IEnumerator decreaseHydration()
    {
        while (true)
        {

            currentHydrationPercent -= 1;
            yield return new WaitForSeconds(10);


        }
    }


    void Update()
    {
        distanceTravelled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition = playerBody.transform.position;

        if (distanceTravelled >=5)
        {
            distanceTravelled = 0;
            currentCalories -= 1;
            currentHydrationPercent -= 1;
        }





        if (Input.GetKeyDown(KeyCode.N))
        {
            currentHealth -= 10;
        }
    }

    public void setHealth(float newHealth)
    {
        currentHealth = newHealth;  
    }

    public void setCalories(float newCalories)
    {
        currentCalories = newCalories;
    }

    public void setHydration(float newHydration)
    {
        currentHydrationPercent = newHydration;
    }

}
