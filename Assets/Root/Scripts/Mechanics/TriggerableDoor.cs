using UnityEngine;

public class TriggerableDoor : TriggerableBehaviour
{
    // MEMBERS
    [SerializeField] protected GameObject door;

    // PROPERTIES
    public GameObject Door => door;

    // METHODS
    public override void Trigger()
    {
        // Disable door
        // (1) if the gobj is the door geometry
        // (2) if the collider is on it
        // (3) if the NavMeshObstacle component is on
        // then
        // (1) the player can see the door is gone
        // (2) the player can pass through the door opening
        // (3) the AI can pass through the door opening
        door.SetActive(false);
    }
}
