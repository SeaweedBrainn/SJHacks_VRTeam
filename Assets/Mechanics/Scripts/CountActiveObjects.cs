using UnityEngine;
using System.Collections.Generic;

public class CountActiveObjects : MonoBehaviour
{
    public List<string> tagsToCount; // List of tags to track
    private Dictionary<string, int> activeObjectCounts = new Dictionary<string, int>();
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
            Debug.Log($"Active objects with tag {tag}: {count}");
        }
    }

    public void DecreaseCount(string tag)
    {
        if (activeObjectCounts.ContainsKey(tag))
        {
            activeObjectCounts[tag]--;
            Debug.Log($"Decreased Active objects with tag {tag}: {activeObjectCounts[tag]}");
            if (activeObjectCounts[tag] == (15 - countToDelete))
            {
                Debug.Log($"Condition met for tag {tag}. Deactivating all objects with this tag.");

                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject obj in objectsWithTag)
                {
                    if (obj.activeInHierarchy) // Optional: Only deactivate active ones
                    {
                        obj.SetActive(false);
                    }
                }
            }
        }
    }
}