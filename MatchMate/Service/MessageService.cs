using MatchMate.Page.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMate.Service
{
    public class MessageService : IMessageService
    {
        public async Task SendMessageAsync(string messageText, List<string> Contacts)
        {
            if (Sms.Default.IsComposeSupported)
            {
                var message = new SmsMessage(messageText, Contacts);

                await Sms.Default.ComposeAsync(message);
            }
        }
    }
}
