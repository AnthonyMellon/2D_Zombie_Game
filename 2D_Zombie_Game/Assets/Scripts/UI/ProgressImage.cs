using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressImage : Image
{
    public floatSO maxReferenceValue;
    public floatSO referenceValue;
    public Gradient colorGradient;
    public bool smoothChange;
    public float changeSpeed;

    private void Update()
    {        
        if(smoothChange)
            fillAmount = Mathf.Lerp(fillAmount, referenceValue.value / maxReferenceValue.value, Time.deltaTime * changeSpeed);
        else
            fillAmount = (referenceValue.value / maxReferenceValue.value);

        if (fillAmount > 0) color = colorGradient.Evaluate(fillAmount);

    }

    private void OnDrawGizmos()
    {
        type = Type.Filled;
    }
}
