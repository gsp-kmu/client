using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    public float rotationSpeed = 100f; // 동전의 회전 속도
    public float upForce = 5f; // 동전이 위로 튀는 힘
    public float fadeinDuration = 1f; // 사라지는 데 걸리는 시간
    public float fadeoutDuration = 0.5f;

    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private bool fadingOut = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        // 좌우 회전을 위해 동전 초기 회전 설정
        rigidBody.angularVelocity = new Vector3(0f, rotationSpeed, 0f);

        // 위로 튀는 힘을 가해서 동전을 올려줍니다.
        rigidBody.AddForce(Vector3.up * upForce, ForceMode.Impulse);

        audioSource = GetComponent<AudioSource>();

        audioSource.Play();
        

        // 동전이 사라질 수 있도록 FadeIn을 시작합니다.
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        // 만약 동전이 아직 사라지는 중이라면 아무것도 하지 않습니다.
        if (fadingOut)
            return;

        // 여기서 원하는 동전의 움직임을 추가로 제어할 수 있습니다.
    }

    // 동전이 사라지도록 FadeIn 및 FadeOut을 제어하는 Coroutine입니다.
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

        // FadeIn이 끝나면 사라지도록 FadeOut을 시작합니다.
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

        // FadeOut이 끝나면 동전을 제거합니다.
        Destroy(gameObject);
    }


}
