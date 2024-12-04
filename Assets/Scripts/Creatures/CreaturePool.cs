/* Script for pooling creatures.
 * 
 * Magdalena Szlapczynski
 * LAST MODIFIED: December 1, 2024
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject creaturePrefab;
    public int poolSize = 10;
    private Queue<GameObject> pool;


    void Start()
    {
        pool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(creaturePrefab); //instantiate prefab
            obj.transform.position = new Vector3(i * 10, 0, 0); //TEMPORARY
        }
    }
}
