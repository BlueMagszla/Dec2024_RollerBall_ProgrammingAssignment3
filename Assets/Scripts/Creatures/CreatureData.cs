/* Script to keep information for the creature that the script is attached to.
 * 
 * Magdalena Szlapczynski
 * LAST MODIFIED: December 4, 2024
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatureData : MonoBehaviour
{

    [SerializeField, Tooltip("From 1 to 10")] public float CatchChance; //catch chance each time player collides with creature
    [SerializeField] public float ScaleMultiplier; //the scale amount that gets multiplied by creature level

    public string CreatureName; 
    public int level; //creatures level

    public TextMeshProUGUI textMeshPro; //for the UI texture
    private void Start() //starts this before anything else
    {
        level = Random.Range(1, 50); //setting a random level for the creature

        //transforming size of creature based on its level
        gameObject.transform.localScale = new Vector3(1f + ScaleMultiplier * level, 1f + ScaleMultiplier * level, 1f + ScaleMultiplier * level);

        textMeshPro.text = "Level " + level;

    } //end of start()

} //end of class
