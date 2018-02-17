using UnityEngine;

namespace Util.Component.UI.Fade
{
    public class FadeInEffect : FadeEffect
    {        
        public override bool doesFinish()
        {
            return alpha == 1;
        }

        protected FadeInEffect(Texture2D fadeOutTexture, float speed, int drawDepth) : base(fadeOutTexture, speed, drawDepth)
        {
            direction = 1;
            alpha = 0;
        }
    }
}