using UnityEngine;

public class MopItUp : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Mop"))
        {
            Destroy(gameObject); // Destroy this object
        }
    }
}