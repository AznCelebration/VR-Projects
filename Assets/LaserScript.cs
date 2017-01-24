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
        particlesys.Clear();
        var main = particlesys.main;
        main.startDelay = 0.05f;
    }
	
	// Update is called once per frame
	void Update () {

    }

    void drawLaser(GameObject currLook) {
        StartCoroutine(enableLaser(currLook));


    }

    IEnumerator enableLaser(GameObject currLook) {
        positions[0] = cam.position - currLook.transform.position.normalized * 2;
        positions[0].y += 0.1f;
        positions[1] = currLook.transform.position;
        particlesys.transform.position = cam.position - currLook.transform.position.normalized;
        particlesys.transform.LookAt(currLook.transform.position);
        laser.SetPositions(positions);
        laser.GetComponent<Renderer>().enabled = true;
        particlesys.Play();
        Vector3 hitpos = currLook.transform.position.normalized * 3;
        yield return new WaitForSeconds(1);
        positions[0] = cam.position + hitpos;
        laser.SetPositions(positions);
        particlesys.Stop();
        laser.GetComponent<Renderer>().enabled = false;
    }
}
