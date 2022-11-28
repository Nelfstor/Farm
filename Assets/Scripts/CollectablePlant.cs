using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plants/Collectables")]
public class CollectablePlant : Plant
{
    public void Collect()
    {
        Destroy(_attachedGameObject);
        Destroy(this); // Pooling can be attached 
    }

}