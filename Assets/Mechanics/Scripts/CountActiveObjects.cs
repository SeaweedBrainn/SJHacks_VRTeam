using UnityEngine;
using System.Collections.Generic;

public class CountActiveObjects : MonoBehaviour
{
    public List<string> tagsToCount; 
    private Dictionary<string, int> activeObjectCounts = new Dictionary<string, int>();
    private Dictionary<string, int> destroyedCounts = new Dictionary<string, int>(); // New: Track how many destroyed
    public int countToDelete = 3;

    void Start()
    {
        foreach (string tag in tagsToCount)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
            int count = 0;

            foreach (GameObject obj in objectsWithTag)
            {
                if (obj.activeInHierarchy)
                {
                    count++;

                    if (obj.GetComponent<ObjectActiveTracker>() == null)
                    {
                        var tracker = obj.AddComponent<ObjectActiveTracker>();
                        tracker.counter = this;
                        tracker.objectTag = tag;
                    }
                }
            }

            activeObjectCounts[tag] = count;
            destroyedCounts[tag] = 0; // Initialize destroyed count
            Debug.Log($"Active objects with tag {tag}: {count}");
        }
    }

    public void DecreaseCount(string tag)
    {
        if (activeObjectCounts.ContainsKey(tag))
        {
            activeObjectCounts[tag]--;
            destroyedCounts[tag]++; // Count how many we've deactivated manually
            Debug.Log($"Decreased Active objects with tag {tag}: {activeObjectCounts[tag]} (Destroyed {destroyedCounts[tag]})");

            if (destroyedCounts[tag] >= countToDelete)
            {
                Debug.Log($"Destroyed {countToDelete} objects for tag {tag}. Deactivating all objects with this tag.");

                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject obj in objectsWithTag)
                {
                    if (obj.activeInHierarchy)
                    {
                        obj.SetActive(false);
                    }
                }

                // Optional: Reset counter if you want this to happen again later
                destroyedCounts[tag] = 0;
            }
        }
    }
}