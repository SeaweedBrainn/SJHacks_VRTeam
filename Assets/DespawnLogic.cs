using UnityEngine;
public class DespawnLogic : MonoBehaviour
{
    private float randomFloat1;

    private float randomFloat2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision other)
    {
        randomFloat1 = Random.Range(0f, 7f); // for floats
        randomFloat2 = Random.Range(0f, 7f); // for floats
        other.transform.position = new Vector3(randomFloat1, 30, randomFloat2); // Reset position to origin
    }
}
