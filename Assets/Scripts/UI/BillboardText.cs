/* Script to attach to the creature prefab's text component in order to have the text face towards the camera.
 * 
 * Magdalena Szlapczynski
 * LAST MODIFIED: December 8, 2024
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardText : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180f, 0); //to flip the billboarding, its backwards without this
    }

}
