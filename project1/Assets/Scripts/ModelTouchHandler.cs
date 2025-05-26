using UnityEngine;

public class ModelTouchHandler : MonoBehaviour
{
    public string modelName;
    public System.Action<string> onTapped;

    void OnMouseDown()
    {
        if (onTapped != null)
            onTapped.Invoke(modelName);
    }
}
