using UnityEngine;
using Cinemachine;
public class CinemachinePOVExt : CinemachineExtension
{
    private InputManager _inputManager;
    private Vector3 _startingRotation;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float clampAngle;
    
    protected override void Awake()
    {
        _inputManager = InputManager.Instance;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if(_startingRotation == null)_startingRotation = transform.localRotation.eulerAngles;
                Vector2 deltaInput = _inputManager.getMouseDelta;
                _startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                _startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                _startingRotation.y = Mathf.Clamp(_startingRotation.y, -clampAngle/2, clampAngle);
                state.RawOrientation = Quaternion.Euler(-_startingRotation.y, _startingRotation.x, 0f);
            }
        }
    }
}
