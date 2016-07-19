using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using jsval = JSApi.jsval;

public partial class UnityEngineManual
{
    public static bool Vector3_GetHashCode(JSVCall vc, int argc)
    {
        int hash = ((Vector3)vc.csObj).GetHashCode();
        JSApi.setInt32((int)JSApi.SetType.Rval, hash);
        return true;
    }

    public static bool Vector3_MoveTowards__Vector3__Vector3__Single(JSVCall vc, int argc)
    {
        Vector3 a0 = JSApi.getVector3S((int)JSApi.GetType.Arg);
        Vector3 a1 = JSApi.getVector3S((int)JSApi.GetType.Arg);
        float a2 = JSApi.getSingle((int)JSApi.GetType.Arg);
        Vector3 ret = Vector3.MoveTowards(a0, a1, a2);
        JSApi.setVector3S((int)JSApi.SetType.Rval, ret);
        return true;
    }

    public static bool Vector3_OrthoNormalize__Vector3__Vector3__Vector3(JSVCall vc, int argc)
    {
        int r_arg0 = JSApi.getArgIndex();
        Vector3 a0 = JSApi.getVector3S((int)JSApi.GetType.ArgRef);

        int r_arg1 = JSApi.getArgIndex();
        Vector3 a1 = JSApi.getVector3S((int)JSApi.GetType.ArgRef);

        int r_arg2 = JSApi.getArgIndex();
        Vector3 a2 = JSApi.getVector3S((int)JSApi.GetType.ArgRef);

        Vector3.OrthoNormalize(ref a0, ref a1, ref a2);

        JSApi.setArgIndex(r_arg0);
        JSApi.setVector3S((int)JSApi.SetType.ArgRef, a0);

        JSApi.setArgIndex(r_arg1);
        JSApi.setVector3S((int)JSApi.SetType.ArgRef, a1);

        JSApi.setArgIndex(r_arg2);
        JSApi.setVector3S((int)JSApi.SetType.ArgRef, a2);
        return true;
    }

    public static bool Vector3_OrthoNormalize__Vector3__Vector3(JSVCall vc, int argc)
    {
        int r_arg0 = JSApi.getArgIndex();
        Vector3 a0 = JSApi.getVector3S((int)JSApi.GetType.ArgRef);

        int r_arg1 = JSApi.getArgIndex();
        Vector3 a1 = JSApi.getVector3S((int)JSApi.GetType.ArgRef);

        Vector3.OrthoNormalize(ref a0, ref a1);

        JSApi.setArgIndex(r_arg0);
        JSApi.setVector3S((int)JSApi.SetType.ArgRef, a0);

        JSApi.setArgIndex(r_arg1);
        JSApi.setVector3S((int)JSApi.SetType.ArgRef, a1);
        return true;
    }

    public static bool Vector3_Project__Vector3__Vector3(JSVCall vc, int argc)
    {
        Vector3 a0 = JSApi.getVector3S((int)JSApi.GetType.Arg);
        Vector3 a1 = JSApi.getVector3S((int)JSApi.GetType.Arg);
        Vector3 ret = Vector3.Project(a0, a1);
        JSApi.setVector3S((int)JSApi.SetType.Rval, ret);
        return true;
    }

    public static bool Vector3_ProjectOnPlane__Vector3__Vector3(JSVCall vc, int argc)
    {
        Vector3 a0 = JSApi.getVector3S((int)JSApi.GetType.Arg);
        Vector3 a1 = JSApi.getVector3S((int)JSApi.GetType.Arg);
        Vector3 ret = Vector3.ProjectOnPlane(a0, a1);
        JSApi.setVector3S((int)JSApi.SetType.Rval, ret);
        return true;
    }

    public static bool Vector3_Reflect__Vector3__Vector3(JSVCall vc, int argc)
    {
        Vector3 a0 = JSApi.getVector3S((int)JSApi.GetType.Arg);
        Vector3 a1 = JSApi.getVector3S((int)JSApi.GetType.Arg);
        Vector3 ret = Vector3.Reflect(a0, a1);
        JSApi.setVector3S((int)(int)JSApi.SetType.Rval, ret);
        return true;
    }

    public static bool Vector3_RotateTowards__Vector3__Vector3__Single__Single(JSVCall vc, int argc)
    {
        Vector3 a0 = JSApi.getVector3S((int)JSApi.GetType.Arg);
        Vector3 a1 = JSApi.getVector3S((int)JSApi.GetType.Arg);
        float a2 = JSApi.getSingle((int)JSApi.GetType.Arg);
        float a3 = JSApi.getSingle((int)(int)JSApi.GetType.Arg);
        Vector3 ret = Vector3.RotateTowards(a0, a1, a2, a3);
        JSApi.setVector3S((int)JSApi.SetType.Rval, ret);
        return true;
    }

    public static bool Vector3_Slerp__Vector3__Vector3__Single(JSVCall vc, int argc)
    {
        Vector3 a0 = JSApi.getVector3S((int)JSApi.GetType.Arg);
        Vector3 a1 = JSApi.getVector3S((int)JSApi.GetType.Arg);
        float a2 = JSApi.getSingle((int)JSApi.GetType.Arg);
        Vector3 ret = Vector3.Slerp(a0, a1, a2);
        JSApi.setVector3S((int)JSApi.SetType.Rval, ret);
        return true;
    }


    public static bool Vector3_SmoothDamp__Vector3__Vector3__Vector3__Single__Single__Single(JSVCall vc, int argc)
    {
        Vector3 a0 = JSApi.getVector3S((int)JSApi.GetType.Arg);
        Vector3 a1 = JSApi.getVector3S((int)JSApi.GetType.Arg);

        int r_arg2 = JSApi.getArgIndex();
        Vector3 a2 = JSApi.getVector3S((int)JSApi.GetType.ArgRef);
        float a3 = JSApi.getSingle((int)JSApi.GetType.Arg);
        float a4 = JSApi.getSingle((int)JSApi.GetType.Arg);
        float a5 = JSApi.getSingle((int)JSApi.GetType.Arg);

        JSApi.setArgIndex(r_arg2);
        Vector3 ret = Vector3.SmoothDamp(a0, a1, ref a2, a3, a4, a5);
        JSApi.setVector3S((int)JSApi.SetType.ArgRef, a2);
        JSApi.setVector3S((int)JSApi.SetType.Rval, ret);
        return true;
    }

    public static bool Vector3_SmoothDamp__Vector3__Vector3__Vector3__Single__Single(JSVCall vc, int argc)
    {
        Vector3 a0 = JSApi.getVector3S((int)JSApi.GetType.Arg);
        Vector3 a1 = JSApi.getVector3S((int)JSApi.GetType.Arg);

        int r_arg2 = JSApi.getArgIndex();
        Vector3 a2 = JSApi.getVector3S((int)JSApi.GetType.ArgRef);
        float a3 = JSApi.getSingle((int)JSApi.GetType.Arg);
        float a4 = JSApi.getSingle((int)JSApi.GetType.Arg);
        Vector3 ret = Vector3.SmoothDamp(a0, a1, ref a2, a3, a4);

        JSApi.setArgIndex(r_arg2);
        JSApi.setVector3S((int)JSApi.SetType.ArgRef, a2);
        JSApi.setVector3S((int)JSApi.SetType.Rval, ret);
        return true;
    }

    public static bool Vector3_SmoothDamp__Vector3__Vector3__Vector3__Single(JSVCall vc, int argc)
    {
        Vector3 a0 = JSApi.getVector3S((int)JSApi.GetType.Arg);
        Vector3 a1 = JSApi.getVector3S((int)JSApi.GetType.Arg);

        int r_arg2 = JSApi.getArgIndex();
        Vector3 a2 = JSApi.getVector3S((int)JSApi.GetType.ArgRef);
        float a3 = JSApi.getSingle((int)JSApi.GetType.Arg);
        Vector3 ret = Vector3.SmoothDamp(a0, a1, ref a2, a3);

        JSApi.setArgIndex(r_arg2);
        JSApi.setVector3S((int)JSApi.SetType.ArgRef, a2);
        JSApi.setVector3S((int)JSApi.SetType.Rval, ret);
        return true;
    }
};