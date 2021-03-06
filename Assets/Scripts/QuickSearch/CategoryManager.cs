﻿using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace h8s
{
    public class CategoryManager : MonoBehaviour
    {
        [SerializeField] private GameObject listingPrefab;

        [Header("References")]
        [SerializeField] private Transform contentContainer;
        [SerializeField] private TextMeshProUGUI btn1Name;
        [SerializeField] private TextMeshProUGUI btn2Name;

        private List<Listing> listings = new List<Listing>();

        public void AddListing(api.NodeTemplate template)
        {
            var obj = Instantiate(listingPrefab, contentContainer);
            var listing = obj.GetComponent<Listing>();
            listing.Initialize(template);
            listings.Add(listing);
        }

        public void SetName(string name)
        {
            btn1Name.text = name;
            btn2Name.text = name;
        }

        public int SetListingKeywordFilter(string keyword)
        {
            var valid_listings_num = 0;
            foreach(var listing in listings)
            {
                var factor = listing.GetKeyworkMatchFactor(keyword);
                if (0 == factor)
                {
                    listing.gameObject.SetActive(false);
                }
                else
                {
                    listing.gameObject.SetActive(true);
                    valid_listings_num++;
                }
            }
            return valid_listings_num;
        }
    }
}
