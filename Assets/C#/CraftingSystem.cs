using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public GameObject CraftingScreenUI;
    public GameObject ToolScreenUI, SurvivalScreenUI, RefinedScreenUI;

    public List<string> inventoryItemList;

    Button toolsBTN, SurvivaBTN, RefinedBTN;

    Button craftAxeBTN, craftPlankBTN;

    Text AxeReq1, AxeReq2, PlankRep1;

    public bool isOpen;

    private Blueprint AxeBLP = new Blueprint("Axe", 2, "Stone", 3, "Stick", 3);
    private Blueprint PlankBLP = new Blueprint("Plank", 1, "Log", 1, "", 0);


    public static CraftingSystem Instance { get; set; }

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

        toolsBTN = CraftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });

        SurvivaBTN = CraftingScreenUI.transform.Find("SurvivalButton").GetComponent<Button>();
        SurvivaBTN.onClick.AddListener(delegate { OpenSurvivaCategory(); });

        RefinedBTN = CraftingScreenUI.transform.Find("RefinedButton").GetComponent<Button>();
        RefinedBTN.onClick.AddListener(delegate { OpenRefinedCategory(); });


        //Axe
        AxeReq1 = ToolScreenUI.transform.Find("Axe").transform.Find("Rep1").GetComponent<Text>();
        AxeReq2 = ToolScreenUI.transform.Find("Axe").transform.Find("Rep2").GetComponent<Text>();

        craftAxeBTN = ToolScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate { CraftAnyIlem(AxeBLP); });


        //Plank
        PlankRep1 = RefinedScreenUI.transform.Find("Plank").transform.Find("Rep1").GetComponent<Text>();

        craftPlankBTN = RefinedScreenUI.transform.Find("Plank").transform.Find("Button").GetComponent<Button>();
        craftPlankBTN.onClick.AddListener(delegate { CraftAnyIlem(PlankBLP); });
    }

    void OpenToolsCategory()
    {
        CraftingScreenUI.SetActive(false);
        ToolScreenUI.SetActive(true);
        SurvivalScreenUI.SetActive(false);
        RefinedScreenUI.SetActive(false);
    }

    void OpenSurvivaCategory()
    {
        CraftingScreenUI.SetActive(false);
        ToolScreenUI.SetActive(false);
        SurvivalScreenUI.SetActive(true);
        RefinedScreenUI.SetActive(false);
    }

    void OpenRefinedCategory()
    {
        CraftingScreenUI.SetActive(false);
        ToolScreenUI.SetActive(false);
        SurvivalScreenUI.SetActive(false);
        RefinedScreenUI.SetActive(true);
    }

    void CraftAnyIlem(Blueprint blueprintToCraft)
    {


        InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);



        if (blueprintToCraft.numOfRequirements == 1)
        {
            InventorySystem.Instance.Removeltem(blueprintToCraft.Rep1, blueprintToCraft.Req1amount);

        }
        else if (blueprintToCraft.numOfRequirements == 2)
        {
            InventorySystem.Instance.Removeltem(blueprintToCraft.Rep1, blueprintToCraft.Req1amount);
            InventorySystem.Instance.Removeltem(blueprintToCraft.Rep2, blueprintToCraft.Req2amount);

        }


        StartCoroutine(calculate());


    }

    public IEnumerator calculate()
    {
        yield return 0;
        InventorySystem.Instance.ReCalculeList();
        RefreshNeededitems();
    }





    void Update()
    {


        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            CraftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;



            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            CraftingScreenUI.SetActive(false);
            ToolScreenUI.SetActive(false);
            SurvivalScreenUI.SetActive(false);
            RefinedScreenUI.SetActive(false);

            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                SelectionManager.Instance.EnabledSelection();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
            }

            isOpen = false;
        }
    }

    public void RefreshNeededitems()
    {
        int stone_count = 0;
        int stick_count = 0;
        int log_count = 0;

        inventoryItemList = InventorySystem.Instance.itemList;

        foreach (string itemName in inventoryItemList)
        {

            switch (itemName)
            {
                case "Stone":
                    stone_count++;
                    break;
                case "Stick":
                    stick_count++;
                    break;
                case "Log":
                    log_count++;
                    break;
            }

        }
        // Axe

        AxeReq1.text = "3 Stone [" + stone_count + "]";
        AxeReq2.text = "3 Stick [" + stick_count + "]";

        if (stone_count >= 3 && stick_count >= 3 && InventorySystem.Instance.CheckSlotsAvailable(1))
        {
            craftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            craftAxeBTN.gameObject.SetActive(false);
        }

        // Plank

        PlankRep1.text = "1 Log [" + log_count + "]";

        if (log_count >= 1 && InventorySystem.Instance.CheckSlotsAvailable( 2 )) 
        {
            craftPlankBTN.gameObject.SetActive(true);

        }
        else
        {
            craftPlankBTN.gameObject.SetActive(false);

        }
    }
}
