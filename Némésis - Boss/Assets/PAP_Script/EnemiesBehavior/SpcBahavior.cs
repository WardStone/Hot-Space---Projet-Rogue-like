using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpcBahavior : MonoBehaviour
{

    public int health;
    public float normalSpeed;
    private int direction; // 1 = Nord / 2 = Est / 3 = Sud / 4 = Ouest
    private int prevDirection;
    protected Rigidbody2D spcRb;
    protected Vector3 dirVector;



    // Start is called before the first frame update
    void Start()
    {
        direction = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == 1)
        {
            dirVector = new Vector2(0, 1);

            spcRb = gameObject.GetComponent<Rigidbody2D>();
            spcRb.velocity = dirVector * normalSpeed * Time.deltaTime;

        } else if (direction == 2)
        {
            dirVector = new Vector2(1, 0);

            spcRb = gameObject.GetComponent<Rigidbody2D>();
            spcRb.velocity = dirVector * normalSpeed * Time.deltaTime;

        } else if (direction == 3)
        {
            dirVector = new Vector2(0, -1);

            spcRb = gameObject.GetComponent<Rigidbody2D>();
            spcRb.velocity = dirVector * normalSpeed * Time.deltaTime;

        } else if (direction == 4)
        {
            dirVector = new Vector2(-1, 0);

            spcRb = gameObject.GetComponent<Rigidbody2D>();
            spcRb.velocity = dirVector * normalSpeed * Time.deltaTime;

        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Wall"))
        {
            prevDirection = direction;

            while(direction == prevDirection)
            {
                direction = Random.Range(1, 5);
            }
            
        }

        if (other.CompareTag("Bullet"))
        {
            health -= 5;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

    
    }

}
