using UnityEngine;

public class bullet : MonoBehaviour
{
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Auto-destroy after time
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
