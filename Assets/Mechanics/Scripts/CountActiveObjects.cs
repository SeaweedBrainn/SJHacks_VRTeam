using UnityEngine;
using System.Collections.Generic;

public class CountActiveObjects : MonoBehaviour
{
    public List<string> tagsToCount; 
    private Dictionary<string, int> activeObjectCounts = new Dictionary<string, int>();
    private Dictionary<string, int> destroyedCounts = new Dictionary<string, int>(); // New: Track how many destroyed
    public int countToDelete = 3;
    public string seedTag = "Seed";
    private FogController fogController;
    private bool seedFogReduced = false; // Track if the seed fog reduction has already occurred
    private bool trashFogReduced = false; // Track if trash fog reduction has already occurred
    
    void Start()
    {
        fogController = FindFirstObjectByType<FogController>(); // Find the FogController in the scene
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
                
                if (tag == seedTag && !seedFogReduced)
                {
                    if (CheckAllObjectsDeactivated(tag))
                    {
                        Debug.Log("All 'seed' objects are deactivated. Reducing fog by half.");
                        fogController.ReduceFogStrength(50); // Reduce fog strength by 50% for the seed tag
                        seedFogReduced = true; // Mark seed fog reduction as done
                    }
                }

                // Check if all trash-related objects are deactivated
                else if (tag != seedTag && !trashFogReduced)
                {
                    if (CheckAllObjectsDeactivated(tag))
                    {
                        Debug.Log("All trash-related objects are deactivated. Reducing remaining fog.");
                        fogController.ReduceFogStrength(50); // Reduce the remaining fog by 50% for trash objects
                        trashFogReduced = true; // Mark trash fog reduction as done
                    }
                }


                // Optional: Reset counter if you want this to happen again later
                destroyedCounts[tag] = 0;
            }
        }
    }
    private bool CheckAllObjectsDeactivated(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objectsWithTag)
        {
            if (obj.activeInHierarchy) // If any object is still active
                return false;
        }
        return true; // All objects are deactivated
    }
}