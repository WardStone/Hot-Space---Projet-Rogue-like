using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpcBahavior : MonoBehaviour
{

    public int health;
    public float speed;
    public float dmgBoxSpawnDelay;
    public GameObject spcDmgBox;
    public Transform[] target;

    private int currentWaypoint;
    private int previousWaypoint;

    protected Rigidbody2D spcRb;
    protected Vector2 dirVector;





    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnDamageBox", 0, dmgBoxSpawnDelay);

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != target[currentWaypoint].position)
        {
            Vector2 pos = Vector2.MoveTowards(transform.position, target[currentWaypoint].position, speed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(pos);
        }

        else
        {
            Debug.Log("(avant) : " + target[currentWaypoint]);
            currentWaypoint = (currentWaypoint + 1) % target.Length;
            Debug.Log("(après) : " + target[currentWaypoint]);

        }

    }

    private void SpawnDamageBox()
    {
        Instantiate(spcDmgBox, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

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
