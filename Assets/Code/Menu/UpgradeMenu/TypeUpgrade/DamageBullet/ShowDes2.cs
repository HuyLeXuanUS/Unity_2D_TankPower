using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDes2 : MonoBehaviour
{
    public DescriptionController descriptionController;  

    void OnMouseEnter()
    {
        if (descriptionController != null)
        {
            descriptionController.ChangeCount(2);
        }
    }

    void OnMouseExit()
    {
        if (descriptionController != null)
        {
            descriptionController.ChangeCount(0);
        }
    }
}
