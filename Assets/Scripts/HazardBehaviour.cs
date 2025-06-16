using UnityEngine;

public class HazardBehaviour : MonoBehaviour
{
    [SerializeField]
    public int fireDamage = 10; // Damage dealt by the hazard
    [SerializeField]
    public int acidDamage = 30;
    [SerializeField]
    public int spikesDamage = 20; // Damage dealt by the hazard

    private void OnTriggerEnter(Collider other)
    {
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