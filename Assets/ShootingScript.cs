using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour {
    
    public GameObject cannonballPrefab;
    public Transform cam;
    public GameObject laser;
    private string mode = "laser";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Shoot (GameObject currLook) {
        GameObject cannonball;
        if (mode == "cannon") {
            cannonball = Instantiate(cannonballPrefab, cam.position, Quaternion.identity);
            cannonball.GetComponent<Rigidbody>().velocity = cam.forward.normalized * 50;
        }
        else if (mode == "laser") {
            Destroy(currLook);
            laser.GetComponent<LaserScript>().SendMessage("drawLaser", currLook);
        }
    }
}
