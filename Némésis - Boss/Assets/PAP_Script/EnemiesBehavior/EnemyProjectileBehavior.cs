using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileBehavior : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;
    private Vector2 vectorDir;
    protected Rigidbody2D bulletRb;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        bulletRb = gameObject.GetComponent<Rigidbody2D>();

        vectorDir = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {

        /*transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        previousPos = player.transform;*/


        /* if (transform.position.x == target.x && transform.position.y == target.y)
         {
             Destroy(gameObject);
         }*/

        
        bulletRb.velocity = vectorDir.normalized * speed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
