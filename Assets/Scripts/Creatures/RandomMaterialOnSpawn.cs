/* Script to attach to creatures.
 * Select meshes and apply materials.
 * Applies them at random.
 * Alternate colors for players to find.
 * 
 * Magdalena Szlapczynski
 * LAST MODIFIED: December 9, 2024
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterialOnSpawn : MonoBehaviour
{
    //arrays to holds parts of meshes
    public GameObject[] Pieces; //apply meshes from character/objects/prefabs in scene
    public Material[] Materials; //apply materials from the Unity folder
    void Start()
    {
        //select random material from the array
        int random = UnityEngine.Random.Range(0, Materials.Length); //random selection
        foreach (var piece in Pieces) { //for each piece, apply a material
            piece.GetComponent<Renderer>().material = Materials[random]; //material stored in renderer

        } //end of for loop

    } //end of start
}
