using UnityEngine;

public class SampleSound : MonoBehaviour
{
    //public GameObject pianoSoundObj;
    private AudioSource pianoSound;

    public bool pianoStartBool;

    private bool pianoOnceBool;

    // Start is called before the first frame update
    void Start()
    {
        pianoSound = GetComponent<AudioSource>();
        //Debug.Log("pianoSound" + pianoSound);
        pianoStartBool = false;
        pianoOnceBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pianoStartBool && !pianoOnceBool)
        {
            Invoke("PianoStart", 1.0f);

            Debug.Log("PianoStart" + pianoSound);

            pianoOnceBool = true;
        }
        else if (!pianoStartBool && pianoOnceBool)
        {
            pianoSound.Stop();
            pianoOnceBool = false;
        }
    }

    void PianoStart()
    {
        pianoSound.Play();
    }
}
