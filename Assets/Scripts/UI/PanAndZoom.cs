using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PanAndZoom : MonoBehaviour
{
    [SerializeField] float panSpeed = 2f;
     GameObject _followObject;
    [SerializeField] bool _enabled = false;

    private CinemachineInputProvider inputProvider;
    private CinemachineVirtualCamera virtualCamera;
    private Transform cameraTransform;
    private void Awake()
    {
        inputProvider = GetComponent<CinemachineInputProvider>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _followObject = GameObject.Find("LookObject").gameObject;
        cameraTransform = virtualCamera.VirtualCameraGameObject.transform;

        virtualCamera.Follow = _followObject.transform;
    }
    private void Update()
    {
        float x = inputProvider.GetAxisValue(0);
        float y = inputProvider.GetAxisValue(1);
        float z = inputProvider.GetAxisValue(2);

        if (enabled)
        {
            if (x != 0 || y != 0) PanScreen(x, y);
        }
    }

    public Vector2 PanDirection(float x, float y)
    {
        Vector2 direction = Vector2.zero;
        if ( y >= Screen.height * .95f)
        {
            direction.y += 1;
        }
        else if (y <= Screen.height * 0.05f)
        {
            direction.y -= 1;
        }
        else if (x >= Screen.width * .95f)
        {
            direction.x += 1;
        }
        else if (x <= Screen.width * 0.05f)
        {
            direction.x -= 1;
        }
        return direction;
    }

    public void PanScreen(float x, float y)
    {
        Vector2 direction = PanDirection(x, y);
        Vector3 direction3D = new Vector3(direction.x, 0, direction.y);
        _followObject.transform.position = Vector3.Lerp(_followObject.transform.position,
    _followObject.transform.position + direction3D * panSpeed, Time.deltaTime);
        //cameraTransform.position = Vector3.Lerp(cameraTransform.position,
        //    cameraTransform.position + (Vector3)direction * panSpeed, Time.deltaTime);
    }
}
