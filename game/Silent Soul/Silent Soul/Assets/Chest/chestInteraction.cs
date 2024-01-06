using UnityEngine;
public class chestInteraction : MonoBehaviour
{
    private Animator animator;
    private readonly float moreDamage = 1f / 2.5f;
    private readonly float monsterSpawns = 1f / 10f;
    private bool triggeredOnce = true;
    private float dropChance;
    private bool itemDropped = false;
    public GameObject boots;
    public GameObject healpotion;
    public GameObject sword;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isOpening", false);
        animator.SetBool("isClosed", false);
        dropChance = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 vector2 = new(gameObject.transform.position.x, gameObject.transform.position.y);
        if (collision.gameObject.CompareTag("Player") && triggeredOnce)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            animator.SetBool("isOpening", true);

            if(dropChance <= monsterSpawns) // erstmal prüfen, ob Random <= 10 %
            {
                //Movement.moveSpeed -= 4f;
                // TODO create Monster with Animation
                Instantiate(boots, vector2, Quaternion.identity);
                itemDropped = true;
            }
            else if(dropChance <= moreDamage && !itemDropped) // danach prüfen, ob Random <= 40 %
            {
                /*
                 * how much more damage will he do or is he getting more speed?
                 */
                float damageORspeed = Random.Range(0f, 1f);
                float halve = 1f / 2f;
                if(damageORspeed <= halve)
                {
                    Instantiate(sword, vector2, Quaternion.identity);
                    itemDropped = true;
                }
                else if (!itemDropped)
                {
                    Instantiate(boots, vector2, Quaternion.identity);
                    itemDropped = true;
                }
            }
            else if(!itemDropped) // wenn beide nicht zutreffen => Spieler wird geheilt
            {
                // Healing and changing of healthbar happens in the movement script
                // spawn healthpotion
                Instantiate(healpotion, vector2, Quaternion.identity);
            }
            triggeredOnce = false;
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !triggeredOnce)
        {
            animator.SetBool("isClosed", true);
            animator.SetBool("isOpening", false);
            Destroy(gameObject, 1.5f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !triggeredOnce)
        {
            animator.SetBool("isClosed", true);
            animator.SetBool("isOpening", false);
            Destroy(gameObject, 1.5f);
        }
    }
}
