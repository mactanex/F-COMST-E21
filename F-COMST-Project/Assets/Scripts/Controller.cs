using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CharacterController))]
public class Controller : MonoBehaviour
{
    public static Controller Instance { get; protected set; }
    [Header("Camera")]
    public Camera MainCamera;
    public Transform PlayerPosition;
    public string TEEST;
    [Header("Control Settings")]
    public float MouseSensitivity = 25.0f;
    public float PlayerSpeed = 5.0f;
    public float RunningSpeed = 7.0f;
    public float JumpSpeed = 10.0f;
    public float gravity = 14.0f;
    public float crouchSmooth = 10f;

    Vector3 lastVelocity = Vector3.zero;

    [Header("Audio")]

    public float PitchMin = 1.0f;
    public float PitchMax = 1.0f;
    public AudioClip[] WalkClips;
    public AudioClip[] RunClips;
    public AudioSource audio;



    Vector3 last_velocity = Vector3.zero;
    bool m_IsPaused = false;

    float m_VerticalAngle, m_HorizontalAngle;
    bool isAirborn = false;

    public float currentVerticalSpeed { get; private set; } = 0.0f;

    public bool LockControl { get; set; }
    public bool CanPause { get; set; } = true;

    public bool Grounded => m_Grounded;
    private bool step = true; 
    CharacterController m_CharacterController;

    Inventory m_inventory;

    bool m_Grounded;
    float m_GroundedTimer;
    bool m_crouching = false;
    bool isMoving = false;

    void Awake()
    {
        Instance = this;
        audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        m_IsPaused = false;
        m_Grounded = true;

        MainCamera.transform.SetParent(PlayerPosition, false);
        MainCamera.transform.localPosition = Vector3.zero;
        MainCamera.transform.localRotation = Quaternion.identity;
        m_CharacterController = GetComponent<CharacterController>();
        m_inventory = GetComponent<Inventory>();
        

        m_VerticalAngle = 0.0f;
        m_HorizontalAngle = transform.localEulerAngles.y;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 m_velocity = Vector3.zero;
        //bool loosedGrounding = false;

        if (!InGameMenu.GameIsPaused && !ColorPuzzleManager.IsActive())
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = !m_IsPaused ? (isRunning ? RunningSpeed : PlayerSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = !m_IsPaused ? (isRunning ? RunningSpeed : PlayerSpeed) * Input.GetAxis("Horizontal") : 0;
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) isMoving = true;
            else isMoving = false;

            m_velocity = (forward * curSpeedX) + (right * curSpeedY);
           
            m_velocity.y = 0;
            if (m_CharacterController.isGrounded)
            {
                currentVerticalSpeed = -gravity * Time.deltaTime;
                if (Input.GetButton("Jump"))
                {
                    currentVerticalSpeed = JumpSpeed;
                    if (m_crouching)
                    {
                        m_CharacterController.height = 2.0f;
                        MainCamera.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Time.deltaTime * crouchSmooth);
                        m_crouching = false;
                    }
                    isAirborn = true;
                    PlayClip("Jump");
                }
            }
            else
            {
                currentVerticalSpeed -= gravity * Time.deltaTime;
            }
            m_velocity.y = currentVerticalSpeed;
            // Move the controller
            m_CharacterController.Move(m_velocity * Time.deltaTime);

            if (isRunning && m_crouching)
            {
                m_CharacterController.height = 2.0f;
                MainCamera.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Time.deltaTime * crouchSmooth);
                m_crouching = false;
            }
            //crouching
            if (Input.GetKeyDown(KeyCode.C))
            {

                if (m_crouching)
                {

                    m_CharacterController.height = 2.0f;
                    MainCamera.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Time.deltaTime * crouchSmooth);
                    m_crouching = false;
                }
                else
                {
                    m_crouching = true;
                    MainCamera.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Time.deltaTime * crouchSmooth);
                    m_CharacterController.height = 1.0f;

                }
            }



            // Turn player

            float turnPlayer = Input.GetAxis("Mouse X") * MouseSensitivity;
            m_HorizontalAngle = m_HorizontalAngle + turnPlayer;

            if (m_HorizontalAngle > 360) m_HorizontalAngle -= 360.0f;
            if (m_HorizontalAngle < 0) m_HorizontalAngle += 360.0f;

            Vector3 currentAngles = transform.localEulerAngles;
            currentAngles.y = m_HorizontalAngle;
            transform.localEulerAngles = currentAngles;

            // Camera look up/down
            var turnCam = -Input.GetAxis("Mouse Y");
            turnCam = turnCam * MouseSensitivity;
            m_VerticalAngle = Mathf.Clamp(turnCam + m_VerticalAngle, -89.0f, 89.0f);
            currentAngles = PlayerPosition.transform.localEulerAngles;
            currentAngles.x = m_VerticalAngle;
            PlayerPosition.transform.localEulerAngles = currentAngles;


            //currentVerticalSpeed = move.magnitude / (PlayerSpeed * Time.deltaTime);
            if (!isAirborn && step)
            {
                if (isRunning)
                {
                    audio.clip = RunClips[Random.Range(0, RunClips.Length)];
                    //audio.volume = 0.1f;
                    StartCoroutine(WaitForFootSteps(0.25f));
                    //AudioSource source = AudioManager.GetSource("Run");
                    if (isMoving && !audio.isPlaying) audio.Play(); // if player is moving and audiosource is not playing play it
                    if (!isMoving) audio.Stop(); // if player is not moving and audiosource is playing stop it

                }
                else
                {
                    audio.clip = WalkClips[Random.Range(0, WalkClips.Length)];
                    StartCoroutine(WaitForFootSteps(0.25f));
                    //audio.volume = 0.1f; 
                    if (isMoving && !audio.isPlaying) audio.Play(); // if player is moving and audiosource is not playing play it
                    if (!isMoving) audio.Stop(); // if player is not moving and audiosource is playing stop it
                }
            }
            if (m_CharacterController.isGrounded)   {
                if (isAirborn)
                {
                    PlayClip("Land");
                    isAirborn = false;
                }

            }

            

        }

        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    public void PlayClip(string clip)
    {
        AudioManager.Play(clip);
    }

    IEnumerator WaitForFootSteps(float stepsLength) { step = false; yield return new WaitForSeconds(stepsLength); step = true; }
}
