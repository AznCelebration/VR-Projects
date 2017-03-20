using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {
    public GameObject player;
    public GameObject ghost;
    public string state;
    public GameObject pellets;
    public GameObject red;
    public bool alive;
    public Material scaredMat;
    public Material origMat;
    public AudioClip die;

    private string mode;
    private string queue;
    private RaycastHit hit;
    private Ray ray;
    private bool north;
    private bool east;
    private bool x;
    private bool z;
    private bool turn;
    private bool scatter;
    private static System.Random rnd;
    private float time;
    private string lastTry;
    private bool inactive;
    private Vector3 lastPos;
    private bool power;
    // Use this for initialization
    void Start () {
        power = false;
        alive = true;
        mode = "east";
        north = false;
        x = false;
        z = false;
        east = false;
        queue = "none";
        state = "init";
        scatter = true;
        rnd = new System.Random();
        time = 0;
        turn = false;
        lastTry = "right";
        inactive = true;
        lastPos = ghost.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        power = player.GetComponent<PacmanScript>().power;
        
        if(pellets.transform.childCount < 120 && ghost.name == "OrangeSpook" && inactive) {
            ghost.transform.position = new Vector3(-6.5f, 0, 0);
            mode = "east";
            inactive = false;
        }
        if (pellets.transform.childCount < 250 && ghost.name == "TealSpook" && inactive) {
            ghost.transform.position = new Vector3(-6.5f, 0, 0);
            mode = "east";
            inactive = false;
        }
        time += Time.deltaTime;
        if(!scatter && time > 20.0f) {
            scatter = true;
            time = 0;
        }
        else if(scatter && time > 8.0f) {
            scatter = false;
            time = 0;
        }
        //scatter = false;
        state = player.GetComponent<PacmanScript>().state;
        if(state == "play") {
            if (power) {
                ghost.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = scaredMat;
            }
            else {
                ghost.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = origMat;
            }

            if (!alive) {
                ghost.transform.position = new Vector3(-6.5f, 0, 0);
                alive = true;
            }
            if (ghost.name == "RedSpook") {
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
            }
            if (ghost.name == "PinkSpook") {
                float n = 0;
                float e = 0;
                if (player.GetComponent<PacmanScript>().mode == "north") n = -4;
                if (player.GetComponent<PacmanScript>().mode == "south") n = 4;
                if (player.GetComponent<PacmanScript>().mode == "east") e = 4;
                if (player.GetComponent<PacmanScript>().mode == "west") e = 4;
                if (ghost.transform.position.x == player.transform.position.x + n) {
                    x = true;
                }
                else if (ghost.transform.position.x > player.transform.position.x + n) {
                    north = true;
                    x = false;
                }
                else {
                    north = false;
                    x = false;
                }
                if (ghost.transform.position.z == player.transform.position.z + e) {
                    z = true;
                }
                else if (ghost.transform.position.z > player.transform.position.z + e) {
                    east = false;
                    z = false;
                }
                else {
                    east = true;
                    z = false;
                }
            }
            if(ghost.name == "OrangeSpook") {
                if(Vector3.Distance(ghost.transform.position,player.transform.position) > 8.0f) {
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
                }
                else {
                    int num = rnd.Next(1, 3);
                    if (num == 1) {
                        queue = "left";
                    }
                    else {
                        queue = "right";
                    }
                }
            }
            if (ghost.name == "TealSpook") {
                float n = 0;
                float e = 0;
                if (player.GetComponent<PacmanScript>().mode == "north") n = -2;
                if (player.GetComponent<PacmanScript>().mode == "south") n = 2;
                if (player.GetComponent<PacmanScript>().mode == "east") e = 2;
                if (player.GetComponent<PacmanScript>().mode == "west") e = 2;
                Vector3 temp = new Vector3(player.transform.position.x + n , player.transform.position.y, player.transform.position.z + e);
                temp = (temp - red.transform.position) * 2;
                if (ghost.transform.position.x == temp.x) {
                    x = true;
                }
                else if (ghost.transform.position.x > temp.x) {
                    north = true;
                    x = false;
                }
                else {
                    north = false;
                    x = false;
                }
                if (ghost.transform.position.z == temp.z) {
                    z = true;
                }
                else if (ghost.transform.position.z > temp.z) {
                    east = false;
                    z = false;
                }
                else {
                    east = true;
                    z = false;
                }
            }
            if (!scatter) {
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
            }

            else {
                int num = rnd.Next(1,3);
                if(num == 1) {
                    queue = "left";
                }
                else {
                    queue = "right";
                }
            }
            
            if(player.GetComponent<PacmanScript>().power && alive) {
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
                        if (east) { queue = "left"; }
                        else { queue = "right"; }
                        break;
                    case "south":
                        if (z) {
                            queue = "none";
                            break;
                        }
                        if (east) { queue = "right"; }
                        else { queue = "left"; }
                        break;
                    case "east":
                        if (x) {
                            queue = "none";
                            break;
                        }
                        if (north) { queue = "right"; }
                        else { queue = "left"; }
                        break;
                    case "west":
                        if (x) {
                            queue = "none";
                            break;
                        }
                        if (north) { queue = "left"; }
                        else { queue = "right"; }
                        break;
                }
            }
            ray = new Ray(ghost.transform.position, ghost.transform.position + ghost.transform.forward);
            if (Physics.Raycast(ray, out hit, 0.55f)) {
                if (hit.collider.gameObject.name == "Wall") {
                    if (queue == "none") { 
                        if(lastTry == "left") {
                            queue = "right";
                            lastTry = "right";
                        }
                        else {
                            queue = "left";
                            lastTry = "left";
                        }
                    }   
                }
            }

            ray = new Ray(ghost.transform.position, ghost.transform.position + ghost.transform.forward);
            if (Physics.Raycast(ray, out hit, 0.55f)) {
                if (hit.collider.gameObject.name == "Wall") {
                    if (queue == "none") {
                        if (lastTry == "left") {
                            queue = "right";
                            lastTry = "right";
                        }
                        else {
                            queue = "left";
                            lastTry = "left";
                        }
                    }
                }
            }

            if (lastPos == ghost.transform.position) {
                if (queue == "left") queue = "right";
                else queue = "left";
            }
            if (!player.GetComponent<PacmanScript>().power) {
                switch (mode) {
                    case "north":
                        ray = new Ray(ghost.transform.position, new Vector3(-1, 0, 0));
                        if (Physics.Raycast(ray, out hit, 0.1f)) {
                            if (hit.collider.gameObject.name == "Pacman") {
                                state = "dead";
                                ghost.GetComponent<AudioSource>().volume = player.GetComponent<PacmanScript>().volScale;
                                ghost.GetComponent<AudioSource>().Play();
                            }
                        }
                        break;
                    case "east":
                        ray = new Ray(ghost.transform.position, new Vector3(0, 0, 1));
                        if (Physics.Raycast(ray, out hit, 0.1f)) {
                            if (hit.collider.gameObject.name == "Pacman") {
                                state = "dead";
                                ghost.GetComponent<AudioSource>().volume = player.GetComponent<PacmanScript>().volScale;
                                ghost.GetComponent<AudioSource>().Play();
                            }
                        }
                        break;
                    case "south":
                        ray = new Ray(ghost.transform.position, new Vector3(1, 0, 0));
                        if (Physics.Raycast(ray, out hit, 0.1f)) {
                            if (hit.collider.gameObject.name == "Pacman") {
                                state = "dead";
                                ghost.GetComponent<AudioSource>().volume = player.GetComponent<PacmanScript>().volScale;
                                ghost.GetComponent<AudioSource>().Play();
                            }
                        }
                        break;
                    case "west":
                        ray = new Ray(ghost.transform.position, new Vector3(0, 0, -1));
                        if (Physics.Raycast(ray, out hit, 0.1f)) {
                            if (hit.collider.gameObject.name == "Pacman") {
                                state = "dead";
                                ghost.GetComponent<AudioSource>().volume = player.GetComponent<PacmanScript>().volScale;
                                ghost.GetComponent<AudioSource>().Play();
                            }
                        }
                        break;
                }
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
                                //ghost.transform.LookAt(new Vector3(ghost.transform.forward.x - 1, ghost.transform.forward.y, ghost.transform.forward.z));
                                turn = true;
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
                                turn = true;
                                //ghost.transform.LookAt(new Vector3(ghost.transform.forward.x + 1, ghost.transform.forward.y, ghost.transform.forward.z));
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
                                turn = true;
                                //ghost.transform.LookAt(new Vector3(ghost.transform.forward.x - 1, ghost.transform.forward.y, ghost.transform.forward.z));
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
                                turn = true;
                                //ghost.transform.LookAt(new Vector3(ghost.transform.forward.x, ghost.transform.forward.y, ghost.transform.forward.z + 1));
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
                                turn = true;
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
                                turn = true;
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
                                turn = true;
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
                                turn = true;
                            }
                            break;
                    }
                    break;
            }
            float speed = 5f * 0.75f; 
            float adj;

            lastPos = ghost.transform.position;
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

            if(turn) {
                switch (mode) {
                    case "north": ghost.transform.Translate(new Vector3(-0.3f, 0, 0), Space.World); break;
                    case "south": ghost.transform.Translate(new Vector3(0.3f, 0, 0), Space.World); break;
                    case "west": ghost.transform.Translate(new Vector3(0, 0, -0.3f), Space.World); break;
                    case "east": ghost.transform.Translate(new Vector3(0, 0, 0.3f), Space.World); break;
                }
                turn = false;
            }
            
        }
        
        player.GetComponent<PacmanScript>().state = state;
    }

    private void LateUpdate() {
        
    }
}
