using System;

namespace Commerce.UpdateCurrency.ApplicationServices
{
    public class HelpCommand : ICommand
    {
        private readonly string helpMessage;

        public HelpCommand(string helpMessage)
        {
            this.helpMessage = helpMessage;
        }

        public void Execute()
        {
            Console.WriteLine(this.helpMessage);
        }
    }
}