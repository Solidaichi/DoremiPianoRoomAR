using UnityEngine;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageRecognitionManager : MonoBehaviour
{

    private ARTrackedImageManager _arTrackedImageManager;

    void Awake()
    {
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    public void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            Debug.Log(trackedImage.name);
            trackedImage.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        }


        foreach (var trackedImage in args.updated)
        {
            Debug.Log(trackedImage.name);
            trackedImage.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        }
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */

}
