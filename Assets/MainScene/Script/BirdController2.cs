using UnityEngine;

public class BirdController2 : MonoBehaviour
{

    public GameObject gObject;

    private float speed;

    // Use this for initialization
    void Start()
    {
        speed = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(gObject.transform.position, Vector3.up, speed);
    }
}
