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
        public bool Inventory { get; private set; }
        public bool InvSlot1 { get; private set; }
        public bool InvSlot2 { get; private set; }
        public bool InvSlot3 { get; private set; }
        public bool InvSlot4 { get; private set; }
        public bool InvSlot5 { get; private set; }
        public bool InvSlot6 { get; private set; }
        public bool InvSlot7 { get; private set; }
        public bool InvSlot8 { get; private set; }
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
        private InputAction _inventoryAction;
        private InputAction _invSlot1Action;
        private InputAction _invSlot2Action;
        private InputAction _invSlot3Action;
        private InputAction _invSlot4Action;
        private InputAction _invSlot5Action;
        private InputAction _invSlot6Action;
        private InputAction _invSlot7Action;
        private InputAction _invSlot8Action;
        #endregion

        // ==================== UNITY METHODS ====================
        #region Unity Methods
        private void Awake()
        {
            // this.HideCursor();

            // Get the current action map and all the actions
            this._currentPlayerCharacterActionMap = this.PlayerCharacterInput.currentActionMap;
            this._moveAction = this._currentPlayerCharacterActionMap.FindAction("Move");
            this._lookAction = this._currentPlayerCharacterActionMap.FindAction("Look");
            this._runAction = this._currentPlayerCharacterActionMap.FindAction("Run");
            this._lookAroundAction = this._currentPlayerCharacterActionMap.FindAction("LookAround");
            this._jumpAction = this._currentPlayerCharacterActionMap.FindAction("Jump");
            this._diveAction = this._currentPlayerCharacterActionMap.FindAction("Dive");
            this._inventoryAction = this._currentPlayerCharacterActionMap.FindAction("Inventory");
            this._invSlot1Action = this._currentPlayerCharacterActionMap.FindAction("Inv Slot 1");
            this._invSlot2Action = this._currentPlayerCharacterActionMap.FindAction("Inv Slot 2");
            this._invSlot3Action = this._currentPlayerCharacterActionMap.FindAction("Inv Slot 3");
            this._invSlot4Action = this._currentPlayerCharacterActionMap.FindAction("Inv Slot 4");
            this._invSlot5Action = this._currentPlayerCharacterActionMap.FindAction("Inv Slot 5");
            this._invSlot6Action = this._currentPlayerCharacterActionMap.FindAction("Inv Slot 6");
            this._invSlot7Action = this._currentPlayerCharacterActionMap.FindAction("Inv Slot 7");
            this._invSlot8Action = this._currentPlayerCharacterActionMap.FindAction("Inv Slot 8");

            // Add the callbacks to the actions
            this._moveAction.performed += this.OnMove;
            this._lookAction.performed += this.OnLook;
            this._runAction.performed += this.OnRun;
            this._lookAroundAction.performed += this.OnLookAround;
            this._jumpAction.performed += this.OnJump;
            this._diveAction.performed += this.OnDive;
            this._inventoryAction.performed += this.OnInventory;
            this._invSlot1Action.performed += this.OnInvSlot1;
            this._invSlot2Action.performed += this.OnInvSlot2;
            this._invSlot3Action.performed += this.OnInvSlot3;
            this._invSlot4Action.performed += this.OnInvSlot4;
            this._invSlot5Action.performed += this.OnInvSlot5;
            this._invSlot6Action.performed += this.OnInvSlot6;
            this._invSlot7Action.performed += this.OnInvSlot7;
            this._invSlot8Action.performed += this.OnInvSlot8;

            this._moveAction.canceled += this.OnMove;
            this._lookAction.canceled += this.OnLook;
            this._runAction.canceled += this.OnRun;
            this._lookAroundAction.canceled += this.OnLookAround;
            this._jumpAction.canceled += this.OnJump;
            this._diveAction.canceled += this.OnDive;
            this._inventoryAction.canceled += this.OnInventory;
            this._invSlot1Action.canceled += this.OnInvSlot1;
            this._invSlot2Action.canceled += this.OnInvSlot2;
            this._invSlot3Action.canceled += this.OnInvSlot3;
            this._invSlot4Action.canceled += this.OnInvSlot4;
            this._invSlot5Action.canceled += this.OnInvSlot5;
            this._invSlot6Action.canceled += this.OnInvSlot6;
            this._invSlot7Action.canceled += this.OnInvSlot7;
            this._invSlot8Action.canceled += this.OnInvSlot8;
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

        private void OnInventory(InputAction.CallbackContext context)
        {
            this.Inventory = context.ReadValueAsButton();
            Debug.Log("Inventory");
        }

        private void OnInvSlot1(InputAction.CallbackContext context)
        {
            this.InvSlot1 = context.ReadValueAsButton();
        }

        private void OnInvSlot2(InputAction.CallbackContext context)
        {
            this.InvSlot2 = context.ReadValueAsButton();
        }

        private void OnInvSlot3(InputAction.CallbackContext context)
        {
            this.InvSlot3 = context.ReadValueAsButton();
        }

        private void OnInvSlot4(InputAction.CallbackContext context)
        {
            this.InvSlot4 = context.ReadValueAsButton();
        }

        private void OnInvSlot5(InputAction.CallbackContext context)
        {
            this.InvSlot5 = context.ReadValueAsButton();
        }

        private void OnInvSlot6(InputAction.CallbackContext context)
        {
            this.InvSlot6 = context.ReadValueAsButton();
        }

        private void OnInvSlot7(InputAction.CallbackContext context)
        {
            this.InvSlot7 = context.ReadValueAsButton();
        }

        private void OnInvSlot8(InputAction.CallbackContext context)
        {
            this.InvSlot8 = context.ReadValueAsButton();
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

        public void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void ShowCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        #endregion
    }
}
