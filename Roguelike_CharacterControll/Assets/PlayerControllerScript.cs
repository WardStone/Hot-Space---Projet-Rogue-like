using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{

    public float movementSpeed;
    private Rigidbody2D playerRb;

    private Vector3 moveInputDirection;
    private Vector2 moveVelocity;
    private Vector2 aimInputDirection;
    private Vector2 bulletDirection;
    private Vector2 fireDirection;

    public GameObject aimingPoint;
    public GameObject shield;
    public GameObject bulletPrefab;
    public GameObject dashTrail;
    public GameObject firePoint;
    private Animator playerAnimator;

  
    private bool canMove = true;
    private bool canDash = true;
    private bool canShoot = true;

    public float dashForce;
    public float timeBetweenShoot;


    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        canMove = true;
    }


    void Update()
    {
        moveInputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"),0.0f);
        moveVelocity = moveInputDirection * movementSpeed;

        playerAnimator.SetFloat("Horizontal", moveInputDirection.x);
        playerAnimator.SetFloat("Vertical", moveInputDirection.y);

        aimInputDirection = new Vector3(Input.GetAxisRaw("HorizontalSecondJoystick"),Input.GetAxisRaw("VerticalSecondJoystick"));
        bulletDirection = new Vector2(Input.GetAxisRaw("HorizontalSecondJoystick"), Input.GetAxisRaw("VerticalSecondJoystick"));
        fireDirection = new Vector3(Input.GetAxisRaw("HorizontalSecondJoystick") * 0.5f, Input.GetAxisRaw("VerticalSecondJoystick") * 0.5f);

        firePoint.transform.localPosition = fireDirection;
        AimAndShoot();
        ActivateShield();
        
        

        if (Input.GetButtonDown("Dash") && canMove == true && canDash == true)
        {
            StartCoroutine(Dash());
        }
        if (Input.GetButton("Fire") && aimInputDirection != Vector2.zero)
        {
            if (canShoot)
            {
                canShoot = false;
                Shoot();
                StartCoroutine(TimeBetween());
            }
           
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
        bulletDirection.Normalize();
        if (aimInputDirection.magnitude > 0.0f)
        {

            aimInputDirection.Normalize();
            aimingPoint.transform.localPosition = aimInputDirection;
            aimingPoint.SetActive(true);
        }
        else
        {
            aimingPoint.SetActive(false);
        }
      
    }

    void ActivateShield()
    {

        Quaternion shieldRotation = Quaternion.LookRotation(aimInputDirection,Vector3.up);

        if (Input.GetButton("Shield"))
        {
            shield.SetActive(true);
            shield.transform.rotation = shieldRotation;
        }
        else
        {
            shield.SetActive(false);
        }
    }

    IEnumerator Dash()
    {
        playerRb.velocity = moveInputDirection.normalized * dashForce;
        dashTrail.SetActive(true);
        canMove = false;
        canDash = false;
        yield return new WaitForSeconds(0.25f);
        dashTrail.SetActive(false);
        canMove = true;
        yield return new WaitForSeconds(0.5f);
        canDash = true;

    }

    IEnumerator TimeBetween()
    {       
            yield return new WaitForSeconds(timeBetweenShoot);
            canShoot = true;
    }

    void Shoot()
    {
        float bulletSpeed = 20f;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
        bullet.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg);
        Destroy(bullet, 2.0f);
    }



}
