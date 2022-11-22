using System.Collections.Generic;
using UnityEngine;

public class HeightObstacle : MonoBehaviour, IObstacle
{
    public void OnHit(Box box)
    {
        box.transform.SetParent(null);
    }
}
