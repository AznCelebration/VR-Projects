using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookScript : MonoBehaviour {
    public Transform cam;
    public GameObject shootController;
    public GameObject buildWallController;
    public GameObject player;

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
            if (currLook.tag == "Interact" || currLook.tag == "BuildWall") {
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
                    switch (currLook.name) {
                        case "Brick(Clone)":
                            shootController.SendMessage("Shoot", currLook);
                            break;
                        case "BuildWallControl":
                            if (!currLook.GetComponent<BuildWallScript>().stacking) {
                                currLook.SendMessage("Rebuild");
                            }
                            break;
                        case "MenuUI":
                            if (!currLook.transform.parent.GetComponent<MenuShootScript>().On) {
                                currLook.transform.parent.SendMessage("Open");
                            }
                            break;
                        case "Close":
                            if (currLook.transform.parent.GetComponent<MenuShootScript>().On) {
                                currLook.transform.parent.SendMessage("Close");
                            }
                            break;
                        case "LeftArrow":
                            shootController.SendMessage("Left");
                            break;
                        case "RightArrow":
                            shootController.SendMessage("Right");
                            break;
                        case "Terrain":
                            player.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
                            break;
                        default: break;
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
