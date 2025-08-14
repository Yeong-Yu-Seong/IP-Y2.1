using UnityEngine;

public class StarEffectTrigger : MonoBehaviour
{
    public ParticleSystem starEffect; // Assign in Inspector

    // Call this method when the player stops the NPC
    public void OnNpcCaught(Vector3 position)
    {
        if (starEffect != null)
        {
            starEffect.transform.position = position;
            starEffect.Play();
        }
    }
}
