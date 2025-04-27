using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MopItUp : MonoBehaviour
{
    public float shrinkSpeed = 0.5f; // How fast the object shrinks
    private bool isShrinking = false; // Is the object currently shrinking?
    private DecalProjector decal; // Reference to DecalProjector

    void Start()
    {
        decal = GetComponent<DecalProjector>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mop"))
        {
            isShrinking = true; // Start shrinking when touched by mop
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Mop"))
        {
            isShrinking = false; // Stop shrinking when mop leaves
        }
    }

    void Update()
    {
        if (isShrinking && decal != null)
        {
            // Shrink decal size
            Vector3 newSize = decal.size - new Vector3(1, 1, 0) * shrinkSpeed * Time.deltaTime;

            // Clamp each axis to minimum 0
            newSize.x = Mathf.Max(newSize.x, 0);
            newSize.y = Mathf.Max(newSize.y, 0);
            newSize.z = Mathf.Max(newSize.z, 0); // Just in case (even though you don't shrink Z)

            decal.size = newSize;

            // If decal is basically gone, deactivate
            if (newSize.x < 0.05f && newSize.y < 0.05f)
            {
                gameObject.SetActive(false); // Deactivate the object
            }
        }
    }
}