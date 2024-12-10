/* Script for pooling creatures.
 * Attach to zones and provide which creatures will spawn in the editor.
 * You can add multiple instances of this script for different creatures to spawn in the same zone.
 * Specifies the trophy (achievement) to activate in the scene.
 * 
 * Magdalena Szlapczynski
 * LAST MODIFIED: December 9, 2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

//ensures the object has a BoxCollider, adds one at runtime to prevent errors if there is not one already
[RequireComponent(typeof(BoxCollider))]
public class CreaturePool : MonoBehaviour
{
    public GameObject creaturePrefab; //creature prefab from assets prefab folder
    public int poolSize; //maximum size of the pool
    public int spawnDelay = 0; //time in seconds between the creature respawns

    public GameObject trophy; //game object in scene to activate
    private ObjectPool<GameObject> creaturePool; //managing gameobject instances

    void Start()
    {
        creaturePool = new ObjectPool<GameObject>( //initializing the pool
            CreateCreature, //when the creature is first initialized
            SpawnCreature, //when the creature is fetched from the pool
            ReturnCreature, //when the creature is returning to the pool
            DestroyCreature, //when the creature is removed from the pool
            true, 
            poolSize //the maximum size of the pool
            );

        for (int i = 0; i < poolSize; i++) //loop through pool size to spawn all creatures 
        {
            creaturePool.Get(); //spawns the creature on start 
        }
    }

    private GameObject CreateCreature() //defines what happens when a object is created
    {
        GameObject creature = Instantiate(creaturePrefab, GetSpawnPosition(), Quaternion.identity); //instantiate the creature
        creature.SetActive(false); //start deactivated
        creature.GetComponent<ReturnToPool>().Pool = creaturePool; //setting reference
        creature.GetComponent<CreatureData>().walkableArea = GetComponent<BoxCollider>(); //setting reference

        return creature; 
    }

    private void SpawnCreature(GameObject creature) //taken from pool, activates object
    {
        creature.SetActive(true); //activate when the creature is retrieved
        creature.GetComponent<CreatureData>().SetLevel(); //set a new level each time a creature spawns
        creature.transform.position = GetSpawnPosition(); //set random spawn position within the zone boundary
        creature.transform.SetParent(null); //to stop the creature from being a child to the zone
    }

    private void ReturnCreature(GameObject creature) //returned to pool, deactivate object
    {
        creature.SetActive(false); //deactivate creature when returned 
        RespawnCreature(); //respawn
    }

    private void DestroyCreature(GameObject creature)
    { //cleanup permanently removed objects, managing memory
        Destroy(creature); //clean the creature when removed from the pool
    }

    private Vector3 GetSpawnPosition() //creating pool objects within bounds of a zone
    {
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>(); //getting the box collider component of the "zone"

        //using box collider from game object
        Vector3 boundCenter = boxCollider.bounds.center; //the middle of the collider (zone's)
        Vector3 boundSize = boxCollider.bounds.size; //the edges of ths collider (zone's)

        //random coordinates between the center and boundaries of the collision
        float x = Random.Range(boundCenter.x - boundSize.x / 2, boundCenter.x + boundSize.x / 2);
        float z = Random.Range(boundCenter.z - boundSize.z / 2, boundCenter.z + boundSize.z / 2);
        Vector3 randomPosition = new Vector3(x, boundCenter.y + 5, z); //creating vector based off of randomly generated coordinates

        return randomPosition;
    }

    private void RespawnCreature() 
    {
        trophy.SetActive(true); //setting the trophy object active
        StartCoroutine(Wait()); //delay before spawning
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(spawnDelay); //delays the respawn of the creature
        creaturePool.Get(); //spawns the creature from pool
    }

    private void OnApplicationQuit() //ensures everything is destroyed
    {
        creaturePool.Clear(); //destroys everything
    }

}