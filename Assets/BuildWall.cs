using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWall : MonoBehaviour {

    public GameObject brick;
    
    // Use this for initialization
    void Start () {
        StartCoroutine(BrickStack());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator BrickStack() {
        
        int numberOfObjects = 54;
        float radius = 20f;
        bool mode = true;

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
                Vector3 origin = new Vector3(-pos.x, 3, -pos.z);
                Instantiate(brick, pos, Quaternion.LookRotation(origin));
            } 
        }
        yield return new WaitForSeconds(5f);
        var bricks = GameObject.FindGameObjectsWithTag("Brick");
        foreach (GameObject clone in bricks) {
            clone.GetComponent<Rigidbody>().constraints = 0;
        }
    }
}
