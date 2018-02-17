using UnityEngine;

namespace Util.Component.UI.Fade
{
    public class FadeInEffect : FadeEffect
    {        
        public override bool doesFinish()
        {
            return alpha == 0;
        }

        public FadeInEffect(Texture2D texture, float speed, int drawDepth) : base(texture, speed, drawDepth)
        {
            direction = -1;
            alpha = 1;
        }
    }
}