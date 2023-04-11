using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HellsGate.Manager;

namespace HellsGate.PlayerCharacter
{
    public class PlayerCharacterController : MonoBehaviour
    {
        [SerializeField] private float AnimationBlendSpeed = 8.9f;
        [SerializeField] private float MouseSensitivity = 15.0f;
        [SerializeField, Range(10, 500)] private float JumpFactor = 150f;
        [SerializeField] private float Dis2Ground = 0.8f;
        [SerializeField] private LayerMask GroundCheck;
        [SerializeField] private float DiveFactor = 20f;
        [SerializeField] private float DiveTime = 0.5f;
        [SerializeField] private Transform Camera;
        [SerializeField] private Transform CameraLookAt;
        [SerializeField] private Transform AltCameraLookAt;
        [SerializeField] private Cinemachine.CinemachineFreeLook FreeLookCamera;

        private Rigidbody _playerCharacterRigidbody;
        private PlayerCharacterInputManager _playerCharacterInputManager;
        private Animator _animator;
        private bool _hasAnimator;
        private int _xVelocityHash;
        private int _yVelocityHash;
        private int _zVelocityHash;
        private int _jumpHash;
        private int _groundedHash;
        private int _fallingHash;
        private int _diveHash;
        private float _xRotation;
        //private bool _isJumping;
        private bool _isGrounded;

        private const float _movementSpeed = 2f;
        private const float _runSpeed = 6f;
        private Vector2 _currentVelocity;

        void Start()
        {
            this._hasAnimator = this.TryGetComponent(out this._animator);
            this._playerCharacterRigidbody = this.GetComponent<Rigidbody>();
            this._playerCharacterInputManager = this.GetComponent<PlayerCharacterInputManager>();

            if (this._hasAnimator)
            {
                this._xVelocityHash = Animator.StringToHash("X_Velocity");
                this._yVelocityHash = Animator.StringToHash("Y_Velocity");
                this._zVelocityHash = Animator.StringToHash("Z_Velocity");
                this._jumpHash = Animator.StringToHash("Jump");
                this._groundedHash = Animator.StringToHash("Grounded");
                this._fallingHash = Animator.StringToHash("Falling");
                this._diveHash = Animator.StringToHash("Dive");
            }
        }

        private void FixedUpdate()
        {
            this.SampleGround();
            this.Move();
            this.HandleJump();
            this.HandleDive();
        }

        private void LateUpdate()
        {
            this.CameraMovements();
        }

        private void Move()
        {
            if (!this._hasAnimator) return;

            float targetSpeed = this._playerCharacterInputManager.Run ? _runSpeed : _movementSpeed;
            if (this._playerCharacterInputManager.Move == Vector2.zero)
            {
                targetSpeed = 0;
            }

            this._currentVelocity.x = Mathf.Lerp(this._currentVelocity.x, targetSpeed * this._playerCharacterInputManager.Move.x, this.AnimationBlendSpeed * Time.fixedDeltaTime);
            this._currentVelocity.y = Mathf.Lerp(this._currentVelocity.y, targetSpeed * this._playerCharacterInputManager.Move.y, this.AnimationBlendSpeed * Time.fixedDeltaTime);

            var xVelocityDiff = this._currentVelocity.x - this._playerCharacterRigidbody.velocity.x;
            var zVelocityDiff = this._currentVelocity.y - this._playerCharacterRigidbody.velocity.z;

            this._playerCharacterRigidbody.AddForce(
                transform.TransformVector(new Vector3(xVelocityDiff, 0, zVelocityDiff)),
                ForceMode.VelocityChange
            );

            this._animator.SetFloat(this._xVelocityHash, this._currentVelocity.x);
            this._animator.SetFloat(this._yVelocityHash, this._currentVelocity.y);
        }

        private void CameraMovements()
        {
            if (!this._hasAnimator) return;
            if (this.FreeLookCamera != null)
            {
                this.FreeLookCamera.m_LookAt = this._playerCharacterInputManager.LookAround ? this.AltCameraLookAt : this.CameraLookAt;
            }
            if (this._playerCharacterInputManager.LookAround) return;

            var lookPos = this.Camera.position - transform.position;
            lookPos = new Vector3(-lookPos.x, 0, -lookPos.z);
            var rotation = Quaternion.LookRotation(lookPos);
            _playerCharacterRigidbody.rotation = Quaternion.Slerp(
                transform.rotation, rotation, Time.smoothDeltaTime * this.MouseSensitivity
            );
        }

        private void HandleJump()
        {
            if (!this._hasAnimator) return;
            if (!this._playerCharacterInputManager.Jump) return;

            this._animator.SetTrigger(this._jumpHash);
        }

        // This is called by the animation event inside the Jumping up animation
        public void JumpAddForce()
        {
            this._playerCharacterRigidbody.AddForce(
                -this._playerCharacterRigidbody.velocity.y * Vector3.up, ForceMode.VelocityChange
            );
            this._playerCharacterRigidbody.AddForce(
                Vector3.up * this.JumpFactor, ForceMode.Impulse
            );
            this._animator.ResetTrigger(this._jumpHash);
        }

        public void SampleGround()
        {
            if (!this._hasAnimator) return;

            RaycastHit hitInfo;
            if (Physics.Raycast(this._playerCharacterRigidbody.worldCenterOfMass,
                Vector3.down, out hitInfo, Dis2Ground + 0.1f, GroundCheck))
            {
                // Grounded
                this._isGrounded = true;
                SetAnimationGrounding();
                return;
            }

            // Falling
            this._isGrounded = false;
            this._animator.SetFloat(this._zVelocityHash, this._playerCharacterRigidbody.velocity.y);
            SetAnimationGrounding();
            return;
        }

        private void SetAnimationGrounding()
        {
            if (!this._hasAnimator) return;

            this._animator.SetBool(this._fallingHash, !this._isGrounded);
            this._animator.SetBool(this._groundedHash, this._isGrounded);
        }

        private void HandleDive()
        {
            if (!this._hasAnimator) return;
            if (!this._playerCharacterInputManager.Dive) return;

            this._animator.SetTrigger(this._diveHash);
        }

        public void DiveStart()
        {
            Debug.Log("DiveStart");
            StartCoroutine(Dive());
        }

        public void DiveEnd()
        {
            this._animator.ResetTrigger(this._diveHash);
        }

        IEnumerator Dive()
        {
            float startTime = Time.time;

            while (Time.time < startTime + this.DiveTime)
            {
                this._playerCharacterRigidbody.MovePosition(
                    (this._playerCharacterRigidbody.transform.position + (this._playerCharacterRigidbody.transform.forward * this.DiveFactor * Time.deltaTime))
                );

                yield return null;
            }
        }
    }
}
