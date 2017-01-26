using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSoundScript : MonoBehaviour {
    public AudioClip BrickHard;
    public AudioClip BrickSoil;
    public AudioSource Audio;
    private bool first = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Cannonball" || collision.gameObject.name == "Laser") {
            Audio.PlayOneShot(BrickHard);
        }
    }*/
}
