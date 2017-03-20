using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public GameObject uiCam;
    public TextMesh menuTitle;
    public Material notHover;
    public Material hoverM;
    public GameObject pacman;
    public TextMesh field;
    public GameObject keyboard;
    public Material backMat;
    public GameObject easy;
    public AudioClip intro;
    public GameObject home;
    public GameObject ghosts;

    private int points;
    private string mode;
    private string queue;
    private RaycastHit hit;
    private Ray ray;
    private GameObject hover;
    private GameObject hover2;
    private string nameField;
    private bool pressed;
    private bool pressed2;
    private bool firstOver;
    private GameObject holding;
    private GameObject holding2;
    private GameObject currDiff;
    private bool diffPress;
    private float volScale;
    float time;
    //private Vector3 cam;
    // Use this for initialization
    void Start() {
        time = 0;
        firstOver = false;
        pressed = false;
        pressed2 = false;
        diffPress = false;
        points = 0;
        state = "init";
        mode = "east";
        queue = "none";
        nameField = "";
        currDiff = easy;
        volScale = 0;
        // cam = player.transform.position + new Vector3(0, 0.5f, 0);
    }

    // Update is called once per frame
    void Update() {
        if(state == "init") {
            menuTitle.text = "\n\nVR Pacman";
            Transform fingerTip;

            if (lHand.transform.childCount > 0 && rHand.transform.childCount > 0) {
                RaycastHit[] hits;
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

                    if (hits[closest].transform.gameObject.tag == "Key") {
                        GameObject hit = hits[closest].transform.gameObject;
                        if(currDiff != null) {
                            if(currDiff.name != hit.name) {
                                currDiff.GetComponent<Renderer>().material = backMat;
                            }
                        }
                        currDiff = hit;
                        hit.GetComponent<Renderer>().material = hoverM;
                    }
                    else if (hits[closest].transform.gameObject.name == "Slider" && OVRInput.Get(OVRInput.Button.One, LControl)) {
                        holding = hits[closest].transform.gameObject;
                    }
                    else if (hits[closest].transform.gameObject.name == "Play" && OVRInput.Get(OVRInput.Button.One, LControl) && holding == null) {
                        state = "start";
                        this.GetComponent<AudioSource>().PlayOneShot(intro);
                        holding = null;
                    }
                }

                

                RaycastHit[] hitsR;
                Transform rfingerTip;
                rfingerTip = rHand.transform.GetChild(0);
                rfingerTip = rfingerTip.transform.GetChild(0);
                rfingerTip = rfingerTip.transform.GetChild(0);
                rfingerTip = rfingerTip.transform.GetChild(2);
                rfingerTip = rfingerTip.transform.GetChild(0);
                rfingerTip = rfingerTip.transform.GetChild(0);
                rfingerTip = rfingerTip.transform.GetChild(0);
                hitsR = Physics.SphereCastAll(rfingerTip.position, 0.01f, rHand.transform.forward, 0f);
                if (hitsR.Length > 0) {
                    int closest = 0;
                    for (int i = 0; i < hitsR.Length; i++) {
                        if (hitsR[i].distance < hitsR[closest].distance) {
                            closest = i;
                        }
                    }

                    if (hitsR[closest].transform.gameObject.tag == "Key") {
                        GameObject hit = hitsR[closest].transform.gameObject;

                        if (currDiff != null) {
                            if (currDiff.name != hit.name) {
                                currDiff.GetComponent<Renderer>().material = backMat;
                            }
                        }
                        currDiff = hit;
                        hit.GetComponent<Renderer>().material = hoverM;
                    }
                    else if (hitsR[closest].transform.gameObject.name == "Slider" && OVRInput.Get(OVRInput.Button.One, RControl)) {
                        holding2 = hitsR[closest].transform.gameObject;
                    }

                    else if (hitsR[closest].transform.gameObject.name == "Play" && OVRInput.Get(OVRInput.Button.One, RControl) && holding2 == null) {
                        state = "start";
                        this.GetComponent<AudioSource>().PlayOneShot(intro);
                        holding2 = null;
                    }
                }

                if (holding != null && OVRInput.Get(OVRInput.Button.One, LControl)) {
                    volScale = System.Math.Abs(-2.2f - holding.transform.position.x) / 0.35f;
                    this.GetComponent<AudioSource>().volume = System.Math.Abs(-2.2f - holding.transform.position.x) / 0.35f;
                    if(!this.GetComponent<AudioSource>().isPlaying) {
                        this.GetComponent<AudioSource>().Play();
                    }
                    
                    
                    if (fingerTip.transform.position.x < -2.2f) {
                        holding.transform.position = new Vector3(-2.2f, holding.transform.position.y, holding.transform.position.z);
                    }
                    else if (fingerTip.transform.position.x > -1.85f) {
                        holding.transform.position = new Vector3(-1.85f, holding.transform.position.y, holding.transform.position.z);
                    }
                    else {
                        holding.transform.position = new Vector3(fingerTip.transform.position.x, holding.transform.position.y, holding.transform.position.z);
                    }
                }

                if (holding2 != null && OVRInput.Get(OVRInput.Button.One, RControl)) {
                    volScale = System.Math.Abs(-2.2f - holding2.transform.position.x) / 0.35f;
                    this.GetComponent<AudioSource>().volume = System.Math.Abs(-2.2f - holding2.transform.position.x) / 0.35f;
                    if (!this.GetComponent<AudioSource>().isPlaying) {
                        this.GetComponent<AudioSource>().Play();
                    }
                    if (rfingerTip.transform.position.x < -2.2f) {
                        holding2.transform.position = new Vector3(-2.2f, holding2.transform.position.y, holding2.transform.position.z);
                    }
                    else if (rfingerTip.transform.position.x > -1.85f) {
                        holding2.transform.position = new Vector3(-1.85f, holding2.transform.position.y, holding2.transform.position.z);
                    }
                    else {
                        holding2.transform.position = new Vector3(rfingerTip.transform.position.x, holding2.transform.position.y, holding2.transform.position.z);
                    }
                }
                if (OVRInput.GetUp(OVRInput.Button.One, LControl)) {
                    if (this.GetComponent<AudioSource>().isPlaying) {
                        this.GetComponent<AudioSource>().Stop();
                    }
                    holding = null;
                }
                if (OVRInput.GetUp(OVRInput.Button.One, RControl)) {
                    if (this.GetComponent<AudioSource>().isPlaying) {
                        this.GetComponent<AudioSource>().Stop();
                    }
                    holding2 = null;
                }
            }
            
        }
        
        if(state == "start") {
            player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(6.5f, 0.5f, -0.5f), 0.1f);
            player.transform.forward = Vector3.MoveTowards(player.transform.forward, new Vector3(0, 0, 1.0f), 0.006f);
            time += Time.deltaTime;
            if(time >= intro.length) {
                state = "play";
                this.GetComponent<AudioSource>().Play();
                home.SetActive(false);
                pacman.SetActive(true);
                uiCam.SetActive(true);
            }
            if(currDiff.name == "Medium") {
                foreach(Transform child in ghosts.transform) {
                    foreach (Transform child2 in child) {
                        child2.gameObject.layer = LayerMask.NameToLayer("Water");
                    }
                }
            }
            if (currDiff.name == "Hard") {
                uiCam.SetActive(false);
            }
        }

        if (state == "play") {
            
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
        if (state == "win" || state == "dead") {
            if(this.GetComponent<AudioSource>().isPlaying) {
                this.GetComponent<AudioSource>().Stop();
            }
            pacman.layer = 0;
            uiCam.SetActive(false);
            if(!firstOver) {
                firstOver = true;
                menuTitle.text = "Game over\nScore: " + points.ToString() + "\nEnter your name";
                pacman.transform.parent = null;
                keyboard.SetActive(true);
            }

            player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(-2, 33, 0), 0.2f);
            player.transform.forward = Vector3.MoveTowards(player.transform.forward, new Vector3(0, -1, 0), 0.05f);

            if (lHand.transform.childCount > 0 && rHand.transform.childCount > 0) {
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

                    if (hits[closest].transform.gameObject.tag == "Key") {
                        if (hover != null && hover.name != hits[closest].transform.gameObject.name) {
                            hover.GetComponent<Renderer>().material = notHover;
                        }
                        hover = hits[closest].transform.gameObject;
                        hover.GetComponent<Renderer>().material = hoverM;
                        if (!pressed) {
                            pressed = true;
                            KeyPressed(true);

                        }

                    }
                    else {
                        if (hover != null) {
                            hover.GetComponent<Renderer>().material = notHover;
                        }
                    }

                }
                else {
                    if (hover != null) {
                        hover.GetComponent<Renderer>().material = notHover;
                    }
                    pressed = false;
                }

                RaycastHit[] hitsR;
                fingerTip = rHand.transform.GetChild(0);
                fingerTip = fingerTip.transform.GetChild(0);
                fingerTip = fingerTip.transform.GetChild(0);
                fingerTip = fingerTip.transform.GetChild(2);
                fingerTip = fingerTip.transform.GetChild(0);
                fingerTip = fingerTip.transform.GetChild(0);
                fingerTip = fingerTip.transform.GetChild(0);
                hitsR = Physics.SphereCastAll(fingerTip.position, 0.01f, rHand.transform.forward, 0f);
                if (hitsR.Length > 0) {
                    int closest = 0;
                    for (int i = 0; i < hitsR.Length; i++) {
                        if (hitsR[i].distance < hitsR[closest].distance) {
                            closest = i;
                        }
                    }

                    if (hitsR[closest].transform.gameObject.tag == "Key") {
                        if (hover2 != null && hover2.name != hitsR[closest].transform.gameObject.name) {
                            hover2.GetComponent<Renderer>().material = notHover;
                        }
                        hover2 = hitsR[closest].transform.gameObject;
                        hover2.GetComponent<Renderer>().material = hoverM;
                        if (!pressed2) {
                            pressed2 = true;
                            KeyPressed(false);
                        }

                    }
                    else {
                        if (hover2 != null) {
                            hover2.GetComponent<Renderer>().material = notHover;
                        }
                    }

                }
                else {
                    if (hover2 != null) {
                        hover2.GetComponent<Renderer>().material = notHover;
                    }
                    pressed2 = false;
                }
            }
        }
        if (pellets.transform.childCount == 0 && state != "win") {
            state = "win";
        }

        pointUI.text = "Points: " + points.ToString();
    }

    void KeyPressed(bool diff) {
        if(diff) {
            if (hover.name == "Space") {
                if (nameField.Length < 15) {
                    nameField += " ";
                }
            }
            else if (hover.name == "Back") {
                if (nameField != "") {
                    nameField = nameField.Remove(nameField.Length - 1);
                }
            }
            else if (hover.name == "Enter") {
                if (nameField != "") {
                    Save();
                    menuTitle.text = "Leaderboard\n";
                    menuTitle.text += Load();
                    keyboard.SetActive(false);
                }
            }
            else {
                if (nameField.Length < 15) {
                    nameField += hover.name;
                }
            }
        }
        else {
            if (hover2.name == "Space") {
                if (nameField.Length < 15) {
                    nameField += " ";
                }
            }
            else if (hover2.name == "Back") {
                if (nameField != "") {
                    nameField = nameField.Remove(nameField.Length - 1);
                }
            }
            else if (hover2.name == "Enter") {
                if (nameField != "") {
                    Save();
                    menuTitle.text = "Leaderboard\n";
                    menuTitle.text += Load();
                    keyboard.SetActive(false);
                }
            }
            else {
                if (nameField.Length < 15) {
                    nameField += hover2.name;
                }
            }
        }
        field.text = nameField;
    }

    void Save() {
        string toWrite = "";
        Dictionary<string, int> scores = LoadNum();
        if(scores != null) {
            scores[nameField] = points;
            foreach (KeyValuePair<string, int> item in scores.OrderByDescending(key => key.Value)) {
                toWrite += item.Key;
                toWrite += "  ";
                toWrite += item.Value.ToString();
                toWrite += "\n";
            }
        }
        else {
            toWrite += nameField;
            toWrite += "  ";
            toWrite += points.ToString();
            toWrite += "\n";
        }
        System.IO.File.WriteAllText("D:/HighScore.txt", toWrite);
    }

    string Load() {
        try {
            string text = System.IO.File.ReadAllText(@"D:/HighScore.txt");
            return text;
        }
        catch { return null; };
    }

    Dictionary<string,int> LoadNum() {
        try {
            Dictionary<string, int> toReturn = new Dictionary<string, int>();
            string[] lines = System.IO.File.ReadAllLines(@"D:/HighScore.txt");
            
            foreach (string line in lines) {
                string[] tokens = line.Split();
                toReturn[tokens[0]] = int.Parse(tokens[tokens.Length-1]);
            }
            return toReturn;
        }
        catch { return null; };
    }
}


