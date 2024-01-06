using UnityEngine;

public class doorOpening : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    private Animator animator;
    public AudioSource audioSource;
    private bool playedOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isOpening", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemySpawner.CanSpawn() && enemySpawner.enemyCount() == 0)
        {
            animator.SetBool("isOpening", true);
            Destroy(gameObject.GetComponent<BoxCollider2D>(), 2f);
            if (!audioSource.isPlaying && playedOnce && audioSource != null)
            {
                audioSource.Play();
                playedOnce = false;
            }
        }
    }
}
