using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    public GameObject playerPallete;
    private float fVelocity = -0.3f;
    public BallPhysics.RayColCheck left;
    public BallPhysics.RayColCheck right;
    public BallPhysics.RayColCheck up;
    public BallPhysics.RayColCheck down;
    public BallPhysics.RayColCheck forw;
    public BallPhysics.RayColCheck bac;
    private float fBallRadius;
    private Vector3 vForce = Vector3.zero;
    private Vector3 vSpeed = Vector3.zero;
    private float fGravityMove;
    private float fAcceleration;
    private float fCollisionCoef = 1f;
    private bool bBallStayOnTable;
    private Vector3 vGravityForce = Vector3.zero;
    public Vector3 vVelocity = Vector3.zero;
    private Vector3 vLastPos = Vector3.zero;
    private bool bIsCollidingZ;
    private bool bIsCollidingY;
    private Vector3 vVelocityFromPos = Vector3.zero;
    private Vector3 vPaleteVelocity = Vector3.zero;
    private Vector3 vPaleteLastPos = Vector3.zero;

    private void Start()
    {
        this.vVelocity = Vector3.forward * 0.06f - Vector3.up * 0.02f;
        this.fBallRadius = this.transform.localScale.x;
        this.left = new BallPhysics.RayColCheck(this.gameObject, Vector3.left, this.fBallRadius);
        this.right = new BallPhysics.RayColCheck(this.gameObject, Vector3.right, this.fBallRadius);
        this.up = new BallPhysics.RayColCheck(this.gameObject, Vector3.up, this.fBallRadius);
        this.down = new BallPhysics.RayColCheck(this.gameObject, Vector3.down, this.fBallRadius);
        this.forw = new BallPhysics.RayColCheck(this.gameObject, Vector3.forward, this.fBallRadius);
        this.bac = new BallPhysics.RayColCheck(this.gameObject, Vector3.back, this.fBallRadius);
    }

    private void FixedUpdate()
    {
        if (this.bBallStayOnTable)
            this.vVelocity.y = 0.0f;
        this.transform.localPosition += this.vVelocity;
        this.vVelocityFromPos = this.transform.localPosition - this.vLastPos;
        this.vVelocityFromPos = this.vVelocityFromPos.normalized;
        this.vLastPos = this.transform.localPosition;
        this.fGravityMove = 9.81f * Time.fixedDeltaTime * Time.fixedDeltaTime;
        this.vGravityForce = Vector3.down * this.fGravityMove;
        this.vVelocity += this.vGravityForce;
        BallPhysics.RayColCheck rayColCheck1 = this.CollisionDetectDown(this.transform.localPosition + this.vVelocity);
        BallPhysics.RayColCheck rayColCheck2 = this.CollisionDetectForwardBack(this.transform.localPosition + this.vVelocity);
        BallPhysics.RayColCheck rayColCheck3 = (BallPhysics.RayColCheck)null;
        if (rayColCheck1 != null)
            rayColCheck3 = rayColCheck1;
        else if (rayColCheck2 != null)
            rayColCheck3 = rayColCheck2;
        this.FixedUpdatePalete();
        if (rayColCheck2 != null && !this.bIsCollidingZ)
        {
            this.bIsCollidingZ = true;
            //Debug.LogError((object)"Colliding!");
            if (!(rayColCheck2.hit.collider.gameObject.tag == "Palete"))
                this.vVelocity = Vector3.Reflect(this.vVelocityFromPos * this.vVelocity.magnitude, rayColCheck2.hit.normal) * this.fCollisionCoef;
        }
        else
            this.bIsCollidingZ = false;
        if (rayColCheck1 != null && !this.bIsCollidingY)
        {
            this.bIsCollidingY = true;
            this.vVelocity.y = -this.vVelocity.y * this.fCollisionCoef;
            if ((double)Mathf.Abs(this.vVelocity.y) >= 0.00499999988824129)
                return;
            this.bBallStayOnTable = true;
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, rayColCheck1.hit.point.y + 0.02f, this.transform.localPosition.z);
        }
        else
            this.bIsCollidingY = false;
    }

    private void FixedUpdatePalete()
    {
        this.vPaleteVelocity = (this.playerPallete.transform.position - this.vPaleteLastPos) / Time.fixedDeltaTime;
        this.vPaleteLastPos = this.playerPallete.transform.position;
        Plane plane = new Plane(this.playerPallete.transform.position, this.playerPallete.transform.position + this.playerPallete.transform.up, this.playerPallete.transform.position + this.playerPallete.transform.right);
        float num = -plane.GetDistanceToPoint(this.transform.position);
        if ((double)num < 0.0)
            this.vVelocity = Vector3.Reflect(this.vVelocityFromPos * this.vVelocity.magnitude + this.vPaleteVelocity * Time.fixedDeltaTime, plane.normal) * this.fCollisionCoef;
        //Debug.LogError((object)(num.ToString() + " VELOCITY: " + (object)this.vPaleteVelocity));
    }

    private BallPhysics.RayColCheck CollisionDetectDown(Vector3 vNextPos)
    {
        vNextPos.x = this.transform.position.x;
        vNextPos.z = this.transform.position.z;
        return (Object)this.down.RayCheck(vNextPos) != (Object)null ? this.down : (BallPhysics.RayColCheck)null;
    }

    private BallPhysics.RayColCheck CollisionDetectForwardBack(Vector3 vNextPos)
    {
        if ((Object)this.forw.RayCheck(vNextPos) != (Object)null)
            return this.forw;
        return (Object)this.bac.RayCheck(vNextPos) != (Object)null ? this.bac : (BallPhysics.RayColCheck)null;
    }

    public class RayColCheck
    {
        public GameObject originObj;
        public Vector3 direction;
        public float fDistance;
        public UnityEngine.RaycastHit hit;
        private float fTimeFromLastCollision;
        public UnityEngine.RaycastHit closestRaycastHit;

        public RayColCheck(GameObject _originObj, Vector3 vDir, float _fDistance)
        {
            this.fTimeFromLastCollision = 0.0f;
            this.originObj = _originObj;
            this.direction = vDir;
            this.fDistance = _fDistance;
        }

        public GameObject RayCheck(Vector3 vNextPos)
        {
            this.fTimeFromLastCollision += Time.fixedDeltaTime;
            float num = Vector3.Distance(this.originObj.transform.position, vNextPos);
            Physics.Linecast(this.originObj.transform.position, this.originObj.transform.position + this.direction * num, out this.hit, ~(1 << LayerMask.NameToLayer("Ball")));
            Debug.DrawRay(this.originObj.transform.position, this.direction * num, Color.red);
            return (Object)this.hit.collider != (Object)null ? this.hit.collider.gameObject : (GameObject)null;
        }
    }
}