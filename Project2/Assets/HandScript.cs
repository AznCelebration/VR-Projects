using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {
    public GameObject hand;
    public OVRInput.Controller Controller;
    public GameObject touch;

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
                holding = hits[closest].transform.gameObject;
                holding.transform.SetParent(hand.transform);
                holding.GetComponent<Rigidbody>().isKinematic = true;
            }

        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, Controller)) {
            if (holding != null) {
                holding.transform.SetParent(null);
                holding.GetComponent<Rigidbody>().isKinematic = false;
                holding.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(Controller);
                holding = null;
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, Controller)) {
            touch.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
    }
}
