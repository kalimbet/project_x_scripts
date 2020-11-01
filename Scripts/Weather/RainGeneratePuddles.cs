using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainGeneratePuddles : MonoBehaviour
{
    public ParticleSystem puddleEffect;
    public List<ParticleCollisionEvent> collisionEvents;
    new ParticleSystem particleSystem; // REDO

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
        //BoxCollider2D box = other.GetComponent<BoxCollider2D>();
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb)
            {
                Vector3 pos = collisionEvents[i].intersection;
                //Debug.Log("Collided " + pos);

                //puddleEffect.transform
                puddleEffect.Play();
            }
        }

    }

}
