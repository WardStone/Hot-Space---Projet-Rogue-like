using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NierCrustac : MonoBehaviour
{
    public Rigidbody2D crustacRB;

    // Start is called before the first frame update
    void Start()
    {
        crustacRB = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 imComing = new Vector2(-1, 0);
        crustacRB.velocity = imComing * 3;
    }
}
