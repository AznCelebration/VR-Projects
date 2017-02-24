using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using UnityEngine.UI;

public class FlyerScript : MonoBehaviour {
    public Transform cam;
    //public GameObject left;
    //public GameObject right;
    public GameObject player;
    public Controller controller;
    public GameObject pointController;
    public Text count;
    public Text timer;
    public AudioClip startBeep;
    public AudioClip startFinal;
    public AudioClip startMusic;
    public AudioClip bg;
    public AudioClip coin;
    public AudioClip Finish;
    public AudioClip car;
    public AudioSource carSource;
    public AudioSource source;

    private Vector3 direction;
    private LeapProvider provider;
    private Frame frame;
    private bool start = true;
    private float time;
    private Vector3 lastCheck;
    private bool crashed = false;
    private bool finish = false;
    private int currPoint;
   
    private AudioSource bgSource;
    
    private int mode = 0;

    // Use this for initialization
    void Start() {
        bgSource = pointController.GetComponent<AudioSource>();
        direction = new Vector3(1, 0, 0);
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
        StartCoroutine(Countdown());
        currPoint = 0;
    }

    // Update is called once per frame
    void Update() {
        if(mode == 0) {
            if(!carSource.isPlaying) {
                carSource.UnPause();
                carSource.pitch = 1f;
            }
            carSource.pitch = 1f;
        }
        if(mode == 1) {
            if(!carSource.isPlaying) {
                carSource.UnPause();
                carSource.pitch = 0.5f;
            }
            carSource.pitch = 0.5f;
        }
        if(mode == 2 || crashed) {
            if(carSource.isPlaying) {
                carSource.Pause();
            }
        }

        frame = provider.CurrentFrame;
        if (frame.Hands.Count > 0) {
            List<Hand> hands = frame.Hands;
            Hand left = null;
            if(hands[0].IsLeft) {
                left = hands[0];
            }
            else if(hands.Count > 1){
                left = hands[1];
            }
            if(left != null) {
                direction = left.Direction.ToVector3();
            }
        }
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(cam.transform.position, 0.1f, direction.normalized, 0f);
        if (hits.Length > 0) {
            int closest = 0;
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].distance < hits[closest].distance) {
                    closest = i;
                }
            }
            if (hits[closest].transform.gameObject.tag == "Checkpoint") {
                if(hits[closest].transform.gameObject.name == currPoint.ToString()) {
                    pointController.SendMessage("newPoint", hits[closest].transform.gameObject);
                    string name = hits[closest].transform.gameObject.name;
                    lastCheck = hits[closest].transform.gameObject.transform.position;
                    Destroy(hits[closest].transform.gameObject);
                    currPoint++;
                    if (name != "0") {
                        StartCoroutine(Get());
                    }
                }
            }
            else if(!crashed){
                StartCoroutine(Respawn());
            }
        }
        if(!finish) {
            time += Time.deltaTime;
            var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
            var seconds = time % 60;//Use the euclidean division for the seconds.
            var fraction = (time * 100) % 100;
            minutes = (int)minutes;
            seconds = (int)seconds;
            //update the label value
            timer.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
        }
    }

    private void LateUpdate() {
        if (frame.Hands.Count > 0 && !start && !crashed && !finish) {
            //player.transform.LookAt(player.transform.position + direction.normalized, player.transform.up);
            if(frame.Hands.Count > 1) {
                if(frame.Hands[0].IsRight) {
                    if(frame.Hands[0].GrabStrength < 0.7f) {
                        mode = 0;
                        player.transform.position += direction.normalized * 5f;
                    }
                    else if(frame.Hands[0].GrabStrength > 0.7f && frame.Hands[1].GrabStrength > 0.7f) {
                        mode = 2;
                    }
                    else {
                        mode = 1;
                        player.transform.position += direction.normalized * 0.5f;
                    }
                }
                else {
                    if (frame.Hands[1].GrabStrength < 0.7f) {
                        mode = 0;
                        player.transform.position += direction.normalized * 5f;
                    }
                    else if (frame.Hands[0].GrabStrength > 0.7f && frame.Hands[1].GrabStrength > 0.7f) {
                        mode = 2;
                    }
                    else {
                        mode = 1;
                        player.transform.position += direction.normalized * 0.5f;
                    }
                }
            }
            else if(frame.Hands[0].IsLeft){
                mode = 1;
                player.transform.position += direction.normalized * 0.5f;
            }
        }
    }
    IEnumerator Respawn() {
        timer.enabled = false;
        crashed = true;
        count.text = "Crashed: 3";
        count.enabled = true;
        yield return new WaitForSeconds(1f);
        count.text = "Crashed: 2";
        yield return new WaitForSeconds(1f);
        count.text = "Crashed: 1";
        yield return new WaitForSeconds(1f);
        count.text = "GO";
        yield return new WaitForSeconds(1f);
        count.enabled = false;
        timer.enabled = true;
        player.transform.position = lastCheck;
        crashed = false;
        
    }

    IEnumerator Countdown() {
        source.PlayOneShot(startMusic, 0.2f);
        yield return new WaitForSeconds(6f);
        source.PlayOneShot(startBeep, 0.5f);
        count.text = "Countdown: 2";
        yield return new WaitForSeconds(1f);
        source.PlayOneShot(startBeep, 0.4f);
        count.text = "Countdown: 1";
        yield return new WaitForSeconds(1f);
        source.PlayOneShot(startFinal, 0.4f);
        count.text = "GO";
        yield return new WaitForSeconds(1f);
        bgSource.PlayOneShot(bg, 0.1f);
        carSource.Play();
        count.enabled = false;
        time = 0f;
        timer.enabled = true;
        start = false;
    }

    IEnumerator Get() {
        count.text = "Checkpoint reached";
        source.PlayOneShot(coin, 1f);
        count.enabled = true;
        yield return new WaitForSeconds(1f);
        count.enabled = false;
    }

    void End() {
        finish = true;
        source.PlayOneShot(Finish, 0.1f);
        carSource.Pause();
        count.text = "Race finished";
    }
}
