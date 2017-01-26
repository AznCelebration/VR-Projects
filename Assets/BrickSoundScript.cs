using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSoundScript : MonoBehaviour {
    public AudioClip BrickHard;
    public AudioClip BrickSoil;
    public AudioSource Audio;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnCollisionEnter(Collision collider) {
        if (collider.gameObject.name == "Cannonball") {
            Audio.PlayOneShot(BrickHard);
        }
    }
}
