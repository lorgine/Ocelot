﻿namespace Ocelot.Errors
{
    public enum OcelotErrorCode
    {
        UnauthenticatedError, 
        UnknownError,
        DownstreampathTemplateAlreadyUsedError,
        UnableToFindDownstreamRouteError,
        CannotAddDataError,
        CannotFindDataError,
        UnableToCompleteRequestError,
        UnableToCreateAuthenticationHandlerError,
        UnsupportedAuthenticationProviderError,
        CannotFindClaimError,
        ParsingConfigurationHeaderError,
        NoInstructionsError,
        InstructionNotForClaimsError,
        UnauthorizedError,
        ClaimValueNotAuthorisedError,
        UserDoesNotHaveClaimError,
        DownstreamPathTemplateContainsSchemeError,
        DownstreamPathNullOrEmptyError,
        DownstreamSchemeNullOrEmptyError,
        DownstreamHostNullOrEmptyError
    }
}
