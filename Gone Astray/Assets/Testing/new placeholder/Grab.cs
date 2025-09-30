using UnityEngine;

public class grab : MonoBehaviour
{
    private bool hold;
    public KeyCode mousebutton;
    public bool holdingRope;
    
    void Update()
    {
        if (Input.GetKey(mousebutton))
        {
            hold = true;
        }
        else
        {
            hold = false;
            Destroy(GetComponent<FixedJoint2D>());
            holdingRope = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(hold == true)
        {
            Rigidbody2D Rb2d = col.transform.GetComponent<Rigidbody2D>();
            if (Rb2d != null)
            {

                FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                fj.connectedBody = Rb2d;
            }
            else
            {
                FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
            }
            if (col.transform.tag == "rope")
            {
                holdingRope = true;
            }
        }
    }
}
