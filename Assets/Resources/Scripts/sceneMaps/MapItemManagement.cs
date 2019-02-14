﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MapItemManagement : MonoBehaviour
{
    public float shakingStartAngle;
    public string messageKey;
    private Sequence shakeObjectSequence;

    public void onLongTapDown()
    {
         UIManager.instance.showToolTip(this.transform.position,LocalizationManager.instance.GetLocalizedValue(messageKey));
    }

    public void onLongTapUp()
    {
        UIManager.instance.hideToolTip();
    }
    public void onTap()
    {
        Debug.Log("Simple tap detected on object: " + this.name);
    }

    public IEnumerator shakeObject()
    {
        GameObject childObjectToShake = this.transform.GetChild(0).gameObject;
        childObjectToShake.transform.Rotate(new Vector3(0, 0, shakingStartAngle));
        yield return null;
        shakeObjectSequence = DOTween.Sequence();
        shakeObjectSequence
                .Append(childObjectToShake.transform.DORotate(new Vector3(0, 0, shakingStartAngle - 60), 1, RotateMode.Fast))
                .SetLoops(-1, LoopType.Yoyo);
        yield return null;
    }

    public void stopShakingObject()
    {
        shakeObjectSequence.Kill();
        GameObject childObjectToShake = this.transform.GetChild(0).gameObject;
        childObjectToShake.transform.Rotate(0, 0, -childObjectToShake.transform.localEulerAngles.z);
    }
}
