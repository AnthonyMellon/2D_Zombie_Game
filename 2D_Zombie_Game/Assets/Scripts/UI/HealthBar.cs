using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class HealthBar : Slider
{
    [SerializeField]
    private Entity_SO player;
    [SerializeField]
    public Gradient healthColours;
    [SerializeField]
    private float lerpSpeed;

    private void FixedUpdate()
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        if (player)
        {
            float val = Mathf.Lerp(m_Value, player.currentHealth / player.maxHealth, Time.deltaTime * lerpSpeed);
            fillRect.transform.GetComponent<Image>().color = healthColours.Evaluate(1);
            Set(val, true);
        }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            UpdateBar();
            UnityEditor.EditorApplication.QueuePlayerLoopUpdate();            
            UnityEditor.SceneView.RepaintAll();
        }
#endif
    }


}
