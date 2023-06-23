using UnityEngine;
using UnityEngine.UI;

public class ImageFadeManager : MonoBehaviour
{

    private GameObject imageObj; // 自分のオブジェクト取得用変数
    [SerializeField] private float fadeStart = 1f; // フェード開始時間
    private bool fadeIn; // trueの場合はフェードイン
    [SerializeField] private float fadeSpeed = 1f; // フェード速度指定
    [HideInInspector] public bool textBool;

    // Start is called before the first frame update
    void Start()
    {
        imageObj = this.gameObject; // 自分のオブジェクト取得
        fadeIn = true;
        textBool = false;
    }

    // Update is called once per frame
    void Update()
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

    private void fadeOutFunction()
    {
        Debug.Log("alpha : " + imageObj.GetComponent<Image>().color.a);
        if (imageObj.GetComponent<Image>().color.a > -5)
        {
            //Debug.Log("fadeout");
            Color tmp = imageObj.GetComponent<Image>().color;
            tmp.a = tmp.a - (Time.deltaTime * fadeSpeed);
            imageObj.GetComponent<Image>().color = tmp;
        }
        else
        {
            Invoke("TextFadeActFunction", 3f);
        }

    }

    void fadeInFunction()
    {
        if (imageObj.GetComponent<Image>().color.a < 255)
        {

            Color tmp = imageObj.GetComponent<Image>().color;
            tmp.a = tmp.a + (Time.deltaTime * fadeSpeed);
            imageObj.GetComponent<Image>().color = tmp;
        }

        Invoke("FadeSwtFunction", 5f);
    }

    void FadeSwtFunction()
    {
        fadeIn = false;
    }

    void TextFadeActFunction()
    {
        textBool = true;
        this.GetComponent<ImageFadeManager>().enabled = false;
    }
}