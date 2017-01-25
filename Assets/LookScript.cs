using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookScript : MonoBehaviour {
    public Transform cam;
    public GameObject shootController;
    private GameObject prev = null;
    private int gazeDuration = 0;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        Ray look = new Ray(cam.position, cam.forward);
        RaycastHit hit;
        if (Physics.Raycast(look, out hit, Mathf.Infinity)) {
            GameObject currLook = hit.collider.gameObject;
            if (currLook.tag == "Interact") {
                if (prev == null) {
                    prev = currLook;
                }
                if (prev == currLook) {
                    if (!(currLook.name == "BuildWallControl")) {
                        gazeDuration++;
                    }
                    else if (!currLook.GetComponent<BuildWallScript>().stacking) {
                        gazeDuration++;
                    }
                }
                else {
                    prev = currLook;
                    gazeDuration = 0;
                }
                if (gazeDuration == 100) {
                    if (currLook.name == "Brick(Clone)") {
                        shootController.SendMessage("Shoot", currLook);
                    }

                    else if (currLook.name == "BuildWallControl") {
                        if (!currLook.GetComponent<BuildWallScript>().stacking) {
                            currLook.SendMessage("Rebuild");
                        }
                    }
                    else if (currLook.name == "MenuUI") {
                        if (!currLook.GetComponent<MenuScript>().On) {
                            currLook.SendMessage("Open");
                        }
                        
                    }
                    gazeDuration = 0;
                }
                
            }
            else {
                gazeDuration = 0;
            }
            cam.GetChild(1).SendMessage("UpdateGaze", gazeDuration);
        }
        else {
            gazeDuration = 0;
            cam.GetChild(1).SendMessage("UpdateGaze", gazeDuration);
        }
	}
}
