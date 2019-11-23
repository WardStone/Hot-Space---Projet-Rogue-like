using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public PlayerStat stats;

    public float movementSpeed;
    private Rigidbody2D playerRb;

    private Vector3 moveInputDirection;
    private Vector2 moveVelocity;
    private Vector2 aimInputDirection;
    private Vector2 bulletDirection;
    private Vector2 fireDirection;
    private Vector2 weaponDirection;


    public GameObject aimingPoint;
    public GameObject firePoint;
    public GameObject player;

    private Animator playerAnimator;

  
    private bool canMove = true;
    private bool canDash = true;
    private bool canShoot = true;

    public float dashForce;
    public float dashDuration;
    public float firstShotDelay;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        canMove = true;
    }


    void Update()
    {
       

        moveInputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"),0.0f);
        moveVelocity = moveInputDirection * stats.playerSpeed;
       
        
        fireDirection = new Vector3(Input.GetAxisRaw("HorizontalSecondJoystick") * 1f, Input.GetAxisRaw("VerticalSecondJoystick") * 1f);
        firePoint.transform.localPosition = fireDirection;
        AimAndShoot();
        

        if (Input.GetButtonDown("Dash") && canMove == true && canDash == true)
        {
            StartCoroutine(Dash());
        }
       

    }

    private void FixedUpdate()
    {

        if (canMove == true)
        {
            playerRb.velocity = moveVelocity;    
        }
    }


    void AimAndShoot()
    {
       Vector3 aimInputDirection = new Vector3(Input.GetAxisRaw("HorizontalSecondJoystick"), Input.GetAxisRaw("VerticalSecondJoystick"));

        bulletDirection.Normalize();
        if (aimInputDirection.magnitude > 0.0f)
        {

            aimInputDirection.Normalize();
            aimingPoint.transform.localPosition =  aimInputDirection;
            aimingPoint.SetActive(true);
        }
        else
        {
            aimingPoint.SetActive(false);
        }
        if (Input.GetButton("Fire") && aimInputDirection != Vector3.zero)
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


    IEnumerator Dash()
    {
        playerRb.velocity = moveInputDirection.normalized * dashForce;
        canMove = false;
        canDash = false;
        yield return new WaitForSeconds(dashDuration);
        
        canMove = true;

        yield return new WaitForSeconds(0.5f);
        canDash = true;

    }


    IEnumerator ShootBullet()
    {
        for (int i = 0; i < stats.howManybulleShot; i++)
        {
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

            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * stats.bulletSpeed;
            bullet.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg);
            Destroy(bullet, stats.bulletLifeSpan);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(stats.delayBeforeNextShot);
        canShoot = true;
        
    }

}
