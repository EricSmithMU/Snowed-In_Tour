using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.Linq;

public class ChristmasTree : MonoBehaviour
{
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
    bool triggerValue = false;
    public Animator myAnim;
    public Transform camera;

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
        OnEnable();
    }

    // Update is called once per frame
    void Update()
    {
        Transform pointer = Pointer;
        Ray ray = new Ray(pointer.position, pointer.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, maxRayDistance, ~excludeLayers))
            {
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                myAnim.Play("Star");
            }
            }
            

    }
}
