using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas m_Canvas;

    [Header("Префабы наград")]
    [SerializeField] private GameObject m_QuantumPrefab;
    [SerializeField] private GameObject m_StarPrefab;
    [SerializeField] private GameObject m_KeyPrefab;
    [SerializeField] private GameObject m_TextPrefab;

    [Header("Позиция появления наград")]
    [SerializeField] private RectTransform m_QuantumStart;
    [SerializeField] private RectTransform m_StarStart;
    [SerializeField] private RectTransform m_KeyStart;
    [SerializeField] private RectTransform m_KeyStart_1;
    [SerializeField] private RectTransform m_KeyStart_2;

    [Header("Позиция назначения наград")]
    [SerializeField] private RectTransform m_QuantumCounter;
    [SerializeField] private RectTransform m_StarCounter;
    [SerializeField] private RectTransform m_KeyCounter;

    [SerializeField] private UITweenAnimator m_TweenAnimator;
    [Header("Колличество наград")]
    [SerializeField] private int m_Quantums;
    [SerializeField] private int m_Stars;
    [SerializeField] private int m_Keys;

    [Header("Текстовые поля")]
    [SerializeField] private TMP_Text m_StarTx;
    [SerializeField] private TMP_Text m_QuantumTx;

    private int currentQuantum = 1325;
    private int currentStar = 11;

    private void Awake()
    {
        m_TweenAnimator.InitUIManager(this);
    }

    private void Start()
    {
        m_QuantumTx.text = currentQuantum.ToString();
        m_StarTx.text = currentStar.ToString();
    }

    public void QuantumTxHandler(int quantumValue)
    {
        currentQuantum += quantumValue;
        m_QuantumTx.text = currentQuantum.ToString();
    }

    public void StarTxHandler()
    {
        currentStar++;
        m_StarTx.text = currentStar.ToString();
    }

    public void StartSpawnRewards()
    {
        StartCoroutine(SpawnRewardsCoroutine(m_Stars, m_Quantums, m_Keys));
    }

    IEnumerator SpawnRewardsCoroutine(int quentityStars, int quantityQuantum, int quantityKeys)
    {
        int starsQuentity = 0; // счетчик звезд
        int quantQuantity = 0; // счетчик квантов
        int keysQuantity = 0;  // счетчик ключей

        int starsOrder;    // заказ на колличество звезд-объектов
        int quantOrder;    // заказ на колличество квант-объектов
        int[] quantsValueToOrder = GetIntArray(quantityQuantum);
        int keysOrder;     // заказ на колличество ключ-объектов

        starsOrder = quentityStars;
        quantOrder = quantsValueToOrder.Length;
        keysOrder = quantityKeys;

        while (starsQuentity < starsOrder)
        {
            yield return new WaitForSeconds(0.2f);
            SpawnStar();
            starsQuentity++;
        }

        yield return new WaitForSeconds(1.5f);

        while (quantQuantity < quantOrder)
        {
            yield return new WaitForSeconds(0.15f);
            SpawnQuantum(quantsValueToOrder[quantQuantity]);
            quantQuantity++;
        }
        SpawnText(quantityQuantum, m_QuantumStart.position);

        yield return new WaitForSeconds(1f);

        while (keysQuantity < keysOrder)
        {
            yield return new WaitForSeconds(0.2f);
            SpawnKey();
            keysQuantity++;
        }
        SpawnText(quantityKeys, m_KeyStart.position);
        yield return null;
    }

    private void SpawnStar()
    {
        Vector3 startPos = m_StarStart.position + new Vector3(Random.Range(-100f, 100f), Random.Range(0f, 100f), 0f);
        var star = Instantiate(m_StarPrefab, startPos, Quaternion.identity);
        star.transform.SetParent(m_Canvas.transform, true);
        m_TweenAnimator.MoveStarAnimation(star, m_StarCounter);
    }

    private void SpawnText(int TextValue, Vector3 StartPos)
    {
        var text = Instantiate(m_TextPrefab, StartPos, Quaternion.identity);
        text.transform.SetParent(m_Canvas.transform, true);
        text.GetComponent<TextMeshProUGUI>().text = "+" + TextValue;
        m_TweenAnimator.MoveTextAnimation(text.gameObject);
    }

    private void SpawnQuantum(int num)
    {
        Vector3 startPos = m_QuantumStart.position + new Vector3(Random.Range(-100f, 100f), Random.Range(0f, 100f), 0f);
        var quant = Instantiate(m_QuantumPrefab, startPos, Quaternion.identity);
        quant.transform.SetParent(m_Canvas.transform, true);
        m_TweenAnimator.MoveQuantumAnimation(quant, m_QuantumCounter, num);
    }

    private void SpawnKey()
    {
        Vector3 startPos = m_KeyStart.position + new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), 0f);
        var key = Instantiate(m_KeyPrefab, startPos, Quaternion.identity);
        key.transform.SetParent(m_Canvas.transform, true);
        m_TweenAnimator.MoveKeyAnimation(key, startPos, m_KeyStart_1.position, m_KeyStart_2.position, m_KeyCounter);
    }

    int[] GetIntArray(int num)
    {
        int[] parts = new int[9];

        // Равномерно разделить число на 9 частей с целочисленным делением
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i] = num / 9;
        }

        // Проверить, является ли сумма частей равной исходному числу
        int sum = 0;
        foreach (int part in parts)
        {
            sum += part;
        }

        // Если сумма частей не равна исходному числу, скорректировать последнее значение parts[8]
        if (sum != num)
        {
            parts[8] += num - sum;
        }

        return parts;
    }
}
