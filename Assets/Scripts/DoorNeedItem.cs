using UnityEngine;

public class DoorNeedItem : MonoBehaviour
{
    private AcrossSceneVars vars;

    private void Start()
    {
        vars = GameObject.FindGameObjectWithTag("AcrossVars").GetComponent<AcrossSceneVars>();
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            if(Input.GetKey(KeyCode.F))
            {
                
            }
        }
    }
}
