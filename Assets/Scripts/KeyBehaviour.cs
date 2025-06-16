using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    [SerializeField]
    private Color highlightColor = Color.yellow;
    [SerializeField]
    AudioClip collectSound;
    private Color originalColor;
    private Renderer keyRenderer;
    AudioSource keyAudioSource;

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

    public void Highlight()
    {
        if (keyRenderer != null)
            keyRenderer.material.color = highlightColor;
    }

    public void Unhighlight()
    {
        if (keyRenderer != null)
            keyRenderer.material.color = originalColor;
    }

    public void Collect(PlayerBehaviour player)
    {
        player.hasKey = true;
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        Destroy(gameObject);
    }
}