using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class restart : MonoBehaviour
{
    public Button RestartButton;
    public float IncreaseSize;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnMouseEnter()
    {
        RestartButton.transform.localScale = new Vector3(IncreaseSize, IncreaseSize, 1);
    }

    public void OnMouseExit()
    {
        RestartButton.transform.localScale = new Vector3(1, 1, 1);
    }

    public void Restart()
    {
        SceneManager.LoadScene("OutdoorsScene");
    }
}
