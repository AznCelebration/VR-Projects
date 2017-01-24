using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {
    public Transform cam;
    public LineRenderer laser;
    public ParticleSystem particlesys;
    private Vector3[] positions = { new Vector3(0, 0, 0), new Vector3(0, 0, 0) };

	// Use this for initialization
	void Start () {
        laser.GetComponent<Renderer>().enabled = false;
        particlesys.Pause();
        var main = particlesys.main;
        main.startDelay = 0;
    }
	
	// Update is called once per frame
	void Update () {
        laser.SetPositions(positions);
    }

    void drawLaser(GameObject currLook) {
        positions[0] = cam.position - currLook.transform.position.normalized * 2;
        positions[0].y += 0.1f;
        positions[1] = currLook.transform.position;
        particlesys.transform.position = cam.position - currLook.transform.position.normalized * 2;
        particlesys.transform.LookAt(currLook.transform.position);
        StartCoroutine(enableLaser(currLook));


    }

    IEnumerator enableLaser(GameObject currLook) {
        particlesys.Play();
        laser.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(1);
        laser.GetComponent<Renderer>().enabled = false;
        particlesys.Pause();
        particlesys.Clear();
    }
}
