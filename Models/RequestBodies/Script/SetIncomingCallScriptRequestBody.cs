namespace PhoneNotify.Models.RequestBodies.Script
{
    public class SetIncomingCallScriptRequestBody
    {
        public string PhoneNumber { get; set; }
        public string Script { get; set; }
    }
}
