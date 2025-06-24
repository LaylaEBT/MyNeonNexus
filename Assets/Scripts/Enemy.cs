using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
