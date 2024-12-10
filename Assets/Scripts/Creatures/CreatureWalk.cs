/* Script for allowing the creatures to walk.
 * Calculates points for agents to walk to.
 * 
 * Magdalena Szlapczynski
 * LAST MODIFIED: December 9, 2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureWalk : MonoBehaviour
{
    private NavMeshAgent agent; //reference to agent component
    public float range; //the range the agent will go

    private void OnValidate() //editor specific
    {
        if (agent == null) { agent = GetComponent<NavMeshAgent>(); } //checks if there is a agent component
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //getting nav mesh agent
    }
    void Update()
    {       //built in agent functions
        if (agent.remainingDistance <= agent.stoppingDistance) //if the path of the agent is finished, meets stopping distance.
        {
            SetNextDestination();
           
        }
    } //end of update
    private void SetNextDestination()
    {
        BoxCollider boxCollider = gameObject.GetComponent<CreatureData>().walkableArea; //getting the box collider component of the "zone"

        //using box collider from game object
        Vector3 boundCenter = boxCollider.bounds.center; //the middle of the collider (zone's)
        Vector3 boundSize = boxCollider.bounds.size; //the edges of ths collider (zone's)

        //random coordinates between the center and boundaries of the collision
        float x = Random.Range(boundCenter.x - boundSize.x / 2, boundCenter.x + boundSize.x / 2);
        float z = Random.Range(boundCenter.z - boundSize.z / 2, boundCenter.z + boundSize.z / 2);

        Vector3 randomPosition = new Vector3(x, gameObject.transform.position.y, z); //creating vector based off of randomly generated coordinates
        Vector3 destinationPoint = randomPosition; //setting the destination point of the agent

        NavMeshHit hit; 
        if (NavMesh.SamplePosition(destinationPoint, out hit, range, NavMesh.AllAreas)) //finds a point for agent to go
        {
            agent.SetDestination(hit.position); //set as destination for the nav agent 
        } //end of if statement
    } //end of function
}
