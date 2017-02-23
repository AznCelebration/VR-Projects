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
	}

    private void LateUpdate() {
        if(frame.Hands.Count > 0) {
            //player.transform.LookAt(player.transform.position + direction.normalized, player.transform.up);
            player.transform.position += direction.normalized * 0.4f;
        }
    }
}
