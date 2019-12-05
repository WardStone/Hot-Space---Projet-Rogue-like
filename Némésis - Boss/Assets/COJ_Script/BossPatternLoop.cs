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
    public bool canTakeDamage = true;
    protected bool canDoAnotherOne = true;
    public Slider healthBar;
    List<int> patternList = new List<int>();

    public bool phase2Started;

    // Les Animators du boss
    public Animator HeadAnimator;
    public Animator BodyAnimator;
    public Animator MaskAnimator;
    public Animator LeftArmAnimator;
    public Animator LeftHandAnimator;
    public Animator RightArmAnimator;
    public int AnimatorRef;

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
    public Transform leftArmOriginalPos;
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
    public Transform shotSpawnPoint;

    //RightArm01Pattern Condition and object

    public GameObject rightArm01;
    public GameObject rightArm01Prefab;
    public Transform rightArmSpawnPoint;
    protected bool canDoRightArm01 = true;
    protected Animator rightArm01Animator;
    public GameObject damagedGround;

    //BossPhase2 Condition and object

    public bool phase2IsOn;

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
        LeftHandAnimator = leftArm01.GetComponent<Animator>();

        HeadAnimator = gameObject.transform.GetChild(1).GetComponent<Animator>();
        BodyAnimator = gameObject.transform.GetChild(2).GetComponent<Animator>();
        LeftArmAnimator = gameObject.transform.GetChild(3).GetComponent<Animator>();


   



        Head01 = Instantiate(Head01Prefab, HeadSpawnPoint.transform.position, Quaternion.identity);
        HeadPoint = Head01.transform.GetChild(0).transform;
        head01Stat = Head01.GetComponent<BossPartStat>();
        MaskAnimator = Head01.GetComponent<Animator>();

        rightArm01 = Instantiate(rightArm01Prefab, rightArmSpawnPoint.transform.position, Quaternion.identity);
        rightArm01Stat = rightArm01.GetComponent<BossPartStat>();
        RightArmAnimator = rightArm01.GetComponent<Animator>();

        AnimatorRef = -1;
        SetAllAnimatorRef();
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



    public IEnumerator takeDamage()
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


        AnimatorRef = 0;
        SetAllAnimatorRef();
        LeftArmAnimator.SetBool("Part2", true);
        LeftHandAnimator.SetBool("Part2", true);
        yield return new WaitForSeconds(1f);
        
        
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
        LeftArmAnimator.SetBool("Part2", false);
        LeftHandAnimator.SetBool("Part2", false);

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

                pattern01FirstDir = leftArmOriginalPos.position - leftArmPoint.position;
                leftArm01Rb.velocity = pattern01FirstDir * returnSpeed;
                comeBackTimer -= Time.deltaTime;
                yield return null;
            }

            pattern01FirstDir = new Vector3(0, 0, 0);
            leftArm01Rb.velocity = pattern01FirstDir * 0;
            yield return new WaitForSeconds(0.25f);
            leftArm01Rb.position = leftSpawnArmPoint.transform.position;
        LeftArmAnimator.SetBool("Part3", true);
        LeftHandAnimator.SetBool("Part3", true);
        yield return new WaitForSeconds(0.5f);
        LeftArmAnimator.SetBool("Part3", false);
        LeftHandAnimator.SetBool("Part3", false);
        LeftArmAnimator.SetBool("Part4", true);
        LeftHandAnimator.SetBool("Part4", true);
        LeftArmAnimator.SetInteger("Ref", -1);
        LeftHandAnimator.SetInteger("Ref", -1);
        leftArm01.GetComponent<BoxCollider2D>().enabled = true;
            yield return new WaitForSeconds(0.25f);
        LeftArmAnimator.SetBool("Part4", false);
        LeftHandAnimator.SetBool("Part4", false);
        Debug.Log("Refrest the pattern");
            canDoLeftArm01 = true;
            RefreshPattern();
            
    }
  

    IEnumerator Head01Pattern()
    {
        
        
        float beamPatternTimer = 3f;
        bool canSpawn = true;

        AnimatorRef = 1;
        SetAllAnimatorRef();
        MaskAnimator.SetBool("Part2", true);
        HeadAnimator.SetBool("Part2", true);

        Head01.GetComponent<BoxCollider2D>().enabled = false;
        while (beamPatternTimer > 0)
        {
            
            if (canSpawn == true)
            {
                canSpawn = false;
                yield return new WaitForSeconds(0.45f);
                
                enemyBullet = enemyBulletPrefab;
                Instantiate(enemyBullet, shotSpawnPoint.transform.position, Quaternion.identity);
                beamPatternTimer -= 0.25f;
                
                canSpawn = true;
                
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        MaskAnimator.SetBool("Part2", false);
        HeadAnimator.SetBool("Part2", false);
        MaskAnimator.SetBool("Part3", true);
        HeadAnimator.SetBool("Part3", true);
        yield return new WaitForSeconds(0.2f);
        MaskAnimator.SetBool("Part3", false);
        HeadAnimator.SetBool("Part3", false);
        MaskAnimator.SetBool("Part4", true);
        HeadAnimator.SetBool("Part4", true);

        yield return new WaitForSeconds(0.2f);
        MaskAnimator.SetBool("Part4", false);
        HeadAnimator.SetBool("Part4", false);

        MaskAnimator.SetInteger("Ref", -1);
        HeadAnimator.SetInteger("Ref", -1);
        Debug.Log("Refresh the pattern");
        canDoHead01 = true;
        RefreshPattern();
        Head01.GetComponent<BoxCollider2D>().enabled = true;
        canSpawn = true;
        
    }

    IEnumerator RightArm01Pattern()
    {
        Debug.Log("Pattern 2 has begun");
        AnimatorRef = 2;
        SetAllAnimatorRef();
        yield return new WaitForSeconds(1f);
        AnimatorRef = -1;
        SetAllAnimatorRef();
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

    void SetAllAnimatorRef()
    {
        HeadAnimator.SetInteger("Ref", AnimatorRef);
        BodyAnimator.SetInteger("Ref",AnimatorRef);
        MaskAnimator.SetInteger("Ref",AnimatorRef);
        LeftArmAnimator.SetInteger("Ref",AnimatorRef);
        LeftHandAnimator.SetInteger("Ref",AnimatorRef);
        RightArmAnimator.SetInteger("Ref",AnimatorRef) ;
}
}
