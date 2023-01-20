/*************************************************************************************
 *
 * 文 件 名:   PerObjectMaterialProperties.cs
 * 描    述: 
 * 
 * 创 建 者：  洪金敏 
 * 创建时间：  2023-01-20 20:28:04
*************************************************************************************/

using UnityEngine;
using Random = UnityEngine.Random;

[DisallowMultipleComponent]
public class PerObjectMaterialProperties : MonoBehaviour
{
    static int baseColorId = Shader.PropertyToID("_BaseColor");
    static int cutoffId = Shader.PropertyToID("_Cutoff");
    static MaterialPropertyBlock block;

    [SerializeField, Range(0f, 1f)]
    float cutoff = 0.5f;

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

        block.SetFloat(cutoffId, cutoff);
        block.SetColor(baseColorId, new Color(Random.value, Random.value, Random.value, 1));
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