using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class SwipeController : MonoBehaviour
{

    private GameObject selectedObject;

    private void OnEnable()
    {
        LeanTouch.OnFingerSwipe += OnFingerSwipe;
        LeanSelectable.OnSelectGlobal += OnSelectGlobal;
    }

    private void OnSelectGlobal(LeanSelectable selectable, LeanFinger finger)
    {
        Debug.Log("Selectable: " + selectable.gameObject);
        selectedObject = selectable.gameObject;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerSwipe -= OnFingerSwipe;
        LeanSelectable.OnSelectGlobal -= OnSelectGlobal;
    }
    private void OnFingerSwipe(LeanFinger finger)
    {
        if (ObjectRotationManager.Instance.moves == 0)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (selectedObject != null)
            {
                if (Physics.Raycast(ray, out hit, 200.0f))
                {
                    if (hit.transform != null)
                    {
                        if (hit.transform.gameObject != selectedObject)
                        {
                            if (hit.transform.CompareTag("props") || hit.transform.CompareTag("bread"))
                            {
                                if (Vector2.Distance(new Vector2(hit.transform.position.x, hit.transform.position.z), new Vector2(selectedObject.transform.position.x, selectedObject.transform.position.z)) <= (selectedObject.transform.localScale.x + 0.1) && Vector2.Distance(new Vector2(hit.transform.position.x, hit.transform.position.z), new Vector2(selectedObject.transform.position.x, selectedObject.transform.position.z)) >= (selectedObject.transform.localScale.x - 0.1))
                                {
                                    ObjectRotationManager.Instance.targetObject = hit.transform.gameObject;
                                    ObjectRotationManager.Instance.flip(selectedObject);
                                }

                            }
                        }
                    }
                }


            }
        }
    }
}
