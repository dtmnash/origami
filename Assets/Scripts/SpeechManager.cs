using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer _KeywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        keywords.Add("Reset World", () =>
        {
            this.BroadcastMessage("OnReset");
        });

        keywords.Add("Drop Sphere", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;

            if (focusObject != null)
                focusObject.SendMessage("OnDrop");
        });

        _KeywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        _KeywordRecognizer.OnPhraseRecognized += (args) =>
        {
            System.Action keywordAction;
            if (keywords.TryGetValue(args.text, out keywordAction))
            {
                keywordAction.Invoke();
            }
        };

        _KeywordRecognizer.Start();
    }
}
