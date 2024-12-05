/* Script for pooling creatures.
 * 
 * Magdalena Szlapczynski
 * LAST MODIFIED: December 5, 2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateFencePost : MonoBehaviour
{
    public GameObject postPrefab; //assigning the cube game object
    public int numberOfCubes = 5; //how many posts
    public float spacing = 2f; //distance between each of the posts
    void Start()
    {
        for (int i = 0; i < numberOfCubes; i++) //to duplicate the cubes 
        {
            Vector3 position = new Vector3(i * spacing, 0, 0); //multiplying each and including spacing
            Instantiate(postPrefab, position, postPrefab.transform.rotation); //duplicating the objects at runtime
        }
    }
}
