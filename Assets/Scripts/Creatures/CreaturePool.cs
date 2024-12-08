/* Script for pooling creatures.
 * Attach to zones and provide which creatures will spawn in editor.
 * You can add multiple for different creatures to spawn in the specific zone.
 * 
 * Magdalena Szlapczynski
 * LAST MODIFIED: December 7, 2024
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
    //public static CreaturePool Instance { get; private set; } //ensures there is only one instance

    public GameObject creaturePrefab; //creature prefab from assets prefab folder
    public int poolSize; //maximum size of the pool
    public int spawnDelay = 0;

    public GameObject trophy;

    private ObjectPool<GameObject> creaturePool; //managing gameobject instances

    void Start()
    {
        creaturePool = new ObjectPool<GameObject>( //initializing the pool
            CreateCreature,
            SpawnCreature, //when the creature is fetched from the pool
            ReturnCreature, //when the creature is returning to the pool
            DestroyCreature, //when the creature is removed from the pool
            true, //checking
            poolSize //the maximum size of the pool
            );

        for (int i = 0; i < poolSize; i++)
        {
            creaturePool.Get();
        }
    }

    private GameObject CreateCreature() //defines what happens when a object is created
    {
        GameObject creature = Instantiate(creaturePrefab, GetSpawnPosition(), Quaternion.identity);
        creature.SetActive(false);
        creature.GetComponent<ReturnToPool>().Pool = creaturePool;


        return creature;
    }

    private void SpawnCreature(GameObject creature) //taken from pool, activates object
    {
        creature.SetActive(true); //activate when the creatue is retrieved

        creature.GetComponent<CreatureData>().SetLevel(); //set a new level each time a creature spawns

        creature.transform.position = GetSpawnPosition(); //set random spawn position within the zone boundary

        creature.transform.SetParent(null); //to stop creature from being a child to the zone parent
    }

    private void ReturnCreature(GameObject creature) //returned to pool, deactivate object
    {
        creature.SetActive(false); //deactivate creature when returned 
        RespawnCreature();
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
        Vector3 randomPosition = new Vector3(x, 0f, z); //creating vector based off of randomly generated coordinates

        return randomPosition;
    }

    private void RespawnCreature()
    {
        trophy.SetActive(true);
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(spawnDelay); //delays the respawn of the creature
        creaturePool.Get();
    }

    private void OnApplicationQuit()
    {
        while (creaturePool.CountInactive > 0)
        {
            GameObject creature = creaturePool.Get();
            Destroy(creature); //destroy all objects
        }
    }

}