using UnityEngine;

public class CameraSmothFollow : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _smoothFactor = 1.4f;
    void FixedUpdate()
    {
        Vector3 pos = Vector3.Lerp(transform.position,_target.position, Time.deltaTime * _smoothFactor);
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
