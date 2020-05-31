using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CategoryManager : MonoBehaviour
{
    [SerializeField] private GameObject listingPrefab;

    [Header("References")]
    [SerializeField] private Transform contentContainer;
    [SerializeField] private TextMeshProUGUI btn1Name;
    [SerializeField] private TextMeshProUGUI btn2Name;

    private List<Listing> listings = new List<Listing>();

    public void AddListing(string name)
    {
        var obj = Instantiate(listingPrefab, contentContainer);
        var listing = obj.GetComponent<Listing>();
        listing.SetName(name);
        listings.Add(listing);
    }

    public void SetName(string name)
    {
        btn1Name.text = name;
        btn2Name.text = name;
    }
}
