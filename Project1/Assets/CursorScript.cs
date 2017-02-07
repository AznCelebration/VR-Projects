using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CursorScript : MonoBehaviour {
    public Transform Reticle;
    public Transform parentCam;
    public Image Loading;
    private Vector3 cursorhit;
    private float retGaze;
	// Use this for initialization
	void Start () {
        parentCam = Reticle.parent;
	}

    // Update is called once per frame
    void Update() {
        Ray look = new Ray(parentCam.position, parentCam.forward);
        RaycastHit hit;
        if (Physics.Raycast(look, out hit, Mathf.Infinity)) {
            if (hit.distance > 30) {
                Reticle.position = parentCam.position + parentCam.forward.normalized * 25;
                Reticle.LookAt(parentCam.position);
            }
            else {
                Reticle.position = hit.point + hit.normal.normalized * 0.1f;
                Reticle.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            }
            
        }
        else {
            Reticle.position = parentCam.position + parentCam.forward.normalized * 25;
            Reticle.LookAt(parentCam.position);
        }
        if (Reticle.name == "LoadingReticle") {
            Loading.fillAmount = retGaze / 100;
        }
    }

    void UpdateGaze(int gaze) {
        retGaze = gaze;
    }
}
