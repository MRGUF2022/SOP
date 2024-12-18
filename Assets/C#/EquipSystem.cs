using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }

    // -- UI -- //
    public GameObject quickSlotsPanel;

    public List<GameObject> quickSlotsList = new List<GameObject>();

    public GameObject NumberHolde;

    public int selectedNumber = -1;
    public GameObject selectedIten;

    public GameObject ToolHolders;

    public GameObject selectedItemModel;


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


    private void Start()
    {
        PopulateSlotList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        { 
            SelectquickSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectquickSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectquickSlot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectquickSlot(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectquickSlot(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectquickSlot(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectquickSlot(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectquickSlot(8);
        }
    }

    void SelectquickSlot(int number)
    {
        if (CheckIfSlotIsFull(number) == true)
        {
            
            


            if (selectedNumber != number)
            {
                selectedNumber = number;

                

                if (selectedIten != null)
                {
                    selectedIten.gameObject.GetComponent<InventoryItem>().isEquipped = false;
                }

                selectedIten = GetSelectedItem(number);
                selectedIten.GetComponent<InventoryItem>().isEquipped = true;

                SetEquippedModel(selectedIten);


                foreach (Transform child in NumberHolde.transform)
                {
                    child.transform.Find("Text").GetComponent<Text>().color = Color.black;
                }
                Text toBeChanged = NumberHolde.transform.Find("number" + number).transform.Find("Text").GetComponent<Text>();
                toBeChanged.color = Color.white;
            }
            else
            {
                selectedNumber = -1;

                

                if (selectedIten != null)
                {
                    selectedIten.gameObject.GetComponent<InventoryItem>().isEquipped = false;
                    selectedIten = null;
                }

                if (selectedItemModel != null)
                {
                    DestroyImmediate(selectedItemModel.gameObject );
                    selectedItemModel = null;
                }

                foreach (Transform child in NumberHolde.transform)
                {
                    child.transform.Find("Text").GetComponent<Text>().color = Color.black;
                }
            }

        } 
    }


    private void SetEquippedModel(GameObject selectedIten)
    {

        if (selectedItemModel != null)
        {
            DestroyImmediate(selectedItemModel.gameObject);
            selectedItemModel = null;
        }


        string selectedItemName = selectedIten.name.Replace("(Clone)", "");
        selectedItemModel = Instantiate(Resources.Load<GameObject>(selectedItemName + "_Model"),
            new Vector3(2f, -0.58f, 0.45f), Quaternion.Euler(-11.105f, 184.01f, -0.774f));
        selectedItemModel.transform.SetParent(ToolHolders.transform, false);
    }


    GameObject GetSelectedItem(int slotNumber)
    {
        return quickSlotsList[slotNumber - 1].transform.GetChild(0).gameObject;
    }

    bool CheckIfSlotIsFull(int slotNumber)
    {

        if (quickSlotsList[slotNumber-1].transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }



    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("quickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }

    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find next free slot
        GameObject availableSlot = FindNextEmptySlot();
        // Set transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);

        InventorySystem.Instance.ReCalculeList();

    }


    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {

        int counter = 0;

        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if (counter == 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}