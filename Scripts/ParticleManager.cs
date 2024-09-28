using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private ParticleSystem pSystem;

    void Start()
    {
        pSystem = GetComponent<ParticleSystem>();
    }

    public void PlayParticles(Vector3 position)
    {
        transform.position = position;
        pSystem.Play();
    }
}
