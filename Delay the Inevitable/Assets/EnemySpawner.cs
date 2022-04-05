using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Game;
using Profiles;
using RuntimeSets;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private Camera focusCam;
    private GameObject player;
    private PolygonCollider2D collider;
    public GameObject zombie;
    private int maxZombos;
    public List<GameObject> Spawned;
    
    [Inject] private DiContainer _container;
    [SerializeField] private EnemyProfile profile;
    [Inject] private EnemyRuntimeSet _enemyRuntimeSet;

    public GameObject[] PossibleEnemies;

    public GameObject SpawnEffect;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        maxZombos = calculateMaxZomboloids(player.GetComponent<PlayerController>().currentLevel);
        focusCam = player.GetComponentInChildren<Camera>();
        collider = this.GetComponent<PolygonCollider2D>();

        StartCoroutine(spawner());
    }

    IEnumerator spawner()
    {
        while (true)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length > maxZombos)
            {
                yield return new WaitForSeconds(5);
                continue;
            }
            yield return new WaitForSeconds(Random.Range(0, 5));
            StartCoroutine(spawnZombo());
        }
    }

    IEnumerator spawnZombo()
    {
        yield return new WaitForSeconds(Random.Range(0, 5));

        float chance = Random.Range(0, 1);
        int count = 1;
        
        // spawning multiple requires enemy on enemy collision checking, so leave count at 1 for now

        while (count > 0)
        {
            GameObject enemy = PossibleEnemies[Random.Range(0, PossibleEnemies.Length)];

            Vector2 chosenS = chooseSpawn();
            GameObject instZ = Instantiate(enemy, chosenS, Quaternion.identity) as GameObject;
            if(SpawnEffect != null)
                Instantiate(SpawnEffect, chosenS, Quaternion.identity);
            //GameObject instZ = _container.InstantiatePrefab(zombie);
            Spawned.Add(instZ);
            count--;
        }
    }

    Vector2 chooseSpawn()
    {
        Vector2 tryUp = new Vector2(player.transform.position.x + 200, player.transform.position.y);
        Vector2 tryDown = new Vector2(player.transform.position.x + 200, player.transform.position.y);
        
        // prevents taking Ls
        int lTolerance = 1100;
        int i = 0;
        
        // check 20 in height and 16 in width at top and base
        while (i < lTolerance)
        {
            int tolerance = 1100;
            int incUp = 0;
            int inDown = 0;

            while (incUp < tolerance)
            {
                // check all points are in polygon collider
                Vector2 TL = new Vector2(tryUp.x - 8, tryUp.y + 10 + incUp);
                Vector2 TR = new Vector2(tryUp.x + 8, tryUp.y + 10 + incUp);
                Vector2 BL = new Vector2(tryUp.x - 8, tryUp.y - 10 + incUp);
                Vector2 BR = new Vector2(tryUp.x + 8, tryUp.y - 10 + incUp);

                if (collider.OverlapPoint(TL) && collider.OverlapPoint(TR) && collider.OverlapPoint(BL) &&
                    collider.OverlapPoint(BR))
                {
                    return new Vector2(tryUp.x, tryUp.y + incUp);
                }
                 
                incUp += 20;
            }
            
            while (inDown < tolerance)
            {
                // check all points are in polygon collider
                Vector2 TL = new Vector2(tryDown.x - 8, tryDown.y + 10 + inDown);
                Vector2 TR = new Vector2(tryDown.x + 8, tryDown.y + 10 + inDown);
                Vector2 BL = new Vector2(tryDown.x - 8, tryDown.y - 10 + inDown);
                Vector2 BR = new Vector2(tryDown.x + 8, tryDown.y - 10 + inDown);

                if (collider.OverlapPoint(TL) && collider.OverlapPoint(TR) && collider.OverlapPoint(BL) &&
                    collider.OverlapPoint(BR))
                {
                    return new Vector2(tryDown.x, tryDown.y + inDown);
                }
                 
                inDown += 20;
            }
            
            tryUp = new Vector2(tryUp.x + 20, player.transform.position.y);
            tryDown = new Vector2(tryUp.x + 20, player.transform.position.y);

            // used for error checking
            i++;
        }

        Debug.Log("Couldn't find position" );
        return new Vector2(player.transform.position.x + 200, player.transform.position.y);
    }

    int calculateMaxZomboloids(int lvl)
    {
        int baseN = 10;
        return baseN + (5 * (lvl % 3)) + (lvl * 3);
    }
}
