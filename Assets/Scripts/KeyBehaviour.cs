/*
* Author: Tan Le Xuan
* Date: 17/06/25
* Description: Allows the player to collect a key, which is used to unlock specific doors.
*/


using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    [SerializeField]
    private Color highlightColor = Color.yellow; // Changeable in Inspector
    [SerializeField]
    AudioClip collectSound; // Sound played when the key is collected
    private Color originalColor; // Original color of the key for unhighlighting
    private Renderer keyRenderer; // Renderer for the key object
    AudioSource keyAudioSource; // Audio source for playing sounds
    
    void Start()
    {
        keyRenderer = GetComponentInChildren<Renderer>();
        if (keyRenderer != null)
        {
            // Use a unique material instance to avoid affecting others
            keyRenderer.material = new Material(keyRenderer.material);
            originalColor = keyRenderer.material.color;
        }
        keyAudioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// Highlights the key when the player is near.
    /// </summary>
    public void Highlight()
    {
        if (keyRenderer != null)
            keyRenderer.material.color = highlightColor;
    }
    /// <summary>
    /// Unhighlights the key when the player is no longer near.
    /// </summary>
    public void Unhighlight()
    {
        if (keyRenderer != null)
            keyRenderer.material.color = originalColor;
    }
    /// <summary>
    /// Handles the interaction when the player collects the key.
    /// Increments the player's key count and plays a sound.
    /// If the player has a key, it sets the hasKey flag to true.
    /// </summary>
    public void Collect(PlayerBehaviour player)
    {
        player.hasKey = true;
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        Destroy(gameObject);
    }
}