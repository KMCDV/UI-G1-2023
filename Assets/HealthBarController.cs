using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Image _healthImage;
    [SerializeField, Range(0f,1f)] private float _currentHp;
    

    public void RemoveHp(float v)
    {
        _healthImage.fillAmount -= v;
    }

    public void SetHp(float v)
    {
        _healthImage.fillAmount = v;
    }

    private void OnValidate()
    {
        SetHp(_currentHp);
    }

    public float CalculatePercantage(float v) => v * 10;

}

[CustomEditor(typeof(HealthBarController))]
public class HealthBarCustomInspector : Editor
{
 
    float valueToRemoveHp = 0;
    private CalculationType currentCalculationType = CalculationType.Add;

    enum CalculationType
    {
        Add,
        Remove,
        Set
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        HealthBarController healthBarController = (HealthBarController)target;
        float newFloat = healthBarController.CalculatePercantage(0.05f);

        EditorGUILayout.BeginHorizontal();
        valueToRemoveHp = EditorGUILayout.FloatField("Hp to Remove", valueToRemoveHp);
        if (valueToRemoveHp > 1)
            valueToRemoveHp = 1;
        if (valueToRemoveHp < 0)
        {
            valueToRemoveHp = 0;
        }

        currentCalculationType = (CalculationType)EditorGUILayout.EnumPopup(currentCalculationType);
        
        if (GUILayout.Button($"{currentCalculationType.ToString()} Hp ({valueToRemoveHp})"))
        {
            switch (currentCalculationType)
            {
                case CalculationType.Add:
                    healthBarController.RemoveHp((float)-valueToRemoveHp);
                    break;
                case CalculationType.Remove:
                    healthBarController.RemoveHp((float)valueToRemoveHp);
                    break;
                case CalculationType.Set:
                    healthBarController.SetHp((float)valueToRemoveHp);
                    break;
            }
            EditorUtility.SetDirty(healthBarController);
        }
        EditorGUILayout.EndHorizontal();
        
        
    }
}