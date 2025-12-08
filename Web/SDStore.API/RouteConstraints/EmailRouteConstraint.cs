namespace SDStore.API.RouteConstraints
{
    using Microsoft.AspNetCore.Routing.Constraints;
    
    public class EmailRouteConstraint() : RegexRouteConstraint(EmailRouteRegex)
    {
        // TODO: Maybe I should change the data annotations to use regex instead of this: [MaxLength(320)] and [EmailAddress]. Also extract constants for that...
        private const string EmailRouteRegex =
            @"^(?=.{1,320}$)[^@\s]+@[^@\s]+\.[^@\s]+$";
    }
}