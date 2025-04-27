using UnityEngine;

public class SmoothScale : MonoBehaviour
{
    public float scaleSpeed = 2.0f; // How fast the object scales
    private Vector3 targetScale = Vector3.one; // (1,1,1)

    void Update()
    {
        // Smoothly scale towards (1,1,1)
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
        
        // Optional: Snap to exact 1,1,1 when close enough
        if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
        {
            transform.localScale = targetScale;
        }
    }
}