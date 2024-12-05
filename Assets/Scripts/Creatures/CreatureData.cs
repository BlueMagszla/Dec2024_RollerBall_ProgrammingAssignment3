/* Script to keep information for the creature that the script is attached to.
 * Can be applied to any creature. 
 * Information includes creature name, creature level, creature catch chance, and creature pool size.
 * Creatures size changes based on level, the higher the level (randomized) mulitply by decided multiplier.
 * Scale multiplier is adjustable in case for any scene size changes, etc
 * 
 * Magdalena Szlapczynski
 * LAST MODIFIED: December 5, 2024
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatureData : MonoBehaviour
{
    [SerializeField, Tooltip("From 1 to 10")] public float CatchChance; //catch chance each time player collides with creature
    [SerializeField] public float ScaleMultiplier; //the scale amount that gets multiplied by creature level

    public string CreatureName; //name of the creature
    protected int level; //creatures level
    public TextMeshProUGUI textMeshPro; //for the UI texture above the creatures

    private void Start() 
    {
        level = Random.Range(1, 50); //setting a random level for the creature

        //transforming size of creature based on its level
        gameObject.transform.localScale = new Vector3(1f + ScaleMultiplier * level, 1f + ScaleMultiplier * level, 1f + ScaleMultiplier * level);

        textMeshPro.text = "Level " + level;

    } //end of start()

} //end of class
