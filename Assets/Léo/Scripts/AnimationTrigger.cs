using DG.Tweening;
using Unity.XR.CoreUtils;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] private Material groundMaterial;
    [Range(0, 50)] private float transitionFactor;
    [Range(0, 20)] private float areaSize;
    private float startTime;
    [SerializeField] private float effectDuration;
    [SerializeField] private float deltaInc; 
    [SerializeField] private float deltaSizeInc;
    private Vector2 _position;
    private bool _hasStarted;
    private MeshRenderer meshRenderer;
    private MaterialPropertyBlock materialPropertyBlock;
    private bool enabled;
    private int _materialIndex;
    
    [SerializeField] private EventManager eventManager;

    private void Awake() {
        materialPropertyBlock = new MaterialPropertyBlock();
    }
    
    private void Update() {
        if (_hasStarted) LaunchTransition();
    }

    private void OnCollisionEnter(Collision other) {
        if(!CollideByBottom(other) && !other.gameObject.CompareTag("Ground") ) return;
        Material material = GetComponent<MeshRenderer>().material;
        material.DOFade(0, 10).onComplete += () => Destroy(gameObject);
        Ray ray = new Ray(transform.position, Vector3.down);
        eventManager.StartAnimations();
        RaycastHit hit = new RaycastHit();
        if (!Physics.Raycast(ray, out hit)) return;
        _position = hit.textureCoord;
        meshRenderer = other.gameObject.GetComponent<MeshRenderer>();
        meshRenderer.AddMaterial(new Material(groundMaterial));
        _materialIndex = meshRenderer.materials.Length - 1;
        areaSize = 0;
        transitionFactor = 0;
        _hasStarted = true;
    }
    
    private bool CollideByBottom(Collision other) => other.transform.position.y < transform.position.y;

    private float UVToPos(float pUVCoordinates) => -5 + (1 - pUVCoordinates) * 10;
    
    private void LaunchTransition() {
        if(meshRenderer == null) return;
        startTime = Time.time;
        transitionFactor += deltaInc;
        transitionFactor = Mathf.Clamp(transitionFactor, 0, 70);
        areaSize += deltaSizeInc;
        areaSize = Mathf.Clamp(areaSize, 0, 5);
        meshRenderer.GetPropertyBlock(materialPropertyBlock, _materialIndex);
        materialPropertyBlock.SetFloat("_transitionFactor", transitionFactor);
        materialPropertyBlock.SetFloat("_AreaSize", areaSize);
        materialPropertyBlock.SetVector("_Position", new Vector3(UVToPos(_position.x),0, UVToPos(_position.y)));
        meshRenderer.SetPropertyBlock(materialPropertyBlock, _materialIndex);
        if (startTime - Time.time > effectDuration) enabled = true;
    }
}
