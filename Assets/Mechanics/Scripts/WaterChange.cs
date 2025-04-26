using UnityEngine;

public class WaterChange : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject); // Destroy this object
    }
}