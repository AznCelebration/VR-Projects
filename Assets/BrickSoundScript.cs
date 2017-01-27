using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSoundScript : MonoBehaviour {
    public AudioClip BrickHard;
    public AudioClip BrickSoil;
    public AudioSource Audio;

    public GameObject BuildWall = null;
    // Use this for initialization
    void Start () {
        BuildWall = GameObject.FindGameObjectWithTag("BuildWall");
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void OnCollisionEnter(Collision collider) {
        if (!BuildWall.GetComponent<BuildWallScript>().stacking) {
            Audio.priority = Random.Range(129, 255);
            Audio.pitch = Random.Range(0.3f, 0.8f);
            if (collider.relativeVelocity.magnitude > 10) {
                if (collider.gameObject.name == "Cannonball") {
                    Audio.PlayOneShot(BrickHard, 0.5f);
                }
                else if (collider.gameObject.name == "Terrain") {
                    Audio.PlayOneShot(BrickSoil, 0.5f);
                }
                else if (collider.gameObject.name == "Brick(Clone)") {
                    Audio.PlayOneShot(BrickHard, 0.5f);
                }
            }
        }
    }
}
