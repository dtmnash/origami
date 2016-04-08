using UnityEngine;
using System.Collections;

public class WorldCursor : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    // Use this for initialization
    void Start()
    {
        meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //do a raycast into the wordl based on teh user's head position
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            //Raycast hit a hologram
            meshRenderer.enabled = true;

            this.transform.position = hitInfo.point;

            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }
}
