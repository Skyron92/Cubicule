using System.Collections;
using UnityEngine;

public class GroundAnimationProvider : MonoBehaviour
{
    private Renderer renderer;
    private MaterialPropertyBlock materialPropertyBlock;

    [Range(0, 50)] private float transitionFactor;
    private float startTime;
    [SerializeField] private float effectDuration;
    [SerializeField] private float deltaInc;
    private bool hasStarted;
    private bool enabled;
    
    private void Awake() {
        renderer = GetComponent<Renderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
    }

    public void SetHasStarted() => hasStarted = !hasStarted;

    private void Update() {
        if(hasStarted) StartCoroutine(LaunchTransition());
#if UNITY_EDITOR
        if (Input.GetButtonDown("Jump")) hasStarted = true;
#endif
    }

    private IEnumerator LaunchTransition() {
        startTime = Time.time;
        enabled = true;
        while (enabled) {
            transitionFactor += deltaInc;
            transitionFactor = Mathf.Clamp(transitionFactor, 0, 50);
            renderer.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetFloat("_transitionFactor", transitionFactor);
            renderer.SetPropertyBlock(materialPropertyBlock);
            if (startTime - Time.time > effectDuration) enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
