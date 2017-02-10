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
    public GameObject hands;

    private string mode;
    private RaycastHit hit;
    private GameObject currObj;
    private string spawn;
    private bool throwing;
    private bool respawn;
    private Vector3 toSpawn;
    private bool sucking;
    private Vector3 toScale;
    private Vector3 toGroupScale;
    private Dictionary<int, GameObject> hits;
    private List<GameObject> childsOfGameobject = new List<GameObject>();
    // Use this for initialization
    void Start() {
        mode = "tele";
        throwing = false;
        respawn = false;
        sucking = false;
        hits = new Dictionary<int, GameObject>();
        toScale = new Vector3(0, 0, 0);
        toGroupScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        Ray ray = new Ray(Controller.transform.position, Controller.transform.forward);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            if (hit.collider.gameObject.name == "Floor") {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, Control)) {
                    if (mode == "tele") {
                        player.transform.position = new Vector3(hit.point.x, 1.2f, hit.point.z);
                    }
                    else if (mode == "spawn" || mode == "move") {
                        throwing = true;
                        toSpawn = hit.point;
                        if (currObj.name == "Grouped") {
                            foreach (Transform child in currObj.transform) {
                                child.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                                child.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
                            }
                        }
                        else {
                            currObj.GetComponent<Rigidbody>().isKinematic = true;
                            currObj.GetComponent<Rigidbody>().detectCollisions = false;
                        }
                        
                    }
                }
            }
            
            else if (hit.collider.gameObject.tag == "Moveable") {
                if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, Control) && mode != "move" && mode != "spawn") {
                    hits[hit.collider.gameObject.transform.parent.parent.gameObject.GetHashCode()] = hit.collider.gameObject.transform.parent.parent.gameObject;
                    GetAllChilds(hit.collider.gameObject.transform.parent.parent.gameObject);
                    foreach (GameObject child in childsOfGameobject) {
                        if(child.GetComponent<Renderer>() != null) {
                            foreach(Material mat in child.GetComponent<Renderer>().materials) {
                                mat.shader = Shader.Find("HighlightShader");
                            }
                        }
                    }
                    childsOfGameobject.Clear();
                }
                
            }
            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, Control) && mode != "move" && mode != "spawn") {
                mode = "move";
                sucking = true;
                if (currObj != null) {
                    Destroy(currObj);
                }
                GameObject parent = new GameObject();
                Vector3 center = new Vector3(0, 0, 0);
                foreach (KeyValuePair<int, GameObject> entry in hits) {
                    entry.Value.GetComponent<Rigidbody>().isKinematic = true;
                    entry.Value.GetComponent<Rigidbody>().detectCollisions = false;
                    center += entry.Value.transform.position;
                }

                center /= hits.Count;
                parent.transform.position = center;
                toGroupScale = parent.transform.localScale * 0.1f;

                foreach (KeyValuePair<int, GameObject> entry in hits) {
                    entry.Value.transform.SetParent(parent.transform);
                    GetAllChilds(entry.Value);
                    foreach (GameObject child in childsOfGameobject) {
                        if (child.GetComponent<Renderer>() != null) {
                            foreach (Material mat in child.GetComponent<Renderer>().materials) {
                                mat.shader = Shader.Find("Diffuse");
                            }
                        }
                    }
                    childsOfGameobject.Clear();
                }
                currObj = parent;
                parent.name = "Grouped";
                hits.Clear();
            }
        }
        
        if(OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, Control)) {
            hands.SetActive(true);
            this.gameObject.SetActive(false);
        }

        if (sucking) {
            Vector3 toPos = Controller.transform.position + Controller.transform.up.normalized * 0.1f;
            switch (currObj.name) {
                case "DeskRigged":
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, toPos, 0.3f);
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, toScale, 0.5f);
                    break;
                case "ChairRigged":
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, toScale, 0.5f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, toPos, 0.3f);
                    break;
                case "CabinetRigged":
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, toScale, 0.5f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, toPos, 0.3f);
                    break;
                case "LockerRigged":
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, toScale, 0.5f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, toPos, 0.3f);
                    break;
                case "TvRigged":
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, toScale, 0.5f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, toPos, 0.3f);
                    break;
                case "whiteboard":
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, toScale, 0.3f);
                    break;
                case "Grouped":
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, toGroupScale, 0.5f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, toPos, 0.3f);
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
                    toPos = new Vector3(toSpawn.x, 1f, toSpawn.z);
                    toScale = DeskRigged.transform.localScale;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, DeskRigged.transform.localScale, 1f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, new Vector3(toSpawn.x, 1f, toSpawn.z), 0.3f);
                    break;
                case "ChairRigged":
                    toPos = new Vector3(toSpawn.x, 1f, toSpawn.z);
                    toScale = ChairRigged.transform.localScale;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, ChairRigged.transform.localScale, 0.05f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, new Vector3(toSpawn.x, 1f, toSpawn.z), 0.3f);
                    break;
                case "CabinetRigged":
                    toPos = new Vector3(toSpawn.x, 1f, toSpawn.z);
                    toScale = CabinetRigged.transform.localScale;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, CabinetRigged.transform.localScale, 0.05f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, new Vector3(toSpawn.x, 1f, toSpawn.z), 0.3f);
                    break;
                case "LockerRigged":
                    toPos = new Vector3(toSpawn.x, 1f, toSpawn.z);
                    toScale = LockerRigged.transform.localScale;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, LockerRigged.transform.localScale, 1f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, new Vector3(toSpawn.x, 1f, toSpawn.z), 0.3f);
                    break;
                case "TvRigged":
                    toPos = new Vector3(toSpawn.x, 1f, toSpawn.z);
                    toScale = TvRigged.transform.localScale;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, TvRigged.transform.localScale, 1f);
                    currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, new Vector3(toSpawn.x, 1f, toSpawn.z), 0.3f);
                    break;
                case "whiteboard":
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, whiteboard.transform.localScale, 0.1f);
                    break;
                case "Grouped":
                    toPos = new Vector3(toSpawn.x, 1.2f, toSpawn.z);
                    toScale = toGroupScale * 10;
                    //currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, toPos, 0.1f);
                    currObj.transform.position = toPos;
                    currObj.transform.localScale = Vector3.MoveTowards(currObj.transform.localScale, toScale, 1f);
                    break;
            }
            if (currObj.transform.position == toPos && currObj.transform.localScale == toScale ) {
                throwing = false;
                if (mode != "move") {
                    switch (spawn) {
                        case "DeskRigged":
                            spawn = "whiteboard";
                            break;
                        case "ChairRigged":
                            spawn = "DeskRigged";
                            break;
                        case "CabinetRigged":
                            spawn = "ChairRigged";
                            break;
                        case "LockerRigged":
                            spawn = "CabinetRigged";
                            break;
                        case "TvRigged":
                            spawn = "LockerRigged";
                            break;
                        case "whiteboard":
                            spawn = "DeskRigged";
                            break;
                    }
                    respawn = true;
                    currObj.GetComponent<Rigidbody>().isKinematic = false;
                    currObj.GetComponent<Rigidbody>().detectCollisions = true;
                    currObj = null;
                }
                else if (mode == "move") {
                    mode = "tele";
                    sucking = false;
                    int childCount = currObj.transform.childCount;
                    for (int i = 0; i < childCount; i++) {
                        currObj.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
                        currObj.transform.GetChild(0).GetComponent<Rigidbody>().detectCollisions = true;
                        currObj.transform.GetChild(0).SetParent(null);
                    }
                    Destroy(currObj);
                    currObj = null;
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
            if ((OVRInput.GetDown(OVRInput.Button.One,Control) || respawn) && !throwing) {
                if (currObj != null) {
                    Destroy(currObj);
                }
                respawn = false;
                switch (spawn) {
                    case "DeskRigged":
                        spawn = "ChairRigged";
                        currObj = Instantiate(ChairRigged, Controller.transform.position + Controller.transform.up.normalized * 0.1f, ChairRigged.transform.rotation);
                        currObj.name = "ChairRigged";
                        currObj.transform.localScale *= 0.1f;
                        break;
                    case "ChairRigged":
                        spawn = "CabinetRigged";
                        currObj = Instantiate(CabinetRigged, Controller.transform.position + Controller.transform.up.normalized * 0.1f, CabinetRigged.transform.rotation);
                        currObj.name = "CabinetRigged";
                        currObj.transform.localScale *= 0.1f;
                        break;
                    case "CabinetRigged":
                        spawn = "LockerRigged";
                        currObj = Instantiate(LockerRigged, Controller.transform.position + Controller.transform.up.normalized * 0.1f, LockerRigged.transform.rotation);
                        currObj.name = "LockerRigged";
                        currObj.transform.localScale *= 0.1f;
                        break;
                    case "LockerRigged":
                        spawn = "TvRigged";
                        currObj = Instantiate(TvRigged, Controller.transform.position + Controller.transform.up.normalized * 0.1f, TvRigged.transform.rotation);
                        currObj.name = "TvRigged";
                        currObj.transform.localScale *= 0.1f;
                        break;
                    case "TvRigged":
                        spawn = "whiteboard";
                        currObj = Instantiate(whiteboard, Controller.transform.position + Controller.transform.up.normalized * 0.1f, whiteboard.transform.rotation);
                        currObj.name = "WhiteBoard";
                        currObj.transform.localScale *= 0.1f;
                        break;
                    case "whiteboard":
                        spawn = "DeskRigged";
                        currObj = Instantiate(DeskRigged, Controller.transform.position + Controller.transform.up.normalized * 0.1f, DeskRigged.transform.rotation);
                        currObj.name = "DeskRigged";
                        currObj.transform.localScale *= 0.1f;
                        break;
                }
                currObj.GetComponent<Rigidbody>().detectCollisions = false;
            }

            float y = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick,Control).y * 1f;
            float x = - OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick,Control).x * 1f;
            currObj.transform.Rotate(new Vector3(0,x,0),Space.World);
            currObj.transform.Rotate(Vector3.Cross(new Vector3(0,1,0), new Vector3(Controller.transform.forward.x,0,Controller.transform.forward.z).normalized), y, Space.World);

            if(OVRInput.GetDown(OVRInput.Button.Two, Control)) {
                mode = "tele";
                Destroy(currObj);
                currObj = null;
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.One,Control) && mode != "spawn" && mode != "move") {
            mode = "spawn";
            currObj = Instantiate(DeskRigged, Controller.transform.position + Controller.transform.up.normalized * 0.1f, DeskRigged.transform.rotation);
            spawn = "DeskRigged";
            currObj.name = "DeskRigged";
            currObj.transform.localScale *= 0.1f;
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

    private List<GameObject> GetAllChilds(GameObject transformForSearch)
    {
        List<GameObject> getedChilds = new List<GameObject>();

        foreach (Transform trans in transformForSearch.transform)
        {
            //Debug.Log (trans.name);
            GetAllChilds(trans.gameObject);
            childsOfGameobject.Add(trans.gameObject);
        }
        return getedChilds;
    }
}
