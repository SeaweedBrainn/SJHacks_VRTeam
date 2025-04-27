using UnityEngine;

public class FogController : MonoBehaviour
{
    public float fogStrength = 0.2f; // 1.0 means full fog density

    void Start()
    {
        // Initially setting fog density to full
        RenderSettings.fog = true;
        RenderSettings.fogColor = Color.gray; // Or your preferred fog color
        RenderSettings.fogMode = FogMode.Exponential; // Choose your fog mode (Exponential, Linear)
        RenderSettings.fogDensity = fogStrength; // Set initial fog density to 1.0 (100% fog)
    }

    // Reduce fog strength by a specified percentage
    public void ReduceFogStrength(float percentage)
    {
        // Ensure the fog strength is between 0 (no fog) and 1 (full fog)
        fogStrength = Mathf.Max(0, fogStrength - (percentage / 100f));

        // Update fog density in Unity
        RenderSettings.fogDensity = fogStrength;
        Debug.Log($"Fog strength reduced. New density: {fogStrength}");
        
        // If fog strength reaches 0, turn off the fog entirely
        if (fogStrength <= 0)
        {
            RenderSettings.fog = false;
            Debug.Log("Fog is completely cleared.");
        }
    }
}