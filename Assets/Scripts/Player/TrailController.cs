using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    TrailRenderer trail;
    Rigidbody2D rb;

    float trailAlpha = 1;
    bool fadingTrail;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();

        trail = this.transform.GetChild(0).GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        
    }
    void LateUpdate()
    {
        if (Mathf.Abs(rb.velocity.y) > 10 && !fadingTrail) StartCoroutine(FadeInTrailRenderer());
        if (Mathf.Abs(rb.velocity.y) < 5 && !fadingTrail) StartCoroutine(FadeOutTrailRenderer());
    }

    void SetTrailToInvisible()
    {
        Gradient trailRendererGradient = new Gradient();
        float alpha = 0;

        trailRendererGradient.SetKeys
        (
            trail.colorGradient.colorKeys,
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 1f) }
        );
        trail.colorGradient = trailRendererGradient;

        trailAlpha = alpha;
    }

    IEnumerator FadeInTrailRenderer()
    {
        fadingTrail = true;
        Gradient trailRendererGradient = new Gradient();
        float fadeSpeed = 1f;
        float timeElapsed = 0f;
        float alpha = 0f;

        //fade in trail
        while (timeElapsed < fadeSpeed)
        {
            alpha = Mathf.Lerp(trailAlpha, 1f, timeElapsed / fadeSpeed);

            trailRendererGradient.SetKeys
            (
                trail.colorGradient.colorKeys,
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 1f) }
            );
            trail.colorGradient = trailRendererGradient;

            timeElapsed += Time.deltaTime;

            //if velocity is less than 5 while fading, stop fading
            if (Mathf.Abs(rb.velocity.y) < 5) break;

            yield return null;
        }

        trailAlpha = alpha;
        fadingTrail = false;
    }
    IEnumerator FadeOutTrailRenderer()
    {
        fadingTrail = true;
        Gradient trailRendererGradient = new Gradient();
        float fadeSpeed = 3f;
        float timeElapsed = 0f;
        float alpha = 1f;

        //fade trail over time
        while (timeElapsed < fadeSpeed)
        {
            alpha = Mathf.Lerp(trailAlpha, 0f, timeElapsed / fadeSpeed);

            trailRendererGradient.SetKeys
            (
                trail.colorGradient.colorKeys,
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 1f) }
            );
            trail.colorGradient = trailRendererGradient;

            timeElapsed += Time.deltaTime;

            //if velocity is greater than 10 while fading, stop fading
            if (Mathf.Abs(rb.velocity.y) > 10) break;

            yield return null;
        }

        trailAlpha = alpha;

        fadingTrail = false;
    }
}
