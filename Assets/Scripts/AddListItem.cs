using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AddListItem : MonoBehaviour {

    public GameObject listItemPrefab;
    public List<string> instancedItems;
    bool canInstanciate;

    NetworkManager nm;

    void Start()
    {
        nm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<NetworkManager>();
    }

    public void InstantiateListItem(string itemName)
    {
        canInstanciate = true;
        if (instancedItems.Count > 0)
        {
            foreach (string instance in instancedItems)
            {
                if(instance == itemName)
                {
                    canInstanciate = false;
                }
            }
        }
        else
        {
            instancedItems.Add(itemName);
            GameObject listItemInstance = Instantiate(listItemPrefab);
            listItemInstance.transform.SetParent(transform);
            listItemInstance.GetComponentInChildren<Text>().text = itemName;
            listItemInstance.GetComponent<Button>().onClick.AddListener(() => { SelectRoom(itemName); });
            canInstanciate = false;
        }
        if (canInstanciate)
        {
            instancedItems.Add(itemName);
            GameObject listItemInstance = Instantiate(listItemPrefab);
            listItemInstance.transform.SetParent(transform);
            listItemInstance.GetComponentInChildren<Text>().text = itemName;
            listItemInstance.GetComponent<Button>().onClick.AddListener(() => { SelectRoom(itemName); });
            canInstanciate = false;
        }
    }

    public void SelectRoom(string roomName)
    {
        nm.SelectRoom(roomName);
    }
}