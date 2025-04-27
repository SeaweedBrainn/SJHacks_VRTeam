using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WaterChange : MonoBehaviour
{
    private DecalProjector Decal; // Reference to DecalProjector
    private MopItUp MopScript; 
    public float fadeSpeed = 0.5f; // Speed at which the fade occurs
    private bool beingHit = false;

    void Start()
    {
        // Get the DecalProjector component from the child object
        Decal = GetComponent<DecalProjector>();
        MopScript = GetComponent<MopItUp>();

        // Ensure the fadeFactor starts at 0 (completely transparent)
        if (Decal != null)
        {
            Decal.fadeFactor = 0f;
        }
        else
        {
            Debug.LogWarning("No DecalProjector found on child!");
        }
    }

    void Update()
    {
        if (beingHit && Decal.fadeFactor < 1f)
        {
            // Increase the fadeFactor over time, gradually fading in
            Decal.fadeFactor += fadeSpeed * Time.deltaTime;
        }
            
        // If the fadeFactor is 1 (completely opaque), we can deactivate the object
        if (Decal != null && Decal.fadeFactor >= 1f)
        {
            MopScript.enabled = true; // Enable the MopItUp script
            this.enabled = false; // Disable this script
        }

        // Reset the hit flag for the next frame
        beingHit = false;
    }

    // Triggered when a particle hits the object
    void OnParticleCollision(GameObject other)
    {
        beingHit = true; // Mark that particles are hitting the object
    }
}