using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public delegate void ReturnedToPool(); //defining a callback method
    public ReturnedToPool ReturnHandler; //stores reference to callback action, allows to assign method
}
