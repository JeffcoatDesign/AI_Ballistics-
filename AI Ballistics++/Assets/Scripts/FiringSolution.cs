using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FiringSolution
{
    public static Vector3? Calculate (Vector3 start, Vector3 end, float muzzleV, bool useMin)
    {
        Vector3 delta = end - start;

        var a = Physics.gravity.sqrMagnitude;
        var b = -4 * (Vector3.Dot(Physics.gravity, delta) + muzzleV * muzzleV);
        var c = 4 * delta.sqrMagnitude;

        var b2minus4ac = (b * b) - (4 * a * c);
        if (b2minus4ac < 0) return null;

        var time0 = Mathf.Sqrt((-b + Mathf.Sqrt(b2minus4ac)) / (2 * a));
        var time1 = Mathf.Sqrt((-b - Mathf.Sqrt(b2minus4ac)) / (2 * a));

        float timeToTarget;

        if (time0 < 0)
        {
            if (time1 < 0)
                return null;
            else
                timeToTarget = time1;
        }
        else if (time1 < 0)
            timeToTarget = time0;
        else
        {
            if (useMin)
                timeToTarget = Mathf.Min(time0, time1);
            else
                timeToTarget = Mathf.Max(time0, time1);
        }

        Vector3 delta2 = delta * 2;
        Vector3 gravTtt2 = Physics.gravity * (timeToTarget * timeToTarget);
        float divisor = 2 * muzzleV * timeToTarget;

        return (delta2 - gravTtt2) / divisor;
    }
}
