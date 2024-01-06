using UnityEngine;
public class chestInteraction1 : MonoBehaviour
{
    private Animator animator;
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
                // Healing and changing of healthbar happens in the movement script
                // spawn healthpotion
                Instantiate(healpotion, vector2, Quaternion.identity);
            
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
