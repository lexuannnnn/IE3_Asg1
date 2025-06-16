using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                player.hasKey = true;
                Destroy(gameObject); // Remove key after pickup
            }
        }
    }
}
