using System;

namespace SavorChef.Domain.Exceptions;

public class AuthExceptions
{
    /// <summary>
    /// Errors related to authentication
    /// </summary>
    public static class Authentication
    {
        public static DomainException InvalidCredentials(Exception? e = null) =>
            new(
                "AUTH_INVALID_CREDENTIALS",
                "Invalid email or password",
                e);
    }

    /// <summary>
    /// Errors related to token operations
    /// </summary>
    public static class Token
    {
        public static DomainException Invalid(Exception? e = null) =>
            new(
                "AUTH_INVALID_TOKEN",
                "The provided token is invalid or has been revoked",
                e);

        public static DomainException Expired(Exception? e = null) =>
            new(
                "AUTH_TOKEN_EXPIRED",
                "The token has expired",
                e);
            
        public static DomainException GenerationFailed(Exception? e = null) =>
            new(
                "AUTH_TOKEN_GENERATION_FAILED",
                "Failed to generate authentication tokens",
                e);
    }

    /// <summary>
    /// Errors related to registration
    /// </summary>
    public static class Registration
    {
        public static DomainException Failed(Exception? e = null) =>
            new(
                "AUTH_REGISTRATION_FAILED",
                "User registration failed",
                e);
        
        public static DomainException EmailTaken(string email, Exception? e = null) =>
            new(
                "AUTH_EMAIL_TAKEN",
                $"The email '{email}' is already in use",
                e);
    }

    /// <summary>
    /// Errors related to authorization
    /// </summary>
    public static class Authorization
    {
        public static DomainException InvalidRefreshToken(Exception? e = null) =>
            new(
                "AUTH_INVALID_REFRESH_TOKEN",
                "The provided refresh token is invalid",
                e);
                
        public static DomainException Unauthorized(Exception? e = null) =>
            new(
                "AUTH_UNAUTHORIZED",
                "Authentication required",
                e);
    }

    /// <summary>
    /// Errors related to configuration
    /// </summary>
    public static class Configuration
    {
        public static DomainException MissingJwtSecret(Exception? e = null) =>
            new(
                "AUTH_CONFIG_MISSING_JWT_SECRET",
                "JWT secret is missing in configuration",
                e);
    }
}