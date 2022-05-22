using UnityEngine;

public class TouchMoveController : MonoBehaviour
{
    #region Constants

    private float MIN_SIZE_FACTOR = 0.1f;

    #endregion

    #region Properties

    public static TouchMoveController Instance { get => _instance; set => _instance = value; }

    #endregion

    #region Unity Editor

    [SerializeField] private GameObject _moveObject;

    #endregion

    #region Private Fields

    private float _initialTouchDistance;
    private Vector3 _inittialScale;

    private static TouchMoveController _instance = null;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _inittialScale = _moveObject.transform.localScale;
        _initialTouchDistance = 0f;
    }

    private void Update()
    {

        CheckRotateAction();
        //CheckSizeAction();
    }

    private void CheckSizeAction()
    {
        if (Input.touchCount == 2)
        {
            Touch[] touch = Input.touches;

            if (Input.touches[0].phase == TouchPhase.Began && Input.touches[1].phase == TouchPhase.Began)
            {
                _initialTouchDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                _inittialScale = _moveObject.transform.localScale;
            }
            else if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                float scaleFactor = currentDistance / _initialTouchDistance;
                float x_factor = _inittialScale.x * scaleFactor;
                float y_factor = _inittialScale.y * scaleFactor;
                float z_factor = _inittialScale.z * scaleFactor;
                x_factor = (x_factor < MIN_SIZE_FACTOR) ? MIN_SIZE_FACTOR : x_factor;
                y_factor = (y_factor < MIN_SIZE_FACTOR) ? MIN_SIZE_FACTOR : y_factor;
                z_factor = (z_factor < MIN_SIZE_FACTOR) ? MIN_SIZE_FACTOR : z_factor;
                _moveObject.transform.localScale = new Vector3(x_factor, y_factor, z_factor);
            }
        }
    }

    private void CheckRotateAction()
    {
        if (Input.touchCount == 1)
        {
            Touch touch0 = Input.GetTouch(0);

            if (touch0.phase == TouchPhase.Moved)
            {
                _moveObject.transform.Rotate(0f, -touch0.deltaPosition.x * Time.deltaTime, 0f);
            }
        }
    }

    #endregion

    #region Public Methods

    public void Switch(bool key)
    {
        //Debug.LogError(key);
        //_isTransform = key;
        //gameObject.SetActive(key);
    }

    #endregion
}
