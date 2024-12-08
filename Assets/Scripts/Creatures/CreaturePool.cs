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
    public static CreaturePool Instance { get; private set; } //ensures there is only one instance

    public GameObject creaturePrefab; //creature prefab from assets prefab folder
    [SerializeField] public int maxPoolSize; //maximum size of the pool
    [SerializeField] private int initialPoolSize = 5; //the size of the pool

    private ObjectPool<GameObject> pool; //managing gameobject instances

    void Start()
    {
        pool = new ObjectPool<GameObject>( //initializing the pool
            OnCreatePoolCreature,
            OnGetFromPool, //when the creature is fetched from the pool
            OnReturnToPool, //when the creature is returning to the pool
            OnDestroyCreature, //when the creature is removed from the pool
            true, //checking
            maxPoolSize //the maximum size of the pool
            );

        //preload the pool dependant with the initial pool size/amount of creatures 
        for (int i = 0; i < initialPoolSize; i++)
        {
            pool.Release(pool.Get());
            OnCreatePoolCreature();


        }
    }

    private GameObject OnCreatePoolCreature() //defines what happens when a object is created
    {
        GameObject creature = Instantiate(creaturePrefab, transform);

        //giving the object a reference to the pool
        creature.GetComponent<ReturnToPool>().Pool = pool; 

        return creature;
    }

    private void OnGetFromPool(GameObject creature) //taken from pool, activates object
    {
        creature.SetActive(true); //activate when the creatue is retrieved
        creature.transform.SetParent(null); //to stop creature from being a child to the zone parent
    }

    private void OnReturnToPool(GameObject creature) //returned to pool, deactivate object
    {
        creature.SetActive(false); //deactivate creature when returned 
    }

    private void OnDestroyCreature(GameObject creature) { //cleanup permanently removed objects, managing memory
        Destroy(creature); //clean the creature when removed from the pool
    }

    public void PositionCreature() //creating pool objects within bounds of a zone
    {
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>(); //getting the box collider component of the "zone"

        //using box collider from game object
        Vector3 boundCenter = boxCollider.bounds.center; //the middle of the collider (zone's)
        Vector3 boundSize = boxCollider.bounds.size; //the edges of ths collider (zone's)

        //random coordinates between the center and boundaries of the collision
        float x = Random.Range(boundCenter.x - boundSize.x / 2, boundCenter.x + boundSize.x / 2); 
        float z = Random.Range(boundCenter.z - boundSize.z / 2, boundCenter.z + boundSize.z / 2);
        Vector3 randomPosition = new Vector3(x, 0f, z); //creating vector based off of randomly generated coordinates

        GameObject creature = pool.Get();

        creature.transform.position = randomPosition;
        creature.transform.rotation = Quaternion.identity;

    }

    private void OnApplicationQuit()
    {
        while (pool.CountInactive > 0)
        {
            GameObject creature = pool.Get();
            Destroy(creature); //destroy all objects
        }
    }

}
