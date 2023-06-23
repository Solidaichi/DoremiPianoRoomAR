using UnityEngine;
using UnityEngine.UI;


[DisallowMultipleComponent]     // 複数アタッチできないようにするため
public class MicrophoneController : MonoBehaviour
{
    // 波形を描画する
    public LineRenderer line;
    public Text text;

    // マイクからの音を拾う
    private new AudioSource audio;
    //private string mic_name = "UAB-80";

    // 波形描画のための変数
    private float[] wave;
    private int wave_num;
    private int wave_count;


    void Start()
    {

        // 波形描画のための変数の初期化
        wave_num = 300;
        wave = new float[wave_num];
        wave_count = 0;

        audio = GetComponent<AudioSource>();

        audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 10, 44100);  // マイクからのAudio-InをAudioSourceに流す
        audio.loop = true;                                      // ループ再生にしておく

        while (!(Microphone.GetPosition("") > 0)) { }             // マイクが取れるまで待つ。空文字でデフォルトのマイクを探してくれる
        audio.Play();
    }

    void FixedUpdate()
    {
        // 諸々の解析
        float hertz = NoteNameDetector.AnalyzeSound(audio, 1024, 0.04f);
        float scale = NoteNameDetector.ConvertHertzToScale(hertz);
        string s = NoteNameDetector.ConvertScaleToString(scale);
        //Debug.Log(hertz + "Hz, Scale:" + scale + ", " + s);
        //Debug.Log("A+");
        text.text = s;

        if (s == "A2")
        {
            var natureObj = GameObject.FindWithTag("A2").transform;
            Debug.Log(natureObj.name);
            foreach (Transform child in natureObj.transform)
            {
                Debug.Log(child.name);
                child.gameObject.SetActive(true);
            }
        }
        else if (s == "D2")
        {
            var natureObj2 = GameObject.FindWithTag("D2").transform;
            foreach (Transform child in natureObj2.transform)
            {
                Debug.Log(child.name);
                child.gameObject.SetActive(true);
            }
        }
        else if (s == "C2")
        {
            var natureObj3 = GameObject.FindWithTag("C2").transform;
            foreach (Transform child in natureObj3.transform)
            {
                Debug.Log(child.name);
                child.gameObject.SetActive(true);
            }
        }


        // 波形描画
        wave[wave_count] = scale;
        NoteNameDetector.ScaleWave(wave, wave_count, line);
        wave_count++;
        if (wave_count >= wave_num) wave_count = 0;
    }
}
