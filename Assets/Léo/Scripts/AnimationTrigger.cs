using System.Collections;
using Unity.XR.CoreUtils;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

[RequireComponent(typeof(SphereCollider))]
public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] private Material groundMaterial;
    [Range(0, 50)] private float transitionFactor;
    [Range(-1, 0)] private float fresnelImitation;
    private float startTime;
    [SerializeField] private float effectDuration;
    [SerializeField] private float deltaInc; 
    private float deltaFresnelInc;
    private Vector2 _position;
    private bool _hasStarted;
    private MeshRenderer meshRenderer;
    private MaterialPropertyBlock materialPropertyBlock;
    private bool enabled;
    private int _materialIndex;

    private void Awake() {
        materialPropertyBlock = new MaterialPropertyBlock();
        deltaFresnelInc = Time.deltaTime / 2;
    }
    
    private void Update() {
        if (_hasStarted) LaunchTransition();
    }

    private void OnCollisionEnter(Collision other) {
        if(!CollideByBottom(other)) return;
        ContactPoint contactPoint = other.contacts[0];
        Ray ray = new Ray(contactPoint.point - contactPoint.normal, contactPoint.normal);
        RaycastHit hit = new RaycastHit();
        if (!Physics.Raycast(ray, out hit)) return;
        Debug.Log(hit.textureCoord);
        _position = hit.textureCoord;
        meshRenderer = other.gameObject.GetComponent<MeshRenderer>();
        
        meshRenderer.AddMaterial(new Material(groundMaterial));
        _materialIndex = meshRenderer.materials.Length - 1;
        fresnelImitation = -1;
        _hasStarted = true;
    }
    
    private bool CollideByBottom(Collision other) => other.transform.position.y < transform.position.y;
    
    private void LaunchTransition() {
       startTime = Time.time;
        transitionFactor += deltaInc;
        transitionFactor = Mathf.Clamp(transitionFactor, 0, 50);
        fresnelImitation += deltaFresnelInc;
        fresnelImitation = Mathf.Clamp(fresnelImitation, -1, 0);
        meshRenderer.GetPropertyBlock(materialPropertyBlock, _materialIndex);
        materialPropertyBlock.SetFloat("_transitionFactor", transitionFactor);
        materialPropertyBlock.SetVector("_Position", new Vector3(_position.x,fresnelImitation , _position.y));
        meshRenderer.SetPropertyBlock(materialPropertyBlock, _materialIndex);
        if (startTime - Time.time > effectDuration) enabled = true;
    }
}
