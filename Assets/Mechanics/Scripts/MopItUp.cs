using System;
using UnityEngine;

public class MopItUp : MonoBehaviour
{
    public float shrinkSpeed = 0.5f; // How fast the object shrinks
    private bool isShrinking = false; // Is the object currently shrinking?

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
        if (isShrinking)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * shrinkSpeed);

            // If the object is small enough, destroy it
            if (transform.localScale.magnitude < 0.05f)
            {
                Destroy(gameObject);
            }
        }
    }
}