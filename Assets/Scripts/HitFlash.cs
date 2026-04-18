using System.Collections;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Material originalMaterial;
    public Material flashMaterial; // 在 Inspector 中指定一个白色/红色材质
    public float flashDuration = 0.2f;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material; // 保存原始材质
    }

    public void TakeDamage()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        // 切换到闪烁材质
        meshRenderer.material = flashMaterial;

        // 等待短暂时间
        yield return new WaitForSeconds(flashDuration);

        // 恢复原始材质
        meshRenderer.material = originalMaterial;
    }
}