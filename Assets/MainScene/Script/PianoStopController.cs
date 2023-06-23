using UnityEngine;

public class PianoStopController : MonoBehaviour
{
    //[SerializeField] private GameObject placeOnPlaneObj;

    //[HideInInspector] public PlaceOnPlane placeOnPlane;
    public GameObject sampleSoundObj;

    public SampleSound sampleSound;

    private void Start()
    {
        //placeOnPlane = placeOnPlaneObj.GetComponent<PlaceOnPlane>();
        sampleSound = sampleSoundObj.GetComponent<SampleSound>();
    }

    public void PianoStop()
    {
        //placeOnPlane.pianoStartBool = false;
        sampleSound.pianoStartBool = false;
        Debug.Log("BtnFalse");

    }
}
