using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    public static SelectionManager Instance {get; set;}


    public bool onTarget;

    public GameObject selectedObject;


    public GameObject interaction_Info_UI;
    Text interaction_text;


    public Image canterDotimage;
    public Image handlcon;

    public bool handIsVisible;

    public GameObject selectedTree;
    public GameObject chopHolder;


    private void Start()
    {
        onTarget = false;
        interaction_text = interaction_Info_UI.GetComponent<Text>();
    }
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

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();


            ChugbulTree chugbulTree = selectionTransform.GetComponent<ChugbulTree>();

            if(chugbulTree && chugbulTree.playerInRange)
            {
                chugbulTree.canBeChopped = true;
                selectedTree = chugbulTree.gameObject;
                chopHolder.gameObject.SetActive(true);
            }
            else
            {
                if (selectedTree != null)
                {
                    selectedTree.gameObject.GetComponent<ChugbulTree>().canBeChopped = false;
                    selectedTree = null;
                    chopHolder.gameObject.SetActive(false);
                }
            }




            if (interactable && interactable.playerInRange)
            {

                onTarget = true;
                selectedObject = interactable.gameObject;
                interaction_text.text = interactable.GetItemName();
                interaction_Info_UI.SetActive(true);


                if (interactable.CompareTag("pickable")) 
                {

                    canterDotimage.gameObject.SetActive(false);
                    handlcon.gameObject.SetActive(true);


                    handIsVisible = true;


                }
                else
                {
                    
                    handlcon.gameObject.SetActive(false);
                    canterDotimage.gameObject.SetActive(true);


                    handIsVisible = false;


                }
            }
            else
            {
                onTarget = false;
                interaction_Info_UI.SetActive(false);
                handlcon.gameObject.SetActive(false);
                canterDotimage.gameObject.SetActive(true);

                handIsVisible = false;

            }
        }
        else
        {
            onTarget = false;
            interaction_Info_UI.SetActive(false);
            handlcon.gameObject.SetActive(false);
            canterDotimage.gameObject.SetActive(true);

            handIsVisible = false;
        }
    }


    public void DisableSelection()
    {
        handlcon.enabled = false;
        canterDotimage.enabled = false;
        interaction_Info_UI.SetActive(false);

        selectedObject = null;
    }

    public void EnabledSelection()
    {
        handlcon.enabled = true;
        canterDotimage.enabled = true;
        interaction_Info_UI.SetActive(true);



    }
}
