using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPatternLoop : MonoBehaviour
{
    protected PlayerStat playerStat;
    protected RockSpawning rockSpawn;
    protected BossPartStat leftArm01Stat;
    protected BossPartStat rightArm01Stat;
    protected BossPartStat head01Stat;
    protected SimpleCameraShakeInCinemachine cameraShake;

    public float bossHealth;
    protected int patternRef;
    protected int patternSaved;
    public bool canTakeDamage = true;
    protected bool canDoAnotherOne = true;
    public Slider healthBar;
    List<int> patternList = new List<int>();

    public bool phase2Started;
    public bool isDead;
    public GameObject explosionMaker;

    // Les Animators du boss
    public Animator HeadAnimator;
    public Animator BodyAnimator;
    public Animator MaskAnimator;
    public Animator LeftArmAnimator;
    public Animator LeftHandAnimator;
    public Animator RightArmAnimator;
    public int AnimatorRef;

    // leftArmPattern condition and object
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
    public Color Part1Color = Color.white;
    public Color Part2Color = Color.cyan;
    public Color Part3Color = Color.green;
    public Color leftArmColor;
    public Color rightArmColor;
    public Color headColor;

    public GameObject fistImpactFX;

    public float leftArmRespawn;

    public GameObject rockProjectile;
    public Vector2 rockProjDir = new Vector2 (0,-1);

    public float patternPart2Timer;

    public GameObject burnZone;

    //HeadPattern condition and object
    protected GameObject Head01;
    public GameObject Head01Prefab;
    protected bool canDoHead01 = true;
    public Transform HeadPoint;
    public Transform HeadSpawnPoint;
    protected bool canSpawn = true;

    public Transform newTargetPoint;
    public GameObject enemyHomingBulletPrefab;
    public GameObject enemyRandomBulletPrefab;
    public GameObject enemyBulletPrefab;
    public GameObject enemyBouncyBulletPrefab;
    protected Vector2 head01BulletDir;
    public float enemyBulletSpeed = 300f;
    public Rigidbody2D enemyBulletRb;
    protected GameObject enemyBullet;
    public Transform shotSpawnPoint;

    public float headRespawn;

    //RightArmPattern Condition and object

    public GameObject rightArm01;
    public GameObject rightArm01Prefab;
    public Transform rightArmSpawnPoint;
    protected bool canDoRightArm01 = true;
    protected Animator rightArm01Animator;
    public GameObject horizontalDamagedGround;
    public Transform horizontalDamagedGroundSpawn;
    public GameObject verticalDamagedGround;
    public Transform verticalDamagedGroundSpawn;

    public int rightArmRespawn;

    //BossPhase2 Condition and object

    public bool phase2IsOn;

    // Start is called before the first frame update
    public GameObject victoryScreen;

    void Start()
    {
        cameraShake = GameObject.FindGameObjectWithTag("CameraShakeManager").GetComponent<SimpleCameraShakeInCinemachine>();

        rockSpawn = GameObject.FindGameObjectWithTag("RockSpawnBoss").GetComponent<RockSpawning>();
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();

        healthBar.value = bossHealth;
        phase2Started = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        prepareAttackPoint = GameObject.FindGameObjectWithTag("prepareAttackPoint").transform;
        ImpactPoint = GameObject.FindGameObjectWithTag("ImpactPoint").transform;


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

        RandomizeBoss();

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
                {
                    StartCoroutine(LeftArmPattern01());
                }
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
        //Pattern RightArm
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
        RightArmChangeColors();
        LeftArmChangeColors();
        HeadChangeColors();
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
            leftArm01Stat.partHealth = 450;
            leftArm01Stat.hasRespawned = true;
            leftArmRespawn = Random.Range(0, 3);
            if (leftArmRespawn == 0)
            {
                leftArm01.tag = "bossLeftArm01";
            }
            if (leftArmRespawn == 1)
            {
                leftArm01.tag = "bossLeftArm01";
            }
            if (leftArmRespawn == 2)
            {
                leftArm01.tag = "bossLeftArm03";
            }
            yield return new WaitForSeconds(0.5f);
            canDoLeftArm01 = true; 
            RefreshPattern();
        }

    }

    IEnumerator RespawnRightArm()
    {
        if (rightArm01Stat.partHealth <= 0)
        {
            yield return new WaitForSeconds(1.5f);
            rightArm01Stat.partHealth = 450;
            rightArm01Stat.hasRespawned = true;
            rightArmRespawn = Random.Range(0, 3);
            if(rightArmRespawn == 0)
            {
                rightArm01.tag = "bossRightArm01";
            }
            if (rightArmRespawn == 1)
            {
                rightArm01.tag = "bossRightArm02";
            }
            if (rightArmRespawn == 2)
            {
                rightArm01.tag = "bossRightArm03";
            }
            yield return new WaitForSeconds(0.5f);
            canDoRightArm01 = true;
            RefreshPattern();
        }

    }

    IEnumerator RespawnHead()
    {
        if (head01Stat.partHealth <= 0)
        {
            yield return new WaitForSeconds(1.5f);
            head01Stat.partHealth = 350;
            head01Stat.hasRespawned = true;
            headRespawn = Random.Range(0, 3);
            if (headRespawn == 0)
            {
                Head01.tag = "bossHead01";
            }
            if (headRespawn == 1)
            {
                Head01.tag = "bossHead02";
            }
            if (headRespawn == 2)
            {
                Head01.tag = "bossHead03";
            }
            yield return new WaitForSeconds(0.5f);
            canDoHead01 = true;
            RefreshPattern();
        }

    }

    IEnumerator LeftArmPattern01()
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
                    leftArm01.GetComponent<SpriteRenderer>().material.color = leftArmColor;
                }


                yield return null;
            }
        LeftArmAnimator.SetBool("Part2", false);
        LeftHandAnimator.SetBool("Part2", false);

        // PatternPart2
        if (leftArm01Stat.partHealth > 0)
        {


            leftArm01.GetComponent<BoxCollider2D>().enabled = true;
            GameObject impactPointSpawn = Instantiate(impactPointSpawnPrefab, ImpactPoint.transform.position, Quaternion.identity);
            pattern01FirstDir = impactPointSpawn.transform.position - leftArmPoint.position;
            leftArm01Rb.velocity = pattern01FirstDir * impactSpeed;
            yield return new WaitForSeconds(0.1f);




            //PatternPart3
            pattern01FirstDir = new Vector3(0, 0, 0);
            leftArm01Rb.velocity = pattern01FirstDir * 1;
            cameraShake.Shake();
            Instantiate(fistImpactFX, leftArm01.transform.position - new Vector3(4.5f, 0), Quaternion.identity);
            if (leftArm01.CompareTag("bossLeftArm02"))
            {
                float x = 0.5f;
                float y = -0.5f;
                float time = 2f;
                rockProjDir = new Vector2(0, -1);
                while (time > 0)
                {
                    time -= 0.25f;
                    Instantiate(rockProjectile, impactPointSpawn.transform.position, Quaternion.identity);
                    if (rockProjDir.x == 1 || rockProjDir.x == -1)
                    {
                        x = -x;

                    }
                    if (rockProjDir.y == 1 || rockProjDir.y == -1)
                    {
                        y = -y;
                    }
                    rockProjDir.x += x;
                    rockProjDir.y += y;
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else if (leftArm01.CompareTag("bossLeftArm03"))
            {
                Instantiate(burnZone, impactPointSpawn.transform.position, Quaternion.identity);
            }

            Destroy(impactPointSpawn);
            yield return new WaitForSeconds(2f);
            leftArm01.GetComponent<BoxCollider2D>().enabled = false;
            if (leftArm01.CompareTag("bossLeftArm01"))
            {
                patternPart2Timer = 1f;
                while (patternPart2Timer > 0 && leftArm01)
                {

                    pattern01FirstDir = prepareAttackPoint.position - leftArmPoint.position;
                    leftArm01Rb.velocity = pattern01FirstDir * moveSpeed;
                    patternPart2Timer -= Time.deltaTime;
                    if (patternPart2Timer <= 0.3f && patternPart2Timer > 0.1f)
                    {
                        leftArm01.GetComponent<SpriteRenderer>().material.color = dashNowColor;
                    }
                    else
                    {
                        leftArm01.GetComponent<SpriteRenderer>().material.color = leftArmColor;
                    }


                    yield return null;
                }
                LeftArmAnimator.SetBool("Part2", false);
                LeftHandAnimator.SetBool("Part2", false);

                if (leftArm01Stat.partHealth > 0)
                {
                    leftArm01.GetComponent<BoxCollider2D>().enabled = true;
                    impactPointSpawn = Instantiate(impactPointSpawnPrefab, ImpactPoint.transform.position, Quaternion.identity);
                    pattern01FirstDir = impactPointSpawn.transform.position - leftArmPoint.position;
                    leftArm01Rb.velocity = pattern01FirstDir * impactSpeed;
                    yield return new WaitForSeconds(0.1f);
                    pattern01FirstDir = new Vector3(0, 0, 0);
                    leftArm01Rb.velocity = pattern01FirstDir * 1;
                    cameraShake.Shake();
                    Instantiate(fistImpactFX, leftArm01.transform.position - new Vector3(4.5f, 0), Quaternion.identity);
                }

                yield return new WaitForSeconds(2f);
            }
        }



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
        if (Head01.tag == "bossHead01")
        {
            MaskAnimator.SetBool("Part2", true);
            HeadAnimator.SetBool("Part2", true);

            Head01.GetComponent<BoxCollider2D>().enabled = false;
            while (beamPatternTimer > 0)
            {

                if (canSpawn == true)
                {
                    canSpawn = false;
                    yield return new WaitForSeconds(0.45f);

                    enemyBullet = enemyHomingBulletPrefab;
                    Instantiate(enemyBullet, shotSpawnPoint.transform.position, Quaternion.identity);
                    beamPatternTimer -= 0.25f;

                    canSpawn = true;

                }
                yield return null;
            }
          
        }
        else if (Head01.tag == "bossHead02")
        {
            MaskAnimator.SetBool("Part2", true);
            HeadAnimator.SetBool("Part2", true);
            Head01.GetComponent<BoxCollider2D>().enabled = false;

            while (beamPatternTimer > 0)
            {

                if (canSpawn == true)
                {
                    canSpawn = false;
                    yield return new WaitForSeconds(0.15f);

                    enemyBullet = enemyRandomBulletPrefab;
                    Instantiate(enemyBullet, shotSpawnPoint.transform.position, Quaternion.identity);
                    beamPatternTimer -= 0.10f;

                    canSpawn = true;

                }
            }
        }

        else if (Head01.tag == "bossHead03")
        {
            MaskAnimator.SetBool("Part2", true);
            HeadAnimator.SetBool("Part2", true);
            Head01.GetComponent<BoxCollider2D>().enabled = false;

            for (int i = 0; i < 2;  i++)
            {

                enemyBullet = enemyBouncyBulletPrefab;
                Instantiate(enemyBullet, shotSpawnPoint.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }
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
        yield return new WaitForSeconds(1.5f);

        if (rightArm01.CompareTag("bossRightArm01"))
        {
            Instantiate(horizontalDamagedGround, horizontalDamagedGroundSpawn.position, Quaternion.identity);
        }
        else if (rightArm01.CompareTag("bossRightArm02"))
        {
            Instantiate(verticalDamagedGround, verticalDamagedGroundSpawn.position,Quaternion.identity);
        }
        else if (rightArm01.CompareTag("bossRightArm03"))
        {
            rockSpawn.brkRockNbr = 20;
            rockSpawn.BossSpawnObject();
        }
        yield return new WaitForSeconds(0.5f);
        AnimatorRef = -1;
        SetAllAnimatorRef();
        yield return new WaitForSeconds(1f);
      
        canDoRightArm01 = true;
        RefreshPattern();
    }
   
    void bossDeath()
    {
        if (bossHealth <= 0 && isDead == false)
        {
            isDead = true;
            Debug.Log(" you won !");
            Instantiate(explosionMaker, gameObject.transform.position, Quaternion.identity);
            MaskAnimator.SetTrigger("dead");
            LeftArmAnimator.SetTrigger("dead");
            RightArmAnimator.SetTrigger("dead");
            HeadAnimator.SetTrigger("dead");
            LeftHandAnimator.SetTrigger("dead");

            StopAllCoroutines();
            StartCoroutine(BossDeathAnimation());
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

    void LeftArmChangeColors()
    {

        leftArm01.GetComponent<SpriteRenderer>().color = leftArmColor;
        if (leftArm01.CompareTag("bossLeftArm01"))
        {
            leftArmColor = Part1Color;
        }
        else if (leftArm01.CompareTag("bossLeftArm02"))
        {
            leftArmColor = Part2Color;
        }
        else if (leftArm01.CompareTag("bossLeftArm03"))
        {
            leftArmColor = Part3Color;
        }
    }

    void RightArmChangeColors()
    {

        rightArm01.GetComponent<SpriteRenderer>().color = rightArmColor;
        if (rightArm01.CompareTag("bossRightArm01"))
        {
            rightArmColor = Part1Color;
        }
        else if (rightArm01.CompareTag("bossRightArm02"))
        {
            rightArmColor = Part2Color;
        }
        else if (rightArm01.CompareTag("bossRightArm03"))
        {
            rightArmColor = Part3Color;
        }
    }

    void HeadChangeColors()
    {

        Head01.GetComponent<SpriteRenderer>().color = headColor;
        if (Head01.CompareTag("bossHead01"))
        {
            headColor = Part1Color;
        }
        else if (Head01.CompareTag("bossHead02"))
        {
            headColor = Part2Color;
        }
        else if (Head01.CompareTag("bossHead03"))
        {
            headColor = Part3Color;
        }
    }

    void RandomizeBoss()
    {
        int headRandom = Random.Range(0,3);
        Debug.Log("random head =" + headRandom);
        int leftArmRandom = Random.Range(0, 3);
        Debug.Log("random head =" + leftArmRandom);
        int RightArmRandom = Random.Range(0, 3);
        Debug.Log("random head =" + RightArmRandom);
        //Randomiez Head
        if(headRandom == 0)
        {
            Head01.tag = "bossHead01";
        }

        else if (headRandom == 1)
        {
            Head01.tag = "bossHead02";
        }

        else if (headRandom == 2)
        {
            Head01.tag = "bossHead03";
        }

        //Randomize LeftArm01
        if (leftArmRandom == 0)
        {
            leftArm01.tag ="bossLeftArm01";
        }

        else if (leftArmRandom == 1)
        {
            leftArm01.tag = "bossLeftArm02";
        }

        else if (leftArmRandom == 2)
        {
            leftArm01.tag = "bossLeftArm03";
        }

        //Randomize RightArm01
        if (RightArmRandom == 0)
        {
            rightArm01.tag ="bossRightArm01";
        }

        else if (RightArmRandom == 1)
        {
            rightArm01.tag = "bossRightArm02";
        }

        else if (RightArmRandom == 2)
        {
            rightArm01.tag = "bossRightArm03";
        }



    }

    IEnumerator BossDeathAnimation()
    {
        GameObject rockSpawner = GameObject.FindGameObjectWithTag("RockSpawnBoss");
        Destroy(rockSpawner);
        HeadAnimator.SetBool("Dead", true);
        RightArmAnimator.SetBool("Dead", true);
        LeftArmAnimator.SetBool("Dead", true);
        LeftHandAnimator.SetBool("Dead",true);
        MaskAnimator.SetBool("Dead",true);
        yield return new WaitForSeconds(4f);
        Destroy(leftArm01);
        Destroy(rightArm01);
        Destroy(Head01);
        yield return new WaitForSeconds(1f);
        Instantiate(victoryScreen);
        Destroy(gameObject);
    }

}
