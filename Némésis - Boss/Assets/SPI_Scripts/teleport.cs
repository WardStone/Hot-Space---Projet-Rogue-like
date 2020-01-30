using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public CameraManager camManager;
    public switchButton Abutton;

    public GameObject tp;
    public GameObject player;
    public bool canTp;
    public int orientation;

    public GameObject tpCoridor;


    public GameObject rootRoom;
    public GameObject tpManager;

    public GameObject camConfiner;

    public GameObject tpSoundGameobject;

    void Start()
    {
        rootRoom = GameObject.FindGameObjectWithTag("RootRoom");
        camManager = GameObject.Find("CamManager").GetComponent<CameraManager>();
        tpManager = rootRoom.transform.GetChild(0).gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        camConfiner = GameObject.Find("CamConfiner");
    }

    void Update()
    {

        if (tpManager.CompareTag("lock") == false & canTp & Input.GetButtonDown("Interact"))
        {
            //Debug.Log("tp toi !!!!");
            switch (orientation)
            {
                case 0: // tp north
                    //player.transform.localPosition = new Vector2 (distanceH,0);
                    player.transform.localPosition = tpCoridor.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.position;
                    StartCoroutine(camManager.MoveCam(new Vector2(camConfiner.transform.position.x, camConfiner.transform.position.y - 2 * 13.1f)));
                    StartCoroutine(camManager.CoolDownTp());
                    StartCoroutine(TpSound());
                    break;

                case 1: //tp east
                    //player.transform.localPosition = new Vector2(0, distanceV);
                    player.transform.localPosition = tpCoridor.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.position;
                    StartCoroutine(camManager.MoveCam(new Vector2(camConfiner.transform.position.x - 2 * 23.56f, camConfiner.transform.position.y)));
                    StartCoroutine(camManager.CoolDownTp());
                    StartCoroutine(TpSound());
                    break;

                case 2: //tp south
                    //player.transform.localPosition = new Vector2(-distanceH, 0);
                    player.transform.localPosition = tpCoridor.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.position;
                    StartCoroutine(camManager.MoveCam(new Vector2(camConfiner.transform.position.x, camConfiner.transform.position.y + 2 * 13.1f)));
                    StartCoroutine(camManager.CoolDownTp());
                    StartCoroutine(TpSound());
                    break;

                case 3:// tp west
                    //player.transform.localPosition = new Vector2(0, -distanceV);
                    player.transform.localPosition = tpCoridor.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.position;
                    //camConfiner.transform.position = new Vector2(camConfiner.transform.position.x + 2 * 23.56f, camConfiner.transform.position.y);
                    StartCoroutine(camManager.MoveCam(new Vector2(camConfiner.transform.position.x + 2 * 23.56f, camConfiner.transform.position.y)));
                    StartCoroutine(camManager.CoolDownTp());
                    StartCoroutine(TpSound());

                    break;

                default:
                    break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(tpManager.CompareTag("lock") == false)
            {
                Abutton = GameObject.Find("Abutton").GetComponent<switchButton>();
                Abutton.canInteract = true;
            }
            canTp = true;
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Abutton = GameObject.Find("Abutton").GetComponent<switchButton>();
            Abutton.canInteract = false;
            canTp = false;
        }
    }

    IEnumerator TpSound()
    {
        tpSoundGameobject = GameObject.Find("TpSound");
        tpSoundGameobject.GetComponent<AudioSource>().enabled = true;
        yield return new WaitForSeconds(1.5f);
        tpSoundGameobject.GetComponent<AudioSource>().enabled = false;
    }
}
