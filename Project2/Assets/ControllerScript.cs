using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour {

    public GameObject Controller;
    public OVRInput.Controller Control;
    public GameObject player;
    public GameObject chair;
    public GameObject locker;
    public GameObject storage;
    public GameObject tv;
    public GameObject desk;
    public GameObject whiteboard;

    private string mode;
    private RaycastHit hit;
    private GameObject currObj;
    private string spawn;
    private bool throwing;
    private bool respawn;
    private Vector3 toSpawn;

    // Use this for initialization
    void Start() {
        mode = "tele";
        throwing = false;
        respawn = false;
    }

    // Update is called once per frame
    void Update() {

        Ray ray = new Ray(Controller.transform.position, Controller.transform.forward);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            if (hit.collider.gameObject.name == "Floor") {
                if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger,Control)) {
                    print(Controller.name);
                    if (mode == "tele") {
                        player.transform.position = new Vector3(hit.point.x, 8, hit.point.z);
                    }
                    else if (mode == "spawn") {
                        throwing = true;
                        toSpawn = hit.point;
                        currObj.GetComponent<Rigidbody>().isKinematic = true;
                        currObj.GetComponent<Rigidbody>().detectCollisions = false;
                    }
                }
            }
        }

        if (throwing) {
            Vector3 toPos = new Vector3(0,0,0);
            Vector3 toScale = new Vector3(0, 0, 0);
            switch (spawn) {
                case "desk":
                    toPos = new Vector3(toSpawn.x, 6f, toSpawn.z);
                    toScale = desk.transform.localScale;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, desk.transform.localScale, 1f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, new Vector3(toSpawn.x, 6f, toSpawn.z), 0.3f);
                    break;
                case "chair":
                    toPos = new Vector3(toSpawn.x, 4f, toSpawn.z);
                    toScale = chair.transform.localScale;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, chair.transform.localScale, 0.05f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, new Vector3(toSpawn.x, 4f, toSpawn.z), 0.3f);
                    break;
                case "storage":
                    toPos = new Vector3(toSpawn.x, 4f, toSpawn.z);
                    toScale = storage.transform.localScale;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, storage.transform.localScale, 0.05f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, new Vector3(toSpawn.x, 4f, toSpawn.z), 0.3f);
                    break;
                case "locker":
                    toPos = new Vector3(toSpawn.x, 6f, toSpawn.z);
                    toScale = locker.transform.localScale;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, locker.transform.localScale, 1f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, new Vector3(toSpawn.x, 6f, toSpawn.z), 0.3f);
                    break;
                case "tv":
                    toPos = new Vector3(toSpawn.x, 6f, toSpawn.z);
                    toScale = tv.transform.localScale;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, tv.transform.localScale, 1f);
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
                respawn = true;
            }
        }

        if (mode == "spawn") {
            if ((OVRInput.GetUp(OVRInput.Button.One,Control) || respawn) && !throwing) {
                if (currObj != null) {
                    Destroy(currObj);
                }
                respawn = false;
                switch (spawn) {
                    case "desk":
                        spawn = "chair";
                        currObj = Instantiate(chair, Controller.transform.position + Controller.transform.up.normalized * 0.1f, chair.transform.rotation);
                        currObj.transform.localScale *= 0.01f;
                        break;
                    case "chair":
                        spawn = "storage";
                        currObj = Instantiate(storage, Controller.transform.position + Controller.transform.up.normalized * 0.1f, storage.transform.rotation);
                        currObj.transform.localScale *= 0.01f;
                        break;
                    case "storage":
                        spawn = "locker";
                        currObj = Instantiate(locker, Controller.transform.position + Controller.transform.up.normalized * 0.1f, locker.transform.rotation);
                        currObj.transform.localScale *= 0.01f;
                        break;
                    case "locker":
                        spawn = "tv";
                        currObj = Instantiate(tv, Controller.transform.position + Controller.transform.up.normalized * 0.1f, tv.transform.rotation);
                        currObj.transform.localScale *= 0.01f;
                        break;
                    case "tv":
                        spawn = "whiteboard";
                        currObj = Instantiate(whiteboard, Controller.transform.position + Controller.transform.up.normalized * 0.1f, whiteboard.transform.rotation);
                        currObj.transform.localScale *= 0.1f;
                        break;
                    case "whiteboard":
                        spawn = "desk";
                        currObj = Instantiate(desk, Controller.transform.position + Controller.transform.up.normalized * 0.1f, desk.transform.rotation);
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
            currObj = Instantiate(desk, Controller.transform.position + Controller.transform.up.normalized * 0.1f, desk.transform.rotation);
            spawn = "desk";
            currObj.transform.localScale *= 0.01f;
            currObj.SetActive(true);
        }
    }

    private void LateUpdate() {
        Controller.GetComponent<LineRenderer>().SetPosition(0, Controller.transform.position - Controller.transform.up.normalized * 0.02f);
        Controller.GetComponent<LineRenderer>().SetPosition(1, hit.point);

        if (currObj != null && !throwing) {
            currObj.transform.position = Controller.transform.position + Controller.transform.up.normalized * 0.1f;
        }
    }
}
