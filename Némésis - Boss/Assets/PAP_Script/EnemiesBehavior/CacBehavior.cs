using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacBehavior : MonoBehaviour
{
    public float health;
    public float normalSpeed;
    public float dashSpeed;
    public float stoppingDistance;

    public float recoveryTime;
    public float timeBeforeDash;

    private bool canMove = true;
    private bool stopDash = false;

    [HideInInspector]
    public Transform player;

    private Vector3 target;

    public Color dashColor = Color.red;
    public Color normalColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) > stoppingDistance && canMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, normalSpeed * Time.deltaTime);
        }

        else if (Vector2.Distance(transform.position, player.position) <= stoppingDistance && canMove == true)
        {
            canMove = false;
            StartCoroutine("DashAttack");
        }

    }

    IEnumerator DashAttack()
    {

        GetComponent<SpriteRenderer>().material.color = dashColor;
        
        yield return new WaitForSeconds(timeBeforeDash);

        target = new Vector2(player.position.x, player.position.y);


        while (transform.position != target && stopDash == false)
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
            transform.position = Vector2.MoveTowards(transform.position, target, dashSpeed * Time.deltaTime);

            yield return new WaitForSeconds(0.005f);

        }

        GetComponent<CircleCollider2D>().isTrigger = false;

        stopDash = false;

        GetComponent<SpriteRenderer>().material.color = normalColor;

        yield return new WaitForSeconds(recoveryTime);

        canMove = true;

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

        if (other.CompareTag("Wall"))
        {
            stopDash = true;
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            stopDash = true;
        }
    }

}
