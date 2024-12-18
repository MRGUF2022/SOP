using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{

    public GameObject ItemInfoUI;

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;

    public List<GameObject> slotList = new List<GameObject>();

    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;

    private GameObject whatSlotToEquip;

    public bool isOpen;

    //public bool isFull;

    public GameObject pickupAlert;
    public Text pickupName;
    public Image pickupImage;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        isOpen = false;


        PopulateSlotList();

        Cursor.visible = false;

    }



    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }




    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;

            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.E) && isOpen)
        {
            inventoryScreenUI.SetActive(false);

            if (!CraftingSystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;


                SelectionManager.Instance.EnabledSelection();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;

            }


            isOpen = false;
        }
    }


    public void AddToInventory(string itemName)
    {
        whatSlotToEquip = FindNextEmptySlot();


        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);

        itemList.Add(itemName);



        TriggerPickupPopUp(itemName, itemToAdd.GetComponent<Image>().sprite);



        ReCalculeList();
        CraftingSystem.Instance.RefreshNeededitems();

    }

    void TriggerPickupPopUp(string itemName, Sprite itemSprite)
    {
        pickupAlert.SetActive(true);

        pickupName.text = itemName;

        pickupImage.sprite = itemSprite;

    }





    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {

            if (slot.transform.childCount == 0)
            {

                return slot;
            }
        }

        return new GameObject();
    }



    public bool CheckSlotsAvailable(int emptyMeeded)
    {
        int emptySlot = 0;

        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount <= 0)
            {
                emptySlot += 1;
            }
        }

        if (emptySlot >= emptyMeeded)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public void Removeltem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;

        for (var i = slotList.Count - 1; i >= 0; i--)
        {
            if (slotList[i].transform.childCount > 0)
            {
                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter != 0)
                {

                    Destroy(slotList[i].transform.GetChild(0).gameObject);

                    counter -= 1;
                }

            }
        }


        ReCalculeList();
        CraftingSystem.Instance.RefreshNeededitems();


    }

    public void ReCalculeList()
    {

        itemList.Clear();

        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {

                string name = slot.transform.GetChild(0).name;

                string str2 = "(Clone)";

                string result = name.Replace(str2, "");



                itemList.Add(result);
            }
        }
    }

}
