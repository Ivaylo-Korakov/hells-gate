using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HellsGate.Manager;

namespace HellsGate.PlayerCharacter
{
    public class PlayerCharacterController : MonoBehaviour
    {
        [SerializeField]
        private float AnimationBlendSpeed = 8.9f;
        // [SerializeField]
        // private Transform CameraRoot;
        [SerializeField]
        private Transform Camera;
        // [SerializeField]
        // private float CameraUpperLimit = -40f;
        // [SerializeField]
        // private float CameraBottomLimit = 70f;
        [SerializeField]
        private float MouseSensitivity = 21.9f;

        private Rigidbody _playerCharacterRigidbody;
        private PlayerCharacterInputManager _playerCharacterInputManager;
        private Animator _animator;
        private bool _hasAnimator;
        private int _xVelocityHash;
        private int _yVelocityHash;
        private float _xRotation;

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
            }
        }

        private void FixedUpdate()
        {
            this.Move();
        }

        private void Update()
        {
            this.CameraMovements();
        }

        private void Move()
        {
            if (!this._hasAnimator) return;

            float targetSpeed = this._playerCharacterInputManager.Run ? _runSpeed : _movementSpeed;
            if (this._playerCharacterInputManager.Move == Vector2.zero)
            {
                targetSpeed = 0.01f;
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
            if (this._playerCharacterInputManager.LookAround) return;

            var Mouse_X = this._playerCharacterInputManager.Look.x;
            var Mouse_Y = this._playerCharacterInputManager.Look.y;

            var lookPos = this.Camera.position - transform.position;
            lookPos = new Vector3(-lookPos.x, 0, -lookPos.z);
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * this.MouseSensitivity);
        }
    }
}
