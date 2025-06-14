using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    bool isOpen = false;
    public void Interact()
    {
        if (isOpen == true)
        {
            Vector3 doorRotation = transform.eulerAngles;
            doorRotation.y += 90f;
            transform.eulerAngles = doorRotation;
            isOpen = false;
        }

        else if (isOpen == false)
        {
            Vector3 doorRotation = transform.eulerAngles;
            doorRotation.y -= 90f;
            transform.eulerAngles = doorRotation;
            isOpen = true;
        }
    }
}