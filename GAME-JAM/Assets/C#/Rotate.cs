using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] bool _lookAtCursor;
    [SerializeField] bool _LookAtPlayer;
    [SerializeField] Transform _Target;


    void Start()
    {
        _mainCamera = Camera.main; 
        if(_lookAtCursor && _LookAtPlayer)
            _lookAtCursor = false; //I know this shit isn't good practice, I just don't care
    }

    void Update()
    {
        if (_lookAtCursor)
        {
            Vector3 mousePosition = Input.mousePosition;
            FaceTarget(mousePosition);
        }
        if (_LookAtPlayer) 
        {
            FaceTarget(Movement.player.transform.position);
        }
        if (!_LookAtPlayer && !_lookAtCursor)
        {
            FaceTarget(_Target.position);
        }
    }

    private void FaceTarget(Vector3 Target)
    {
        Vector3 mouseWorldPosition;
        Vector3 mousePosition = Target;
        Vector3 direction;
        if (_lookAtCursor)
        {
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            direction = mouseWorldPosition - transform.position;
        }
        else
            direction = Target - transform.position;
        transform.up = direction;
    }
}

