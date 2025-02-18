using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Disslove : MonoBehaviour
{
    public Material shaderMaterial;
    public Material shadowMaterial;

    [Range(0, 1)] public float DissloveAmount = 0;

    private Material _material;

    public Image ShadowImage;
    private Material _shadowMaterial;

    public bool RandomEffect = true;

    string DefaultEffect = "_EDITION_REGULAR";
    string CurrentEffect = "REGULAR";
    private void Awake()
    {
        Image image = GetComponent<Image>();
        _material = new Material(shaderMaterial);

        
        image.material = _material;

        if (RandomEffect)
        {
            string[] editions = new string[5];
            editions[0] = "REGULAR";
            editions[1] = "POLYCHROME";
            editions[2] = "FOIL";
            editions[3] = "NEGATIVE";
            editions[4] = "RAINBOW";

            for (int i = 0; i < _material.enabledKeywords.Length; i++)
            {
                _material.DisableKeyword(_material.enabledKeywords[i]);
            }

            var keyname = "_EDITION_" + editions[Random.Range(0, editions.Length)];
            CurrentEffect = keyname;
            _material.EnableKeyword(keyname);
            _material.SetFloat("_Dissolve", DissloveAmount);
        }
        
        if (ShadowImage != null)
        {
            ShadowImage.material = Instantiate(shadowMaterial);
            _shadowMaterial = ShadowImage.material;
            _shadowMaterial.SetFloat("_Dissolve", DissloveAmount);
        }
        
    }

    public void SetEffect(bool v)
    {
        for (int i = 0; i < _material.enabledKeywords.Length; i++)
        {
            _material.DisableKeyword(_material.enabledKeywords[i]);
        }

        var keyname = v? CurrentEffect : DefaultEffect;
        _material.EnableKeyword(keyname);
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    public void Play(float start, float end, float duration,Action callback = null)
    {
        //Debug.Log($"Play Disslove");
        _material.SetFloat("_Dissolve", start);
        _material.DOFloat(end, "_Dissolve", duration).onComplete = () =>
        {
            callback?.Invoke();
        };
        if (ShadowImage != null)
        {
            _shadowMaterial.SetFloat("_Dissolve", start);

            _shadowMaterial.DOFloat(end, "_Dissolve", duration);
        }
        
        
    }
    void Update()
    {
        // _material.SetFloat("_Dissolve", DissloveAmount);
        // _shadowMaterial.SetFloat("_Dissolve", DissloveAmount);

        // 获取当前对象的父对象的局部旋转，以四元数的形式表示
        Quaternion currentRotation = transform.parent.localRotation;

        // 将四元数转换为欧拉角
        Vector3 eulerAngles = currentRotation.eulerAngles;

        // 获取欧拉角中的X轴和Y轴角度
        float xAngle = eulerAngles.x;
        float yAngle = eulerAngles.y;
        
        // 使用ClampAngle函数限制X轴角度在-90度到90度之间
        xAngle = ClampAngle(xAngle, -90f, 90f);
        // 使用ClampAngle函数限制Y轴角度在-90度到90度之间
        yAngle = ClampAngle(yAngle, -90f, 90f);
        
        _material.SetVector("_Rotation",
            new Vector2(ExtensionMethods.Remap(xAngle, -20, 20, -.5f, .5f),
                ExtensionMethods.Remap(yAngle, -20, 20, -.5f, .5f)));
    }
    
    // 定义ClampAngle函数，用于将角度限制在指定范围内
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -180f)
            angle += 360f;
        if (angle > 180f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
   
}

public static class ExtensionMethods
{

    /// <summary>
    /// 将value从from1到to1的范围映射到from2到to2的范围
    /// 归一化: 首先将 value 在原始范围 [from1, to1] 中的位置归一化到 [0, 1] 范围内。
    /// 这是通过计算 (value - from1) / (to1 - from1) 实现的。
    /// 
    /// 重新映射: 然后，将归一化后的值乘以目标范围 [from2, to2] 的长度 (to2 - from2)，
    /// 并加上目标范围的起始值 from2，从而将值映射到目标范围。
    /// </summary>
    /// <param name="value"></param>
    /// <param name="from1"></param>
    /// <param name="to1"></param>
    /// <param name="from2"></param>
    /// <param name="to2"></param>
    /// <returns></returns>
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}