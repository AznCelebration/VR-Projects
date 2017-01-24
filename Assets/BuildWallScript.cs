using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWallScript : MonoBehaviour {
    public Texture Reset;
    public Texture Stack;
    public GameObject brick;
    public GameObject status;
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

    void Rebuild()
    {
        foreach (GameObject clone in brickList) {
            Destroy(clone);
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
                Vector3 origin = new Vector3(-pos.x, 3, -pos.z);
                brickclone = Instantiate(brick, pos, Quaternion.LookRotation(origin));
                brickList.Add(brickclone);
            } 
        }
        yield return new WaitForSeconds(6f);
        foreach (GameObject clone in brickList) {
            clone.GetComponent<Rigidbody>().constraints = 0;
        }
        stacking = false;
    }
}
