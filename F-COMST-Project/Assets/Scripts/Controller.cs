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
    [Header("Control Settings")]
    public float MouseSensitivity = 25.0f;
    public float PlayerSpeed = 5.0f;
    public float RunningSpeed = 7.0f;
    public float JumpSpeed = 12.0f;
    public float gravity = 10f;
    public float crouchSmooth = 10f;

    Vector3 lastVelocity = Vector3.zero;
    [Header("Audio")]
    public AudioClip[] Clips;
    public float PitchMin = 1.0f;
    public float PitchMax = 1.0f;
    public AudioSource source => m_Source;
    AudioSource m_Source;
    public AudioClip FootstepPlayer;
    public AudioClip JumpingAudioCLip;
    public AudioClip LandingAudioClip;

    
    
    Vector3 last_velocity = Vector3.zero;
    bool m_IsPaused = false;

    float m_VerticalAngle, m_HorizontalAngle;


    public float currentVerticalSpeed { get; private set; } = 0.0f;

    public bool LockControl { get; set; }
    public bool CanPause { get; set; } = true;

    public bool Grounded => m_Grounded;

    CharacterController m_CharacterController;

    Inventory m_inventory;

    bool m_Grounded;
    float m_GroundedTimer;
    bool m_crouching = false;


    void Awake()
    {
        Instance = this;
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
        //if (CanPause && Input.GetButtonDown("Menu"))
        //{
        //    m_IsPaused = !m_IsPaused;
        //}
        InternalLockUpdate();
        bool wasGrounded = m_Grounded;
        bool loosedGrounding = false;
        if (!m_CharacterController.isGrounded)
        {
            if (m_Grounded)
            {
                m_GroundedTimer += Time.deltaTime;
                if (m_GroundedTimer >= 0.1f)
                {
                    loosedGrounding = true;
                    m_Grounded = false;
                }
            }
                
        }
        else
        {
            m_GroundedTimer = 0.0f;
            m_Grounded = true;
        }

        if (!m_IsPaused && !LockControl)
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = !m_IsPaused ? (isRunning ? RunningSpeed : PlayerSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = !m_IsPaused ? (isRunning ? RunningSpeed : PlayerSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = m_velocity.y;
            m_velocity = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && !m_IsPaused && m_Grounded)
            {
                m_velocity.y = JumpSpeed;
            }
            else
            {
                m_velocity.y = movementDirectionY;
            }

            if (!m_CharacterController.isGrounded)
            {
                m_velocity.y -= gravity * (Time.fixedDeltaTime)*40;
            }

            if (isRunning)
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
                    MainCamera.transform.position = Vector3.Lerp(transform.position,new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z),Time.deltaTime* crouchSmooth);
                    m_crouching = false;
                }
                else
                {
                    m_crouching = true;
                    MainCamera.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Time.deltaTime*crouchSmooth);
                    m_CharacterController.height = 1.0f;

                }
            }

            // Move the controller
            m_CharacterController.Move(m_velocity * Time.deltaTime);

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
            
            
            
        }

    }


    //controls the locking and unlocking of the mouse
    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_IsPaused = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_IsPaused = false;
        }

        if (!m_IsPaused)
        {
            UnlockCursor();
        }
        else if (m_IsPaused)
        {
            LockCursor();
        }
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DisplayCursor(bool display)
    {
        m_IsPaused = display;
        Cursor.lockState = display ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = display;
    }

    public void PlayClip(AudioClip clip, float pitchMin, float pitchMax)
    {
        m_Source.pitch = Random.Range(pitchMin, pitchMax);
        m_Source.PlayOneShot(clip);
    }
}
