using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFragment : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector2(10,10));
    }


}
