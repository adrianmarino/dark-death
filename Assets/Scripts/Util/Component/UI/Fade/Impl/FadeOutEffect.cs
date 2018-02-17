using UnityEngine;

namespace Util.Component.UI.Fade
{
    public class FadeOutEffect : FadeEffect
    {        
        public override bool doesFinish()
        {
            return alpha == 1;
        }

        public FadeOutEffect(Texture2D texture, float speed, int drawDepth) : base(texture, speed, drawDepth)
        {
            direction = 1;
            alpha = 0;
        }
    }
}