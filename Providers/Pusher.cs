namespace MBNotifications.Providers
{
    public class Pusher
    {
        PushOver _pushOver { get; set; }
        SMTP _smtp { get; set; }
        PushALot _pushALot { get; set; }


        public Pusher()
        {
            _pushOver = new PushOver();
            _smtp = new SMTP();
            _pushALot = new PushALot();
        }

        public async void Push(string message, int priority)
        {
            await _pushOver.Push(message);
            await _smtp.Push(message);
            await _pushALot.Push(message);
        }
    }
}
