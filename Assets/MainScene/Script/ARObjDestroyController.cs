using UnityEngine;

public class ARObjDestroyController : MonoBehaviour
{
    public GameObject arSessionOrigine;
    [HideInInspector] public PlaceOnPlane place;
    //[SerializeField] private GameObject sample;


    // Start is called before the first frame update
    void Update()
    {
        place = arSessionOrigine.GetComponent<PlaceOnPlane>();
    }

    public void ObjDestroy()
    {
        // place.spawnedObject.transform
        foreach (Transform child in place.spawnedObject.transform)
        {
            // 一つずつ破棄する
            Destroy(child.gameObject);
        }

        Destroy(place.spawnedObject);
    }
}
