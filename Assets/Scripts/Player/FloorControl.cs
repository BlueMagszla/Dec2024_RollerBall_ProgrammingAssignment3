/* Script to enable rotation controls. Only triggers get this component if they are to be controlled.
 * 
 * Magdalena Szlapczynski
 * LAST MODIFIED: December 1, 2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorControl : MonoBehaviour
{
    public int rotateSpeed = 100; //rotation speed of floor

    public void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player")) //if the colliding object has a player tag
        {
            if (Input.GetKey(KeyCode.D)) //rotating the object based on inputs
            {
                transform.parent.gameObject.transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime); //fifth of a unit
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.parent.gameObject.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime); //fifth of a unit
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.parent.gameObject.transform.Rotate(rotateSpeed * Time.deltaTime, 0, 0); //fifth of a unit
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.parent.gameObject.transform.Rotate(-rotateSpeed * Time.deltaTime, 0, 0); //fifth of a unit
            }
        }
    } //end of trigger stay

} //end of script
