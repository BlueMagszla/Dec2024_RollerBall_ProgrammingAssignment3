/* Script for managing the score and the combos for the player.
 * 
 * 
 * Magdalena Szlapczynski
 * LAST MODIFIED: December 7, 2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using TMPro;
using System.Diagnostics.CodeAnalysis;

public class ScoreScript : MonoBehaviour
{
    //player score
    public int playerScore; 
    //variables for combo
    public int comboCount = 0; //start it at 0, keeps count of combo
    int scoreBonus = 0; //the current bonus being added
    public string currentComboType; //the current creature for the combo


    //variables for timer
    public float comboTimer = 5f; //time window for the combo
    private float timer;

    public TMP_Text textMeshPro; //for the UI texture

    void Update()
    {
        if (comboCount > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //resetting the combo if the timer runs out
                currentComboType = null; 
                comboCount = 0;
            }
        }

    }
    public void OnCreatureCaught(GameObject creature)
    {
        
        string creatureType = creature.GetComponent<CreatureData>().creatureName; //get the creature's name

        if (creatureType == currentComboType) //if the creature's name matches the current combo type
        {
            comboCount++; //add one to the counter 
        }
        else //if it does not match, change to new creature type and reset combo
        {
            currentComboType = creatureType;
            comboCount = 1;
        }

        timer = comboTimer; //reset the timer every time the player makes the same kind of catch

        scoreBonus = comboCount * 10; //multiplier for score bonus
        playerScore = playerScore + scoreBonus; //add combo 

        Debug.Log("Combo is: " + comboCount + (" and the score is: " + playerScore));

        textMeshPro.text = "Score: " + playerScore;
    }
}