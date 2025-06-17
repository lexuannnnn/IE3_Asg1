/*
* Author: Tan Le Xuan
* Date: 17/06/25
* Description: Allows the player to collect items that increase score and progression.
*/

using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    [SerializeField]
    int collectibleValue = 1; // Score for collectible count
    [SerializeField]
    AudioClip collectSound; // Sound played when the collectible is collected

    private Renderer collectibleRenderer; // Renderer for the collectible object
    private Color originalColor; // Original color of the collectible for unhighlighting
    public Color highlightColor = Color.yellow; // Changeable in Inspector
    AudioSource collectibleAudioSource; // Audio source for playing sounds

    /// <summary>
    /// Initializes the collectible object.
    /// Sets up the renderer and audio source.
    /// </summary>
    void Start()
    {
        collectibleRenderer = GetComponent<Renderer>();

        // Optional but recommended: use unique material instance
        collectibleRenderer.material = new Material(collectibleRenderer.material);

        originalColor = collectibleRenderer.material.color;

        collectibleAudioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Handles the interaction when the player collects the collectible.
    /// Increments the player's collectible count and plays a sound.
    /// </summary>
    public void Collect()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerBehaviour playerScript = player.GetComponent<PlayerBehaviour>();
            if (playerScript != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
                playerScript.ModifyCount(collectibleValue);
            }
        }
        Destroy(gameObject); // Destroy the coin object
    }

    /// <summary>
    /// Highlights the collectible when the player is near.
    /// </summary>
    public void Highlight()
    {
        if (collectibleRenderer != null)
        {
            collectibleRenderer.material.color = highlightColor;
        }
    }

    /// <summary>
    /// Unhighlights the collectible when the player moves away.
    /// </summary>
    public void Unhighlight()
    {
        if (collectibleRenderer != null)
        {
            collectibleRenderer.material.color = originalColor;
        }
    }
}
