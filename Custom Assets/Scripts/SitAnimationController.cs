using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitAnimationController : MonoBehaviour
{
    public Animator myanim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            myanim.Play("Sitting Laughing");
        }
    }
}
