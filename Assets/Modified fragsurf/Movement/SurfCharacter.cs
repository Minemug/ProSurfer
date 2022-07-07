using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Fragsurf.Movement {

    /// <summary>
    /// Easily add a surfable character to the scene
    /// </summary>
    [AddComponentMenu("Fragsurf/Surf Character")]
    public class SurfCharacter : MonoBehaviour, ISurfControllable
    {

        public enum ColliderType
        {
            Capsule,
            Box
        }

        ///// Fields /////
        ///finishui
        public GameObject FinishScreen, HUD, EscapeOverlay;
        public Text TimeVar, MaxSpeed;
        public AudioSource FinishSound, GameMusic;
        //overlay during the game
        public Text Speed;
        public Text Keys;
        public Text Timer;
//<<<<<<< HEAD
        [Header("Physics Settings")] public Vector3 colliderSize = new Vector3(1f, 2f, 1f);

        [HideInInspector]
        public ColliderType collisionType
        {
            get { return ColliderType.Box; }
        } 
        // Capsule doesn't work anymore; I'll have to figure out why some other time, sorry.


        public float weight = 75f;
        public float rigidbodyPushForce = 2f;
        public bool solidCollider = false;

        [Header("View Settings")] public Transform viewTransform;
        public Transform playerRotationTransform;

        [Header("Crouching setup")] public float crouchingHeightMultiplier = 0.5f;
        public float crouchingSpeed = 10f;
        float defaultHeight;

        bool
            allowCrouch =
                true; // This is separate because you shouldn't be able to toggle crouching on and off during gameplay for various reasons

        [Header("Features")] public bool crouchingEnabled = true;
        public bool slidingEnabled = false;
        public bool laddersEnabled = true;
        public bool supportAngledLadders = true;

        [Header("Step offset (can be buggy, enable at your own risk)")]
        public bool useStepOffset = false;

        public float stepOffset = 0.35f;

        [Header("Movement Config")] [SerializeField]
        public MovementConfig movementConfig;

        
        private GameObject _groundObject;
        private Vector3 _baseVelocity;
        private Collider _collider;
        private Vector3 _angles;
        private Vector3 _startPosition = new Vector3(-43, -5, 7);
        private GameObject _colliderObject;
        private GameObject _cameraWaterCheckObject;
        private CameraWaterCheck _cameraWaterCheck;
        
        private MoveData _moveData = new MoveData();
        private SurfController _controller = new SurfController();

        private Rigidbody rb;

        private List<Collider> triggers = new List<Collider>();
        private int numberOfTriggers = 0;

        private bool underwater = false;
        bool TimerStarted = false;
        private float _timer = 0;
        ///// Properties /////

        public MoveType moveType
        {
            get { return MoveType.Walk; }
        }

        public MovementConfig moveConfig
        {
            get { return movementConfig; }
        }

        public MoveData moveData
        {
            get { return _moveData; }
        }

        public new Collider collider
        {
            get { return _collider; }
        }

        public GameObject groundObject
        {

            get { return _groundObject; }
            set { _groundObject = value; }

        }

        public Vector3 baseVelocity
        {
            get { return _baseVelocity; }
        }

        public Vector3 forward
        {
            get { return viewTransform.forward; }
        }

        public Vector3 right
        {
            get { return viewTransform.right; }
        }

        public Vector3 up
        {
            get { return viewTransform.up; }
        }

        Vector3 prevPosition;
        

        ///// Methods /////

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, colliderSize);
        }

        private void Awake()
        {

            _controller.playerTransform = playerRotationTransform;

            if (viewTransform != null)
            {

                _controller.camera = viewTransform;
                _controller.cameraYPos = viewTransform.localPosition.y;

            }

        }

        private void Start()
        {

            _colliderObject = new GameObject("PlayerCollider");
            _colliderObject.layer = gameObject.layer;
            _colliderObject.transform.SetParent(transform);
            _colliderObject.transform.rotation = Quaternion.identity;
            _colliderObject.transform.localPosition = Vector3.zero;
            _colliderObject.transform.SetSiblingIndex(0);

            // Water check
            _cameraWaterCheckObject = new GameObject("Camera water check");
            _cameraWaterCheckObject.layer = gameObject.layer;
            _cameraWaterCheckObject.transform.position = viewTransform.position;

            SphereCollider _cameraWaterCheckSphere = _cameraWaterCheckObject.AddComponent<SphereCollider>();
            _cameraWaterCheckSphere.radius = 0.1f;
            _cameraWaterCheckSphere.isTrigger = true;

            Rigidbody _cameraWaterCheckRb = _cameraWaterCheckObject.AddComponent<Rigidbody>();
            _cameraWaterCheckRb.useGravity = false;
            _cameraWaterCheckRb.isKinematic = true;

            _cameraWaterCheck = _cameraWaterCheckObject.AddComponent<CameraWaterCheck>();

            prevPosition = transform.position;

            if (viewTransform == null)
                viewTransform = Camera.main.transform;

            if (playerRotationTransform == null && transform.childCount > 0)
                playerRotationTransform = transform.GetChild(0);

            _collider = gameObject.GetComponent<Collider>();

            if (_collider != null)
                GameObject.Destroy(_collider);

            // rigidbody is required to collide with triggers
            rb = gameObject.GetComponent<Rigidbody>();
            if (rb == null)
                rb = gameObject.AddComponent<Rigidbody>();

            allowCrouch = crouchingEnabled;

            rb.isKinematic = true;
            rb.useGravity = false;
            rb.angularDrag = 0f;
            rb.drag = 0f;
            rb.mass = weight;


            switch (collisionType)
            {

                // Box collider
                case ColliderType.Box:

                    _collider = _colliderObject.AddComponent<BoxCollider>();

                    var boxc = (BoxCollider)_collider;
                    boxc.size = colliderSize;

                    defaultHeight = boxc.size.y;

                    break;

                // Capsule collider
                case ColliderType.Capsule:

                    _collider = _colliderObject.AddComponent<CapsuleCollider>();

                    var capc = (CapsuleCollider)_collider;
                    capc.height = colliderSize.y;
                    capc.radius = colliderSize.x / 2f;

                    defaultHeight = capc.height;

                    break;

            }

            _moveData.slopeLimit = movementConfig.slopeLimit;

            _moveData.rigidbodyPushForce = rigidbodyPushForce;

            _moveData.slidingEnabled = slidingEnabled;
            _moveData.laddersEnabled = laddersEnabled;
            _moveData.angledLaddersEnabled = supportAngledLadders;

            _moveData.playerTransform = transform;
            _moveData.viewTransform = viewTransform;
            _moveData.viewTransformDefaultLocalPos = viewTransform.localPosition;

            _moveData.defaultHeight = defaultHeight;
            _moveData.crouchingHeight = crouchingHeightMultiplier;
            _moveData.crouchingSpeed = crouchingSpeed;

            _collider.isTrigger = !solidCollider;
            _moveData.origin = transform.position;
            _startPosition = transform.position;

            _moveData.useStepOffset = useStepOffset;
            _moveData.stepOffset = stepOffset;
            
        }
        float maximumSpeed = 1;
        private void Update()
        {
            //Respawn
            if (transform.position.y < -300 || Input.GetKeyDown(KeyCode.R))
                ResetPosition();
            // Speed indicator
            Speed.text = _moveData.velocity.magnitude.ToString("F2");
            //save highest speed
            if (_moveData.velocity.magnitude > maximumSpeed)
                maximumSpeed = _moveData.velocity.magnitude;
            _colliderObject.transform.rotation = Quaternion.identity;
            
            //UpdateTestBinds ();

            // ESCAPE 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (EscapeOverlay.activeInHierarchy)
                {
                    EscapeOverlay.SetActive(false);
                    PlayTheGame();
                }
                else
                {
                    EscapeOverlay.SetActive(true);
                    StopTheGame();

                }
            }

            UpdateMoveData();


            // Previous movement code
            Vector3 positionalMovement = transform.position - prevPosition;
            transform.position = prevPosition;
            moveData.origin += positionalMovement;

            // Triggers
            if (numberOfTriggers != triggers.Count)
            {
                numberOfTriggers = triggers.Count;

                underwater = false;
                triggers.RemoveAll(item => item == null);
                foreach (Collider trigger in triggers)
                {

                    if (trigger == null)
                        continue;

                    if (trigger.GetComponentInParent<Water>())
                        underwater = true;

                }

            }

            _moveData.cameraUnderwater = _cameraWaterCheck.IsUnderwater();
            _cameraWaterCheckObject.transform.position = viewTransform.position;
            moveData.underwater = underwater;

            if (allowCrouch)
                _controller.Crouch(this, movementConfig, Time.deltaTime);

            _controller.ProcessMovement(this, movementConfig, Time.deltaTime);

            transform.position = moveData.origin;
            prevPosition = transform.position;

            _colliderObject.transform.rotation = Quaternion.identity;
            if (TimerStarted)
            {
                _timer += Time.deltaTime;
                Timer.text = FormatTime(_timer);
            }
        }
    
        public string FormatTime( float time )
        {
            int minutes = (int) time / 60 ;
            int seconds = (int) time - 60 * minutes;
            int milliseconds = (int) (1000 * (time - minutes * 60 - seconds));
            return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds );
        }
        private void UpdateTestBinds()
        {

            if (Input.GetKeyDown(KeyCode.Backspace))
                ResetPosition();

        }

        private void ResetPosition()
        {

            moveData.velocity = Vector3.zero;
            moveData.origin = _startPosition;
            _timer = 0;
            TimerStarted = false;
            Timer.text = "0";
        }

        private void UpdateMoveData()
        {

            _moveData.verticalAxis = Input.GetAxisRaw("Vertical");
            _moveData.horizontalAxis = Input.GetAxisRaw("Horizontal");

            _moveData.sprinting = Input.GetButton("Sprint");

            if (Input.GetButtonDown("Crouch"))
                _moveData.crouching = true;

            if (!Input.GetButton("Crouch"))
                _moveData.crouching = false;

            

            bool moveLeft = _moveData.horizontalAxis < 0f;
            bool moveRight = _moveData.horizontalAxis > 0f;
            bool moveFwd = _moveData.verticalAxis > 0f;
            bool moveBack = _moveData.verticalAxis < 0f;
            bool jump = Input.GetButton("Jump");

            if (!moveLeft && !moveRight)
                _moveData.sideMove = 0f;
            else if (moveLeft)
                _moveData.sideMove = -moveConfig.acceleration;
            else if (moveRight)
                _moveData.sideMove = moveConfig.acceleration;

            if (!moveFwd && !moveBack)
                _moveData.forwardMove = 0f;
            else if (moveFwd)
                _moveData.forwardMove = moveConfig.acceleration;
            else if (moveBack)
                _moveData.forwardMove = -moveConfig.acceleration;

            if (Input.GetButtonDown("Jump"))
                _moveData.wishJump = true;

            if (!Input.GetButton("Jump"))
                _moveData.wishJump = false;

            _moveData.viewAngles = _angles;

        }

        private void DisableInput()
        {

            _moveData.verticalAxis = 0f;
            _moveData.horizontalAxis = 0f;
            _moveData.sideMove = 0f;
            _moveData.forwardMove = 0f;
            _moveData.wishJump = false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static float ClampAngle(float angle, float from, float to)
        {

            if (angle < 0f)
                angle = 360 + angle;

            if (angle > 180f)
                return Mathf.Max(angle, 360 + from);

            return Mathf.Min(angle, to);

        }

        private void OnTriggerEnter(Collider other)
        {

            if (!triggers.Contains(other))
                triggers.Add(other);
            if (other.gameObject.tag == "Finish")
            {
                if (TimerStarted) TimerStarted = false;

                Debug.Log(_timer);
                StopTheGame();
                //actually stop the time
                HUD.SetActive(false);
                GameMusic.Stop();
                FinishSound.Play();
                //show to finish overlay
                FinishScreen.SetActive(true);
                TimeVar.text = FormatTime(_timer);
                MaxSpeed.text = maximumSpeed.ToString("F2");


                //disable onscreen variables
                Speed.gameObject.SetActive(false);
                Keys.gameObject.SetActive(false);
            }

        }

        private static void StopTheGame()
        {
            Time.timeScale = 0;
            // show the cursor back
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        private static void PlayTheGame()
        {
            Time.timeScale = 1;
            // show the cursor back
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnTriggerExit(Collider other)
        {

            if (triggers.Contains(other))
                triggers.Remove(other);
            if (other.gameObject.tag == "Timer")
            {
                if (!TimerStarted) TimerStarted = true;
                
            }
        }

        private void OnCollisionStay(Collision collision)
        {

            if (collision.rigidbody == null)
                return;

            Vector3 relativeVelocity = collision.relativeVelocity * collision.rigidbody.mass / 50f;
            Vector3 impactVelocity = new Vector3(relativeVelocity.x * 0.0025f, relativeVelocity.y * 0.00025f,
                relativeVelocity.z * 0.0025f);

            float maxYVel = Mathf.Max(moveData.velocity.y, 10f);
            Vector3 newVelocity = new Vector3(moveData.velocity.x + impactVelocity.x,
                Mathf.Clamp(moveData.velocity.y + Mathf.Clamp(impactVelocity.y, -0.5f, 0.5f), -maxYVel, maxYVel),
                moveData.velocity.z + impactVelocity.z);

            newVelocity = Vector3.ClampMagnitude(newVelocity, Mathf.Max(moveData.velocity.magnitude, 30f));
            moveData.velocity = newVelocity;

        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "Finish")
            {
                Debug.Log("You Win!");
            }
        }
    }
}

