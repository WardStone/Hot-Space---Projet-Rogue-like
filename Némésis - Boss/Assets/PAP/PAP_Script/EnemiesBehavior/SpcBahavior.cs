using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpcBahavior : MonoBehaviour
{

    public int health;
    public float speed;
    public float dmgBoxSpawnDelay;
    public GameObject spcDmgBox;

    public Transform[] target1;
    public Transform[] target2;

    private int currentWaypoint;
    private int previousPath;
    private int currentPath = 1;
    private bool canChangePath = true;

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
        if (transform.position != target1[currentWaypoint].position && currentPath == 1)
        {
            Vector2 pos = Vector2.MoveTowards(transform.position, target1[currentWaypoint].position, speed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(pos);
        }

        else if (transform.position != target2[currentWaypoint].position && currentPath == 2)
        {
            Vector2 pos = Vector2.MoveTowards(transform.position, target2[currentWaypoint].position, speed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(pos);
        }


        else
        {
            previousPath = currentPath;


            if (target1[currentWaypoint].CompareTag("Branch") && canChangePath == true || target2[currentWaypoint].CompareTag("Branch") && canChangePath == true)
            {
                currentPath = Random.Range(1, 3);
                Debug.Log("le current path est : " + currentPath);
                StartCoroutine("NextWaypoint");
            }

            if (currentPath == previousPath)
            {
                currentWaypoint = (currentWaypoint + 1) % target1.Length;
            }

            if (currentPath != previousPath)
            {
                currentWaypoint = (currentWaypoint) % target2.Length;
            }


        }

    }

    IEnumerator NextWaypoint()
    {
        canChangePath = false;
        yield return new WaitForSeconds(2f);
        canChangePath = true;
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
