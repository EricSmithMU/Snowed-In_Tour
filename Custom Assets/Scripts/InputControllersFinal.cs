using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;
using UnityEngine.AI;
public class InputControllersFinal : MonoBehaviour
{
    [System.Serializable]
    public class Callback : UnityEvent<Ray, RaycastHit> { }
    [SerializeField]
    private XRNode xrNodeL = XRNode.LeftHand;
    private XRNode xrNodeR = XRNode.RightHand;
    private InputDeviceRole inputDevice;
    private List<InputDevice> devices = new List<InputDevice>();
    private InputDevice device;
    public Transform leftHandAnchor = null;
    public Transform rightHandAnchor = null;
    public Transform centerEyeAnchor = null;
    public float maxRayDistance = 500;
    public LayerMask excludeLayers;
    public GameObject xrRig;
    bool triggerValue;
    bool primaryButton;
    public GameObject door;
    public Transform Fire;
    public GameObject Target;
    public Transform camera;
    public Transform panel;
    bool secondaryButton;
    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(xrNodeL, devices);
        InputDevices.GetDevicesAtXRNode(xrNodeR, devices);
        device = devices.FirstOrDefault();
        //Add InputDevices here
    }
    void OnEnable()
    {
        if (!device.isValid)
        {
            GetDevice();
        }
        //Check if devices is enabled then enable it
    }
    //In the awake we will find the left and right hand controllers and assign them to anchors
    private void Awake()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Transform>();
        if (leftHandAnchor == null)
        {
            Debug.LogWarning("Assign LeftHandAnchor in the inspector!");
            GameObject left = GameObject.Find("LeftHand Controller");
            if (left != null)
            {
                leftHandAnchor = left.transform;
            }
        }
        if (rightHandAnchor == null)
        {
            Debug.LogWarning("Assign RightHandAnchor in the inspector!");
            GameObject right = GameObject.Find("RightHand Controller");
            if (right != null)
            {
                rightHandAnchor = right.transform;
            }
        }
        if (centerEyeAnchor == null)
        {
            Debug.LogWarning("Assign CenterEyeAnchor in the inspector!");
            GameObject center = GameObject.Find("CenterEyeAnchor");
            if (center != null)
            {
                centerEyeAnchor = center.transform;
            }
        }
    }
    //we can create a Pointer of type transform and assign the left or right to be active
    public Transform Pointer
    {
        get
        {
            if (rightHandAnchor == null)
            {
                return leftHandAnchor;
            }
            return rightHandAnchor;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (!device.isValid)//checking again to make sure device is assigned
        {
            GetDevice();
        }

        if(device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButton) && secondaryButton)
        {
            panel.position = new Vector3(camera.position.x,
                camera.position.y, camera.position.z + 0.5f);
            panel.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        }

        Transform pointer = Pointer;
        if (pointer == null)
        {
            return;
        }
        Ray rayPointer = new Ray(pointer.position, pointer.forward);
        RaycastHit hit;
        NavMeshHit navHit;
        if (Physics.Raycast(rayPointer, out hit, maxRayDistance, ~excludeLayers))
        {
            if (NavMesh.SamplePosition(hit.point, out navHit, 1.0f, NavMesh.AllAreas))
            {

                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {
                    Target.transform.position = navHit.position;
                }
                if (device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButton) && primaryButton)

                {
                    Instantiate(Fire, navHit.position, Quaternion.identity);

                }
            }
            Renderer rend;
            if (hit.transform.gameObject.tag == "Door")
            {
                rend = hit.transform.gameObject.GetComponent<Renderer>();
                rend.material.SetColor("_Color", Color.green);
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {
                    xrRig.transform.position = new Vector3(13f, -3.8f, 12f);//Values might be different for each person
                }


                else
                {
                    rend = door.transform.gameObject.GetComponent<Renderer>();
                    rend.material.SetColor("_Color", Color.white);
                }
            }
            else
            {
            }
            if (hit.transform.gameObject.tag == "Tree")
            {
                rend = hit.transform.gameObject.GetComponent<Renderer>();
                rend.material.SetColor("_Color", Color.green);


                /*
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)

                {
                    Instantiate(Fire, new Vector3(61.05119f, 0.9580374f, -3.604604f), Quaternion.identity);

                } */
            }
            if (hit.transform.gameObject.tag == "InsideDoor")
            {
                rend = hit.transform.gameObject.GetComponent<Renderer>();
                rend.material.SetColor("_Color", Color.green);
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {
                    xrRig.transform.position = new Vector3(13.2f, -3.8f, 3.51f);
                }


                else
                {
                    rend = door.transform.gameObject.GetComponent<Renderer>();
                    rend.material.SetColor("_Color", Color.white);
                }
            }
        }
    }
}


/* 
 * 
 * if (!device.isValid)//checking again to make sure device is assigned
        {
            GetDevice();
        }
        Transform pointer = Pointer;
        if (pointer == null)
        {
            return;
        }

    Ray rayPointer = new Ray(pointer.position, pointer.forward);
        RaycastHit hit;


    if (hit.transform.gameObject.tag == "Tree")
            {
                rend = hit.transform.gameObject.GetComponent<Renderer>();
                rend.material.SetColor("_Color", Color.red);
            }
    
     if (Physics.Raycast(rayPointer, out hit, maxRayDistance, ~excludeLayers))
        {
            if (NavMesh.SamplePosition(hit.point, out navHit, 1.0f, NavMesh.AllAreas))
            {

                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {
                    Target.transform.position = navHit.position;
                }
                if (device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButton) && primaryButton)

                {
                    Instantiate(Fire, navHit.position, Quaternion.identity);

                }
            }
            Renderer rend;
            if (hit.transform.gameObject.tag == "Door")
            {
                rend = hit.transform.gameObject.GetComponent<Renderer>();
                rend.material.SetColor("_Color", Color.green);
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {
                    xrRig.transform.position = new Vector3(13f, -3.8f, 12f);//Values might be different for each person
                }


                else
                {
                    rend = door.transform.gameObject.GetComponent<Renderer>();
                    rend.material.SetColor("_Color", Color.white);
                }
            }
            else
            {
            }
            if (hit.transform.gameObject.tag == "Tree")
            {
                rend = hit.transform.gameObject.GetComponent<Renderer>();
                rend.material.SetColor("_Color", Color.green);


                /*
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)

                {
                    Instantiate(Fire, new Vector3(61.05119f, 0.9580374f, -3.604604f), Quaternion.identity);

                } */
