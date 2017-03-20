using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {
    public GameObject player;
    public GameObject ghost;
    public string state;

    private string mode;
    private string queue;
    private RaycastHit hit;
    private Ray ray;
    private bool north;
    private bool east;
    private bool x;
    private bool z;
    // Use this for initialization
    void Start () {
        mode = "east";
        north = false;
        x = false;
        z = false;
        east = false;
        queue = "none";
        state = "play";
    }
	
	// Update is called once per frame
	void Update () {
        state = player.GetComponent<PacmanScript>().state;
        if(state == "play") {
            if (ghost.transform.position.x == player.transform.position.x) {
                x = true;
            }
            else if (ghost.transform.position.x > player.transform.position.x) {
                north = true;
                x = false;
            }
            else {
                north = false;
                x = false;
            }
            if (ghost.transform.position.z == player.transform.position.z) {
                z = true;
            }
            else if (ghost.transform.position.z > player.transform.position.z) {
                east = false;
                z = false;
            }
            else {
                east = true;
                z = false;
            }

            switch (mode) {
                case "north":
                    if (z) {
                        queue = "none";
                        break;
                    }
                    if (east) { queue = "right"; }
                    else { queue = "left"; }
                    break;
                case "south":
                    if (z) {
                        queue = "none";
                        break;
                    }
                    if (east) { queue = "left"; }
                    else { queue = "right"; }
                    break;
                case "east":
                    if (x) {
                        queue = "none";
                        break;
                    }
                    if (north) { queue = "left"; }
                    else { queue = "right"; }
                    break;
                case "west":
                    if (x) {
                        queue = "none";
                        break;
                    }
                    if (north) { queue = "right"; }
                    else { queue = "left"; }
                    break;
            }

            switch (mode) {
                case "north":
                    ray = new Ray(ghost.transform.position, new Vector3(-1, 0, 0));
                    if (Physics.Raycast(ray, out hit, 0.1f)) {
                        if (hit.collider.gameObject.name == "Pacman") {
                            state = "dead";
                        }
                    }
                    break;
                case "east":
                    ray = new Ray(ghost.transform.position, new Vector3(0, 0, 1));
                    if (Physics.Raycast(ray, out hit, 0.1f)) {
                        if (hit.collider.gameObject.name == "Pacman") {
                            state = "dead";
                        }
                    }
                    break;
                case "south":
                    ray = new Ray(ghost.transform.position, new Vector3(1, 0, 0));
                    if (Physics.Raycast(ray, out hit, 0.1f)) {
                        if (hit.collider.gameObject.name == "Pacman") {
                            state = "dead";
                        }
                    }
                    break;
                case "west":
                    ray = new Ray(ghost.transform.position, new Vector3(0, 0, -1));
                    if (Physics.Raycast(ray, out hit, 0.1f)) {
                        if (hit.collider.gameObject.name == "Pacman") {
                            state = "dead";
                        }
                    }
                    break;
            }

            switch (queue) {
                case "none": break;
                case "left":
                    switch (mode) {
                        case "east":
                            if (System.Math.Abs((int)(ghost.transform.position.z * 10) % 10) == 5) {
                                ray = new Ray(ghost.transform.position, new Vector3(-1, 0, 0));
                                if (Physics.Raycast(ray, out hit, 0.55f)) {
                                    if (hit.collider.gameObject.name == "Wall") {
                                        break;
                                    }
                                }
                                ghost.transform.position = new Vector3(ghost.transform.position.x, ghost.transform.position.y,
                                (float)(System.Math.Truncate((double)ghost.transform.position.z * 10.0) / 10.0));
                                mode = "north";
                                queue = "none";
                            }
                            break;
                        case "west":
                            if (System.Math.Abs((int)(ghost.transform.position.z * 10) % 10) == 5) {
                                ray = new Ray(ghost.transform.position, new Vector3(1, 0, 0));
                                if (Physics.Raycast(ray, out hit, 0.55f)) {
                                    if (hit.collider.gameObject.name == "Wall") {
                                        break;
                                    }
                                }
                                ghost.transform.position = new Vector3(ghost.transform.position.x, ghost.transform.position.y,
                                (float)(System.Math.Truncate((double)ghost.transform.position.z * 10.0) / 10.0));
                                mode = "south";
                                queue = "none";
                            }
                            break;
                        case "north":
                            if (System.Math.Abs((int)(ghost.transform.position.x * 10) % 10) == 5) {
                                ray = new Ray(ghost.transform.position, new Vector3(0, 0, -1));
                                if (Physics.Raycast(ray, out hit, 0.55f)) {
                                    if (hit.collider.gameObject.name == "Wall") {
                                        break;
                                    }
                                }
                                ghost.transform.position = new Vector3(
                                (float)(System.Math.Truncate((double)ghost.transform.position.x * 10.0) / 10.0),
                                ghost.transform.position.y, ghost.transform.position.z);
                                mode = "west";
                                queue = "none";
                            }
                            break;
                        case "south":
                            if (System.Math.Abs((int)(ghost.transform.position.x * 10) % 10) == 5) {
                                ray = new Ray(ghost.transform.position, new Vector3(0, 0, 1));
                                if (Physics.Raycast(ray, out hit, 0.55f)) {
                                    if (hit.collider.gameObject.name == "Wall") {
                                        break;
                                    }
                                }
                                ghost.transform.position = new Vector3(
                                (float)(System.Math.Truncate((double)ghost.transform.position.x * 10.0) / 10.0),
                                ghost.transform.position.y, ghost.transform.position.z);
                                mode = "east";
                                queue = "none";
                            }
                            break;
                    }
                    break;
                case "right":
                    switch (mode) {
                        case "east":
                            if (System.Math.Abs((int)(ghost.transform.position.z * 10) % 10) == 5) {
                                ray = new Ray(ghost.transform.position, new Vector3(1, 0, 0));
                                if (Physics.Raycast(ray, out hit, 0.55f)) {
                                    if (hit.collider.gameObject.name == "Wall") {
                                        break;
                                    }
                                }
                                ghost.transform.position = new Vector3(ghost.transform.position.x, ghost.transform.position.y,
                                (float)(System.Math.Truncate((double)ghost.transform.position.z * 10.0) / 10.0));
                                mode = "south";
                                queue = "none";
                            }
                            break;
                        case "west":
                            if (System.Math.Abs((int)(ghost.transform.position.z * 10) % 10) == 5) {
                                ray = new Ray(ghost.transform.position, new Vector3(-1, 0, 0));
                                if (Physics.Raycast(ray, out hit, 0.55f)) {
                                    if (hit.collider.gameObject.name == "Wall") {
                                        break;
                                    }
                                }
                                ghost.transform.position = new Vector3(ghost.transform.position.x, ghost.transform.position.y,
                                (float)(System.Math.Truncate((double)ghost.transform.position.z * 10.0) / 10.0));
                                mode = "north";
                                queue = "none";
                            }
                            break;
                        case "north":
                            if (System.Math.Abs((int)(ghost.transform.position.x * 10) % 10) == 5) {
                                ray = new Ray(ghost.transform.position, new Vector3(0, 0, 1));
                                if (Physics.Raycast(ray, out hit, 0.55f)) {
                                    if (hit.collider.gameObject.name == "Wall") {
                                        break;
                                    }
                                }
                                ghost.transform.position = new Vector3(
                                (float)(System.Math.Truncate((double)ghost.transform.position.x * 10.0) / 10.0),
                                ghost.transform.position.y, ghost.transform.position.z);
                                mode = "east";
                                queue = "none";
                            }
                            break;
                        case "south":
                            if (System.Math.Abs((int)(ghost.transform.position.x * 10) % 10) == 5) {
                                ray = new Ray(ghost.transform.position, new Vector3(0, 0, -1));
                                if (Physics.Raycast(ray, out hit, 0.55f)) {
                                    if (hit.collider.gameObject.name == "Wall") {
                                        break;
                                    }
                                }
                                ghost.transform.position = new Vector3(
                                (float)(System.Math.Truncate((double)ghost.transform.position.x * 10.0) / 10.0),
                                ghost.transform.position.y, ghost.transform.position.z);
                                mode = "west";
                                queue = "none";
                            }
                            break;
                    }
                    break;
            }
            float speed = 5f * 0.75f; 
            float adj;
            switch (mode) {
                case "east":
                    ray = new Ray(ghost.transform.position, new Vector3(0, 0, 1));
                    if (Physics.Raycast(ray, out hit, 0.51f)) {
                        if (hit.collider.gameObject.name == "Wall") {
                            /* ghost.transform.position = new Vector3(ghost.transform.position.x, ghost.transform.position.y,
                                 (float)(System.Math.Truncate((double)ghost.transform.position.z * 10.0) / 10.0));*/
                            if ((int)ghost.transform.position.z > 0) {
                                adj = 0.5f;
                            }
                            else {
                                adj = -0.5f;
                            }
                            ghost.transform.position = new Vector3(ghost.transform.position.x, ghost.transform.position.y,
                                (int)ghost.transform.position.z + adj);
                            break;
                        }
                    }
                    ghost.transform.Translate(new Vector3(0, 0, Time.deltaTime * speed), Space.World);
                    break;
                case "west":
                    ray = new Ray(ghost.transform.position, new Vector3(0, 0, -1));
                    if (Physics.Raycast(ray, out hit, 0.51f)) {
                        if (hit.collider.gameObject.name == "Wall") {
                            /*ghost.transform.position = new Vector3(ghost.transform.position.x, ghost.transform.position.y,
                                (float)(System.Math.Truncate((double)ghost.transform.position.z * 10.0) / 10.0));*/
                            if ((int)ghost.transform.position.z > 0) {
                                adj = 0.5f;
                            }
                            else {
                                adj = -0.5f;
                            }
                            ghost.transform.position = new Vector3(ghost.transform.position.x, ghost.transform.position.y,
                                    (int)ghost.transform.position.z + adj);
                            break;
                        }
                    }
                    ghost.transform.Translate(new Vector3(0, 0, -Time.deltaTime * speed), Space.World);
                    break;
                case "north":
                    ray = new Ray(ghost.transform.position, new Vector3(-1, 0, 0));
                    if (Physics.Raycast(ray, out hit, 0.51f)) {

                        if (hit.collider.gameObject.name == "Wall") {
                            /*ghost.transform.position = new Vector3(
                                (float)(System.Math.Truncate((double)ghost.transform.position.x * 10.0) / 10.0),
                                ghost.transform.position.y, ghost.transform.position.z); */
                            if ((int)ghost.transform.position.x > 0) {
                                adj = 0.5f;
                            }
                            else {
                                adj = -0.5f;
                            }
                            ghost.transform.position = new Vector3((int)ghost.transform.position.x + adj,
                                 ghost.transform.position.y, ghost.transform.position.z);
                            break;
                        }
                    }
                    ghost.transform.Translate(new Vector3(-Time.deltaTime * speed, 0, 0), Space.World);
                    break;
                case "south":
                    ray = new Ray(ghost.transform.position, new Vector3(1, 0, 0));
                    if (Physics.Raycast(ray, out hit, 0.51f)) {
                        if (hit.collider.gameObject.name == "Wall") {
                            /*ghost.transform.position = new Vector3(
                                (float)(System.Math.Truncate((double)ghost.transform.position.x * 10.0) / 10.0),
                                ghost.transform.position.y, ghost.transform.position.z);*/
                            if ((int)ghost.transform.position.x > 0) {
                                adj = 0.5f;
                            }
                            else {
                                adj = -0.5f;
                            }
                            ghost.transform.position = new Vector3((int)ghost.transform.position.x + adj,
                                    ghost.transform.position.y, ghost.transform.position.z);
                            break;
                        }
                    }
                    ghost.transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0), Space.World);
                    break;
            }

            switch (mode) {
                case "north": ghost.transform.forward = new Vector3(ghost.transform.forward.x - 1, ghost.transform.forward.y, ghost.transform.forward.z); break;
                case "west": ghost.transform.forward = new Vector3(ghost.transform.forward.x, ghost.transform.forward.y, ghost.transform.forward.z - 1); break;
                case "east": ghost.transform.forward = new Vector3(ghost.transform.forward.x, ghost.transform.forward.y, ghost.transform.forward.z + 1); break;
                case "south": ghost.transform.forward = new Vector3(ghost.transform.forward.x + 1, ghost.transform.forward.y, ghost.transform.forward.z); break;
            }
        }

        player.GetComponent<PacmanScript>().state = state;
    }

    private void LateUpdate() {
        
    }
}
