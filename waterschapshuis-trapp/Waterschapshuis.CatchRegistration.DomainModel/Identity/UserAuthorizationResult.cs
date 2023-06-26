namespace Waterschapshuis.CatchRegistration.DomainModel.Identity
{
    public enum UserAuthorizationStatus
    {
        Success = 1,// user found and approved and active
        NotAuthenticated = 2,//not authenticated
        NotAuthorized = 3, // user exists in the db but has not been authorized (e.g. it was authenticated on AD)
        NotFound = 4 // user not found in the db

    }
}
