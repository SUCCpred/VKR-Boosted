using UnityEngine;

public class LookAt : MonoBehaviour
{
    #region Unity Editor

    [SerializeField] private Transform _targetTransform;

    #endregion

    #region Unity Methods

    private void Update()
    {
        transform.LookAt(_targetTransform.position);
        transform.Rotate(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
    }

    #endregion
}
