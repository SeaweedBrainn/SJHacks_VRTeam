using UnityEngine;

public class GoToTransform : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform targetTransform; // The target transform to move towards
    public Vector3 alternatePosition; // The alternate position to move to
    
    public void GoToTarget()
    {
        if (targetTransform != null)
        {
            transform.position = targetTransform.position;
        }
        else
        {
            transform.position = alternatePosition;
        }
    }
}
