using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [HideInInspector] public bool isGrabbing = false;
    [Header("Scripts Ref:")]
    public GrapplingRope grappleRope;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;
    public LineRenderer aimLineRenderer; // New reference for Aim line renderer

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistance = 20;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequency = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    public bool isGrappling = false;  // Keeps track of grappling state

    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        aimLineRenderer.enabled = true; // Aim line active by default
    }

    private void Update()
    {
        if (!isGrabbing)
        {
            HandleGrappling();
        }
    }

    void HandleGrappling()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isGrappling)
            {
                SetGrapplePoint();
                aimLineRenderer.enabled = false; // Hide Aim line when shooting
            }
            else
            {
                ReleaseGrapple();
            }
        }
    }

    void SetGrapplePoint()
    {
        Vector2 direction = gunPivot.right;
        RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, direction);

        if (_hit && (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll))
        {
            if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistance || !hasMaxDistance)
            {
                grapplePoint = _hit.point;
                grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                grappleRope.enabled = true;
                isGrappling = true;

                Grapple();
            }
        }
    }

    public void Grapple()
    {
        m_springJoint2D.autoConfigureDistance = false;

        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequency;
        }

        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 firePointDistanceVector = firePoint.position - gunHolder.position;

                    m_springJoint2D.distance = firePointDistanceVector.magnitude;
                    m_springJoint2D.frequency = launchSpeed;
                    m_springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    m_rigidbody.gravityScale = 0;
                    m_rigidbody.linearVelocity = Vector2.zero;
                    break;
            }
        }
    }

    void ReleaseGrapple()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        isGrappling = false;
        aimLineRenderer.enabled = true; // Re-enable Aim line after releasing
        Debug.Log("Grapple released.");
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistance);
        }
    }
}
