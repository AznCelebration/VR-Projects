using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {
    public GameObject hand;
    public OVRInput.Controller Controller;
    public GameObject touch;
    public GameObject player;
    public GameObject centereye;
    public GameObject ChairRigged;
    public GameObject LockerRigged;
    public GameObject CabinetRigged;
    public GameObject TvRigged;
    public GameObject DeskRigged;
    public GameObject whiteboard;
    public GameObject ControllerObj;

    private GameObject holding;
    // Use this for initialization
    void Start () {
        holding = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, Controller)) {
            RaycastHit[] hits;
            hits = Physics.SphereCastAll(hand.transform.position, 0.01f, hand.transform.forward, 0f);
            if(hits.Length > 0) {
                int closest = 0;
                for (int i = 0; i < hits.Length; i++) {
                    if(hits[i].distance < hits[closest].distance) {
                        closest = i;
                    }
                }
                if(hits[closest].transform.gameObject.tag == "Model" || hits[closest].transform.gameObject.tag == "Board") {
                    holding = hits[closest].transform.gameObject;
                    holding.transform.SetParent(hand.transform);
                    holding.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, Controller)) {
            if (holding != null && holding.transform.parent == hand.transform) {
                holding.transform.SetParent(null);
                if(holding.name != "WhiteBoard") {
                    holding.GetComponent<Rigidbody>().isKinematic = false;
                    holding.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(Controller);
                }
                else {
                    RaycastHit[] hits;
                    hits = Physics.SphereCastAll(hand.transform.position, 0.01f, hand.transform.forward, 0f);
                    if (hits.Length > 0) {
                        int closest = 0;
                        for (int i = 0; i < hits.Length; i++) {
                            if (hits[i].distance < hits[closest].distance && hits[i].collider.transform.root.name == "Walls") {
                                closest = i;
                            }
                        }
                        holding.transform.position = hits[closest].point;
                        holding.transform.up = hits[closest].normal.normalized;
                    }
                    else {
                        holding.GetComponent<Rigidbody>().isKinematic = false;
                        holding.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(Controller);
                    }
                }
                holding = null;
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, Controller)) {
            touch.SetActive(true);
            this.gameObject.SetActive(false);
            ControllerObj.GetComponent<ControllerScript>().mode = "tele";
        }
        if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, Controller) > 0) {
            player.transform.position += centereye.transform.forward.normalized * OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, Controller) * Time.deltaTime;
        }
        if (OVRInput.GetDown(OVRInput.Button.One, Controller)) {
            Save();
        }
        else if (OVRInput.GetDown(OVRInput.Button.Two, Controller)) {
            Load();
        }
    }

    void Save() {
        GameObject[] all = GameObject.FindGameObjectsWithTag("Model");
        string toWrite = "";
        foreach(GameObject model in all) {
            toWrite += model.name.Trim();
            toWrite += " ";
            toWrite += model.transform.position.x;
            toWrite += " ";
            toWrite += model.transform.position.y;
            toWrite += " ";
            toWrite += model.transform.position.z;
            toWrite += " ";
            toWrite += model.transform.eulerAngles.x;
            toWrite += " ";
            toWrite += model.transform.eulerAngles.y;
            toWrite += " ";
            toWrite += model.transform.eulerAngles.z;
            toWrite += '\n';
        }
        System.IO.File.WriteAllText("D:/SavedFurniture.txt", toWrite);
    }

    void Load() {
        GameObject[] all = GameObject.FindGameObjectsWithTag("Model");
        foreach(GameObject model in all) {
            Destroy(model);
        }
        try {
            string[] lines = System.IO.File.ReadAllLines(@"D:/SavedFurniture.txt");
            GameObject spawn;
            foreach (string line in lines) {
                print(line);
                string[] tokens = line.Split();
                foreach (string x in tokens)
                {
                    print(x);
                }
                switch (tokens[0]) {
                    case "DeskRigged":
                        spawn = Instantiate(DeskRigged, new Vector3(float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3])),
                            Quaternion.Euler(float.Parse(tokens[4]), float.Parse(tokens[5]),
                            float.Parse(tokens[6])));
                        spawn.name = "DeskRigged";
                        break;
                    case "ChairRigged":
                        spawn = Instantiate(ChairRigged, new Vector3(float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3])),
                            Quaternion.Euler(ChairRigged.transform.rotation.x + float.Parse(tokens[4]), ChairRigged.transform.rotation.y + float.Parse(tokens[5]),
                            ChairRigged.transform.rotation.z + float.Parse(tokens[6])));
                        spawn.name = "ChairRigged";
                        break;
                    case "CabinetRigged":
                        spawn = Instantiate(CabinetRigged, new Vector3(float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3])),
                            Quaternion.Euler(CabinetRigged.transform.rotation.x + float.Parse(tokens[4]), CabinetRigged.transform.rotation.y + float.Parse(tokens[5]),
                            CabinetRigged.transform.rotation.z + float.Parse(tokens[6])));
                        spawn.name = "CabinetRigged";
                        break;
                    case "LockerRigged":
                        spawn = Instantiate(LockerRigged, new Vector3(float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3])),
                            Quaternion.Euler(LockerRigged.transform.rotation.x + float.Parse(tokens[4]), LockerRigged.transform.rotation.y + float.Parse(tokens[5]),
                            LockerRigged.transform.rotation.z + float.Parse(tokens[6])));
                        spawn.name = "LockerRigged";
                        break;
                    case "TvRigged":
                        spawn = Instantiate(TvRigged, new Vector3(float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3])),
                            Quaternion.Euler(TvRigged.transform.rotation.x + float.Parse(tokens[4]), TvRigged.transform.rotation.y + float.Parse(tokens[5]),
                            TvRigged.transform.rotation.z + float.Parse(tokens[6])));
                        spawn.name = "TvRigged";
                        break;
                    case "WhiteBoard":
                        spawn = Instantiate(whiteboard, new Vector3(float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3])),
                            Quaternion.Euler(DeskRigged.transform.rotation.x + float.Parse(tokens[4]), DeskRigged.transform.rotation.y + float.Parse(tokens[5]),
                            DeskRigged.transform.rotation.z + float.Parse(tokens[6])));
                        spawn.name = "WhiteBoard";
                        break;
                }

            }
       }
       catch { print("Load failed");  } ;
    }
}
