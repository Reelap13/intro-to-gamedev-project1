using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceResources : MonoBehaviour
{
    public List<NameAmount> materials;
    [SerializeField] private List<GameObject> resources;
    private int currentX = -60;

    // Update is called once per frame
    public void PlaceResourcesFunc()
    {
        foreach(GameObject resource in resources)
        {
            resource.SetActive(false);
        }
        foreach(NameAmount item in materials)
        {
            foreach(GameObject res in resources)
            {
                if(item.name == res.name)
                {
                    AddResource(res);
                    res.GetComponentInChildren<Text>().text = item.amount.ToString();
                }
            }
        }
        currentX = -60;
    }

    void AddResource(GameObject resource)
    {
        resource.SetActive(true);
        RectTransform _transform = resource.GetComponent<RectTransform>();
        _transform.localPosition = new(currentX, _transform.localPosition.y, transform.localPosition.z);
        currentX += 15;
    }
}
