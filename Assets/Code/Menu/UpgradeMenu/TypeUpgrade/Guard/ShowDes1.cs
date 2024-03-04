using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDes1 : MonoBehaviour
{
    public DescriptionController descriptionController; 

    void OnMouseEnter()
    {
        if (descriptionController != null)
        {
            descriptionController.ChangeCount(1);
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
