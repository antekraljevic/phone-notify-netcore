namespace PhoneNotify.Models.RequestBodies.ListMember
{
    public class AlterListIDRequestBody
    {
        public int ListID { get; set; }
        public int ParentListID { get; set; }
        public string ListName { get; set; }
    }
}
