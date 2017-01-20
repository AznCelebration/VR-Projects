using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookControl : MonoBehaviour {
    public Transform cam;
    public GameObject cannonballPrefab;
    private GameObject prev = null;
    private int gazeDuration = 0;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        GameObject cannonball;
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
            if (gazeDuration == 100) {
                if (currLook.tag == "Brick")
                {
                    //Destroy(currLook);
                    cannonball = Instantiate(cannonballPrefab, cam.position, Quaternion.identity);
                    cannonball.GetComponent<Rigidbody>().velocity = cam.forward.normalized * 50;
                    gazeDuration = 0;
                }

                else if(currLook.name == "BuildWallControl") {
                    currLook.SendMessage("Rebuild");
                }
            }
            

        }
	}
}
