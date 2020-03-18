﻿using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( EgoComponent ) )]
public class OnCollisionEnterComponent : MonoBehaviour
{
    public List< Collision > collisionEnters = new List< Collision >();

    void OnCollisionEnter( Collision collision )
    {
        collisionEnters.Add( collision );
    }
}