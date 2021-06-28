namespace PhoneNotify.Models.RequestBodies.ListMember
{
    public class AddListMemberRequestBody
    {
        public int ListID { get; set; }
        public string PhoneNumber { get; set; }
        public string ClientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
