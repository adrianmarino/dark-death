using UnityEngine;
using Fps.Player.Animation;

namespace Fps.Player
{
    [RequireComponent(typeof(ConfigurableJoint))]
    [RequireComponent(typeof(PlayerMotor))]
    [RequireComponent(typeof(Headbob))]
    public class PlayerController : MonoBehaviour
    {
        //-----------------------------------------------------------------------------
        // Event Methods
        //-----------------------------------------------------------------------------

        void Start()
        {
            motor = GetComponent<PlayerMotor>();
            joint = GetComponent<ConfigurableJoint>();
            SetJointSettings(jointSpring);
            motor.Reset();
        }

        void Update()
        {
            if (Pause) return;

            UpdateLookRotation();
            UpdatePosition();
        }

        //-----------------------------------------------------------------------------
        // Private Methods
        //-----------------------------------------------------------------------------

        void UpdatePosition()
        {
            UpdateMovement();
            UpdateJump();
        }

        void UpdateMovement()
        {
            var variation = KeyboardMovementVariation();
            if (variation.y == 0 && variation.x == 0)
            {
                Vector3 velocity = Util.VelocityVector.create(transform, Vector2.zero, 0);
                motor.Move(velocity);
            }
            else
            {
                Vector3 velocity = Util.VelocityVector.create(
                    transform,
                    variation,
                    Speed
                );
                motor.Move(velocity);
            }
        }

        void UpdateJump()
        {
            if (!grounded)
            {
                grounded = Util.ObjectElement.IsGrounded(this, floorDistance);
                SetJointSettings(jointSpring);
            }

            Vector3 thruterForceVector = Vector4.zero;
            if (Util.Input.GetJumpButtonDown() && grounded)
            {
                thruterForceVector = Vector3.up * thrusterForce;
                SetJointSettings(0f);
                grounded = false;
            }

            motor.ApplyTrusterForce(thruterForceVector);
        }

        void UpdateLookRotation()
        {
            Vector3 rotation = new Vector3(0f, MouseMovementVariation().x, 0f) * lookSensibility;
            motor.Rotate(rotation);

            motor.RotateCamera(MouseMovementVariation().y * lookSensibility);
        }

        Vector2 KeyboardMovementVariation()
        {
            return Util.Input.KeyboardHorVerMovementDelta();
        }

        Vector2 MouseMovementVariation()
        {
            return Util.Input.MouseHorVerMovementDelta();
        }


        void SetJointSettings(float _jointSpring)
        {
            if (joint != null)
                joint.yDrive = new JointDrive {positionSpring = _jointSpring, maximumForce = jointMaxForce};
        }


        //-----------------------------------------------------------------------------
        // Properties
        //-----------------------------------------------------------------------------

        float Speed
        {
            get { return Util.Input.GetRunButton() ? runSpeed : walkSpeed; }
        }

        public bool Pause { get; set; }

        //-----------------------------------------------------------------------------
        // Attributes
        //-----------------------------------------------------------------------------

        [SerializeField] private float walkSpeed = 5f;

        [SerializeField] private float runSpeed = 50f;

        [SerializeField] private float lookSensibility = 3f;

        [SerializeField] private float thrusterForce = 1000f;

        [Header("Spring settings:")] [SerializeField]
        private float jointSpring = 20f;

        [SerializeField] private float jointMaxForce = 40f;

        [SerializeField] private float floorDistance = 2f;
        
        [SerializeField] private bool pause;

        private PlayerMotor motor;

        private ConfigurableJoint joint;

        private AudioSource audioSource;

        private bool grounded = true;

        private float currentSpeed;
        
    }
}