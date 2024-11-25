using UnityEngine;

public class FireworkEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem firework;

    public void PlayFirework()
    {
        if (firework != null)
        {
            firework.Play();
        }
    }
}

