using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyButtonInteractor : MonoBehaviour
{
    public TextMeshProUGUI myText;
    public GameObject tablet;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void IntroButton()
    {
        myText.text = "It looks we have been Snowed-In. You should take a look around and see if we can get out of here in time for Winter Break.";
        button.SetActive(true);
    }

    public void PhaseOneButton()
    {
        myText.text = "I would talk to the worker in the Potential Energy Cafe. I think she said something about getting out of here.";
        tablet.SetActive(true);
    }
    public void PhaseTwoButton()
    {
        myText.text = "YES! Earlier, I saw a window on the second floor in the front room that was not completly coverd by snow. Maybe we can get out that way, but I'm not sure how to get there since the elevator is broken. I would talk to Santa in the MacLab maybe he knows how.";
        tablet.SetActive(true);
    }
    public void NaughtyButton()
    {
        myText.text = "You are ... Naughty";
        button.SetActive(true);
    }
    public void PhaseThreeButton()
    {
        myText.text = "Yes, I would say take the elevator but ..... Talk to one of my elves, surely they can get you my ladder. He should be fixing the elevator right now";
        tablet.SetActive(true);
    }
    public void HowIsItButton()
    {
        myText.text = "I just can't figure out how to get this elevator to work right";
        button.SetActive(true);
    }
    public void LadderButton()
    {
        myText.text = "Sure, treat you elf. I gave it to Elsa. She should be in the front room.";
        tablet.SetActive(true);
    }
    public void HaveLadderButton()
    {
        myText.text = "Yes, I have the laddder, here it is.";
        tablet.SetActive(true);
        button.SetActive(true);
    }
    public void LeaveGameButton()
    {
        myText.text = "Leaving Game ...";
    }
}
