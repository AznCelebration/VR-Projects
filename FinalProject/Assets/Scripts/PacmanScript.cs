using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanScript : MonoBehaviour {
    public GameObject player;
    public GameObject testCam;

    private string mode;
    private string queue;
    private RaycastHit hit;
    private Ray ray;
    private Vector3 cam;
    // Use this for initialization
    void Start () {
        mode = "east";
        queue = "none";
        cam = player.transform.position + new Vector3(0, 0.5f, 0);
	}
	
	// Update is called once per frame
	void Update () {
        cam = player.transform.position + new Vector3(0, 0.5f, 0);
        if (Input.GetKeyDown("a")) {
            if(queue == "left") {
                queue = "none";
            }
            else {
                queue = "left";
            }
        }
        if (Input.GetKeyDown("d")) {
            if (queue == "right") {
                queue = "none";
            }
            else {
                queue = "right";
            }
        }

        if (Input.GetKeyDown("s")) {
            switch (mode) {
                case "east":
                    mode = "west";
                    break;
                case "west":
                    mode = "east";
                    break;
                case "north":
                    mode = "south";
                    break;
                case "south":
                    mode = "north";
                    break;
            }
        }
        switch (mode) {
            case "north":
                ray = new Ray(cam, new Vector3(-1, 0, 0));
                if (Physics.Raycast(ray, out hit, 0.1f)) {
                    if (hit.collider.gameObject.name == "Pellet") {
                        Destroy(hit.collider.gameObject);
                    }
                }
                break;
            case "east":
                ray = new Ray(cam, new Vector3(0, 0, 1));
                if (Physics.Raycast(ray, out hit, 0.1f)) {
                    if (hit.collider.gameObject.name == "Pellet") {
                        Destroy(hit.collider.gameObject);
                    }
                }
                break;
            case "south":
                ray = new Ray(cam, new Vector3(1, 0, 0));
                if (Physics.Raycast(ray, out hit, 0.1f)) {
                    if (hit.collider.gameObject.name == "Pellet") {
                        Destroy(hit.collider.gameObject);
                    }
                }
                break;
            case "west":
                ray = new Ray(cam, new Vector3(0, 0, -1));
                if (Physics.Raycast(ray, out hit, 0.1f)) {
                    if (hit.collider.gameObject.name == "Pellet") {
                        Destroy(hit.collider.gameObject);
                    }
                }
                break;
        }

        switch (queue) {
            case "none": break;
            case "left":
                switch (mode) {
                    case "east":
                        if (System.Math.Abs((int)(player.transform.position.z * 10) % 10) == 5) {
                            ray = new Ray(cam, new Vector3(-1, 0, 0));
                            if (Physics.Raycast(ray, out hit, 0.55f)) {
                                if (hit.collider.gameObject.name == "Wall") {
                                    break;
                                }
                            }
                            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,
                            (float)(System.Math.Truncate((double)player.transform.position.z * 10.0) / 10.0));
                            mode = "north";
                            queue = "none";
                        }
                        break;
                    case "west":
                        if (System.Math.Abs((int)(player.transform.position.z * 10) % 10) == 5) {
                            ray = new Ray(cam, new Vector3(1, 0, 0));
                            if (Physics.Raycast(ray, out hit, 0.55f)) {
                                if (hit.collider.gameObject.name == "Wall") {
                                    break;
                                }
                            }
                            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,
                            (float)(System.Math.Truncate((double)player.transform.position.z * 10.0) / 10.0));
                            mode = "south";
                            queue = "none";
                        }
                        break;
                    case "north":
                        if (System.Math.Abs((int)(player.transform.position.x * 10) % 10) == 5) {
                            ray = new Ray(cam, new Vector3(0, 0, -1));
                            if (Physics.Raycast(ray, out hit, 0.55f)) {
                                if (hit.collider.gameObject.name == "Wall") {
                                    break;
                                }
                            }
                            player.transform.position = new Vector3(
                            (float)(System.Math.Truncate((double)player.transform.position.x * 10.0) / 10.0),
                            player.transform.position.y, player.transform.position.z);
                            mode = "west";
                            queue = "none";
                        }
                        break;
                    case "south":
                        if (System.Math.Abs((int)(player.transform.position.x * 10) % 10) == 5) {
                            ray = new Ray(cam, new Vector3(0, 0, 1));
                            if (Physics.Raycast(ray, out hit, 0.55f)) {
                                if (hit.collider.gameObject.name == "Wall") {
                                    break;
                                }
                            }
                            player.transform.position = new Vector3(
                            (float)(System.Math.Truncate((double)player.transform.position.x * 10.0) / 10.0),
                            player.transform.position.y, cam.z);
                            mode = "east";
                            queue = "none";
                        }
                        break;
                }
                break;
            case "right":
                switch (mode) {
                    case "east":
                        if (System.Math.Abs((int)(player.transform.position.z * 10) % 10) == 5) {
                            ray = new Ray(cam, new Vector3(1, 0, 0));
                            if (Physics.Raycast(ray, out hit, 0.55f)) {
                                if (hit.collider.gameObject.name == "Wall") {
                                    break;
                                }
                            }
                            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,
                            (float)(System.Math.Truncate((double)player.transform.position.z * 10.0) / 10.0));
                            mode = "south";
                            queue = "none";
                        }
                        break;
                    case "west":
                        if (System.Math.Abs((int)(player.transform.position.z * 10) % 10) == 5) {
                            ray = new Ray(cam, new Vector3(-1, 0, 0));
                            if (Physics.Raycast(ray, out hit, 0.55f)) {
                                if (hit.collider.gameObject.name == "Wall") {
                                    break;
                                }
                            }
                            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,
                            (float)(System.Math.Truncate((double)player.transform.position.z * 10.0) / 10.0));
                            mode = "north";
                            queue = "none";
                        }
                        break;
                    case "north":
                        if (System.Math.Abs((int)(player.transform.position.x * 10) % 10) == 5) {
                            ray = new Ray(cam, new Vector3(0, 0, 1));
                            if (Physics.Raycast(ray, out hit, 0.55f)) {
                                if (hit.collider.gameObject.name == "Wall") {
                                    break;
                                }
                            }
                            player.transform.position = new Vector3(
                            (float)(System.Math.Truncate((double)player.transform.position.x * 10.0) / 10.0),
                            player.transform.position.y, player.transform.position.z);
                            mode = "east";
                            queue = "none";
                        }
                        break;
                    case "south":
                        if (System.Math.Abs((int)(player.transform.position.x * 10) % 10) == 5) {
                            ray = new Ray(cam, new Vector3(0, 0, -1));
                            if (Physics.Raycast(ray, out hit, 0.55f)) {
                                if (hit.collider.gameObject.name == "Wall") {
                                    break;
                                }
                            }
                            player.transform.position = new Vector3(
                            (float)(System.Math.Truncate((double)player.transform.position.x * 10.0) / 10.0),
                            player.transform.position.y, cam.z);
                            mode = "west";
                            queue = "none";
                        }
                        break;
                }
                break;
        }
        float speed = 3f;
        float adj;
        switch (mode) {
            case "east":
                ray = new Ray(cam, new Vector3(0,0,1));
                if (Physics.Raycast(ray, out hit, 0.51f)) {
                    if (hit.collider.gameObject.name == "Wall") {
                        /* player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,
                             (float)(System.Math.Truncate((double)player.transform.position.z * 10.0) / 10.0));*/
                        if((int)player.transform.position.z > 0) {
                            adj = 0.5f;
                        }
                        else {
                            adj = -0.5f;
                        }
                        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,
                            (int)player.transform.position.z + adj);
                        break;
                    }
                }
                player.transform.Translate(new Vector3(0,0,Time.deltaTime * speed),Space.World);
                break;
            case "west":
                ray = new Ray(cam, new Vector3(0, 0, -1));
                if (Physics.Raycast(ray, out hit, 0.51f)) {
                    if (hit.collider.gameObject.name == "Wall") {
                        /*player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,
                            (float)(System.Math.Truncate((double)player.transform.position.z * 10.0) / 10.0));*/
                        if ((int)player.transform.position.z > 0) {
                            adj = 0.5f;
                        }
                        else {
                            adj = -0.5f;
                        }
                        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,
                                (int)player.transform.position.z + adj);
                        break;
                    }
                }
                player.transform.Translate(new Vector3(0, 0, -Time.deltaTime * speed), Space.World);
                break;
            case "north":
                ray = new Ray(cam, new Vector3(-1, 0, 0));
                if (Physics.Raycast(ray, out hit, 0.51f)) {
                    
                    if (hit.collider.gameObject.name == "Wall") {
                        /*player.transform.position = new Vector3(
                            (float)(System.Math.Truncate((double)player.transform.position.x * 10.0) / 10.0),
                            player.transform.position.y, player.transform.position.z); */
                        if ((int)player.transform.position.x > 0) {
                            adj = 0.5f;
                        }
                        else {
                            adj = -0.5f;
                        }
                        player.transform.position = new Vector3((int)player.transform.position.x + adj,
                             player.transform.position.y, player.transform.position.z);
                        break;
                    }
                }
                player.transform.Translate(new Vector3(-Time.deltaTime * speed, 0,0), Space.World);
                break;
            case "south":
                ray = new Ray(cam, new Vector3(1, 0, 0));
                if (Physics.Raycast(ray, out hit, 0.51f)) {
                    if (hit.collider.gameObject.name == "Wall") {
                        /*player.transform.position = new Vector3(
                            (float)(System.Math.Truncate((double)player.transform.position.x * 10.0) / 10.0),
                            player.transform.position.y, player.transform.position.z);*/
                        if ((int)player.transform.position.x > 0) {
                            adj = 0.5f;
                        }
                        else {
                            adj = -0.5f;
                        }
                        player.transform.position = new Vector3((int)player.transform.position.x + adj,
                                player.transform.position.y, player.transform.position.z);
                            break;
                    }
                }
                player.transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0), Space.World);
                break;
        }

	}
}
