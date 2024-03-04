using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDes3 : MonoBehaviour
{
    public DescriptionController descriptionController; 

    void OnMouseEnter()
    {
        if (descriptionController != null)
        {
            descriptionController.ChangeCount(3);
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
