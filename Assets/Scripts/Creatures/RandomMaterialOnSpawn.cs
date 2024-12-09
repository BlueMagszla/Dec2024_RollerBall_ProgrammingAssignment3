using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomMaterialOnSpawn : MonoBehaviour
{
    public GameObject[] Pieces;
    public Material[] Materials;
    // Start is called before the first frame update
    void Start()
    {
        int random = UnityEngine.Random.Range(0, Materials.Length);
        foreach (var piece in Pieces) {
            piece.GetComponent<Renderer>().material = Materials[random];
        }
    }
}
