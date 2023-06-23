using UnityEngine;
using UnityEngine.UI;


public class TextFadeManager : MonoBehaviour
{

    private GameObject textObj; // 自分のオブジェクト取得用変数
    [SerializeField] private float fadeStart = 1f; // フェード開始時間
    private bool fadeIn; // trueの場合はフェードイン
    [SerializeField] private float fadeSpeed = 1f; // フェード速度指定

    public GameObject imageObj;
    [HideInInspector] public ImageFadeManager imageFadeManager;

    // Start is called before the first frame update
    void Start()
    {
        textObj = this.gameObject; // 自分のオブジェクト取得
        fadeIn = true;

        imageFadeManager = imageObj.GetComponent<ImageFadeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (imageFadeManager.textBool)
        {
            if (fadeStart > 0f)
            {
                fadeStart -= Time.deltaTime;
            }
            else
            {
                if (fadeIn)
                {
                    fadeInFunction();
                }
                else
                {
                    fadeOutFunction();
                }
            }
        }

    }

    private void fadeOutFunction()
    {
        Debug.Log("fadeout");
        Color tmp = textObj.GetComponent<Text>().color;
        tmp.a = tmp.a - (Time.deltaTime * fadeSpeed);
        textObj.GetComponent<Text>().color = tmp;
    }

    void fadeInFunction()
    {
        if (imageObj.GetComponent<Image>().color.a < 255)
        {

            Color tmp = textObj.GetComponent<Text>().color;
            tmp.a = tmp.a + (Time.deltaTime * fadeSpeed);
            textObj.GetComponent<Text>().color = tmp;
        }

        Invoke("FadeSwtFunction", 6f);
    }

    void FadeSwtFunction()
    {
        fadeIn = false;
    }
}