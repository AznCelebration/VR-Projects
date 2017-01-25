using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {
    public Transform Menu;
    public Transform Cam;
    public GameObject LeftArrow;
    public GameObject RightArrow;
    public GameObject CloseButton;
    public GameObject MenuUI;
    public bool On = false;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {

    }
	
	void LateUpdate () {
        Vector3 forward = new Vector3(Cam.forward.x, 0, Cam.forward.z);
        if (!On) {
            if (Menu.position.y == 0.5f) {
                Menu.position = new Vector3(Cam.position.x, 0.5f, Cam.position.z) + forward.normalized * 3.5f;
                if (MenuUI.tag != "Interact") {
                    MenuUI.tag = "Interact";
                }
            }
            else {
                Menu.position = Vector3.MoveTowards(Menu.position, new Vector3(Menu.position.x, 0.5f, Menu.position.z), 0.1f);
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
}
