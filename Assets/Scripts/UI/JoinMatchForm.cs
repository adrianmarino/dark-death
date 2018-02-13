using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

namespace Fps.UI
{
    public class JoinMatchForm : MonoBehaviour
    {
        private void Start()
        {
            RefreshMatches();
        }

        public void OnRefresh()
        {
            RefreshMatches();
        }

        private void RefreshMatches()
        {
            message("Loading...");
            networkService.SearchMatch("", 20, OnMatchResults);
        }

        private void OnMatchResults(bool success, string extendedInfo, List<MatchInfoSnapshot> matchesInfo)
        {
            if (!success || matches == null) {
                message("Couldn't get matches");
                return;
            }
            if (matchesInfo.Count == 0) {
                message("Not found matches");
                return;
            }

            clearMatches();
            loadMatchesFrom(matchesInfo);
        }

        private void loadMatchesFrom(List<MatchInfoSnapshot> matchesInfo)
        {
            matchesInfo.ForEach(match => matches.Add(addMatchItem(match)));
        }

        private GameObject addMatchItem(MatchInfoSnapshot matchInfo)
        {
            GameObject matchItem = Instantiate(matchListItemPrefab);
            matchItem.transform.SetParent(matchListParent);
            var matchListItem = matchItem.GetComponent<MatchListItem>();
            
            matchListItem.Intialize(matchInfo, match =>
            {
                networkService.JoinMatch(match, (success, info, data) =>
                {
                    if (success)
                        message("Joining to " + matchInfo.name + " match...");
                    else
                        message("Couldn't join to " + matchInfo.name + " match");
                });
                
            });
            return matchItem;
        }

        private void clearMatches()
        {
            matches.ForEach(Destroy);
            matches.Clear();
        }

        private void message(string value)
        {
            statusIndicator.text = value;
        }

        #region Attributes
        
        [SerializeField] private Text statusIndicator;

        [SerializeField] private NetworkService networkService;

        [SerializeField] private GameObject matchListItemPrefab;

        [SerializeField] private Transform matchListParent;

        private List<GameObject> matches;

        #endregion

        public JoinMatchForm()
        {
            matches = new List<GameObject>();
        }
    }
}