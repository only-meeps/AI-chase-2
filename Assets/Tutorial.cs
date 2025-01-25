using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public TMP_Text PrimaryText;
    public GameObject ControllerControls;
    public GameObject KeyboardControls;
    public GameObject SprintBar;
    public GameObject Objective;
    public GameObject AI;
    public int Loops;
    public int LoopsAfterObjective;
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScreen");
        }
        if (Loops < 300)
        {
            PrimaryText.text = "Welcome to AI Chase 2!";
        }
        else if (Loops > 300 & Loops < 1000)
        {
            PrimaryText.text = "Lets get you moving around";
        }
        else if (Loops > 1000 & Loops < 1600)
        {
            PrimaryText.text = "In AI Chase 2 you can use controller and keyboard to move!";
        }
        else if (Loops > 1600 & Loops < 2400)
        {
            PrimaryText.text = "";
            KeyboardControls.SetActive(true);
        }
        else if (Loops > 2400 & Loops < 3600)
        {
            KeyboardControls.SetActive(false);
            ControllerControls.SetActive(true);
        }
        else if (Loops > 3600 & Loops < 3800)
        {
            PrimaryText.text = "Try and move around a bit";
            ControllerControls.SetActive(false);
        }
        else if(Loops > 3800 & Loops < 4300)
        {
            PrimaryText.text = "";
        }
        else if (Loops > 4300 & Loops < 4600)
        {
            PrimaryText.text = "Up at the top is your sprint bar!";
            SprintBar.SetActive(true);
        }
        else if (Loops > 4600 & Loops < 5000)
        {
            PrimaryText.text = "It tells you how much longer you can sprint and jump";
        }
        else if (Loops > 5000 & Loops < 5300)
        {
            PrimaryText.text = "If it runs out you can't sprint or jump anymore!";
        }
        else if (Loops > 5300 & LoopsAfterObjective < 1)
        {
            PrimaryText.text = "Try and collect the objective. Tip : Holding down the jump button next to an object makes you jump higher";
            if (Objective != null)
            {
                Objective.SetActive(true);
            }
        }
        else if (Objective == null & LoopsAfterObjective < 200)
        {
            PrimaryText.text = "Good job!";
        }
        else if (LoopsAfterObjective > 200 & LoopsAfterObjective < 400)
        {
            PrimaryText.text = "There are enemies that prevent you from getting the objectives";
        }
        else if(LoopsAfterObjective > 400 & LoopsAfterObjective < 700)
        {
            PrimaryText.text = "If you touch one of these enemies you will die, so don't touch them";
        }
        else if(LoopsAfterObjective > 700 & LoopsAfterObjective < 1000)
        {
            PrimaryText.text = "This tutorial only covers the normal ai, but you will find other ai too";
        }
        else if(LoopsAfterObjective > 1000 & LoopsAfterObjective < 1300)
        {
            PrimaryText.text = "Ok, here he comes! Survive as long as possible";
            AI.SetActive(true);
        }
        else if(LoopsAfterObjective > 1300)
        {
            PrimaryText.text = "";
        }
        if (Objective == null)
        {
            LoopsAfterObjective++;
        }
        else
        {
            Loops++;
        }

    }
}
