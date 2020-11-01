using UnityEngine;

// Creates an "infinite" background. Scroll a background along with the player camera.
// This is an altered version of the script which should be attached to an empty gameobject. All 3 background elements must be children to this object.

public class ScrollBackgroundGameObject : MonoBehaviour
{
    private float length, startposX, startposY;
    public GameObject Camera;
    public float horizontalParallaxEffect; // How much to scroll the background. Bigger the value, slower the scroll.
    public float verticalParallaxEffect;


    void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        length = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x; //This sould be the central sprite!
    }


    void Update()
    {
        float temp = (Camera.transform.position.x * (1 - horizontalParallaxEffect));
        // float tempY = (Camera.transform.position.y * (1 - verticalParallaxEffect));

        float distX = (Camera.transform.position.x * horizontalParallaxEffect);
        float distY = (Camera.transform.position.y * verticalParallaxEffect);

        transform.position = new Vector3(startposX + distX, startposY + distY, transform.position.z);

        // duplicate the backgrounds.
        if (temp > startposX + length)
        {
            startposX += length;
        }
        else if (temp < startposX - length)
        {
            startposX -= length;
        }
    }
}
