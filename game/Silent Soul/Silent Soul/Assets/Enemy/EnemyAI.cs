using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float checkradius;
    public float attackradius;
    public bool shouldrotate;
    public LayerMask whatisplayer;
    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    public GameObject player;
    public Vector3 dir;
    public SpriteRenderer spriteRenderer;
    private bool isinchaserange;
    private bool isinattackrange;
    public int BossHealth;
    private int currentHealth;
    public int health;
    public PolygonCollider2D AttackLeft;
    public PolygonCollider2D AttackRight;
    public SpriteRenderer sprite;
    public float cooldown = 3;
    public float cooldownTimer;
    public Health bossHealthBar;
    public AudioSource enemyDies;
    public int amountDamage;

    private void Start()
    {
        if(gameObject.tag == "Boss")
        {
            currentHealth = BossHealth;
            bossHealthBar.SetMaxHealth(BossHealth);
        }
        if(gameObject.tag == "Enemy")
        {
            currentHealth = health;
            bossHealthBar.SetMaxHealth(health);
        }
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        anim.SetBool("isDead", false);
        AttackLeft.enabled = false;
    }

    private void Update()
    {
        anim.SetBool("isRunning", isinattackrange);

        isinchaserange = Physics2D.OverlapCircle(transform.position, checkradius, whatisplayer);
        isinattackrange = Physics2D.OverlapCircle(transform.position, attackradius, whatisplayer);

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        if (shouldrotate)
        {
            anim.SetFloat("X", dir.x);
            anim.SetFloat("Y", dir.y);
        }

        AttackThePlayer();
    }


      


    void AttackThePlayer(){


        if (isinchaserange && !isinattackrange && anim.GetBool("isDeadBoss") == false && anim.GetBool("isDead") == false)
        {
            movecharacter(movement);
            anim.SetBool("Attack", false);
            
        }

        if (isinattackrange && anim.GetBool("isDeadBoss") == false && anim.GetBool("isDead") == false)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Attack", true);
            movecharacter(movement);
  
            

            if (spriteRenderer.flipX = dir.x < 0)
            {
                transform.position += speed * Time.deltaTime * dir;
                AttackLeft.enabled = true;
                AttackRight.enabled = false;
            }
            else
                {
                transform.position += speed * Time.deltaTime * dir;
                AttackLeft.enabled = false;
                AttackRight.enabled = true;
            }
        }
    }
    

    void movecharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (speed * Time.deltaTime * dir));
    }

    public void TakeDamage(int amountDamage)
    {
        health -= amountDamage;
        Debug.Log("Damage:"+amountDamage);
        bossHealthBar.SetHealth(health);
        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            Debug.Log("test");
            if (!enemyDies.isPlaying)
            {
                enemyDies.Play();
            }
            anim.SetBool("isDead", true);
            rb.simulated = false;
            Destroy(gameObject, 1.5f);
        }
    }

    public void BossTakeDamage(int amountDamage)
    {
        BossHealth -= amountDamage;
        bossHealthBar.SetHealth(BossHealth);
        StartCoroutine(FlashRed());

        if (BossHealth <= 0)
        {
            if (!enemyDies.isPlaying)
            {
                enemyDies.Play();
            }
            rb.simulated = false;
            anim.SetBool("isDeadBoss", true);
            Destroy(gameObject, 2f);
        }

    }
/*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Animator>().GetBool("Attacking")
            && collision.gameObject.CompareTag("Player")
            && gameObject.tag != "Boss")
        {
            TakeDamage(amountDamage);
            
        }

        if (collision.gameObject.GetComponent<Animator>().GetBool("Attacking")
            && collision.gameObject.CompareTag("Player")
            && gameObject.tag == "Boss")
        {
            BossTakeDamage(amountDamage);
            
        }


    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Animator>().GetBool("Attacking")
            && collision.gameObject.CompareTag("Player") 
            && gameObject.tag != "Boss")
        {
            TakeDamage(amountDamage);
            
        }

        if (collision.gameObject.GetComponent<Animator>().GetBool("Attacking")
            && collision.gameObject.CompareTag("Player")
            && gameObject.tag == "Boss")
        {
            BossTakeDamage(amountDamage);
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Animator>().GetBool("Attacking")
            && collision.gameObject.CompareTag("Player")
            && gameObject.tag != "Boss")
        {
            TakeDamage(amountDamage);
            
        }

        if (collision.gameObject.GetComponent<Animator>().GetBool("Attacking")
            && collision.gameObject.CompareTag("Player")
            && gameObject.tag == "Boss")
        {
            BossTakeDamage(amountDamage);
            
        }


    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "MyWeapon"){
           StartCoroutine(FlashRed());
        }
    }
*/
    public IEnumerator FlashRed(){
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }



}
