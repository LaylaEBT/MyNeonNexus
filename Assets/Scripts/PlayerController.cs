using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
   
    [SerializeField] private float originalSpeed = 3f;
    [SerializeField] private float boostedSpeed = 8f;

    private float currentSpeed;
    
    
    private Rigidbody2D rb;
    private Vector2 move;
    private bool Shield = false;

    public int playerHealth = 3;

    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Transform firePoint;
 
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       currentSpeed = originalSpeed;
     
    }

    
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");  
        move = new Vector2(moveX, moveY);
        move.Normalize();

        if (Input.GetKeyDown(KeyCode.Space))
        {
         Shoot();
        }

        HandleRotation();  
        
    }

    void HandleRotation()
    {
      if (move != Vector2.zero)
        {
            float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = move * currentSpeed;
    }
    

    private IEnumerator SpeedBoost()
     {
        
        currentSpeed = boostedSpeed;

        yield return new WaitForSeconds(30f);

        currentSpeed = originalSpeed;
     
     }

     private IEnumerator ShieldBoost()
     {
      
        Shield = true;

        yield return new WaitForSeconds(45f);

        Shield = false;
     
     }
     public void TakeDamage(int damage)
     {
        playerHealth -= damage;

     }

    void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.CompareTag("Shield"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(ShieldBoost());
        }
        else if (collision.CompareTag("SpeedBoost"))
        {
           Destroy(collision.gameObject);
           StartCoroutine(SpeedBoost()); 
        }

     }

    void OnCollisionEnter2D(Collision2D collision)
     {
        if (collision.gameObject.CompareTag("Enemy") && !Shield)
        {
           TakeDamage(1);
           Debug.Log(playerHealth);
           if ( playerHealth < 1)
           {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
           }
        }
     }

   void Shoot()
   {
      GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
      Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
      rbb.linearVelocity = transform.up * bulletSpeed;

   }

}
