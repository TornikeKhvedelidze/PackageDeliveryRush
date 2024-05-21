using UnityEngine;

[CreateAssetMenu(menuName = "CreateScriptable/FloatReference")]
public class FloatReference : ScriptableObject
{
    [SerializeField]
    private float value;

    public float Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public static implicit operator float(FloatReference reference)
    {
        return reference.Value;
    }

    public static implicit operator FloatReference(float value)
    {
        var reference = CreateInstance<FloatReference>();
        reference.Value = value;
        return reference;
    }
}