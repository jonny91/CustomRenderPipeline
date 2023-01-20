using System;
using UnityEngine;

[DisallowMultipleComponent]
public class PerObjectMaterialProperties : MonoBehaviour
{
    static int baseColorId = Shader.PropertyToID("_BaseColor");
    static MaterialPropertyBlock block;

    [SerializeField]
    Color baseColor = Color.white;

    private void Awake()
    {
        OnValidate();
        // ClearBlock();
    }

    void OnValidate()
    {
        if (block == null)
        {
            block = new MaterialPropertyBlock();
        }

        block.SetColor(baseColorId, baseColor);
        GetComponent<Renderer>().SetPropertyBlock(block);
    }

    public void ClearBlock()
    {
        if (block != null)
        {
            block.Clear();
        }
    }
}