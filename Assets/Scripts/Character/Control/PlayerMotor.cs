using UnityEngine;

namespace Fps.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMotor : MonoBehaviour
    {
        //-----------------------------------------------------------------------------
        // Event Methods
        //-----------------------------------------------------------------------------

        void Start()
        {
            ShowHideCursor();
            _rigidbody = GetComponent<Rigidbody>();
            velocity = rotation = thrusterForce = Vector3.zero;
            cameraRotation = 0f;
        }

        void FixedUpdate()
        {
            ShowHideCursor();
            
            if (Pause) return;

            UpdatePosition();
            UpdateRotation();
        }

        //-----------------------------------------------------------------------------
        // Public Methods
        //-----------------------------------------------------------------------------

        public void Reset()
        {
            Move(Vector3.zero);
            Rotate(Vector3.zero);
            if (_camera != null)
                _camera.transform.Rotate(Vector3.zero);
        }

        public void ApplyTrusterForce(Vector3 thrusterForce)
        {
            this.thrusterForce = thrusterForce;
        }

        public void Move(Vector3 velocity)
        {
            this.velocity = velocity;
            // Debug.Log($"Velocity: {velocity}. Angular Velocity: {GetComponent<Rigidbody>().angularVelocity}");
        }

        public void Rotate(Vector3 rotation)
        {
            this.rotation = rotation;
        }

        public void RotateCamera(float cameraRotation)
        {
            this.cameraRotation = cameraRotation;
        }

        //-----------------------------------------------------------------------------
        // Private Methods
        //-----------------------------------------------------------------------------

        void ShowHideCursor()
        {
            Util.Input.HideCursor(hideCursor);
        }

        void UpdatePosition()
        {
            Util.RigidbodyUtil.Move(_rigidbody, velocity);
            Util.RigidbodyUtil.AddForce(_rigidbody, thrusterForce, ForceMode.Impulse);
        }

        void UpdateRotation()
        {
            Util.RigidbodyUtil.Rotate(_rigidbody, rotation);
            SetCameraRotation(cameraRotation);
        }

        void SetCameraRotation(float xRotation)
        {
            if (_camera == null)
                return;

            currentCameraRotation -= xRotation;
            currentCameraRotation = Mathf.Clamp(
                currentCameraRotation,
                -cameraRotationLimit,
                cameraRotationLimit
            );
            _camera.transform.localEulerAngles = new Vector3(currentCameraRotation, 0f, 0f);
        }
        
        //-----------------------------------------------------------------------------
        // Properties
        //-----------------------------------------------------------------------------

        public bool Pause
        {
            get { return pause; }
            set
            {
                hideCursor = !value;
                pause = value;
            }
        }

        //-----------------------------------------------------------------------------
        // Attributes
        //-----------------------------------------------------------------------------

        [SerializeField] private Camera _camera;

        [SerializeField] private float cameraRotationLimit = 85f;

        [SerializeField] private bool hideCursor = true;

        [SerializeField] private bool pause;

        private Rigidbody _rigidbody;

        private Vector3 velocity, rotation, thrusterForce;

        private float cameraRotation, currentCameraRotation;
    }
}