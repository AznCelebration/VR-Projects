using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class FlyerScript : MonoBehaviour {
    public Transform cam;
    //public GameObject left;
    //public GameObject right;
    public GameObject player;
    public Controller controller;
    public GameObject pointController;

    private Vector3 direction;
    private LeapProvider provider;
    private Frame frame;
    // Use this for initialization
    void Start () {
        direction = new Vector3(1, 0, 0);
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }
	
	// Update is called once per frame
	void Update () {
        frame = provider.CurrentFrame;
        if (frame.Hands.Count > 0) {
            List<Hand> hands = frame.Hands;
            Hand firstHand = hands[0];
            direction = firstHand.Direction.ToVector3();
        }
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(cam.transform.position, 0.01f, direction.normalized, 0f);
        if (hits.Length > 0) {
            int closest = 0;
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].distance < hits[closest].distance) {
                    closest = i;
                }
            }
            if (hits[closest].transform.gameObject.tag == "Checkpoint") {
                pointController.SendMessage("newPoint", hits[closest].transform.gameObject.name);
                Destroy(hits[closest].transform.gameObject);
            }
        }
    }

    private void LateUpdate() {
        if(frame.Hands.Count > 0) {
            //player.transform.LookAt(player.transform.position + direction.normalized, player.transform.up);
            player.transform.position += direction.normalized * 0.5f;
        }
    }
}
