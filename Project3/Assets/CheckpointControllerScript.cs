using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckpointControllerScript : MonoBehaviour {
    public GameObject player;
    public GameObject checkpoint;

    private bool[] checkpoints;
    private GameObject currCheck;
	// Use this for initialization
	void Start () {
        int count = 0;
        try {
            string[] lines = System.IO.File.ReadAllLines(@"D:/Checkpoint.txt");
            foreach (string line in lines) {
                string[] tokens = line.Split();
                GameObject curr = Instantiate(checkpoint, new Vector3((float.Parse(tokens[0])/10), (float.Parse(tokens[1])/10), (float.Parse(tokens[2])/10)),Quaternion.identity);
                curr.name = count.ToString();
                if(count == 0) {
                    player.transform.position = curr.transform.position;
                }
                count++;
            }
            checkpoints = new bool[count];
            for (int i = 0; i < checkpoints.Length; i++) checkpoints[i] = false;
            currCheck = GameObject.FindGameObjectWithTag("Checkpoint");
        }
        catch { print("Load failed"); };
    }
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.LookAt(currCheck.transform.position);
        var main = this.gameObject.GetComponent<ParticleSystem>().main;
        main.startSpeed = Math.Abs(8 - Vector3.Distance(currCheck.transform.position, this.transform.position) * 0.001f);

    }

    void newPoint(string num) {
        checkpoints[int.Parse(num)] = true;
        GameObject[] checks = GameObject.FindGameObjectsWithTag("Checkpoint");
        float minDist = float.MaxValue;
        foreach(GameObject point in checks) {
            if (!checkpoints[int.Parse(point.name)]) {
                if(Vector3.Distance(point.transform.position,this.transform.position) < minDist) {
                    minDist = Vector3.Distance(point.transform.position, this.transform.position);
                    currCheck = point;
                }
            }
        }
    }
}
