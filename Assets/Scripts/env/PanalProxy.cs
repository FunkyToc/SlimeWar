using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanalProxy : MonoBehaviour
{
    private PannalInteraction ps;

    private void Start()
    {
        ps = GetComponentInParent<PannalInteraction>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerTag>() != null)
        {
            ps.PlayerIn();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<PlayerTag>() != null)
        {
            ps.PlayerOut();
        }
    }
}
