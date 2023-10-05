using MatchMate.Page.Service;
namespace MatchMate.Wasm.Service
{
    public class MessageService : IMessageService
    {
        public async Task SendMessageAsync(string messageText, List<string> Contacts)
        {
            //if (Sms.Default.IsComposeSupported)
            //{
            //    var message = new SmsMessage(messageText, Contacts);
            //    await Sms.Default.ComposeAsync(message);
            //}
        }
    }
}
