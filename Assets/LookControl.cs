using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookControl : MonoBehaviour {
    public Transform cam;
    private int gazeDuration = 0;
    // Use this for initialization
    void Start () {
        Debug.DrawRay(cam.position, cam.forward, Color.red, 10.0f);
    }
	
	// Update is called once per frame
	void Update () {
        GameObject prev = null;
        
        Ray look = new Ray(cam.position, cam.forward);
        RaycastHit hit;
        if (Physics.Raycast(look, out hit, Mathf.Infinity)) {
            GameObject currLook = hit.collider.gameObject;
            if (prev == null) {
                prev = currLook;
            }
            if(prev == currLook) {
                gazeDuration++;
            }
            else {
                prev = currLook;
                gazeDuration = 0;
            }
            if (currLook.tag == "Brick" && gazeDuration == 10) {
                Destroy(currLook);
                gazeDuration = 0;
            }
        }
	}
}
