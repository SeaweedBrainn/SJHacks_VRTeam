using System;
using UnityEngine;

public class ObjectActiveTracker : MonoBehaviour
{
    public CountActiveObjects counter;
    public string objectTag;
    
    private void OnDisable()
    {
        counter.DecreaseCount(objectTag);
    }
}