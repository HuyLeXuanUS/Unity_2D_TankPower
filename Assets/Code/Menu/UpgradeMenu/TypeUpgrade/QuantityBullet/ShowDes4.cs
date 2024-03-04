using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDes4 : MonoBehaviour
{
    public DescriptionController descriptionController; 

    void OnMouseEnter()
    {
        if (descriptionController != null)
        {
            descriptionController.ChangeCount(4);
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
