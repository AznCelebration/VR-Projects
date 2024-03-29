﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWallScript : MonoBehaviour {
    public Texture Reset;
    public Texture Stack;

    public GameObject brick;
    public GameObject status;
    public GameObject player;
    
    public AudioClip BrickHard;
    public AudioSource Audio;

    public bool stacking = false;
    private List<GameObject> brickList = new List<GameObject>();

    // Use this for initialization
    void Start () {
        StartCoroutine(BrickStack());
    }
	
	// Update is called once per frame
	void Update () {
		if (stacking) {
            status.GetComponent<Renderer>().material.mainTexture = Stack;
        }
        else {
            status.GetComponent<Renderer>().material.mainTexture = Reset;
        }
	}

    void LateUpdate() {
        status.transform.position = new Vector3(player.transform.position.x, 35f, player.transform.position.z);
    }

    void Rebuild()
    {
        foreach (GameObject clone in brickList) {
            Destroy(clone);
        }
        GameObject[] cannonballs = GameObject.FindGameObjectsWithTag("Cannonball");
        foreach (GameObject ball in cannonballs) {
            Destroy(ball);
        }
        GameObject[] brickFrags = GameObject.FindGameObjectsWithTag("BrickFrags");
        foreach (GameObject frag in brickFrags) {
            Destroy(frag);
        }
        brickList.Clear();
        StartCoroutine(BrickStack());

    }

    IEnumerator BrickStack() {
        stacking = true;
        int numberOfObjects = 54;
        float radius = 20f;
        bool mode = true;
        GameObject brickclone;
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.5f);
            mode = mode ? false : true;
            for (int j = 0; j < numberOfObjects; j++)
            {
                float angle = j * Mathf.PI * 2 / numberOfObjects;
                if (mode)
                {
                    angle += 0.15f;
                }
                Vector3 pos = new Vector3(Mathf.Cos(angle), 3, Mathf.Sin(angle)) * radius;
                Vector3 origin = new Vector3(-pos.x, pos.y, -pos.z);
                brickclone = Instantiate(brick, pos, Quaternion.LookRotation(origin));
                brickclone.transform.rotation = Quaternion.Euler(0, brickclone.transform.eulerAngles.y, 0);
                brickList.Add(brickclone);
            }
        }
        yield return new WaitForSeconds(4f);
        foreach (GameObject clone in brickList) {
            if (clone != null) {
                clone.GetComponent<Rigidbody>().constraints = 0;
            }
        }
        stacking = false;
    }
}
