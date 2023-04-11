using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace HellsGate.Manager
{
    public class PlayerCharacterInputManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput PlayerCharacterInput;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool Run { get; private set; }
        public bool LookAround { get; private set; }
        public bool Jump { get; private set; }

        private InputActionMap _currentPlayerCharacterActionMap;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _runAction;
        private InputAction _lookAroundAction;
        private InputAction _jumpAction;

        private void Awake()
        {
            this.HideCursor();

            this._currentPlayerCharacterActionMap = this.PlayerCharacterInput.currentActionMap;
            this._moveAction = this._currentPlayerCharacterActionMap.FindAction("Move");
            this._lookAction = this._currentPlayerCharacterActionMap.FindAction("Look");
            this._runAction = this._currentPlayerCharacterActionMap.FindAction("Run");
            this._lookAroundAction = this._currentPlayerCharacterActionMap.FindAction("LookAround");
            this._jumpAction = this._currentPlayerCharacterActionMap.FindAction("Jump");

            this._moveAction.performed += this.OnMove;
            this._lookAction.performed += this.OnLook;
            this._runAction.performed += this.OnRun;
            this._lookAroundAction.performed += this.OnLookAround;
            this._jumpAction.performed += this.OnJump;

            this._moveAction.canceled += this.OnMove;
            this._lookAction.canceled += this.OnLook;
            this._runAction.canceled += this.OnRun;
            this._lookAroundAction.canceled += this.OnLookAround;
            this._jumpAction.canceled += this.OnJump;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            this.Move = context.ReadValue<Vector2>();
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            this.Look = context.ReadValue<Vector2>();
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            this.Run = context.ReadValueAsButton();
        }

        private void OnLookAround(InputAction.CallbackContext context)
        {
            this.LookAround = context.ReadValueAsButton();
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            this.Jump = context.ReadValueAsButton();
        }

        private void OnEnable()
        {
            this._currentPlayerCharacterActionMap.Enable();
        }

        private void OnDisable()
        {
            this._currentPlayerCharacterActionMap.Disable();
        }

        private void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
