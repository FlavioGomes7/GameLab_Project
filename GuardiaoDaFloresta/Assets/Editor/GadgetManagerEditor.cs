using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GadgetManager))]
public class GadgetManagerEditor : Editor
{
    private void OnSceneGUI()
    {
        GadgetManager gadget = (GadgetManager)target;


        Color c = Color.green;
        if (gadget.gadgetAlertStage == GadgetAlertStage.Atirar)
        {
            c = Color.red;
        }
        else
        {
            c = Color.green;
        }


        Handles.color = new Color(c.r, c.g, c.b, 0.3f);
        Handles.DrawSolidArc(gadget.transform.position, gadget.transform.up,
        Quaternion.AngleAxis(-gadget.gadgetFovAngle / 2f, gadget.transform.up) * gadget.transform.forward, gadget.gadgetFovAngle, gadget.gadgetFov);
        Handles.color = c;
        gadget.gadgetFov = Handles.ScaleValueHandle(gadget.gadgetFov, gadget.transform.position + gadget.transform.forward * gadget.gadgetFov,
        gadget.transform.rotation, 3, Handles.SphereHandleCap, 1);


        
    }
}
