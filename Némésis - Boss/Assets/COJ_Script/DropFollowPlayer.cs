using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFollowPlayer : MonoBehaviour
{

    public Transform playerPos;
    public GameManagerScript gameManagerOBJ;

    Vector3 _velocity = Vector3.zero;
    public float MinModifier;
    public float MaxModifier;
    bool isFollowing = false;

    private void Start()
    {
        gameManagerOBJ = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Start is called before the first frame update
    public void StartFollowing()
    {
        isFollowing = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, playerPos.position, ref _velocity, Time.deltaTime * Random.Range(MinModifier, MaxModifier));
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManagerOBJ.playerMoney += 1;
            Destroy(gameObject);
        }
    }
}
