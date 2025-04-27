using UnityEngine;
using System.Collections;

public class ChangeBounciness : MonoBehaviour
{
    public float delay = 3.0f; // Delay in seconds before changing the bounciness
    private Collider objectCollider; // The object's collider
    private PhysicsMaterial originalMaterial; // Reference to the original PhysicMaterial

    void Start()
    {
        // Get the collider component of the object
        objectCollider = GetComponent<Collider>();

        // Check if the collider exists and store the original material
        if (objectCollider != null)
        {
            originalMaterial = objectCollider.material;
        }

        // Start the coroutine to change bounciness after a delay
        if (originalMaterial != null)
        {
            StartCoroutine(ChangeBouncinessAfterDelay());
        }
    }

    private IEnumerator ChangeBouncinessAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Create a new PhysicMaterial with bounciness set to 0
        PhysicsMaterial noBounceMaterial = new PhysicsMaterial();
        noBounceMaterial.bounciness = 0f;

        // Apply the new material to the object's collider
        objectCollider.material = noBounceMaterial;

        Debug.Log("Bounciness changed to 0.");
    }
}