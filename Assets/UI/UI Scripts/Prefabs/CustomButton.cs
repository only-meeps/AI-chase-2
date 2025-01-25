using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class CustomButton : MonoBehaviour
{
    public Button Button;
    public float IncreaseSize;
    public Vector3 ogSize;
    //Handles MouseEnter
    private void Start()
    {
        ogSize = transform.localScale;
    }
    public void OnMouseEnter()
    {
        //Increase size of button
        Button.transform.localScale = ogSize * IncreaseSize;
    }
    //Handles MouseExit
    public void OnMouseExit()
    {
        //Decrease size of button
        Button.transform.localScale = ogSize;
    }
}
