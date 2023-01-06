namespace InstagramSystem.Commons
{

    enum EUserRole
    {
        Admin = 1,
        User = 2,
    }

    enum EUserFollowerStatus
    {
        Pending = 1,
        Approve = 2,
        Reject = 3
    }

    enum EPostType
    {
        None = 0,
        Sell =1,
    }
    enum EUserPrivacy
    {
        Public = 0,
        Private = 1
    }
}
