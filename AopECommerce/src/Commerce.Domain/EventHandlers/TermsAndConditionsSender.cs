using System;
using Ploeh.Samples.Commerce.Domain.Events;

namespace Ploeh.Samples.Commerce.Domain.EventHandlers
{
    public class TermsAndConditionsSender : IEventHandler<CustomerCreated>
    {
        private readonly IMessageService messageService;
        private readonly ITermsRepository repository;

        public TermsAndConditionsSender(
            IMessageService messageService, ITermsRepository repository)
        {
            if (messageService == null) throw new ArgumentNullException(nameof(messageService));
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            this.messageService = messageService;
            this.repository = repository;
        }

        public void Handle(CustomerCreated e)
        {
            string text = this.repository.GetActiveTerms();

            this.messageService.SendTermsAndConditions(e.MailAddress, text);
        }
    }
}