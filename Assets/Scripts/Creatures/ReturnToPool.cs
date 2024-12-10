/* Script for returning to pool.
 * Stores reference to pool creature originates from. 
 * Used by PlayerCollision script to return creature to pool once caught. 
 * 
 * Magdalena Szlapczynski
 * LAST MODIFIED: December 7, 2024
 */

using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(PoolableObject))]
public class ReturnToPool : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; } //reference to pool that the creature belongs to
    public void Awake()
    {
        GetComponent<PoolableObject>().ReturnHandler = OnReturnToPool; 
    }
    public void OnReturnToPool()
    {
        Pool.Release(gameObject); //release back to the pool
    }
}
