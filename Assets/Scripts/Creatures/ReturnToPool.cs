/* Script for returning to pool 
 * 
 * 
 * Magdalena Szlapczynski
 * LAST MODIFIED: December 7, 2024
 */

using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(PoolableObject))]
public class ReturnToPool : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; } //keep instances of the game objects

    // Start is called before the first frame update
    public void Awake()
    {
        GetComponent<PoolableObject>().ReturnHandler = OnReturnToPool; //reference to poolable object code
    }
    public void OnReturnToPool()
    {
        Pool.Release(gameObject); //release back to the pool
    }
}
