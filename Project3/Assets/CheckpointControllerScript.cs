using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointControllerScript : MonoBehaviour {

    public GameObject checkpoint;

    private bool[] checkpoints;
	// Use this for initialization
	void Start () {
        int count = 0;
        try {
            string[] lines = System.IO.File.ReadAllLines(@"D:/Checkpoint.txt");
            foreach (string line in lines) {
                string[] tokens = line.Split();
                GameObject curr = Instantiate(checkpoint, new Vector3((float.Parse(tokens[0])/10), (float.Parse(tokens[1])/10), (float.Parse(tokens[2])/10)),Quaternion.identity);
                curr.name = count.ToString();
                count++;
            }
            checkpoints = new bool[count];
            for (int i = 0; i < checkpoints.Length; i++) checkpoints[i] = false;
        }
        catch { print("Load failed"); };
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void newPoint(string num) {
        checkpoints[int.Parse(num)] = true;
    }
}
