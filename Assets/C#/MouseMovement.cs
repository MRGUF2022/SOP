using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float YRotation = 0f;

    void Start()
    {
        //Låser markøren til midten af ​​skærmen og gør den usynlig
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        if (!InventorySystem.Instance.isOpen && !CraftingSystem.Instance.isOpen)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            //styre rotation omkring x-aksen (se op og ned)
            xRotation -= mouseY;

            //vi klemmer rotationen, så vi ikke kan overrotere (som i det virkelige liv)
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //styre rotation omkring y-aksen (se op og ned)
            YRotation += mouseX;

            //anvender begge rotationer
            transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
        }
    }
}