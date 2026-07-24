public interface IChatService
{
    Task SendMessageAsync(string senderUserId, string receiverUserId, string message);
}