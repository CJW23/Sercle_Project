using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClicker : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if (hit.transform != null)
                {
                    GameObject target = hit.transform.gameObject;

                    if (!target.GetComponent<Unit>())
                    {
                        Debug.Log("타겟이 아닌 다른게 클릭 됐는데...?");
                        return;
                    }

                    UIManager.instance.NowUnit = target.GetComponent<Unit>();
                }
            }
            else
            {
                Debug.Log("암것도 클릭이 안됨!");
                //UIManager.instance.NowUnit = null;
            }
        }
    }
}
