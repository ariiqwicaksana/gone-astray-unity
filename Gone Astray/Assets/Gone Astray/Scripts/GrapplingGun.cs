using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
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

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;

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
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    private bool isGrappling = false;  // Keeps track of grappling state

    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
    }

    private void Update()
    {
        HandleGrappling(); // Grapple using F key
    }

    void HandleGrappling()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isGrappling)
            {
                SetGrapplePoint();
            }
            else
            {
                ReleaseGrapple();
            }
        }
    }

    void SetGrapplePoint()
    {
        // Use gun's current rotation to determine the grapple direction
        Vector2 direction = gunPivot.right;  // Right side of the gun is the fire direction
        RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, direction);

        if (_hit && (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll))
        {
            if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
            {
                grapplePoint = _hit.point;
                grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                grappleRope.enabled = true;
                isGrappling = true;

                Grapple();  // Start the grappling process
            }
        }
    }

    public void Grapple()
    {
        m_springJoint2D.autoConfigureDistance = false;

        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequncy;
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
        // Disable the rope and spring joint, but keep gravity scale at 0
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        isGrappling = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }
}
