/*
* Author: Tan Le Xuan
* Date: 17/06/25
* Description: Deals damage to the player when they come into contact with hazard objects like fire, acid, or spikes.
*/

using UnityEngine;

public class HazardBehaviour : MonoBehaviour
{
    
    /// <summary>
    /// Damage dealt by fire hazards.
    /// </summary>
    [SerializeField]
    public int fireDamage = 10;

    /// <summary>
    /// Damage dealt by acid hazards.
    /// </summary>
    [SerializeField]
    public int acidDamage = 30;

    /// <summary>
    /// Damage dealt by spike hazards.
    /// </summary>
    [SerializeField]
    public int spikesDamage = 20;

    AudioSource hazardAudioSource;

    void Start()
    {
        hazardAudioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Handles the interaction when the player enters a hazard trigger.
    /// Applies damage based on the type of hazard.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        hazardAudioSource.Play();
        if (other.CompareTag("Player"))
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                // Check the type of hazard and apply damage accordingly
                if (gameObject.CompareTag("Fire"))
                {
                    player.ModifyHealth(fireDamage);
                }
                else if (gameObject.CompareTag("Acid"))
                {
                    player.ModifyHealth(acidDamage);
                }
                else if (gameObject.CompareTag("Spikes"))
                {
                    player.ModifyHealth(spikesDamage);
                }
            }
        }
    }
}