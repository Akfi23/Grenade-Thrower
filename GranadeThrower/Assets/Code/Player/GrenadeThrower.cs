using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GrenadeThrower : GrenadPicker
{
    [SerializeField] private GameObject _cursor;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private LineRenderer _trajectoryLine;
    [SerializeField] private int _lineSegment;

    private Vector3 _directionVelocity;
    private new Camera camera;

    private bool isRedGrenadeChoosen = true;
    private bool isBlueGrenadeChoosen = false;

    public event UnityAction OnAimStay;
    public event UnityAction OnGrenadeThrowed;
    public static event UnityAction ThrownGrenade;


    private void Start()
    {
        _trajectoryLine.positionCount = _lineSegment;
        camera = Camera.main;
        StartCoroutine("FindTargetsWithDelay", .2f);
    }
    
    private void Update()
    {
        ChoseGrenadeType();
        _trajectoryLine.positionCount = 0;

        if (Input.GetMouseButton(0))
        {
            OnAimStay?.Invoke();
            CalculateDistance();
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            if (redGrenades.Count > 0 || blueGrenades.Count > 0)
            {
                if (isRedGrenadeChoosen == true)
                {
                    ThrowGranade(redGrenades);
                }
                else if (isBlueGrenadeChoosen == true) 
                {
                    ThrowGranade(blueGrenades);
                }
            }
            else 
            {
                Debug.LogError("NO Grenades at all");
            }
            OnGrenadeThrowed?.Invoke();
            _cursor.SetActive(false);
        }
    }

    private Vector3 CalculateVelocity(Vector3 target, Vector3 origin,float time) 
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        float posY = distance.y;
        float posXZ = distanceXZ.magnitude;

        float velocityXZ = posXZ / time;
        float velocityY = posY / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;

        result *= velocityXZ;
        result.y = velocityY;
        return result;
    }

    private void CalculateDistance() 
    {

        Ray cameraRay = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(cameraRay,out hit, 20f, _layer)) 
        {
            _cursor.SetActive(true);
            _cursor.transform.position = hit.point + Vector3.up * 0.1f;

            _directionVelocity = CalculateVelocity(hit.point, _thrower.position, 1f);

            DrawTrajectoryLine(_directionVelocity);

            transform.rotation = Quaternion.LookRotation(new Vector3(_directionVelocity.x,0,_directionVelocity.z));                                         
        }
    }

    private void ThrowGranade(List<GameObject> grenades) 
    {
        GameObject newGrenade;

        if (grenades.Count>0) 
        {
            TryGetEnemy(out newGrenade, grenades);

            newGrenade.transform.position = _thrower.position;
            newGrenade.SetActive(true);
            newGrenade.GetComponent<Rigidbody>().velocity = _directionVelocity;
            newGrenade.GetComponent<Grenade>().isThrowed = true;
            RemoveFromList(newGrenade,grenades);
            ThrownGrenade?.Invoke();
            
        }
        else 
        {
            Debug.LogError("NO Grenades");
        }
    }

    private void DrawTrajectoryLine(Vector3 vector) 
    {
        _trajectoryLine.positionCount = 10;
        for (int i = 0; i < _lineSegment; i++)
        {
            Vector3 position = CalculatePositionInTime(vector, i / (float)_lineSegment);
            _trajectoryLine.SetPosition(i, position);
        }
    }

    private Vector3 CalculatePositionInTime(Vector3 vector,float time) 
    {
        Vector3 XZ = vector;
        XZ.y = 0f;

        Vector3 result = _thrower.position+ vector * time;

        float yPos = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time))+(vector.y*time)+_thrower.position.y;

        result.y = yPos;
        return result;
    }

    private void ChoseGrenadeType() 
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            isRedGrenadeChoosen = true;
            isBlueGrenadeChoosen = false;
            Debug.Log("Red");
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isRedGrenadeChoosen = false;
            isBlueGrenadeChoosen = true;
            Debug.Log("Blue");
        }
    }
}
