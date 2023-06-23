using UnityEngine;

public class PianoStartController : MonoBehaviour
{
    //[SerializeField] private GameObject placeOnPlaneObj;
    public GameObject pianoSound;

    //[HideInInspector] public PlaceOnPlane placeOnPlane;
    public SampleSound sampleSound;

    private void Start()
    {
        //placeOnPlane = placeOnPlaneObj.GetComponent<PlaceOnPlane>();
        sampleSound = pianoSound.GetComponent<SampleSound>();
    }

    public void PianoStart()
    {
        //placeOnPlane.pianoStartBool = true;
        sampleSound.pianoStartBool = true;
        Debug.Log("BtnTrue");
    }
}
