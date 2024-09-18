using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class DivisionByZeroChecker : MonoBehaviour
{
    void Start()
    {
        CheckAllScriptsForDivisionByZero();
    }

    void CheckAllScriptsForDivisionByZero()
    {
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();

        foreach (var script in allScripts)
        {
            Type scriptType = script.GetType();
            FieldInfo[] fields = scriptType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] properties = scriptType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            // Avoid checking UI components like Slider, Text, Image
            if (script is Slider || script is Text || script is Image)
            {
                continue;
            }

            try
            {
                foreach (FieldInfo field in fields)
                {
                    CheckFieldForDivisionByZero(field, script);
                }

                foreach (PropertyInfo property in properties)
                {
                    CheckPropertyForDivisionByZero(property, script);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error while checking {scriptType.Name}: {ex.Message}");
            }
        }
    }

    void CheckFieldForDivisionByZero(FieldInfo field, object script)
    {
        if (field.FieldType == typeof(float) || field.FieldType == typeof(int) || field.FieldType == typeof(double))
        {
            object fieldValue = field.GetValue(script);
            if (fieldValue != null && IsZero(fieldValue))
            {
                Debug.LogWarning($"Potential division by zero detected in script {script.GetType().Name} for field {field.Name}");
            }
        }
    }

    void CheckPropertyForDivisionByZero(PropertyInfo property, object script)
    {
        if (property.CanRead && (property.PropertyType == typeof(float) || property.PropertyType == typeof(int) || property.PropertyType == typeof(double)))
        {
            object propertyValue = property.GetValue(script);
            if (propertyValue != null && IsZero(propertyValue))
            {
                Debug.LogWarning($"Potential division by zero detected in script {script.GetType().Name} for property {property.Name}");
            }
        }
    }

    bool IsZero(object value)
    {
        if (value is float f && f == 0f) return true;
        if (value is int i && i == 0) return true;
        if (value is double d && d == 0.0) return true;
        return false;
    }
}
