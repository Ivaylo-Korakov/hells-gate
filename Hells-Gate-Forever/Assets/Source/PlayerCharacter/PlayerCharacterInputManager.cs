using UnityEngine;
using UnityEngine.InputSystem;

namespace HellsGate.Manager
{
    public class PlayerCharacterInputManager : MonoBehaviour
    {
        // ==================== SERIALIZED FIELDS ====================
        #region Serialized Fields
        [SerializeField] private PlayerInput PlayerCharacterInput;
        #endregion

        // ==================== PUBLIC PROPERTIES ====================
        #region Public Properties
        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool Run { get; private set; }
        public bool LookAround { get; private set; }
        public bool Jump { get; private set; }
        public bool Dive { get; private set; }
        #endregion

        // ==================== INPUT ACTIONS ====================
        #region Input Actions and Maps
        private InputActionMap _currentPlayerCharacterActionMap;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _runAction;
        private InputAction _lookAroundAction;
        private InputAction _jumpAction;
        private InputAction _diveAction;
        #endregion

        // ==================== UNITY METHODS ====================
        #region Unity Methods
        private void Awake()
        {
            this.HideCursor();

            // Get the current action map and all the actions
            this._currentPlayerCharacterActionMap = this.PlayerCharacterInput.currentActionMap;
            this._moveAction = this._currentPlayerCharacterActionMap.FindAction("Move");
            this._lookAction = this._currentPlayerCharacterActionMap.FindAction("Look");
            this._runAction = this._currentPlayerCharacterActionMap.FindAction("Run");
            this._lookAroundAction = this._currentPlayerCharacterActionMap.FindAction("LookAround");
            this._jumpAction = this._currentPlayerCharacterActionMap.FindAction("Jump");
            this._diveAction = this._currentPlayerCharacterActionMap.FindAction("Dive");

            // Add the callbacks to the actions
            this._moveAction.performed += this.OnMove;
            this._lookAction.performed += this.OnLook;
            this._runAction.performed += this.OnRun;
            this._lookAroundAction.performed += this.OnLookAround;
            this._jumpAction.performed += this.OnJump;
            this._diveAction.performed += this.OnDive;

            this._moveAction.canceled += this.OnMove;
            this._lookAction.canceled += this.OnLook;
            this._runAction.canceled += this.OnRun;
            this._lookAroundAction.canceled += this.OnLookAround;
            this._jumpAction.canceled += this.OnJump;
            this._diveAction.canceled += this.OnDive;
        }
        #endregion

        // ==================== Input Actions Methods ====================
        #region Input Actions Methods
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

        private void OnDive(InputAction.CallbackContext context)
        {
            this.Dive = context.ReadValueAsButton();
        }
        #endregion

        // ==================== UTILS ====================
        #region Utils
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
        #endregion
    }
}
