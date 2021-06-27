namespace PhoneNotify.Models.RequestBodies.ListMember
{
    public class AlterListMemberRequestBody
    {
        public int ListMemberID { get; set; }
        public string ClientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
