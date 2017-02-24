using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CheckpointControllerScript : MonoBehaviour {
    public GameObject player;
    public GameObject checkpoint;
    public GameObject compass;
    public Text dist;
    public GameObject cam;

    private bool[] checkpoints;
    private GameObject currCheck;
    private int currPoint;
    private bool finish = false;
	// Use this for initialization
	void Start () {
        int count = 0;
        try {
            string[] lines = System.IO.File.ReadAllLines(@"D:/Checkpoint.txt");
            foreach (string line in lines) {
                string[] tokens = line.Split();
                GameObject curr = Instantiate(checkpoint, new Vector3((float.Parse(tokens[0])/12), (float.Parse(tokens[1])/12), (float.Parse(tokens[2])/12)),Quaternion.identity);
                curr.name = count.ToString();
                if(count == 0) {
                    player.transform.position = curr.transform.position;
                    //Destroy(curr);
                }
                if(count == 1) {
                    player.transform.LookAt(curr.transform.position);
                    currCheck = curr;
                }
                count++;
            }
            checkpoints = new bool[count];
            for (int i = 0; i < checkpoints.Length; i++) checkpoints[i] = false;
            //checkpoints[0] = true;
            currPoint = 1;
        }
        catch { print("Load failed"); };
    }
	
	// Update is called once per frame
	void Update () {
        bool flag = true;
        foreach (bool point in checkpoints) {
            if (!point) {
                flag = false;
                break;
            }
        }
        if (flag && !finish) {
            player.SendMessage("End");
            finish = true;
            this.gameObject.GetComponent<ParticleSystem>().Stop();
            this.gameObject.GetComponent<ParticleSystem>().Clear();
        }
        if(!finish) {
            this.gameObject.transform.LookAt(currCheck.transform.position);
            var main = this.gameObject.GetComponent<ParticleSystem>().main;
            main.startSpeed = Math.Abs(8 - Vector3.Distance(currCheck.transform.position, this.transform.position) * 0.001f);
            Vector3 camForward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
            Vector3 check = currCheck.transform.position - player.transform.position;
            check.y = 0;
            Vector3 referenceRight = Vector3.Cross(Vector3.up, camForward);
            float angle = Vector3.Angle(check, camForward);
            float sign = Mathf.Sign(Vector3.Dot(check, referenceRight));
            float finalAngle = sign * angle;
            dist.text = Vector3.Distance(player.transform.position, currCheck.transform.position).ToString();
            //if(Vector3.Angle(camForward, check) < 180) {
                compass.transform.localEulerAngles = new Vector3(0, 0, -finalAngle);
            /*}
            else {
                compass.transform.localEulerAngles = new Vector3(0, 0, Vector3.Angle(camForward, check));
            }*/
            //print(compass.transform.eulerAngles.x);
        }
    }

    void newPoint(GameObject Point) {
        checkpoints[int.Parse(Point.name)] = true;
        foreach(GameObject point in GameObject.FindGameObjectsWithTag("Checkpoint")) {
            if(int.Parse(point.name) == currPoint) {
                currCheck = point;
                currPoint++;
                break;
            }
        }
    }
}
