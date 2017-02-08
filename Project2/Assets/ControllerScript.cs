using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour {

    public GameObject Left;
    public GameObject Right;
    public GameObject player;
    public GameObject chair;
    public GameObject locker;
    public GameObject storage;
    public GameObject tv;
    public GameObject desk;
    public GameObject whiteboard;

    private string leftMode;
    private string rightMode;
    private RaycastHit leftHit;
    private RaycastHit rightHit;
    private GameObject leftcurrObj;
    private GameObject rightcurrObj;
    private string leftspawn;
    private string rightspawn;
    private bool leftThrowing;
    private bool rightThrowing;

    // Use this for initialization
    void Start() {
        leftMode = "tele";
        rightMode = "tele";
        leftcurrObj.SetActive(false);
        rightcurrObj.SetActive(false);
        leftThrowing = false;
        rightThrowing = false;
    }

    // Update is called once per frame
    void Update() {

        Ray leftRay = new Ray(Left.transform.position, Left.transform.forward);
        Ray rightRay = new Ray(Right.transform.position, Right.transform.forward);

        if (Physics.Raycast(leftRay, out leftHit, Mathf.Infinity)) {
            if (leftHit.collider.gameObject.name == "Floor") {
                if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger)) {
                    player.transform.position = new Vector3(leftHit.point.x, 8, leftHit.point.z);
                }
            }
        }

        if (Physics.Raycast(rightRay, out rightHit, Mathf.Infinity)) {
            if (rightHit.collider.gameObject.name == "Floor") {
                if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)) {
                    player.transform.position = new Vector3(rightHit.point.x, 8, rightHit.point.z);
                }
            }

        }

        if (leftMode == "spawn") {
            if (OVRInput.GetUp(OVRInput.RawButton.X)) {
                Destroy(leftcurrObj);
                switch (leftspawn) {
                    case "desk":
                        leftspawn = "chair";
                        leftcurrObj = Instantiate(chair, Left.transform.position + Left.transform.up.normalized * 0.1f, chair.transform.rotation);
                        leftcurrObj.transform.localScale *= 0.01f;
                        break;
                    case "chair":
                        leftspawn = "storage";
                        leftcurrObj = Instantiate(storage, Left.transform.position + Left.transform.up.normalized * 0.1f, storage.transform.rotation);
                        leftcurrObj.transform.localScale *= 0.01f;
                        break;
                    case "storage":
                        leftspawn = "locker";
                        leftcurrObj = Instantiate(locker, Left.transform.position + Left.transform.up.normalized * 0.1f, locker.transform.rotation);
                        leftcurrObj.transform.localScale *= 0.01f;
                        break;
                    case "locker":
                        leftspawn = "tv";
                        leftcurrObj = Instantiate(tv, Left.transform.position + Left.transform.up.normalized * 0.1f, tv.transform.rotation);
                        leftcurrObj.transform.localScale *= 0.01f;
                        break;
                    case "tv":
                        leftspawn = "whiteboard";
                        leftcurrObj = Instantiate(whiteboard, Left.transform.position + Left.transform.up.normalized * 0.1f, whiteboard.transform.rotation);
                        leftcurrObj.transform.localScale *= 0.1f;
                        break;
                    case "whiteboard":
                        leftspawn = "desk";
                        leftcurrObj = Instantiate(desk, Left.transform.position + Left.transform.up.normalized * 0.1f, desk.transform.rotation);
                        leftcurrObj.transform.localScale *= 0.01f;
                        break;
                }
            }

            if(OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger)) {
                leftThrowing = true;
            }

            if(leftThrowing) {
                Vector3.MoveTowards(leftcurrObj.transform.position, new Vector3(leftHit.point.x, 0.5f, leftHit.point.z), 1f);
                if (leftcurrObj.transform.position == leftHit.point) {
                    leftThrowing = false;
                }
            }
            float y = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).y * 1f;
            float x = - OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).x * 1f;
            leftcurrObj.transform.Rotate(new Vector3(0,x,0),Space.World);
            leftcurrObj.transform.Rotate(Vector3.Cross(new Vector3(0,1,0), new Vector3(Left.transform.forward.x,0,Left.transform.forward.z).normalized), y, Space.World);
        }

        if (rightMode == "spawn") {
            if (OVRInput.GetUp(OVRInput.RawButton.A)) {
                Destroy(rightcurrObj);
                switch (rightspawn) {
                    case "desk":
                        rightspawn = "chair";
                        rightcurrObj = Instantiate(chair, Right.transform.position + Right.transform.up.normalized * 0.1f, chair.transform.rotation);
                        rightcurrObj.transform.localScale *= 0.01f;
                        break;
                    case "chair":
                        rightspawn = "storage";
                        rightcurrObj = Instantiate(storage, Right.transform.position + Right.transform.up.normalized * 0.1f, storage.transform.rotation);
                        rightcurrObj.transform.localScale *= 0.01f;
                        break;
                    case "storage":
                        rightspawn = "locker";
                        rightcurrObj = Instantiate(storage, Right.transform.position + Right.transform.up.normalized * 0.1f, locker.transform.rotation);
                        rightcurrObj.transform.localScale *= 0.01f;
                        break;
                    case "locker":
                        rightspawn = "tv";
                        rightcurrObj = Instantiate(storage, Right.transform.position + Right.transform.up.normalized * 0.1f, tv.transform.rotation);
                        rightcurrObj.transform.localScale *= 0.1f;
                        break;
                    case "tv":
                        rightspawn = "whiteboard";
                        rightcurrObj = Instantiate(storage, Right.transform.position + Right.transform.up.normalized * 0.1f, whiteboard.transform.rotation);
                        rightcurrObj.transform.localScale *= 0.1f;
                        break;
                    case "whiteboard":
                        rightspawn = "desk";
                        rightcurrObj = Instantiate(storage, Right.transform.position + Right.transform.up.normalized * 0.1f, desk.transform.rotation);
                        rightcurrObj.transform.localScale *= 0.01f;
                        break;
                }
            }
            float y = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y * 1f;
            float x = -OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x * 1f;
            rightcurrObj.transform.Rotate(new Vector3(0, x, 0), Space.World);
            rightcurrObj.transform.Rotate(Vector3.Cross(new Vector3(0, 1, 0), new Vector3(Right.transform.forward.x, 0, Right.transform.forward.z).normalized), y, Space.World);
        }

        if (OVRInput.GetUp(OVRInput.RawButton.A) && rightMode != "spawn") {
            rightMode = "spawn";
            rightcurrObj = Instantiate(desk, Right.transform.position + Right.transform.up.normalized * 0.1f, desk.transform.rotation);
            rightspawn = "desk";
            rightcurrObj.transform.localScale *= 0.01f;
            rightcurrObj.SetActive(true);

        }

        if (OVRInput.GetUp(OVRInput.RawButton.X) && leftMode != "spawn") {
            leftMode = "spawn";
            leftcurrObj = Instantiate(desk, Left.transform.position + Left.transform.up.normalized * 0.1f, desk.transform.rotation);
            leftspawn = "desk";
            leftcurrObj.transform.localScale *= 0.01f;
            leftcurrObj.SetActive(true);
        }
    }

    private void LateUpdate() {
        Left.GetComponent<LineRenderer>().SetPosition(0, Left.transform.position - Left.transform.up.normalized * 0.02f);
        Left.GetComponent<LineRenderer>().SetPosition(1, leftHit.point);

        Right.GetComponent<LineRenderer>().SetPosition(0, Right.transform.position - Right.transform.up.normalized * 0.02f);
        Right.GetComponent<LineRenderer>().SetPosition(1, rightHit.point);

        if (leftcurrObj != null) {
            leftcurrObj.transform.position = Left.transform.position + Left.transform.up.normalized * 0.1f;
        }

        if (rightcurrObj != null) {
            rightcurrObj.transform.position = Right.transform.position + Right.transform.up.normalized * 0.1f;
        }
    }
}
