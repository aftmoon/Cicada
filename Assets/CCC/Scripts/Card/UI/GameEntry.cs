using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameEntry : MonoBehaviour
{
    public Card Card;
    public float Delay = 1;
    public float FadeInTime = 1;
    public float MaskDelay;
    public Disslove TitleDisslove;

    public Image Mask;

    public Color Color1;
    public Color Color2;
    public Image BGImage;

    public GameObject StartBTN;
    // Start is called before the first frame update
    void Start()
    {
        var Mat = BGImage.material;
        BGImage.material = new Material(Mat);
        Card.cardVisual.DissloveEffect.Play(0, 0, 0);
        Card.cardVisual.transform.parent.localScale = Vector3.one *1.5f;
        TitleDisslove.Play(0, 0, 0);
        StartCoroutine(Play());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(MaskDelay);
        Mask.DOFade(1, 2).From(0);
        yield return new WaitForSeconds(Delay);
        Card.cardVisual.DissloveEffect.Play(0, 1, 2);
        
        yield return new WaitForSeconds(FadeInTime);
        BGImage.material.SetColor("_color1", Color1);
        BGImage.material.SetColor("_color2", Color2);
        Mask.DOFade(0, 2).From(1);
        yield return new WaitForSeconds(2);
        TitleDisslove.Play(0, 1, 2);
        yield return new WaitForSeconds(2);
        StartBTN.SetActive(true);
        StartBTN.transform.DOMoveY(StartBTN.transform.position.y, 1).From(StartBTN.transform.position.y - 100);

    }
}
