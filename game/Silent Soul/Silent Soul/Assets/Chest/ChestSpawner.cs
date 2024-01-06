using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chest;
    public float SpawnRate;
    private bool canSpawn = true;
    private float maxSpawned;
    private float count = 0;
    public Grid grid;
    private List<GameObject> chestList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartSpawn();
    }
    public void Awake()
    {
        maxSpawned = Random.Range(2, 4);
    }
    public void EnemySpawn()
    {
        while (canSpawn)
        {
            float randXPos = Random.Range(grid.transform.position.x - 6, grid.transform.position.x + 6);
            float randYPOS = Random.Range(grid.transform.position.y - 4, grid.transform.position.y);
            Vector2 SpawnPos = new Vector2(randXPos, randYPOS);

            GameObject newSpawned = Instantiate(chest, SpawnPos, Quaternion.identity);
            chestList.Add(newSpawned);
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
        foreach (GameObject go in chestList)
        {
            if (go.GetComponent<Animator>().GetBool("isClosed"))
            {
                chestList.Remove(go);
            }
        }
    }

}
