using UnityEngine;

public class TouchObjController : MonoBehaviour
{
    [SerializeField]
    private SelectedObject[] selectedObjects;

    [SerializeField]
    private Camera arCamera;

    private Vector2 touchPosition;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObj;
                if (Physics.Raycast(ray, out hitObj))
                {
                    SelectedObject selectedObjects = hitObj.transform.GetComponent<SelectedObject>();
                    if (selectedObjects != null)
                    {
                        ChangeSelectedObject(selectedObjects);
                    }
                }
            }
        }
    }

    private void ChangeSelectedObject(SelectedObject selected)
    {
        foreach (SelectedObject current in selectedObjects)
        {
            if (selected != current)
            {
                current.IsSelected = false;

            }
            else
            {
                current.IsSelected = true;
                Debug.Log("TouchWood" + current);
            }
        }
    }
}
