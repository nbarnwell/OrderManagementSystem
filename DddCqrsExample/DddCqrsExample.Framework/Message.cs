namespace DddCqrsExample.Framework
{
    public abstract class Message
    {
        public override string ToString()
        {
            return GetMessageText();
        }

        protected abstract string GetMessageText();
    }
}