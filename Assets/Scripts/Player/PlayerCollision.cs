/* Script for handling player collision
 * 
 * Magdalena Szlapczynski
 * LAST MODIFIED: December 6, 2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Const;

public class PlayerCollision : MonoBehaviour
{
    private ScoreScript scoreScript;

    public GameObject damager; //damage volume to activate and hurt the player

    private void Start()
    {
        scoreScript = GetComponent<ScoreScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Creature"))
        {
            
           int catchRoll = Random.Range(1, 10); //random int between 1-10
            print("Catch roll generated: " + catchRoll);

            if (catchRoll >= other.gameObject.GetComponent<CreatureData>().CatchChance)
            { //successful catch

                other.gameObject.GetComponent<ReturnToPool>().Pool.Release(other.gameObject);
                scoreScript.OnCreatureCaught(other.gameObject);

            }
            else
            { //unsuccessful catch
                print("Try again");

                Vector3 impulse = ((this.transform.position - other.transform.position).normalized * 15f); //how far the player is thrown in opposite direction
                impulse.y = 5; //how hard player gets thrown upwards
                GetComponent<Rigidbody>().AddForce(impulse, ForceMode.Impulse); //push back from creature

                damager.GetComponent<DamageVolume>().TryDamage(gameObject);
            

            }

        }

    }

}
