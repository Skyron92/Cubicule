using Unity.XR.CoreUtils;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] private Material groundMaterial;
    [Range(0, 50)] private float transitionFactor;
    [Range(-.13f, 0)] private float fresnelImitation;
    private float startTime;
    [SerializeField] private float effectDuration;
    [SerializeField] private float deltaInc; 
    [SerializeField] private float deltaFresnelInc;
    private Vector2 _position;
    private bool _hasStarted;
    private MeshRenderer meshRenderer;
    private MaterialPropertyBlock materialPropertyBlock;
    private bool enabled;
    private int _materialIndex;

    private void Awake() {
        materialPropertyBlock = new MaterialPropertyBlock();
    }
    
    private void Update() {
        if (_hasStarted) LaunchTransition();
    }

    private void OnCollisionEnter(Collision other) {
        if(!CollideByBottom(other)) return;
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit = new RaycastHit();
        if (!Physics.Raycast(ray, out hit)) return;
        _position = hit.textureCoord;
        meshRenderer = other.gameObject.GetComponent<MeshRenderer>();
        meshRenderer.AddMaterial(new Material(groundMaterial));
        _materialIndex = meshRenderer.materials.Length - 1;
        fresnelImitation = -.12f;
        transitionFactor = 0;
        _hasStarted = true;
    }
    
    private bool CollideByBottom(Collision other) => other.transform.position.y < transform.position.y;

    private float UVToPos(float pUVCoordinates) => -5 + (1 - pUVCoordinates) * 10;
    
    private void LaunchTransition() {
        if(meshRenderer == null) return;
        startTime = Time.time;
        transitionFactor += deltaInc;
        transitionFactor = Mathf.Clamp(transitionFactor, 0, 50);
        fresnelImitation += deltaFresnelInc;
        fresnelImitation = Mathf.Clamp(fresnelImitation, -.12f, 0);
        meshRenderer.GetPropertyBlock(materialPropertyBlock, _materialIndex);
        materialPropertyBlock.SetFloat("_transitionFactor", transitionFactor);
        materialPropertyBlock.SetVector("_Position", new Vector3(UVToPos(_position.x),fresnelImitation, UVToPos(_position.y)));
        meshRenderer.SetPropertyBlock(materialPropertyBlock, _materialIndex);
        if (startTime - Time.time > effectDuration) enabled = true;
    }
}
