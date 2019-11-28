using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacBehavior : MonoBehaviour
{
    public float normalSpeed;
    public float chargeSpeed;
    public float stoppingDistance;
    public bool canMove = true;

    public Transform player;
    public Vector3 dashDir;

    public Color dashNowColor = Color.red;
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
        dashDir = player.position;
        GetComponent<SpriteRenderer>().material.color = dashNowColor;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().material.color = normalColor;
        canMove = true;
        transform.position = transform.position - dashDir;
        canMove = false;
        yield return new WaitForSeconds(1f);

    }

}
