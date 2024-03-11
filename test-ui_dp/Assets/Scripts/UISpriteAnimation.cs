using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    [SerializeField] private Image m_Image;
    [SerializeField] private Sprite[] m_SpritesArray;
    [SerializeField] private float m_AnimationSpeed = 0.1f;

    private int spriteIndex;
    private Coroutine animCoroutine;

    [SerializeField] private bool isDone;

    private void Start()
    {
        animCoroutine = StartCoroutine(UIAnimationCoroutine());
    }

    public void PlayUIAnimation()
    {
        isDone = false;
        if (animCoroutine != null)
        {
            animCoroutine = StartCoroutine(UIAnimationCoroutine());
        }
    }

    public void StopUIAnimation()
    {
        isDone = true;
        if (animCoroutine != null)
        {
            animCoroutine = null;
            StopCoroutine(UIAnimationCoroutine());
        }
    }

    IEnumerator UIAnimationCoroutine()
    {
        spriteIndex = 0;
        while (!isDone)
        {
            yield return new WaitForSeconds(m_AnimationSpeed);
            m_Image.sprite = m_SpritesArray[spriteIndex];
            spriteIndex++;
            if (spriteIndex > m_SpritesArray.Length - 1) spriteIndex = 0;
        }
        yield return null;
    }
}
