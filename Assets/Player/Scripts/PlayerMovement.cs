using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    public Image SprintBar;
    public float Sprint = 10000;
    float mouseDeltaX;
    float mouseDeltaY;
    public InputActionAsset inputActions;
    public PlayerControls PlayerControls;
    private InputAction move;
    private InputAction MouseDelta;
    private InputAction Jump;
    private InputAction SprintInput;
    private InputAction ChangeCamera;
    private InputAction LookController;
    Vector2 MoveDirection;
    public GameObject PlayerHead;
    public GameObject PlayerBody;

    public GameObject FirstPersonCam;
    public GameObject ThirdPersonCam;
    public float movementSpeed = 0.01f;
    public float rotationSpeed = 0.02f;
    public Rigidbody rb;
    public PlayerFeet playerFeet;
    public float Jumpforce;
    public float airspeed;
    bool CursorShown = false;
    public float airMass;
    public float groundMass;
    public bool thirdperson;
    public float cameraSpeed;
    public float distance;
    public float minVerticalAngle;
    public float maxVerticalAngle;
    public bool isTouchingGround;
    private int TouchingForUpdates;
    public TMP_Text LevelDisplay;
    public TMP_Text SeedDisplay;
    public TMP_Text ObjectiveDisplay;
    public TMP_Text InstructionsText;
    private int ShowingStartScreen = 1000;
    public GameObject TextBoxes;
    public GameObject RespawnPoint;
    private int Loops = 0;
    private float textTransparensy = 1.0f;
    public TMP_Text TimerDisplay;
    private float Timer;
    private int ChangedCamRecent;
    public AudioClip JumpAudioClip;
    public AudioSource playerAudioSource;
    public Camera cam;
    // Start is called before the first frame update
    public void Awake()
    {
        PlayerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        playerFeet = GetComponentInChildren<PlayerFeet>();
        GameObject gameObject = this.gameObject;
        PlayerBody = this.gameObject;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
        rb.inertiaTensorRotation = Quaternion.identity;
        
    }

    public void Start()
    {
        RespawnPoint = GameObject.Find("Spawn Point");
        Respawn();
        
    }

    public void OnCollisionStay(Collision collision)
    {
        isTouchingGround = true;
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touched an objective");
        if(other.gameObject.tag == "Objective")
        {
            Destroy(other.gameObject);
        }
        
    }

    public void OnCollisionExit(Collision collision)
    {
        isTouchingGround = false;
    }
    public void OnEnable()
    {
        move = PlayerControls.Movement.PlayerMovement;
        MouseDelta = PlayerControls.Movement.Look;
        Jump = PlayerControls.Movement.Jump;
        SprintInput = PlayerControls.Movement.Sprint;
        ChangeCamera = PlayerControls.Movement.ChangeCamera;
        LookController = PlayerControls.Movement.LookController;
        LookController.Enable();
        ChangeCamera.Enable();
        SprintInput.Enable();
        Jump.Enable();
        MouseDelta.Enable();
        move.Enable();
    }
    public void OnDisable()
    {
        move = PlayerControls.Movement.PlayerMovement;
        MouseDelta = PlayerControls.Movement.Look;
        Jump = PlayerControls.Movement.Jump;
        SprintInput = PlayerControls.Movement.Sprint;
        ChangeCamera = PlayerControls.Movement.ChangeCamera;
        LookController = PlayerControls.Movement.LookController;
        LookController.Disable();
        ChangeCamera.Disable();
        SprintInput.Disable();
        Jump.Disable();
        MouseDelta.Disable();
        move.Disable();
    }
    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "Endless" || SceneManager.GetActiveScene().name == "SampleScene")
        {
            
            Timer += Time.deltaTime;
            string TimerString = Timer.ToString();
            TimerDisplay.text = TimerString;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        ChangedCamRecent -= 1;
        Loops++;
        var theBarRectTransform = SprintBar.transform as RectTransform;
        theBarRectTransform.sizeDelta = new Vector2(Sprint/20, theBarRectTransform.sizeDelta.y);
        MoveDirection = move.ReadValue<Vector2>();
        mouseDeltaX = MouseDelta.ReadValue<Vector2>().x;
        mouseDeltaY = MouseDelta.ReadValue<Vector2>().y;
        if(mouseDeltaX != 0 || mouseDeltaY != 0)
        {
            transform.Rotate(0, mouseDeltaX * 0.1f, 0);
            PlayerHead.transform.Rotate(-mouseDeltaY * 0.1f, 0, 0);
        }
        else if(LookController.ReadValue<Vector2>().x != 0 || LookController.ReadValue<Vector2>().y != 0)
        {
            transform.Rotate(0, LookController.ReadValue<Vector2>().x * 2, 0);
            PlayerHead.transform.Rotate(-LookController.ReadValue<Vector2>().y * 2, 0, 0);
        }

        rb.angularVelocity = Vector3.zero;
        Vector3 movement = new Vector3();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            CheckForEndlessHighScore();
            SceneManager.LoadScene("TitleScreen");
        }
        if (ChangeCamera.IsPressed() & ChangedCamRecent < 0)
        {
            if (thirdperson)
            {
                thirdperson = false;
            }
            else if (!thirdperson)
            {
                thirdperson = true;
            }
            ChangedCamRecent = 50;
        }
        if (thirdperson)
        {
            ThirdPersonCam.SetActive(true);
            FirstPersonCam.SetActive(false);
        }
        else
        {
            ThirdPersonCam.SetActive(false);
            FirstPersonCam.gameObject.SetActive(true);
        }
        if (SprintInput.IsPressed() & Sprint > 0)
        {
            Sprint -= 6;
            movement = new Vector3(MoveDirection.x, 0, MoveDirection.y) * movementSpeed * 2 * Time.deltaTime;
        }
        else
        {
            movement = new Vector3(MoveDirection.x, 0, MoveDirection.y) * movementSpeed * Time.deltaTime;
        }


        movement = transform.TransformDirection(movement);
        rb.MovePosition(rb.position + movement);
        if (Jump.IsPressed() & Sprint > 0 & isTouchingGround & TouchingForUpdates <= 2)
        {
            Sprint -= 500;
            TouchingForUpdates += 1;
            jump();
        }
        else
        {
            TouchingForUpdates = 0;
        }

        if (Input.GetMouseButtonDown(2))
        {
            if (CursorShown)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
        if(SprintInput.IsPressed() == false & Sprint < 10000)
        {
            Sprint += 15;
        }
        if (SceneManager.GetActiveScene().name != "TutorialScene")
        {
            if (ShowingStartScreen > 0)
            {
                {
                    if (SceneManager.GetActiveScene().name == "OutdoorsScene")
                    {
                        LevelDisplay.text = "Level " + PlayerPrefs.GetInt("Score");
                        SeedDisplay.text = "Seed " + PlayerPrefs.GetString("SeedString");
                        GameObject[] AllObjectives = GameObject.FindGameObjectsWithTag("Objective");
                        ///Debug.Log(AllObjectives.Length);
                        ObjectiveDisplay.text = "Collect " + AllObjectives.Length + " Objectives to win";
                        ShowingStartScreen -= 1;
                    }
                    else if(SceneManager.GetActiveScene().name == "SampleScene")
                    {
                        LevelDisplay.text = "CLASSIC";
                        SeedDisplay.text = "";
                        ObjectiveDisplay.text = "SURVIVE";
                        ShowingStartScreen -= 1;
                    }
                    else
                    {
                        LevelDisplay.text = "ENDLESS";
                        SeedDisplay.text = "Seed " + PlayerPrefs.GetString("SeedString");
                        ObjectiveDisplay.text = "";
                        ShowingStartScreen -= 1;
                    }
                    if (ShowingStartScreen < 500)
                    {
                        textTransparensy -= 0.01f;
                        InstructionsText.color = new Color(1, 1, 1, textTransparensy);
                        SeedDisplay.color = new Color(1, 1, 1, textTransparensy);
                        ObjectiveDisplay.color = new Color(1, 1, 1, textTransparensy);
                        LevelDisplay.color = new Color(1, 1, 1, textTransparensy);
                    }
                }
            }
            else if (ShowingStartScreen <= 0 & TextBoxes.activeSelf == true)
            {
                TextBoxes.SetActive(false);
            }
        }

        if(transform.position.y < -15 & SceneManager.GetActiveScene().name != "TutorialScene")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            CheckForEndlessHighScore();
            SceneManager.LoadScene("game over");
        }
        else if(SceneManager.GetActiveScene().name == "TutorialScene" & transform.position.y < -15)
        {
            Respawn();
        }
    }
    void CheckForEndlessHighScore()
    {
        if ((SceneManager.GetActiveScene().name == "Endless" || SceneManager.GetActiveScene().name == "SampleScene") & PlayerPrefs.GetInt("Difficulty") > 1 & PlayerPrefs.GetFloat("Difficulty") < 7)
        {
            if (PlayerPrefs.GetFloat("EndlessHighScore") < Timer)
            {
                PlayerPrefs.SetFloat("EndlessHighScore", Timer);
                PlayerPrefs.SetString("EndlessHighScoreSeed", PlayerPrefs.GetString("SeedString"));
            }
        }
    }

    public void jump()
    {
        rb.AddForce(Vector3.up * Jumpforce);
        Sprint -= 15;
    }
    public void Respawn()
    {
        rb.MovePosition(RespawnPoint.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            CheckForEndlessHighScore();
            SceneManager.LoadScene("game over");
        }
    }
}
