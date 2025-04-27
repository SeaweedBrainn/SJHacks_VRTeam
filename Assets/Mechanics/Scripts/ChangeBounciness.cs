using UnityEngine;
using System.Collections;

public class ChangeBounciness : MonoBehaviour
{
    public GameObject oneObject;
    private Collider objectCollider; 
    private PhysicsMaterial originalMaterial; // (correct spelling: PhysicMaterial)

    void Start()
    {
        objectCollider = oneObject.GetComponent<Collider>();

        if (objectCollider != null)
        {
            // 🛑 NOT material
            originalMaterial = objectCollider.sharedMaterial;
        }
    }

    public void onButtonPress()
    {
        if (originalMaterial != null)
        {
            originalMaterial.bounciness = 0f;
        }
    }
}