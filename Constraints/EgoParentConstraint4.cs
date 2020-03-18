﻿using UnityEngine;
using System.Collections.Generic;

public class EgoParentConstraint< C1, C2, C3, C4, CS1 > : EgoParentConstraint
    where C1 : Component
    where C2 : Component
    where C3 : Component
    where C4 : Component
    where CS1 : EgoConstraint, new()
{
    public EgoParentConstraint()
    {
        childConstraint = new CS1();
        childConstraint.parentConstraint = this;

        _mask[ ComponentUtils.Get< C1 >() ] = true;
        _mask[ ComponentUtils.Get< C2 >() ] = true;
        _mask[ ComponentUtils.Get< C3 >() ] = true;
        _mask[ ComponentUtils.Get< C4 >() ] = true;
        _mask[ ComponentUtils.Get< EgoComponent >() ] = true;
    }

    protected override EgoBundle CreateBundle( EgoComponent egoComponent )
    {
        return new EgoBundle< C1, C2, C3, C4 >(
            egoComponent.GetComponent< C1 >(),
            egoComponent.GetComponent< C2 >(),
            egoComponent.GetComponent< C3 >(),
            egoComponent.GetComponent< C4 >()
        );
    }

    public override void CreateConstraintCallbacks( EgoInterface egoInterface )
    {
        egoInterface.AddAddedComponentCallback( typeof( C1 ), CreateBundles );
        egoInterface.AddDestroyedComponentCallback( typeof( C1 ), CreateBundles );

        egoInterface.AddAddedComponentCallback( typeof( C2 ), CreateBundles );
        egoInterface.AddDestroyedComponentCallback( typeof( C2 ), CreateBundles );

        egoInterface.AddAddedComponentCallback( typeof( C3 ), CreateBundles );
        egoInterface.AddDestroyedComponentCallback( typeof( C3 ), CreateBundles );

        egoInterface.AddAddedComponentCallback( typeof( C4 ), CreateBundles );
        egoInterface.AddDestroyedComponentCallback( typeof( C4 ), CreateBundles );

        egoInterface.AddSetParentCallback( SetParent );
    }

    public delegate void ForEachGameObjectWithChildrenDelegate(
        EgoComponent egoComponent,
        C1 component1,
        C2 component2,
        C3 component3,
        C4 component4,
        CS1 childConstraint
    );

    public void ForEachGameObject( ForEachGameObjectWithChildrenDelegate callback )
    {
        var lookup = GetLookup( rootBundles );
        foreach( var kvp in lookup )
        {
            currentEgoComponent = kvp.Key;
            var bundle = kvp.Value as EgoBundle< C1, C2, C3, C4 >;
            callback(
                currentEgoComponent,
                bundle.component1,
                bundle.component2,
                bundle.component3,
                bundle.component4,
                childConstraint as CS1
            );
        }
    }
}