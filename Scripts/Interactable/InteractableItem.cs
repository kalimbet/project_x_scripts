using UnityEngine;

// Base class to be used for any interactable object in the game.
// Interactalbe objects should extend from this class.
public class InteractableItem : MonoBehaviour
{
    public float radius = 1f; // interactable radius
    public Transform interactionTransform; // from where we interact with an item

    CircleCollider2D trigger; // remember to trigger only on player\layer!

    int playerLayerIndex = 9;   // Index of the player level. Objects will interact only with things on player level.

    // bool hasInteracted = false;

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }

    private void Start() // Creates an interactable trigger zone
    {
        trigger = gameObject.AddComponent<CircleCollider2D>();
        trigger.radius = radius * 4;
        trigger.isTrigger = true;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == playerLayerIndex)  // avoid interacting with enemies
        {
            Interact();
        }        
    }

    void OnDrawGizmosSelected() // Draw interaction radius in editor
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);

    }

}
