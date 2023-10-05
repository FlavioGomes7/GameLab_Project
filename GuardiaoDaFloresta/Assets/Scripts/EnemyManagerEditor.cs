using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyManager))]
public class EnemyManagerEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyManager enemy = (EnemyManager)target;


        Color c = Color.green;
        if(enemy.alertStage == AlertStage.Curioso)
        {
            c = Color.Lerp(Color.green, Color.red, enemy.alertLevel / 300f);
        }
        else if(enemy.alertStage == AlertStage.Matar)
        {
            c = Color.red;
        }


        Handles.color = new Color(c.r, c.g, c.b, 0.3f);
        Handles.DrawSolidArc(enemy.transform.position, enemy.transform.up,
        Quaternion.AngleAxis(-enemy.fovAngle / 2f, enemy.transform.up) * enemy.transform.forward, enemy.fovAngle, enemy.fov);
        Handles.color = c;
        enemy.fov = Handles.ScaleValueHandle(enemy.fov, enemy.transform.position + enemy.transform.forward * enemy.fov, 
        enemy.transform.rotation, 3, Handles.SphereHandleCap, 1);
    }
}
