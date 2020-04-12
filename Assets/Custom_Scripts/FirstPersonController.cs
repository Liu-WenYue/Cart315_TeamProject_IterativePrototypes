using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.
        [SerializeField] private AudioClip low_healthSound; 

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;
        private AudioSource healthbarAudio;
        private bool lh_already_playing = false; //low health sound is already playing 

        public AudioSource[] audios = new AudioSource[3];

        public GameObject vegetable; //reference of prefab 
        private GameObject current_vegetable; 
        private Collider v;
        public Vegetable daikon;
        bool daikon_picked;
        public bool daikon_already_used = false;
        private bool daikon_created = false; 
        public AudioClip drop;
        AudioSource daikon_dropAudio; 


        public Potion heal;
        bool has_potion;
        //bool used_potion = false;
        public DrinkPotionAudio potionAudio; 

        private HealthBar player_health;

        public Shield protection;
        bool has_shield;
        bool used_shield = false;
        private float stopHealth;


        //private int num_daikon = 1;
        //private int num_potion = 1;
       // private int num_shield = 1;

       // public GameObject notfound1;
       // public GameObject daikon_active;
        public GameObject daikon_used;

        //public GameObject notfound2;
        //public GameObject potion_active;
        public GameObject potion_used; 

        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            //m_AudioSource = GetComponent<AudioSource>(); //gets only one audio source
			m_MouseLook.Init(transform , m_Camera.transform);


            //v = vegetable.GetComponent<Collider>();

            //Can you get an instance to this current player's health
            player_health = GetComponent<HealthBar>();

            audios = GetComponents<AudioSource>(); //already sets with the two arrays automatically
            m_AudioSource = audios[0];
            healthbarAudio = audios[1];
            daikon_dropAudio = audios[2]; 
            //audios[0] = m_AudioSource;
            //audios[1] = healthbarAudio; 
        }


        // Update is called once per frame
        private void Update()
        {
            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;


            //DAIKON IS picked
            
            
            if((Vegetable.num_daikon == 1)&&(!daikon_created))
            {
                Debug.Log("Creating daikon instance"); 
                daikon_already_used = false;
                current_vegetable = Instantiate(vegetable, new Vector3(vegetable.transform.position.x, vegetable.transform.position.y, vegetable.transform.position.z), Quaternion.identity);
                current_vegetable.transform.Rotate(0, 90, 0); 

                v = current_vegetable.GetComponent<Collider>();
                v.isTrigger = false;
                current_vegetable.GetComponent<MeshRenderer>().enabled = false;
                daikon_created = true; 
            }
            

           // daikon_picked = daikon.daikon_ispicked; -> cuz vegetable is destroyed 

            //if(daikon_picked)
            //{
                if((!daikon_already_used)&&(daikon_created))
                {

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Player pressed E - DAIKON");
                        current_vegetable.GetComponent<MeshRenderer>().enabled = true;

                        current_vegetable.transform.position = Vector3.MoveTowards(vegetable.transform.position, this.transform.position, 1000f * Time.deltaTime);

                        //translate downwards
                        //vegetable.transform.position.Set(-18f, 0.2f, -4.16f);
                        current_vegetable.transform.Translate(new Vector3(0, 0, -0.1f));

                        current_vegetable.transform.Rotate(new Vector3(90, 90, 0));
                        

                        audios[2].clip = drop;
                        audios[2].PlayOneShot(drop); 

                        daikon_already_used = true; //for this current one - for sound 
                        daikon_created = false; //for this one 
                        
                        Vegetable.num_daikon = 0; 

                        daikon_used.SetActive(true);
                    }
                }
            //}

            has_potion = heal.potion_picked; 

            if((has_potion)&&(Input.GetKeyDown(KeyCode.R)))
            {
                Debug.Log("Player pressed R - USING POTION");

                //if (!used_potion)
                //{
                    //Debug.Log(player_health.getHitpoint());
                    Debug.Log("Player used potion!");
                    potionAudio.DrinkingAudio();
                    Potion.num_potion = 0; //because we used the potion

                    potion_used.SetActive(true); 

                    float current_health = player_health.getHitpoint();
                    //Add 50.0f
                    if (current_health < 30)
                    {
                        if ((current_health + 50.0f) >= 30)
                        {
                            player_health.LowHealth.enabled = false;
                            player_health.currentHealth.enabled = true;
                        }
                    }

                    player_health.setHitpoint(current_health + 50.0f);
                    if (player_health.getHitpoint() > player_health.getMaxHitPoint())
                    {
                        player_health.setHitpoint(player_health.getMaxHitPoint());
                        player_health.UpdateHealthBar();
                    }
                    else
                    {
                        player_health.UpdateHealthBar();
                    }
                    //has_potion = false; 
                    //Debug.Log(player_health.getHitpoint());
                    //Destroy(heal);
                //}
            }

            has_shield = protection.shield_picked;

            if ((has_shield) && (Input.GetKeyDown(KeyCode.T)))
            {

                if (!used_shield)
                {
                    used_shield = true;
                    Debug.Log("Using Shield");
                    StartCoroutine(ProtectionTime());

                    //Debug.Log("Finished protection coroutine");
                    player_health.setHitpoint(stopHealth);
                    player_health.UpdateHealthBar();
                }
            }

            //Own code for audio source 
            if (player_health.getHitpoint() < 30)
            {
                if (!lh_already_playing)
                {
                    PlayLowHealthSound(); 
                }
            }
            else if(player_health.getHitpoint() >= 30)
            {
                if(lh_already_playing)
                {
                    StopLowHealthSound(); 
                }
            }
        }

        IEnumerator ProtectionTime()
        {
            Debug.Log("Player protected from damage");
            //current_health point
            stopHealth = player_health.getHitpoint();
            Debug.Log(stopHealth);

            //player_health.setHitpoint(stopHealth);
            //player_health.UpdateHealthBar();

            player_health.TakeDamage(0);
            player_health.UpdateHealthBar();

            yield return new WaitForSeconds(10);
            //Safe guard function 
            player_health.setHitpoint(stopHealth);
            player_health.UpdateHealthBar();

            Debug.Log("Counted 3 seconds");
            //Debug.Log(player_health.getHitpoint());

            player_health.TakeDamage(20);

            //player_health.setHitpoint(stopHealth);
            //player_health.UpdateHealthBar();
        }

        private void PlayLowHealthSound()
        {
            audios[1].clip = low_healthSound;
            //audios[1].volume = 0.3f; 
            audios[1].Play();
            audios[1].loop = true;
            lh_already_playing = true;
        }

        private void StopLowHealthSound()
        {
            audios[1].loop = false;
            audios[1].Stop();
            lh_already_playing = false; //stopped playing 
        }

        private void PlayLandingSound()
        {
            audios[0].clip = m_LandSound;
            audios[0].Play(); 
            /*
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            */
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;


            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();
        }


        private void PlayJumpSound()
        {
            audios[0].clip = m_JumpSound;
            audios[0].Play(); 
            /*
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
            */ 
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);

            audios[0].clip = m_FootstepSounds[n];
            audios[0].PlayOneShot(audios[0].clip); 

            /*
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            */ 
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            //m_FootstepSounds[0] = m_AudioSource.clip;
            m_FootstepSounds[0] = audios[0].clip; 
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;
            // m_IsWalking = true;
            

            
#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
           /// m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
           /// speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            speed = m_WalkSpeed; 
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }

    
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
}
