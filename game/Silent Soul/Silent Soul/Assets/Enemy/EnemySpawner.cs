using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public float SpawnRate;
    private bool canSpawn = true;
    private float maxSpawned;
    private float count = 0;
    public Grid grid;
    private List<GameObject> enemyList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartSpawn();
    }
    public void Awake()
    {
        if(enemy.tag == "Enemy")
        {
            maxSpawned = Random.Range(2, 5);
        }
        if(enemy.tag == "Boss")
        {
            maxSpawned = 1;
        }
    }
    public void EnemySpawn()
    {
        while (canSpawn)
        {
            float randXPos = Random.Range(grid.transform.position.x - 5.5f, grid.transform.position.x + 5.5f);
            float randYPOS = Random.Range(grid.transform.position.y - 4, grid.transform.position.y);
            Vector2 SpawnPos = new Vector2(randXPos, randYPOS);

            GameObject newSpawned = Instantiate(enemy, SpawnPos, Quaternion.identity);
            enemyList.Add(newSpawned);
            count++;
            if (count == maxSpawned)
            {
                StopSpawn();
            }
        }
    }

    public void StartSpawn()
    {
        InvokeRepeating("EnemySpawn", 1f, SpawnRate);
    }

    public bool CanSpawn()
    {
        return canSpawn;
    }

    public void StopSpawn()
    {
        canSpawn = false;
    }

    public void Update()
    {
        foreach (GameObject go in enemyList)
        {
            go.GetComponent<EnemyAI>().amountDamage = player.GetComponent<Movement>().amountDamage;
            if (go.GetComponent<Animator>().GetBool("isDead") || go.GetComponent<Animator>().GetBool("isDeadBoss"))
            {
                enemyList.Remove(go);
            }
        }
    }

    public int enemyCount()
    {
        return enemyList.Count;
    }

    public List<GameObject> returnList()
    {
        return enemyList;
    }
}
