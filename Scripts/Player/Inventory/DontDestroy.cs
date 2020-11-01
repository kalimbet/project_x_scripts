using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance = null;

    private void Start()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

}
