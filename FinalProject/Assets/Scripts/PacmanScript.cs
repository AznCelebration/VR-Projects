using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacmanScript : MonoBehaviour {
    public GameObject player;
    public GameObject testCam;
    public GameObject lHand;
    public GameObject rHand;
    public OVRInput.Controller LControl;
    public OVRInput.Controller RControl;
    public string state;
    public GameObject pellets;
    public Text pointUI;
    public Camera uiCam;
    public TextMesh menuTitle;
    public Material notHover;
    public Material hoverM;
    public GameObject pacman;
    public TextMesh field;

    private int points;
    private string mode;
    private string queue;
    private RaycastHit hit;
    private Ray ray;
    private GameObject hover;
    private string nameField;
    private bool pressed;
    //private Vector3 cam;
    // Use this for initialization
    void Start () {
        pressed = false;
        points = 0;
        state = "play";
        mode = "east";
        queue = "none";
        nameField = "";
       // cam = player.transform.position + new Vector3(0, 0.5f, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if(state == "play") {
            if (OVRInput.GetDown(OVRInput.Button.Two, LControl)) {
                if (queue == "left") {
                    queue = "none";
                }
                else {
                    queue = "left";
                }
            }
            if (OVRInput.GetDown(OVRInput.Button.Two, RControl)) {
                if (queue == "right") {
                    queue = "none";
                }
                else {
                    queue = "right";
                }
            }

            if (OVRInput.GetDown(OVRInput.Button.One, RControl) || OVRInput.GetDown(OVRInput.Button.One, RControl)) {
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
                    ray = new Ray(player.transform.position, new Vector3(-1, 0, 0));
                    if (Physics.Raycast(ray, out hit, 0.1f)) {
                        if (hit.collider.gameObject.name == "Pellet") {
                            Destroy(hit.collider.gameObject);
                            points += 10;
                        }
                    }
                    break;
                case "east":
                    ray = new Ray(player.transform.position, new Vector3(0, 0, 1));
                    if (Physics.Raycast(ray, out hit, 0.1f)) {
                        if (hit.collider.gameObject.name == "Pellet") {
                            Destroy(hit.collider.gameObject);
                            points += 10;
                        }
                    }
                    break;
                case "south":
                    ray = new Ray(player.transform.position, new Vector3(1, 0, 0));
                    if (Physics.Raycast(ray, out hit, 0.1f)) {
                        if (hit.collider.gameObject.name == "Pellet") {
                            Destroy(hit.collider.gameObject);
                            points += 10;
                        }
                    }
                    break;
                case "west":
                    ray = new Ray(player.transform.position, new Vector3(0, 0, -1));
                    if (Physics.Raycast(ray, out hit, 0.1f)) {
                        if (hit.collider.gameObject.name == "Pellet") {
                            Destroy(hit.collider.gameObject);
                            points += 10;
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
                                ray = new Ray(player.transform.position, new Vector3(-1, 0, 0));
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
                                ray = new Ray(player.transform.position, new Vector3(1, 0, 0));
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
                                ray = new Ray(player.transform.position, new Vector3(0, 0, -1));
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
                                ray = new Ray(player.transform.position, new Vector3(0, 0, 1));
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
                    }
                    break;
                case "right":
                    switch (mode) {
                        case "east":
                            if (System.Math.Abs((int)(player.transform.position.z * 10) % 10) == 5) {
                                ray = new Ray(player.transform.position, new Vector3(1, 0, 0));
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
                                ray = new Ray(player.transform.position, new Vector3(-1, 0, 0));
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
                                ray = new Ray(player.transform.position, new Vector3(0, 0, 1));
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
                                ray = new Ray(player.transform.position, new Vector3(0, 0, -1));
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
                    }
                    break;
            }
            float speed = 5f;
            float adj;
            switch (mode) {
                case "east":
                    ray = new Ray(player.transform.position, new Vector3(0, 0, 1));
                    if (Physics.Raycast(ray, out hit, 0.51f)) {
                        if (hit.collider.gameObject.name == "Wall") {
                            /* player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,
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
                    player.transform.Translate(new Vector3(0, 0, Time.deltaTime * speed), Space.World);
                    break;
                case "west":
                    ray = new Ray(player.transform.position, new Vector3(0, 0, -1));
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
                    ray = new Ray(player.transform.position, new Vector3(-1, 0, 0));
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
                    player.transform.Translate(new Vector3(-Time.deltaTime * speed, 0, 0), Space.World);
                    break;
                case "south":
                    ray = new Ray(player.transform.position, new Vector3(1, 0, 0));
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
        if(state == "win") {
            uiCam.enabled = false;

            menuTitle.text = "Game over\nScore: " + points.ToString() + "\nEnter your name";
            player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(-2, 33, 0),0.2f);
            player.transform.forward = Vector3.MoveTowards(player.transform.forward, new Vector3(0, -1, 0), 0.05f);

            RaycastHit[] hits;
            Transform fingerTip;
            fingerTip = lHand.transform.GetChild(0);
            fingerTip = fingerTip.transform.GetChild(0);
            fingerTip = fingerTip.transform.GetChild(0);
            fingerTip = fingerTip.transform.GetChild(2);
            fingerTip = fingerTip.transform.GetChild(0);
            fingerTip = fingerTip.transform.GetChild(0);
            fingerTip = fingerTip.transform.GetChild(0);
            hits = Physics.SphereCastAll(fingerTip.position, 0.01f, lHand.transform.forward, 0f);
            if (hits.Length > 0) {
                int closest = 0;
                for (int i = 0; i < hits.Length; i++) {
                    if (hits[i].distance < hits[closest].distance) {
                        closest = i;
                    }
                }
                
                if(hits[closest].transform.gameObject.tag == "Key") {
                    if (hover != null && hover.name != hits[closest].transform.gameObject.name) {
                        hover.GetComponent<Renderer>().material = notHover;
                    }
                    hover = hits[closest].transform.gameObject;
                    hover.GetComponent<Renderer>().material = hoverM;
                    if(!pressed) {
                        pressed = true;
                        KeyPressed();
                    }
                    
                }
                else {
                    if (hover != null) {
                        hover.GetComponent<Renderer>().material = notHover;
                    }
                }
                
            }
            else {
                if(hover != null) {
                    hover.GetComponent<Renderer>().material = notHover;
                }
                pressed = false;
            }
        }
        if(pellets.transform.childCount == 270) {
            state = "win";
            pacman.SetActive(false);
        }

        pointUI.text = "Points: " + points.ToString();
	}

    void KeyPressed() {
        if (hover.name == "Space") {
            if(nameField.Length < 15) {
                nameField += " ";
            }
        }
        else if (hover.name == "Back") {
            if (nameField != "") {
                nameField = nameField.Remove(nameField.Length - 1);
            }
        }
        else if (hover.name == "Enter"){

        }
        else {
            if (nameField.Length < 15) {
                nameField += hover.name;
            }
        }
        field.text = nameField;
    }
}


