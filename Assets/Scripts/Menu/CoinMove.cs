using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    public float rotationSpeed = 100f; // ������ ȸ�� �ӵ�
    public float upForce = 5f; // ������ ���� Ƣ�� ��
    public float fadeinDuration = 1f; // ������� �� �ɸ��� �ð�
    public float fadeoutDuration = 0.5f;

    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private bool fadingOut = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        // �¿� ȸ���� ���� ���� �ʱ� ȸ�� ����
        rigidBody.angularVelocity = new Vector3(0f, rotationSpeed, 0f);

        // ���� Ƣ�� ���� ���ؼ� ������ �÷��ݴϴ�.
        rigidBody.AddForce(Vector3.up * upForce, ForceMode.Impulse);

        audioSource = GetComponent<AudioSource>();

        audioSource.Play();
        

        // ������ ����� �� �ֵ��� FadeIn�� �����մϴ�.
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        // ���� ������ ���� ������� ���̶�� �ƹ��͵� ���� �ʽ��ϴ�.
        if (fadingOut)
            return;

        // ���⼭ ���ϴ� ������ �������� �߰��� ������ �� �ֽ��ϴ�.
    }

    // ������ ��������� FadeIn �� FadeOut�� �����ϴ� Coroutine�Դϴ�.
    IEnumerator FadeIn()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        float timer = 0f;
        while (timer < fadeinDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeinDuration);
            yield return null;
        }

        // FadeIn�� ������ ��������� FadeOut�� �����մϴ�.
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        float timer = 0f;
        fadingOut = true;
        while (timer < fadeoutDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeoutDuration);
            yield return null;
        }

        // FadeOut�� ������ ������ �����մϴ�.
        Destroy(gameObject);
    }


}
