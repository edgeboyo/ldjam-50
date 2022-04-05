using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

public class LevelGen : MonoBehaviour
{
    private GameObject platforms;
    private Transform platformsHolder;

    public GameObject[] upPlats;
    public GameObject[] downPlats;
    public GameObject[] flatPlats;
    
    private GameObject playerInst;
    public GameObject player;
    
    public void generate(int size)
    {
        checkScene();
        makeLevel(size);
        instantiatePlayer();
    }
    
    // gather what's in scene
    private void checkScene()
    {
        playerInst = GameObject.FindWithTag("Player");
        
        GameObject[] brds = GameObject.FindGameObjectsWithTag("Floor");
        foreach (GameObject brd in brds)
        {
            Destroy(brd);
        }
    }
    
    public void makeLevel(int size)
    {
        platforms = new GameObject("Platforms");
        platforms.tag = "Floor";
        platformsHolder = platforms.transform;
        platforms.AddComponent<CompositeCollider2D>();
        platforms.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        
        Vector2 currentPos = Vector2.zero;
        int currentPlats = 0;

        GameObject lastPlat = null;
        GameObject[] lastPlats = null;

        while (currentPlats < size)
        {
            GameObject toPlat = null;
            GameObject toInst = null;
                
            if (currentPlats == 0)
            {
                toPlat = randomFlat();
                toInst = Instantiate(toPlat, currentPos, Quaternion.identity) as GameObject;
                
                currentPlats++;
                lastPlat = toPlat;
                lastPlats = flatPlats;
                currentPos.x += toPlat.GetComponent<SpriteRenderer>().bounds.size.x;
                
                toInst.transform.SetParent(platformsHolder);
                continue;
            }
            
            // choose any
            int choice = Random.Range(1, 4);
            switch (choice)
            {
                case 1:
                    toPlat = randomFlat();
                    lastPlats = flatPlats;
                    break;
                case 2:
                    toPlat = randomUp();
                    lastPlats = upPlats;
                    break;
                case 3:
                    toPlat = randomDown();
                    lastPlats = downPlats;
                    break;
                default:
                    Debug.Log("Uh Oh Stinkyyyyy, Poopy");
                    break;
            }

            float yDiff = Math.Abs(toPlat.GetComponent<SpriteRenderer>().bounds.size.y -
                                   lastPlat.GetComponent<SpriteRenderer>().bounds.size.y);
            
            toInst = Instantiate(toPlat, new Vector2(currentPos.x, currentPos.y + yDiff), Quaternion.identity) as GameObject;

            currentPlats++;
            currentPos.x += toPlat.GetComponent<SpriteRenderer>().bounds.size.x;
            currentPos.y += yDiff;
            lastPlat = toPlat;
            
            toInst.transform.SetParent(platformsHolder);
        }
    }

    GameObject randomFlat()
    {
        return flatPlats[Random.Range(0,flatPlats.Length)];
    }
    
    GameObject randomUp()
    {
        return upPlats[Random.Range(0,upPlats.Length)];
    }
    
    GameObject randomDown()
    {
        return downPlats[Random.Range(0,downPlats.Length)];
    }
    
    GameObject randomDownB()
    {
        return downPlats[Random.Range(1,downPlats.Length)];
    }
    
    void instantiatePlayer()
    {
        if (playerInst == null)
        {
            playerInst = Instantiate(player, new Vector3(0, 1, 0f), Quaternion.identity) as GameObject;
        }
        else
        {
            playerInst.transform.position = new Vector3(0, 1, 0);
        }
    }
}
