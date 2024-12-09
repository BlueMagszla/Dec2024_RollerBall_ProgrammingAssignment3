



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
        agent = GetComponent<NavMeshAgent>();
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
        Vector3 randomPosition = new Vector3(x, 0f, z); //creating vector based off of randomly generated coordinates

        Vector3 destinationPoint = randomPosition;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(destinationPoint, out hit, range, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }




    } //end of function
}
