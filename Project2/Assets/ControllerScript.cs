using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour {

    public GameObject Controller;
    public OVRInput.Controller Control;
    public GameObject player;
    public GameObject ChairRigged;
    public GameObject LockerRigged;
    public GameObject CabinetRigged;
    public GameObject TvRigged;
    public GameObject DeskRigged;
    public GameObject whiteboard;

    private string mode;
    private RaycastHit hit;
    private GameObject currObj;
    private string spawn;
    private bool throwing;
    private bool respawn;
    private Vector3 toSpawn;
    private bool sucking;
    private Vector3 toScale;

    // Use this for initialization
    void Start() {
        mode = "tele";
        throwing = false;
        respawn = false;
        sucking = false;
    }

    // Update is called once per frame
    void Update() {

        Ray ray = new Ray(Controller.transform.position, Controller.transform.forward);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            if (hit.collider.gameObject.name == "Floor") {
                if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, Control)) {
                    if (mode == "tele") {
                        player.transform.position = new Vector3(hit.point.x, 8, hit.point.z);
                    }
                    else if (mode == "spawn" || mode == "move") {
                        throwing = true;
                        toSpawn = hit.point;
                        currObj.GetComponent<Rigidbody>().isKinematic = true;
                        currObj.GetComponent<Rigidbody>().detectCollisions = false;
                    }
                }
            }
            
            else if (hit.collider.gameObject.tag == "Moveable") {
                if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, Control) && mode != "move") {
                    mode = "move";
                    sucking = true;
                    if (currObj != null) {
                        Destroy(currObj);
                    }
                    currObj = hit.collider.gameObject.transform.parent.parent.gameObject;
                    currObj.GetComponent<Rigidbody>().isKinematic = true;
                    currObj.GetComponent<Rigidbody>().detectCollisions = false;
                    toScale = currObj.transform.localScale * 0.01f;
                }
            }
        }
        

        if (sucking) {
            Vector3 toPos = Controller.transform.position + Controller.transform.up.normalized * 0.1f;
            switch (currObj.name) {
                case "DeskRigged":
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, toScale, 1f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, toPos, 0.3f);
                    break;
                case "ChairRigged":
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, toScale, 1f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, toPos, 0.3f);
                    break;
                case "CabinetRigged":
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, toScale, 1f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, toPos, 0.3f);
                    break;
                case "LockerRigged":
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, toScale, 1f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, toPos, 0.3f);
                    break;
                case "TvRigged":
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, toScale, 1f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, toPos, 0.3f);
                    break;
                case "whiteboard":
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, toScale, 0.3f);
                    break;
            }

            if (currObj.transform.position == toPos && currObj.transform.localScale == toScale) {
                sucking = false;
            }
        }
        
        if (throwing) {
            Vector3 toPos = new Vector3(0,0,0);
            Vector3 toScale = new Vector3(0, 0, 0);
            string toCheck = "";
            if (mode == "move") {
                toCheck = currObj.name;
            }
            else if (mode == "spawn") {
                toCheck = spawn;
            }
            switch (toCheck) {
                case "DeskRigged":
                    toPos = new Vector3(toSpawn.x, 6f, toSpawn.z);
                    toScale = DeskRigged.transform.localScale;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, DeskRigged.transform.localScale, 1f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, new Vector3(toSpawn.x, 6f, toSpawn.z), 0.3f);
                    break;
                case "ChairRigged":
                    toPos = new Vector3(toSpawn.x, 4f, toSpawn.z);
                    toScale = ChairRigged.transform.localScale;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, ChairRigged.transform.localScale, 0.05f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, new Vector3(toSpawn.x, 4f, toSpawn.z), 0.3f);
                    break;
                case "CabinetRigged":
                    toPos = new Vector3(toSpawn.x, 4f, toSpawn.z);
                    toScale = CabinetRigged.transform.localScale;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, CabinetRigged.transform.localScale, 0.05f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, new Vector3(toSpawn.x, 4f, toSpawn.z), 0.3f);
                    break;
                case "LockerRigged":
                    toPos = new Vector3(toSpawn.x, 6f, toSpawn.z);
                    toScale = LockerRigged.transform.localScale;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, LockerRigged.transform.localScale, 1f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, new Vector3(toSpawn.x, 6f, toSpawn.z), 0.3f);
                    break;
                case "TvRigged":
                    toPos = new Vector3(toSpawn.x, 6f, toSpawn.z);
                    toScale = TvRigged.transform.localScale;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, TvRigged.transform.localScale, 1f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, new Vector3(toSpawn.x, 6f, toSpawn.z), 0.3f);
                    break;
                case "whiteboard":
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, whiteboard.transform.localScale, 0.1f);
                    break;
            }

            if (currObj.transform.position == toPos && currObj.transform.localScale == toScale ) {
                throwing = false;
                currObj.GetComponent<Rigidbody>().isKinematic = false;
                currObj.GetComponent<Rigidbody>().detectCollisions = true;
                currObj = null;
                if(mode != "move") {
                    respawn = true;
                }
                else {
                    mode = "tele";
                }
            }
        }

        if (mode == "move") {
            float y = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, Control).y * 1f;
            float x = -OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, Control).x * 1f;
            currObj.transform.Rotate(new Vector3(0, x, 0), Space.World);
            currObj.transform.Rotate(Vector3.Cross(new Vector3(0, 1, 0), new Vector3(Controller.transform.forward.x, 0, Controller.transform.forward.z).normalized), y, Space.World);
        }

        if (mode == "spawn") {
            if ((OVRInput.GetUp(OVRInput.Button.One,Control) || respawn) && !throwing) {
                if (currObj != null) {
                    Destroy(currObj);
                }
                respawn = false;
                switch (spawn) {
                    case "DeskRigged":
                        spawn = "ChairRigged";
                        currObj = Instantiate(ChairRigged, Controller.transform.position + Controller.transform.up.normalized * 0.1f, ChairRigged.transform.rotation);
                        currObj.transform.localScale *= 0.01f;
                        break;
                    case "ChairRigged":
                        spawn = "CabinetRigged";
                        currObj = Instantiate(CabinetRigged, Controller.transform.position + Controller.transform.up.normalized * 0.1f, CabinetRigged.transform.rotation);
                        currObj.transform.localScale *= 0.01f;
                        break;
                    case "CabinetRigged":
                        spawn = "LockerRigged";
                        currObj = Instantiate(LockerRigged, Controller.transform.position + Controller.transform.up.normalized * 0.1f, LockerRigged.transform.rotation);
                        currObj.transform.localScale *= 0.01f;
                        break;
                    case "LockerRigged":
                        spawn = "TvRigged";
                        currObj = Instantiate(TvRigged, Controller.transform.position + Controller.transform.up.normalized * 0.1f, TvRigged.transform.rotation);
                        currObj.transform.localScale *= 0.01f;
                        break;
                    case "TvRigged":
                        spawn = "whiteboard";
                        currObj = Instantiate(whiteboard, Controller.transform.position + Controller.transform.up.normalized * 0.1f, whiteboard.transform.rotation);
                        currObj.transform.localScale *= 0.1f;
                        break;
                    case "whiteboard":
                        spawn = "DeskRigged";
                        currObj = Instantiate(DeskRigged, Controller.transform.position + Controller.transform.up.normalized * 0.1f, DeskRigged.transform.rotation);
                        currObj.transform.localScale *= 0.01f;
                        break;
                }
            }

            float y = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick,Control).y * 1f;
            float x = - OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick,Control).x * 1f;
            currObj.transform.Rotate(new Vector3(0,x,0),Space.World);
            currObj.transform.Rotate(Vector3.Cross(new Vector3(0,1,0), new Vector3(Controller.transform.forward.x,0,Controller.transform.forward.z).normalized), y, Space.World);
        }

        if (OVRInput.GetUp(OVRInput.Button.One,Control) && mode != "spawn") {
            mode = "spawn";
            currObj = Instantiate(DeskRigged, Controller.transform.position + Controller.transform.up.normalized * 0.1f, DeskRigged.transform.rotation);
            spawn = "DeskRigged";
            currObj.transform.localScale *= 0.01f;
            currObj.SetActive(true);
        }
    }

    private void LateUpdate() {
        Controller.GetComponent<LineRenderer>().SetPosition(0, Controller.transform.position - Controller.transform.up.normalized * 0.02f);
        Controller.GetComponent<LineRenderer>().SetPosition(1, hit.point);

        if (currObj != null && !throwing && !sucking) {
            currObj.transform.position = Controller.transform.position + Controller.transform.up.normalized * 0.1f;
        }
    }
}
