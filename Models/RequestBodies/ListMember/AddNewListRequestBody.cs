namespace PhoneNotify.Models.RequestBodies.ListMember
{
    public class AddNewListRequestBody
    {
        public string ListName { get; set; }
        public int ParentListID { get; set; }
    }
}
