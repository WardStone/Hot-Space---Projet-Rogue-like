using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public GameObject tp;
    public GameObject player;
    public bool canTp;
    public int orientation;

    public GameObject tpCoridor;

    public float distanceV;
    public float distanceH;

    void Start()
    {
        distanceH = 25f;
        distanceV = 25f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        if (canTp & Input.GetButtonDown("Interact"))
        {
            Debug.Log("tp toi !!!!");
            switch (orientation)
            {
                case 0: // tp north
                    //player.transform.localPosition = new Vector2 (distanceH,0);
                    player.transform.localPosition = tpCoridor.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.position;
                    break;

                case 1: //tp east
                    //player.transform.localPosition = new Vector2(0, distanceV);
                    player.transform.localPosition = tpCoridor.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.position;
                    break;

                case 2: //tp south
                    //player.transform.localPosition = new Vector2(-distanceH, 0);
                    player.transform.localPosition = tpCoridor.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.position;
                    break;

                case 3:// tp west
                    //player.transform.localPosition = new Vector2(0, -distanceV);
                    player.transform.localPosition = tpCoridor.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.position;
                    break;

                default:
                    break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            canTp = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            canTp = false;
    }
}
