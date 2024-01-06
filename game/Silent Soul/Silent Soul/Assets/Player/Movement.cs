using System.Collections;
using UnityEngine;
using TMPro;

public enum PlayerState
{
    Attacking,
    Idle
}

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public PlayerState currentState;
    public static int maxHealth = 2000;
    public static int currentHealth;
    public Animator anim;
    public Health healthBar;
    public GameObject gameOverScreen;
    public SpriteRenderer sprite;
    public AudioSource audioSource;
    public AudioSource audioWalking;
    public AudioSource audioCollecting;
    public AudioSource audioHealing;
    public float cooldown = 3;
    public float cooldownTimer;
    public EdgeCollider2D boss;
    private bool statChange = false;
    public int amountDamage = 20;
    public TMP_Text healpotionText;
    public TMP_Text swordText;
    public TMP_Text bootsText;
    private int counterHealpotion = 0;
    private int counterSword = 0;
    private int counterBoots = 0;
    public BoxCollider2D ColliderUp;
    public BoxCollider2D StandartBoxCollider;


    void React()
    {
        transform.position = new Vector3(18.422f, -22.434f, 0);
    }



    void Start()
    {
        currentState = PlayerState.Idle;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        ColliderUp.enabled = false;
        StandartBoxCollider.enabled = true;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        PlayerAttack();
        PlayerDead();
        UpdateAnimationsAndMove();
    }


    bool touched = false;
    void FixedUpdate() {

        // find the current active attack gameobject
        GameObject attack;
        attack = transform.Find("LeftAttack").gameObject;
        if (attack && !attack.activeSelf) attack = transform.Find("RightAttack").gameObject;
        if (attack && !attack.activeSelf) attack = transform.Find("UpAttack").gameObject;
        if (attack && !attack.activeSelf) attack = transform.Find("DownAttack").gameObject;

        // we have an active attack
        if (attack && attack.activeSelf) {

            // current attack has hit already
            if (touched)
                return;

            // get all enemies
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] bosse = GameObject.FindGameObjectsWithTag("Boss");


            foreach (GameObject enemy in enemies) {
                BoxCollider2D bc = enemy.GetComponent<BoxCollider2D>();  // the "body" of the enemy
                PolygonCollider2D pc = attack.GetComponent<PolygonCollider2D>();  // the area of the weapon

                // check if they are touching
                if (bc.IsTouching(pc)) {

                    // this attack animation has reached some enemies
                    // but we dont want to decrease enemy health multiple times
                    // so we remember it has hit
                    touched = true;

                    EnemyAI ai = enemy.GetComponent<EnemyAI>();
                    ai.TakeDamage(ai.amountDamage);
                    StartCoroutine(ai.FlashRed());
                    Debug.Log("health: " + ai.health);
                }
            }

            foreach (GameObject boss in bosse) {
                
                BoxCollider2D bc = boss.GetComponent<BoxCollider2D>();  // the "body" of the enemy
                PolygonCollider2D pc = attack.GetComponent<PolygonCollider2D>();  // the area of the weapon

                // check if they are touching
                if (bc.IsTouching(pc)) {

                    // this attack animation has reached some enemies
                    // but we dont want to decrease enemy health multiple times
                    // so we remember it has hit
                    touched = true;

                    EnemyAI ai = boss.GetComponent<EnemyAI>();
                    ai.BossTakeDamage(ai.amountDamage);
                    StartCoroutine(ai.FlashRed());
                    Debug.Log("health: " + ai.health);
                }
            }
        }
        else {
            // if we have no current attack anymore
            // we can reset the touch logic
            touched = false;
        }      
    }

    void PlayerAttack()
    {
         if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if(cooldownTimer < 0)
        {
            cooldownTimer = 0;    
        }

         if (Input.GetKeyDown("space") && cooldownTimer == 0 && currentState != PlayerState.Attacking)
        {
            anim.SetBool("Attacking", true);
            cooldownTimer = cooldown;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            UpdateAnimationsAndMove();
            anim.SetBool("Attacking", false);
        }
        
    }

    void PlayerDead()
    {
        if (currentHealth <= 0)
        {
            anim.SetBool("isDead", true);
            anim.SetBool("Speed", false);
            rb.simulated = false;
            gameOverScreen.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Healpotion"))
        {
            if (!audioHealing.isPlaying)
            {
                audioHealing.Play();
            }
            if (currentHealth == maxHealth)
            {
                maxHealth += 500;
                currentHealth = maxHealth;
                healthBar.SetMaxHealth(maxHealth);
            }
            else if(currentHealth != maxHealth)
            {
                int difference = maxHealth - currentHealth;
                if(difference <= 500)
                {
                    currentHealth += difference;
                    healthBar.SetHealth(currentHealth);
                }
                else if(difference > 500)
                {
                    currentHealth += 500;
                    healthBar.SetHealth(currentHealth);
                }
            }
            Destroy(collision.gameObject);
            counterHealpotion++;
            healpotionText.text = counterHealpotion.ToString();
        }
        if (collision.gameObject.CompareTag("Sword"))
        {
            if (!audioCollecting.isPlaying)
            {
                audioCollecting.Play();
            }
            float damageAmount1 = Random.Range(0f, 1f);
            float Attack40 = 1f / 3f;    //33 %
            float Attack20 = 2f / 3f;    //66 %
            amountDamage += 5;
            
            Debug.Log(amountDamage);
            Destroy(collision.gameObject);
            counterSword++;
            swordText.text = counterSword.ToString();
        }
        if (collision.gameObject.CompareTag("Boots"))
        {
            if (!audioCollecting.isPlaying)
            {
                audioCollecting.Play();
            }
            float damageAmount = Random.Range(0f, 1f);
            float Speed05 = 1f / 18f;       //5 %
            float Speed10 = 1f / 10f;       //10 %
            float Speed20 = 1f / 5f;        //20 %
            if (damageAmount <= Speed05)
            {
                moveSpeed += 1.2f;
                statChange = true;
            }
            else if (damageAmount <= Speed10 && !statChange)
            {
                moveSpeed += 1f;
                statChange = true;
            }
            else if (damageAmount <= Speed20 && !statChange)
            {
                moveSpeed += 0.5f;
                statChange = true;
            }
            else if (!statChange)
            {
                moveSpeed += 0.2f;
                statChange = true;
            }
            Destroy(collision.gameObject);
            counterBoots++;
            bootsText.text = counterBoots.ToString();
        }
    }

    void TakeDamage(int damage)
    {
        StartCoroutine(FlashRed());
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void OnTriggerEnter2D(Collider2D colission)
    {

        if (colission.CompareTag("AttackLeft") || colission.CompareTag("AttackRight") )
        {
            TakeDamage(20);
            StartCoroutine(FlashRed());
        }
    }

    

    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    void UpdateAnimationsAndMove()
    {
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);


        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (!audioWalking.isPlaying)
            {
                audioWalking.Play();
            }
            anim.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
            MoveCharacter();

        }

        if (Input.GetAxisRaw("Vertical") == 1){
            ColliderUp.enabled = true;
            StandartBoxCollider.enabled = false;
        } else if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == -1){
            ColliderUp.enabled = false;
            StandartBoxCollider.enabled = true;
        }

    }


    void MoveCharacter()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        anim.SetBool("isDead", false);
    }

}
