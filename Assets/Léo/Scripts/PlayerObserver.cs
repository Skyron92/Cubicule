using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerObserver : MonoBehaviour
{
    [SerializeField] private InputActionReference userPresenceIAR;
    private InputAction UserPresenceIA => userPresenceIAR.action;

    private float _time;
    private bool _isRemoved;

    private void Awake() {
        UserPresenceIA.Enable();
        UserPresenceIA.canceled += context => _isRemoved = true;
        UserPresenceIA.started += context => {
            _isRemoved = false;
            _time = 0;
        };
    }

    private void Update() {
        if (_isRemoved) {
            _time += Time.deltaTime;
            if (_time >= 10f) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}