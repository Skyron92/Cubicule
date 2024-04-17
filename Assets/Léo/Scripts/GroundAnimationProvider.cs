using System.Collections;
using UnityEngine;

public class GroundAnimationProvider : MonoBehaviour
{
    private Renderer renderer;
    private MaterialPropertyBlock materialPropertyBlock;

    [SerializeField, Range(0, 50)] private float transitionFactor;
    [SerializeField] private float startTime;
    [SerializeField] private float effectDuration;
    [SerializeField] private float deltaInc;
    private bool hasStarted;
    [SerializeField] private bool enabled;
    
    private void Awake() {
        renderer = GetComponent<Renderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
    }

    public void SetHasStarted() => hasStarted = !hasStarted;

    private void Update() {
        if(hasStarted) StartCoroutine(LaunchTransition());
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
            Debug.Log(transitionFactor);
            if (startTime - Time.time < effectDuration) enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
