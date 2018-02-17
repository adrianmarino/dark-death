using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

namespace Fps.UI
{
    public class MatchListItem : MonoBehaviour
    {
        public delegate void OnJoinMatchDelegate(MatchInfoSnapshot match);
        
        public void Intialize(MatchInfoSnapshot match, OnJoinMatchDelegate onJoinMatch)
        {
            this.match = match;
            this.onJoinMatch = onJoinMatch;
            setupLabel(match);
        }

        public void OnJoinMatch()
        {
            onJoinMatch(match);
        }

        private void setupLabel(MatchInfoSnapshot match)
        {
            label.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")";
        }
        
        #region Attributes

        [SerializeField] private Text label;

        private OnJoinMatchDelegate onJoinMatch;

        private MatchInfoSnapshot match;

        #endregion
    }
}