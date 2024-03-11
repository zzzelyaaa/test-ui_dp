using System.Collections;
using UnityEngine;

public class UITweenAnimator : MonoBehaviour
{
    //public static UITweenAnimator InstanceTween;
    private UIManager uiManager;

    public void InitUIManager(UIManager Manager) { uiManager = Manager; }

    public void MoveTextAnimation(GameObject go)
    {
        Vector3 randFalenPos = go.transform.position + new Vector3(0, 300f, 0f);
        LeanTween.move(go, randFalenPos, 2f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.scale(go, new Vector3(2.5f, 2.5f, 1f), 1f).setEase(LeanTweenType.easeInOutQuad).setDestroyOnComplete(go);
    }

    public void MoveStarAnimation(GameObject go, RectTransform endPosition)
    {
        Vector3 randFalenPos = go.transform.position + new Vector3(Random.Range(-50f, 50f), -100f, 0f);
        LeanTween.move(go, randFalenPos, 1f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.rotateAround(go, Vector3.forward, -90, 0.3f).setDelay(0.2f);
        LeanTween.scale(go, new Vector3(1.5f, 1.5f, 1f), 0.5f).setEase(LeanTweenType.easeInCirc);
        LeanTween.rotateAround(go, Vector3.forward, -360, 1f).setLoopClamp().setDelay(0.5f);
        LeanTween.move(go, endPosition, 0.5f).setDelay(1f).setEase(LeanTweenType.easeInCubic).setDestroyOnComplete(go);
        LeanTween.scale(endPosition.parent.gameObject, new Vector3(0.5f, 0.5f, 1f), 0.1f).setDelay(1.5f).setOnComplete(StarValueSend).setEase(LeanTweenType.easeInOutBounce);
        LeanTween.scale(endPosition.parent.gameObject, new Vector3(1f, 1f, 1f), 0.2f).setDelay(1.6f).setEase(LeanTweenType.easeInOutElastic);
    }

    public void MoveQuantumAnimation(GameObject go, RectTransform endPosition, int quantumValue)
    {

        Vector3 randFalenPos = go.transform.position + new Vector3(Random.Range(-100f, 100f), -100f, 0f);
        LeanTween.move(go, randFalenPos, 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(go, endPosition, 0.5f).setDelay(0.5f).setEase(LeanTweenType.easeInCubic).setDestroyOnComplete(go);
        LTDescr tween = LeanTween.scale(endPosition.parent.gameObject, new Vector3(0.5f, 0.5f, 1f), 0.1f).setDelay(1f).setEase(LeanTweenType.easeInOutBounce);
        LeanTween.scale(endPosition.parent.gameObject, new Vector3(1f, 1f, 1f), 0.1f).setDelay(1.1f).setEase(LeanTweenType.easeInOutElastic);
        StartCoroutine(QuantumValueSendCoroutine(quantumValue, tween));
    }

    IEnumerator QuantumValueSendCoroutine(int num, LTDescr tween)
    {
        yield return new WaitForSeconds(1f);
        uiManager.QuantumTxHandler(num);
        yield return null;
    }


    public void MoveKeyAnimation(GameObject go, Vector3 startPosition, Vector3 startPosition_1, Vector3 startPosition_2, RectTransform endPosition)
    {
        LeanTween.scale(go, new Vector3(1.2f, 1.2f, 1f), 0.5f).setEase(LeanTweenType.easeInOutQuad);

        LTBezierPath path = new LTBezierPath(new Vector3[] { startPosition, startPosition_1, startPosition_2, endPosition.position });
        LeanTween.move(go, path, 0.5f).setDelay(0.5f).setEase(LeanTweenType.easeInOutSine).setDestroyOnComplete(go);

        LeanTween.scale(go, new Vector3(0.5f, 0.5f, 1f), 0.5f).setDelay(0.5f).setEase(LeanTweenType.easeInCirc);

        LeanTween.scale(endPosition.gameObject, new Vector3(0.5f, 0.5f, 1f), 0.1f).setDelay(1f).setEase(LeanTweenType.easeInOutBounce);
        LeanTween.scale(endPosition.gameObject, new Vector3(1f, 1f, 1f), 0.1f).setDelay(1.1f).setEase(LeanTweenType.easeInOutElastic);

    }

    private void StarValueSend()
    {
        uiManager.StarTxHandler();
    }
}
