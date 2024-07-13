using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoryCategoryButton : MonoBehaviour
{
    private float extraPos;

    public bool pressed { private get; set; }

    private Vector3 startingPos;

    private UIButtonColor buttonState;

    void Start()
    {
        startingPos = transform.localPosition;
        buttonState = GetComponent<UIButtonColor>();
    }

    void Update()
    {
        transform.localPosition = new Vector3(startingPos.x - extraPos, startingPos.y, startingPos.z);

        if (pressed)
        {
            if (extraPos < 50f)
            {
                extraPos += Time.deltaTime * 240f;
            }
        }
        else if (buttonState.state == UIButtonColor.State.Hover)
        {
            if (extraPos < 10f)
            {
                extraPos += Time.deltaTime * 120f;
            }
        }
        else if (buttonState.state == UIButtonColor.State.Normal)
        {
            if (extraPos > 0f)
            {
                extraPos -= Time.deltaTime * 120f;
            }
        }
    }
}
