using UnityEngine;

// Creates an "infinite" background. Scroll a background along with the player camera.
// Attach this script to main center sprite of background.

public class ScrollBackgroundLegacy : MonoBehaviour
{
    private float length, startposX, startposY;
    public GameObject Camera;
    public float horizontalParallaxEffect; // How much to scroll the background. Bigger the value, slower the scroll.
    public float verticalParallaxEffect;
    
    
    void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;       
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
        } else if (temp < startposX - length)
        {
            startposX -= length;
        }
    }
}
