using UnityEngine;

public class SphereCommands : MonoBehaviour
{
    Vector3 _OriginalPosition;

    void Start()
    {
        _OriginalPosition = this.transform.localPosition;
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        // If the sphere has no Rigidbody component, add one to enable physics.
        if (!this.GetComponent<Rigidbody>())
        {
            var rigidbody = this.gameObject.AddComponent<Rigidbody>();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }

    void OnReset()
    {
        //Remove physics first
        var rigidBody = this.GetComponent<Rigidbody>();

        if (rigidBody != null)
        {
            DestroyImmediate(rigidBody);
            this.transform.localPosition = _OriginalPosition;
        }
    }

    void OnDrop()
    {
        OnSelect();
    }
}