using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimManager : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private InputActionReference iarGrabRef;
    [SerializeField] private InputActionReference iarTriggerRef;
    private static readonly int GripParameter = Animator.StringToHash("Grip");
    private static readonly int TriggerParameter = Animator.StringToHash("Trigger");

    private InputAction GrabInputAction => iarGrabRef.action;
    private InputAction TriggerInputAction => iarTriggerRef.action;
    
    void Awake() {
        GrabInputAction.Enable();
        GrabInputAction.started += context => animator.SetFloat(GripParameter, 1); 
        GrabInputAction.canceled += context => animator.SetFloat(GripParameter, 0);
        TriggerInputAction.Enable();
        TriggerInputAction.started += context => animator.SetFloat(TriggerParameter, 1);
        TriggerInputAction.canceled += context => animator.SetFloat(TriggerParameter, 0);
    }
}
