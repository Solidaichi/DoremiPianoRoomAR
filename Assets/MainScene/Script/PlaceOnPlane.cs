using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;




[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField, Tooltip("AR空間に表示するプレハブを登録")] GameObject arObj;
    public GameObject birdSoundObj, windSoundObj, uiObj;

    [HideInInspector] public bool pianoStartBool;

    public GameObject spawnedObject;
    private ARRaycastManager raycastManager;
    //[HideInInspector] bool destroyObj;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool pianoBtn, microphoneSwt;
    private AudioSource audio;

    private GameObject arObjects_Wood, arObjects_Rock, arObjects_Parrot;
    private int rand;

    [SerializeField]
    private string[] arObjTags = { "Rock", "Wood", "Parrot" };

    private List<Transform> arObjChild_Rock = new List<Transform>(); //Listの宣言
    private List<Transform> arObjChild_Wood = new List<Transform>(); //Listの宣言
    private List<Transform> arObjChild_Parrot = new List<Transform>(); //Listの宣言

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Start()
    {

        audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 10, 44100);  // マイクからのAudio-InをAudioSourceに流す
        audio.loop = true;                                      // ループ再生にしておく
        //audio.mute = true;                                      // マイクからの入力音なので音を流す必要がない
        while (!(Microphone.GetPosition("") > 0)) { }             // マイクが取れるまで待つ。空文字でデフォルトのマイクを探してくれる
        audio.Play();

        pianoBtn = false;
        microphoneSwt = false;
    }

    void Update()
    {
        //Debug.Log("Spawnobject : " + spawnedObject);
        if (Input.touchCount > 0)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            if (raycastManager.Raycast(touchPosition, hits, TrackableType.Planes))
            {
                // Raycastの衝突情報は距離によってソートされるため、0番目が最も近い場所でヒットした情報となります

                var hitPose = hits[0].pose;

                if (spawnedObject)
                {
                    //spawnedObject.transform.position = hitPose.position;
                }
                else
                {
                    Debug.Log("Spawn");
                    spawnedObject = Instantiate(arObj, hitPose.position, Quaternion.identity);
                    AwakeObjArrayRock(arObjTags[0]);
                    AwakeObjArrayWood(arObjTags[1]);
                    AwakeObjArrayParrot(arObjTags[2]);

                    //birdSound.Play();
                    //windSound.Play();

                    pianoBtn = true;
                    microphoneSwt = true;
                }
            }
        }

        if (pianoBtn)
        {
            uiObj.SetActive(true);
        }

        if (microphoneSwt)
        {
            MicrophoneManager();
        }


    }

    void MicrophoneManager()
    {
        // 諸々の解析
        float hertz = NoteNameDetector.AnalyzeSound(audio, 1024, 0.04f);
        float scale = NoteNameDetector.ConvertHertzToScale(hertz);
        string s = NoteNameDetector.ConvertScaleToString(scale);
        Debug.Log(hertz + "Hz, Scale:" + scale + ", " + s);
        //Debug.Log("A+");

        if (s.Contains("C") || s.Contains("D") || s.Contains("E"))
        {
            rand = Random.Range(0, arObjChild_Rock.Count - 1);
            if (!arObjChild_Rock[rand].gameObject.activeSelf)
            {
                arObjChild_Rock[rand].gameObject.SetActive(true);
            }

        }
        else if (s.Contains("F") || s.Contains("G") || s == "A+")
        {
            rand = Random.Range(0, arObjChild_Rock.Count - 1);
            if (!arObjChild_Wood[rand].gameObject.activeSelf)
            {
                arObjChild_Wood[rand].gameObject.SetActive(true);
            }

        }
        else if (s.Contains("B"))
        {
            rand = Random.Range(0, arObjChild_Parrot.Count - 1);
            {
                arObjChild_Parrot[rand].gameObject.SetActive(true);
            }
        }
    }

    void AwakeObjArrayRock(string tag)
    {
        var natureTransform = GameObject.FindWithTag(tag).transform;


        foreach (Transform child in natureTransform.transform)
        {
            //Debug.Log();
            arObjChild_Rock.Add(child);
            //Debug.Log(arChildObjects_Wood[i]);
            child.gameObject.SetActive(false);

        }
    }

    void AwakeObjArrayWood(string tag)
    {
        var natureTransform = GameObject.FindWithTag(tag).transform;
        int i = 0;

        foreach (Transform child in natureTransform.transform)
        {
            //Debug.Log();
            arObjChild_Wood.Add(child);
            //Debug.Log(arChildObjects_Wood[i]);
            child.gameObject.SetActive(false);
            i++;
        }
    }

    void AwakeObjArrayParrot(string tag)
    {
        var natureTransform = GameObject.FindWithTag(tag).transform;


        foreach (Transform child in natureTransform.transform)
        {
            //Debug.Log();
            arObjChild_Parrot.Add(child);
            //Debug.Log(arChildObjects_Wood[i]);
            //child.gameObject.SetActive(false);

        }
    }
}
