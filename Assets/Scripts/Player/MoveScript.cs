/* Script for movement of AI agent creatures.
 * 
 * Magdalena Szlapczynski
 * LAST MODIFIED: December 7, 2024
 */

using UnityEngine;
using UnityEngine.AI;

public class MoveScript : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    private void OnValidate()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //mouse position, temp

        {
            Ray mouseToWorldRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mouseToWorldRay, out RaycastHit hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        }
    }
}
