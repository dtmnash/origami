using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GazeGestureManager : MonoBehaviour
{
    public static GazeGestureManager Instance { get; private set; }

    GestureRecognizer _Recognizer;
    public GameObject FocusedObject { get; private set; }

    // Use this for initialization
    void Start()
    {
        Instance = this;

        _Recognizer = new GestureRecognizer();
        _Recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");
            }
        };

        _Recognizer.StartCapturingGestures();
    }


    // Update is called once per frame
    void Update()
    {
        GameObject oldFocusObject = FocusedObject;

        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            FocusedObject = null;
        }

        if(FocusedObject != oldFocusObject)
        {
            _Recognizer.CancelGestures();
            _Recognizer.StartCapturingGestures();
        }
    }
}
