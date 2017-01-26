using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShootScript : MonoBehaviour {
    public Transform Menu;
    public Transform Cam;

    public GameObject LeftArrow;
    public GameObject RightArrow;
    public GameObject CloseButton;
    public GameObject MenuUI;
    public GameObject MenuScreen;
    public GameObject CannonballPrefab;
    public GameObject Laser;
    public GameObject BrickFrag;

    public Texture Main;
    public Texture Lazer;
    public Texture Cannon;
    public Texture None;
    public Texture Blank;

    public AudioClip CannonBlast;
    public AudioClip Outside;

    public bool On = false;
    private string mode = "none";

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (!On) {
            MenuScreen.GetComponent<Renderer>().material.mainTexture = Main;
        }
        else {
            switch (mode) {
                case "none":
                    MenuScreen.GetComponent<Renderer>().material.mainTexture = None;
                    break;
                case "laser":
                    MenuScreen.GetComponent<Renderer>().material.mainTexture = Lazer;
                    break;
                case "cannon":
                    MenuScreen.GetComponent<Renderer>().material.mainTexture = Cannon;
                    break;
                default: break;
            }
        }
    }
	
	void LateUpdate () {
        Vector3 forward = new Vector3(Cam.forward.x, 0, Cam.forward.z);
        if (!On) {
            if (Menu.position.y == 0.5f) {
                Menu.position = new Vector3(Cam.position.x, 0.5f, Cam.position.z) + forward.normalized * 5f;
                if (MenuUI.tag != "Interact") {
                    MenuUI.tag = "Interact";
                }
            }
            else {
                Menu.position = Vector3.MoveTowards(Menu.position, new Vector3(Menu.position.x, 0.5f, Menu.position.z), 0.1f);
                if(Menu.position.y != 0.5f) {
                    MenuScreen.GetComponent<Renderer>().material.mainTexture = Blank;
                }
                
            }
        }
        else {
            if (Menu.position.y == 5f && !CloseButton.activeSelf) {
                LeftArrow.SetActive(true);
                RightArrow.SetActive(true);
                CloseButton.SetActive(true);
            }
            else {
                Menu.position = Vector3.MoveTowards(Menu.position, new Vector3(Menu.position.x, 5f, Menu.position.z), 0.1f);
                if(Menu.position.y != 5f) {
                    MenuScreen.GetComponent<Renderer>().material.mainTexture = Blank;
                }
            }
        }
        Menu.LookAt(Cam.position);
    }

    void Open() {
        On = true;
        MenuUI.tag = "Untagged";
    }

    void Close() {
        On = false;
        LeftArrow.SetActive(false);
        RightArrow.SetActive(false);
        CloseButton.SetActive(false);
    }

    void Shoot(GameObject currLook) {
        GameObject cannonball;
        AudioSource audio = Menu.GetComponent<AudioSource>();
        if (mode == "cannon") {
            audio.PlayOneShot(CannonBlast);
            cannonball = Instantiate(CannonballPrefab, Cam.position, Quaternion.identity);
            cannonball.GetComponent<Rigidbody>().velocity = Cam.forward.normalized * 50;
        }
        else if (mode == "laser") {
            Laser.GetComponent<LaserScript>().SendMessage("drawLaser", currLook);
            GameObject brickFrag = Instantiate(BrickFrag, currLook.transform.position, currLook.transform.rotation);
            foreach (Transform child in brickFrag.transform) {
                child.GetComponent<Rigidbody>().velocity = Cam.forward.normalized * 50;
            }
            Destroy(currLook);
        }
    }

    void Left() {
        switch (mode) {
            case "none":
                mode = "laser";
                break;
            case "laser":
                mode = "cannon";
                break;
            case "cannon":
                mode = "none";
                break;
            default: break;
        }
    }

    void Right() {
        switch (mode) {
            case "none":
                mode = "cannon";
                break;
            case "laser":
                mode = "none";
                break;
            case "cannon":
                mode = "laser";
                break;
            default: break;
        }
    }
}
