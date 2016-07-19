using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using jsval = JSApi.jsval;

public partial class UnityEngineManual
{
    public static void ListA1_Count(JSVCall vc)
    {
        IList list = vc.csObj as IList;
        if (list != null)
        {
            JSApi.setInt32((int)JSApi.SetType.Rval, list.Count);
        }
        else
        {
            JSApi.setInt32((int)JSApi.SetType.Rval, 0);
        }
    }
};