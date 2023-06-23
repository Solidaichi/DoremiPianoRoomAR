using System.Collections.Generic;
using UnityEngine;

public class MicrophoneInstantiateController : MonoBehaviour
{
    // マイクからの音を拾う
    private new AudioSource audio;
    //private string mic_name = "UAB-80";

    private GameObject arObjects_Wood, arObjects_Rock, arObjects_Parrot;
    private int rand;

    [SerializeField]
    private string[] arObjTags = { "Rock", "Wood", "Parrot" };

    private List<Transform> arObjChild_Rock = new List<Transform>(); //Listの宣言
    private List<Transform> arObjChild_Wood = new List<Transform>(); //Listの宣言
    private List<Transform> arObjChild_Parrot = new List<Transform>(); //Listの宣言

    private void Awake()
    {
        AwakeObjArrayRock(arObjTags[0]);
        AwakeObjArrayWood(arObjTags[1]);
        AwakeObjArrayParrot(arObjTags[2]);
    }

    void Start()
    {

        audio = GetComponent<AudioSource>();

        audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 10, 44100);  // マイクからのAudio-InをAudioSourceに流す
        audio.loop = true;                                      // ループ再生にしておく
        //audio.mute = true;                                      // マイクからの入力音なので音を流す必要がない
        while (!(Microphone.GetPosition("") > 0)) { }             // マイクが取れるまで待つ。空文字でデフォルトのマイクを探してくれる
        audio.Play();
    }

    void Update()
    {
        // 諸々の解析
        float hertz = NoteNameDetector.AnalyzeSound(audio, 1024, 0.04f);
        float scale = NoteNameDetector.ConvertHertzToScale(hertz);
        string s = NoteNameDetector.ConvertScaleToString(scale);
        //Debug.Log(hertz + "Hz, Scale:" + scale + ", " + s);
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
