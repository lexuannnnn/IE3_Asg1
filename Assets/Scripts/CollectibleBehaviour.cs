using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    // Coin value that will be added to the player's score
    [SerializeField]
    int collectibleValue = 1; // Score for collectible count

    private Renderer collectibleRenderer;
    private Color originalColor;
    public Color highlightColor = Color.yellow; // Changeable in Inspector

    void Start()
    {
        collectibleRenderer = GetComponent<Renderer>();

        // Optional but recommended: use unique material instance
        collectibleRenderer.material = new Material(collectibleRenderer.material);

        originalColor = collectibleRenderer.material.color;
    }

    // Method to collect the coin
    public void Collect()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerBehaviour playerScript = player.GetComponent<PlayerBehaviour>();
            if (playerScript != null)
            {
                playerScript.ModifyCount(collectibleValue);
            }
        }
        Destroy(gameObject); // Destroy the coin object
    }

    // Highlight the coin (e.g. change its color)
    public void Highlight()
    {
        if (collectibleRenderer != null)
        {
            collectibleRenderer.material.color = highlightColor;
        }
    }

    // Remove the highlight from the coin
    public void Unhighlight()
    {
        if (collectibleRenderer != null)
        {
            collectibleRenderer.material.color = originalColor;
        }
    }
}
