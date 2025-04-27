using UnityEngine;

public class ObjectActiveTracker : MonoBehaviour
{
    public CountActiveObjects counter;
    public string objectTag;

    void Update()
    {
        if (!gameObject.activeInHierarchy)
        {
            counter.DecreaseCount(objectTag);
            Destroy(this); // Remove this component to avoid multiple calls
        }
    }
}