using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(DoubleClickAttribute))]
public class DoubleClickObjectInspector : Editor
{
}
#endif

public class DoubleClickAttribute : Button
{
    public float doubleClickTime = 0.3f;
    public UnityEvent OnDoubleClick;

    private float lastClickTime;

    private void DoubleClicked()
    {
        if (Time.time - lastClickTime < doubleClickTime)
        {
            OnDoubleClick.Invoke();
            return;
        }
        lastClickTime = Time.time;
    }

    protected override void OnEnable()
    {
        onClick.AddListener(DoubleClicked);
    }

    protected override void OnDisable()
    {
        onClick.RemoveListener(DoubleClicked);
    }
}