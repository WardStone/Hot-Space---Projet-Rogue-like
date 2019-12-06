using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPatternLoop : MonoBehaviour
{
    public PlayerStat playerStat;
    public BossPartStat leftArm01Stat;
    public BossPartStat rightArm01Stat;
    public BossPartStat head01Stat;

    public float bossHealth = 300;
    protected int patternRef;
    protected int patternSaved;
    protected bool canTakeDamage = true;
    protected bool canDoAnotherOne = true;
    public Slider healthBar;
    List<int> patternList = new List<int>();

    public bool phase2Started;




    // leftArm01Pattern condition and object
    protected GameObject leftArm01;
    public GameObject leftArm01Prefab;
    protected float leftArm01Speed = 5f;
    protected bool canDoLeftArm01 = true;
    protected bool canDo02 = false;
    protected bool canDo03 = false;
    protected bool canDo04 = false;
    public Transform leftSpawnArmPoint;
    public Transform prepareAttackPoint;
    public Transform leftArmPoint;
    public Transform ImpactPoint;
    public Vector3 pattern01FirstDir;
    public Rigidbody2D leftArm01Rb;
    public GameObject impactPointSpawnPrefab;
    public Color dashNowColor = Color.red;
    public Color normalColor = Color.white;

    //Head01Pattern condition and object
    protected GameObject Head01;
    public GameObject Head01Prefab;
    protected bool canDoHead01 = true;
    public Transform HeadPoint;
    public Transform HeadSpawnPoint;
    protected bool canSpawn = true;

    public Transform newTargetPoint;
    public GameObject enemyBulletPrefab;
    protected Vector2 head01BulletDir;
    public float enemyBulletSpeed = 100f;
    public Rigidbody2D enemyBulletRb;
    protected GameObject enemyBullet;

    //RightArm01Pattern Condition and object

    public GameObject rightArm01;
    public GameObject rightArm01Prefab;
    public Transform rightArmSpawnPoint;
    protected bool canDoRightArm01 = true;
    protected Animator rightArm01Animator;
    public GameObject damagedGround;

    //BossPhase2 Condition and object

    protected Vector2 bossCoreDirection;
    public Rigidbody2D bossCoreRb;
    public float coreSpeed = 300f;
    protected bool phase2IsOn = true;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = bossHealth;
        phase2Started = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        leftArm01 = Instantiate(leftArm01Prefab,leftSpawnArmPoint.transform.position,Quaternion.identity);
        leftArmPoint = leftArm01.transform.GetChild(0).transform;
        leftArm01Rb = leftArm01.GetComponent<Rigidbody2D>();
        leftArm01Stat = leftArm01.GetComponent<BossPartStat>();

        

   

        bossCoreDirection = Vector2.one.normalized;

        Head01 = Instantiate(Head01Prefab, HeadSpawnPoint.transform.position, Quaternion.identity);
        HeadPoint = Head01.transform.GetChild(0).transform;
        head01Stat = Head01.GetComponent<BossPartStat>();

        rightArm01 = Instantiate(rightArm01Prefab, rightArmSpawnPoint.transform.position, Quaternion.identity);
        rightArm01Animator = rightArm01.GetComponent<Animator>();
        rightArm01Stat = rightArm01.GetComponent<BossPartStat>();

        for (int i = 0; i < 3; i++)
        {
            patternList.Add(i);

        }
        patternSaved = -1;


        BossPatternSelection();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if (bossHealth <= 500)
        {
           
            bossCoreRb.velocity = bossCoreDirection * coreSpeed * Time.deltaTime;
            phase2Started = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;

        }
        // Pattern LeftArm01
        if(patternRef == 0 && canDoLeftArm01 == true)
        {
            canDoLeftArm01 = false;
            if(leftArm01Stat.partHealth > 0)
            {
                StartCoroutine(LeftArmPattern01Part1());
            }
            else
            {
                StartCoroutine(RespawnLeftArm());
            }
            
            
        }
 

        //Pattern Head01
        if (patternRef == 1 && canDoHead01 == true)
        {
            canDoHead01 = false;
            if(head01Stat.partHealth > 0)
            {
                StartCoroutine(Head01Pattern());
            }
            else
            {
                StartCoroutine(RespawnHead());
            }
        }
        if (patternRef == 2 && canDoRightArm01 == true)
        {
            canDoRightArm01 = false;
            if(rightArm01Stat.partHealth > 0)
            {
                StartCoroutine(RightArm01Pattern());
                Debug.Log("patternRef Accessed");
            }
            else
            {
                StartCoroutine(RespawnRightArm());
            }
        }




    }
    private void Update()
    {
        bossDeath();
    }

    void BossPatternSelection()
    {
        int iCurrentPattern = Random.Range(0, patternList.Count);
        Debug.Log("PatternList =" + patternList.Count);
        if (iCurrentPattern != -1)
        {
            patternRef = patternList[iCurrentPattern];
            Debug.Log("PatternRef =" + patternRef);
            if (patternSaved != -1)
            {
                patternList.Add(patternSaved);
                Debug.Log("patternRefSaved" + patternSaved + "has been added");
            }
        }
    }



    void RefreshPattern()
    {
        patternSaved = patternRef;
        patternList.Remove(patternRef);
        Debug.Log("PatternRef" + patternRef + "has been removed");
        BossPatternSelection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("It has triggered");
        if (collision.CompareTag("Bullet"))
        {
            if (canTakeDamage == true)
            {
                StartCoroutine(takeDamage());
            }
        }

        if (collision.CompareTag("WallY"))
        {
            bossCoreDirection.y = -bossCoreDirection.y;
        }

        if (collision.CompareTag("WallX"))
        {
            bossCoreDirection.x = -bossCoreDirection.x;
        }
    }

    IEnumerator takeDamage()
    {
        bossHealth -= playerStat.bulletDamage;
        healthBar.value = bossHealth;
        canTakeDamage = false;
        Debug.Log("bosshealth =" + bossHealth);
        yield return new WaitForSeconds(0.01f);
        canTakeDamage = true;

    }

    IEnumerator RespawnLeftArm()
    {
        if (leftArm01Stat.partHealth <= 0)
        {
            yield return new WaitForSeconds(1.5f);
            leftArm01Stat.partHealth = 200;
            leftArm01Stat.hasRespawned = true;
            Debug.Log("I'm back baby Boi");
            yield return new WaitForSeconds(0.5f);
            Debug.Log("here my new move cunt");
            canDoLeftArm01 = true; 
            RefreshPattern();
        }

    }

    IEnumerator RespawnRightArm()
    {
        if (rightArm01Stat.partHealth <= 0)
        {
            yield return new WaitForSeconds(1.5f);
            rightArm01Stat.partHealth = 200;
            rightArm01Stat.hasRespawned = true;
            Debug.Log("I'm back baby Boi");
            yield return new WaitForSeconds(0.5f);
            Debug.Log("here my new move cunt");
            canDoRightArm01 = true;
            RefreshPattern();
        }

    }

    IEnumerator RespawnHead()
    {
        if (head01Stat.partHealth <= 0)
        {
            yield return new WaitForSeconds(1.5f);
            head01Stat.partHealth = 200;
            head01Stat.hasRespawned = true;
            Debug.Log("I'm back baby Boi");
            yield return new WaitForSeconds(0.5f);
            Debug.Log("here my new move cunt");
            canDoHead01 = true;
            RefreshPattern();
        }

    }

    IEnumerator LeftArmPattern01Part1()
    {
        float moveSpeed = 5f;
        float pattern1Timer = 2f;
        float impactSpeed = 15f;
        float comeBackTimer = 0.25f;
        float returnSpeed = 10f;

        leftArm01.GetComponent<BoxCollider2D>().enabled = false;
        while (pattern1Timer > 0 && leftArm01)
            {

                pattern01FirstDir = prepareAttackPoint.position - leftArmPoint.position;
                leftArm01Rb.velocity = pattern01FirstDir * moveSpeed;
                pattern1Timer -= Time.deltaTime;
                if (pattern1Timer <= 0.3f && pattern1Timer > 0.1f)
                {
                    leftArm01.GetComponent<SpriteRenderer>().material.color = dashNowColor;
                }
                else
                {
                    leftArm01.GetComponent<SpriteRenderer>().material.color = normalColor;
                }


                yield return null;
            }

        // PatternPart2
            leftArm01.GetComponent<BoxCollider2D>().enabled = true;
            GameObject impactPointSpawn = Instantiate(impactPointSpawnPrefab, ImpactPoint.transform.position, Quaternion.identity);
            pattern01FirstDir = impactPointSpawn.transform.position - leftArmPoint.position;
            leftArm01Rb.velocity = pattern01FirstDir * impactSpeed;
            yield return new WaitForSeconds(0.1f);
            Destroy(impactPointSpawn);

            //PatternPart3
            pattern01FirstDir = new Vector3(0, 0, 0);
            leftArm01Rb.velocity = pattern01FirstDir * 1;
            yield return new WaitForSeconds(2f);
        leftArm01.GetComponent<BoxCollider2D>().enabled = false;
            //PatternPArt4
            while (comeBackTimer > 0)
            {

                pattern01FirstDir = leftSpawnArmPoint.position - leftArmPoint.position;
                leftArm01Rb.velocity = pattern01FirstDir * returnSpeed;
                comeBackTimer -= Time.deltaTime;
                yield return null;
            }

            pattern01FirstDir = new Vector3(0, 0, 0);
            leftArm01Rb.velocity = pattern01FirstDir * 0;
            yield return new WaitForSeconds(0.25f);
            leftArm01Rb.position = leftSpawnArmPoint.transform.position;
            leftArm01.GetComponent<BoxCollider2D>().enabled = true;
            yield return new WaitForSeconds(0.25f);
            Debug.Log("Refrest the pattern");
            canDoLeftArm01 = true;
            RefreshPattern();
            
    }
  

    IEnumerator Head01Pattern()
    {
        
        float beamPatternTimer = 3f;
        bool canSpawn = true;

        Head01.GetComponent<BoxCollider2D>().enabled = false;
        while (beamPatternTimer > 0)
        {
            
            if (canSpawn == true)
            {
                canSpawn = false;
                yield return new WaitForSeconds(0.45f);
                
                enemyBullet = enemyBulletPrefab;
                Instantiate(enemyBullet, Head01.transform.position, Quaternion.identity);
                beamPatternTimer -= 0.25f;
                
                canSpawn = true;
                
            }
            yield return null;
        }
 
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Refrest the pattern");
        canDoHead01 = true;
        RefreshPattern();
        Head01.GetComponent<BoxCollider2D>().enabled = true;
        canSpawn = true;
        
    }

    IEnumerator RightArm01Pattern()
    {
        Debug.Log("Pattern 2 has begun");
       
        rightArm01Animator.SetBool("doPattern", true);
        yield return new WaitForSeconds(1f);
        rightArm01Animator.SetBool("doPattern", false);
        damagedGround.SetActive(true);
        yield return new WaitForSeconds(2f);
        canDoRightArm01 = true;
        RefreshPattern();
        yield return new WaitForSeconds(2f);
        damagedGround.SetActive(false);
    }
   
    void bossDeath()
    {
        if (bossHealth <= 0)
        {
            Debug.Log(" you won !");
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}
