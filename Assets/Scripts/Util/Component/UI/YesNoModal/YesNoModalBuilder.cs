using Fps.UI;

namespace Util.Component.UI
{
    public class YesNoModalBuilder
    {
        public YesNoModalBuilder Question(string value)
        {
            question = value;
            return this;
        }
        
        public YesNoModalBuilder NotClose()
        {
            return CloseOption(YesNoModal.CloseOption.NotClose);
        }
        
        public YesNoModalBuilder CloseOption(YesNoModal.CloseOption value)
        {
            closeOption = value;
            return this;
        }
        
        public YesNoModalBuilder OnYes(YesNoModal.OnYesDelegate onYes)
        {
            this.onYes = onYes;
            return this;
        }
        
        
        public YesNoModalBuilder OnNo(YesNoModal.OnNoDelegate onNo)
        {
            this.onNo = onNo;
            return this;
        }

        public void Show()
        {
            modal.Show(question, closeOption, onYes, onNo);
        }

        private string question;
        
        private YesNoModal.CloseOption closeOption;

        private YesNoModal.OnYesDelegate onYes;

        private YesNoModal.OnNoDelegate onNo;

        private YesNoModal modal;
        
        public YesNoModalBuilder(YesNoModal modal)
        {
            question = "Make a question!";
            closeOption = YesNoModal.CloseOption.CloseAfterAnswer;
            onYes = it => { };
            onNo = it => { };
            this.modal = modal;
        }
    }
}