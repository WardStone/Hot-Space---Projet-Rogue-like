using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public PlayerStat stats;
    public SimpleCameraShakeInCinemachine shakeCam;

    public float movementSpeed;
    private Rigidbody2D playerRb;

    private Vector3 moveInputDirection;
    private Vector2 moveVelocity;
    private Vector2 aimInputDirection;
    private Vector2 bulletDirection;
    private Vector2 fireDirection;
    private Vector2 weaponDirection;
    private Vector2 laserDirection;


    public GameObject aimingPoint;
    public GameObject firePoint;
    public GameObject laserPoint;
    public GameObject player;
    public GameObject gunFlash;

    private Animator playerlegs, playerTorso, playerArms;

    public bool isDashing = false;

  
    public bool canMove = true;
    public bool canDash = true;
    private bool canShoot = true;
    public bool laserScope = true;

    public float dashForce;
    public float dashDuration;
    public float firstShotDelay;
    public int soundIndex;

    public LineRenderer laserScopeRenderer;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        shakeCam = GameObject.FindGameObjectWithTag("CameraShakeManager").GetComponent<SimpleCameraShakeInCinemachine>();
        canMove = true;
        playerTorso = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
        playerlegs = gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Animator>();
        player = gameObject;
    }


    void Update()
    {
       

        moveInputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"),0.0f);
        moveVelocity = moveInputDirection * stats.playerSpeed;
       
      
        fireDirection = new Vector2(Input.GetAxisRaw("HorizontalSecondJoystick") * 1f, Input.GetAxisRaw("VerticalSecondJoystick") * 1f);
        firePoint.transform.localPosition = fireDirection;

        laserDirection = new Vector2(Input.GetAxisRaw("HorizontalSecondJoystick") * 25f, Input.GetAxisRaw("VerticalSecondJoystick") * 25f);
        laserPoint.transform.localPosition = laserDirection;
        laserScopeRenderer = laserPoint.GetComponent<LineRenderer>();
        AimAndShoot();
        playerTorso.SetFloat("HorizontalAim", Input.GetAxis("HorizontalSecondJoystick"));
        playerTorso.SetFloat("VerticalAim", Input.GetAxis("VerticalSecondJoystick"));
        playerTorso.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        playerTorso.SetFloat("Vertical", Input.GetAxis("Vertical"));

        playerlegs.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        playerlegs.SetFloat("Vertical", Input.GetAxis("Vertical"));
        playerlegs.SetFloat("HorizontalAim", Input.GetAxis("HorizontalSecondJoystick"));
        playerlegs.SetFloat("VerticalAim", Input.GetAxis("VerticalSecondJoystick"));



        if (Input.GetButtonDown("Dash") && canMove == true && canDash == true)
        {
            StartCoroutine(Dash());
        }

        if (stats.weaponR == 1)
        {
            LaserScoping();
        }




    }

    private void FixedUpdate()
    {
        Debug.Log("Axis equal" +Input.GetAxis("Fire"));
        if (canMove == true)
        {
            playerRb.velocity = moveVelocity;    
        }

        if(moveInputDirection.magnitude > 0.2)
        {
            playerTorso.SetBool("isRunning", true);
            playerlegs.SetBool("isRunning", true);
        }
        else
        {
            playerTorso.SetBool("isRunning", false);
            playerlegs.SetBool("isRunning", false);
        }
    }


    void AimAndShoot()
    {
       Vector3 aimInputDirection = new Vector3(Input.GetAxisRaw("HorizontalSecondJoystick"), Input.GetAxisRaw("VerticalSecondJoystick"));

        bulletDirection.Normalize();
        if (aimInputDirection.magnitude > 0.0f)
        {
            float posMultiplier = 2.5f;
            playerTorso.SetBool("isAiming", true);
            aimInputDirection.Normalize();
            aimingPoint.transform.localPosition =  aimInputDirection * posMultiplier;
            aimingPoint.SetActive(true);
        }
        else
        {
            playerTorso.SetBool("isAiming", false);
            aimingPoint.SetActive(false);
        }
        if (Input.GetAxis("Fire") == 1 && aimInputDirection != Vector3.zero)
        {
            firstShotDelay += Time.deltaTime;
            
            if (canShoot == true && firstShotDelay >= stats.delayBeforeFirstShot)
            {
                Debug.Log("You shot !");
                canShoot = false;
                StartCoroutine(ShootBullet());
            }

        }
        else
        {
            firstShotDelay = 0;
        }
    }

    void LaserScoping()
    {
        laserScopeRenderer.enabled = true;
        laserScopeRenderer.SetPosition(0, firePoint.transform.position);
        laserScopeRenderer.SetPosition(1, laserPoint.transform.position);
    }

    IEnumerator Dash()
    {
        playerRb.velocity = moveInputDirection.normalized * dashForce;
        player.GetComponent<CapsuleCollider2D>().enabled = false;
        canMove = false;
        canDash = false;
        yield return new WaitForSeconds(dashDuration);
        
        canMove = true;
        player.GetComponent<CapsuleCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        canDash = true;

    }


    IEnumerator ShootBullet()
    {
        /*if (soundIndex > stats.bulletSoundHolder.transform.childCount -1)
        {
            soundIndex = 0;
        }*/
        Instantiate(stats.bulletSoundHolder.transform.GetChild(soundIndex), gameObject.transform.position, Quaternion.identity);
        soundIndex = Random.Range(0, stats.bulletSoundHolder.transform.childCount);

        for (int i = 0; i < stats.howManybulleShot; i++)
        {
            Instantiate(gunFlash, firePoint.transform.position, Quaternion.identity);
            StartCoroutine(shakeCam.bulletShake());

            Debug.Log("Cool it shoot");
            if (aimingPoint.transform.position.x >= 0.5f || aimingPoint.transform.position.x <= -0.5f)
            {
                firePoint.transform.position += new Vector3(0f, Random.Range(-stats.weaponAccuracy, stats.weaponAccuracy));
            }

            if (aimingPoint.transform.position.y >= 0.5f || aimingPoint.transform.position.y <= -0.5f)
            {
                firePoint.transform.position += new Vector3(Random.Range(-stats.weaponAccuracy, stats.weaponAccuracy),0f);
            }


            bulletDirection = firePoint.transform.position - player.transform.position;
            GameObject bullet = Instantiate(stats.bulletPrefab, firePoint.transform.position, Quaternion.identity);

            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection.normalized * stats.bulletSpeed;
            bullet.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg);
            Destroy(bullet, stats.bulletLifeSpan);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(stats.delayBeforeNextShot);
        canShoot = true;
        
    }

}
