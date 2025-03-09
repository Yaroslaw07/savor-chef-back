namespace SavorChef.Domain.Exceptions;

public class UserExceptions
{
        /// <summary>
        /// Errors related to user not being found
        /// </summary>
        public static class NotFound
        {
            public static DomainException ById(int id, Exception? e = null) => 
                new (
                    "USER_NOT_FOUND", 
                    $"User with ID {id} was not found.",
                    e);

            public static DomainException ByEmail(string email, Exception? e = null) => 
                new(
                "USER_NOT_FOUND_BY_EMAIL",
                $"No user found with email {email}",
                e);
        }

        /// <summary>
        /// Errors related to user creation
        /// </summary>
        public static class Create
        {
            public static DomainException EmailAlreadyExists(string email, Exception? e = null) => 
                new(
                "USER_EMAIL_DUPLICATE",
                $"Email {email} is already in use",
                e
            );

            public static DomainException InvalidEmail(string email, Exception? e = null) => 
                new(
                "USER_INVALID_EMAIL",
                $"Email {email} is not in a valid format",
                e);
            
            public static DomainException Failed(Exception? e = null) =>
                new(
                "USER_CREATION_FAILED",
                "Failed to create user",
                e
            );
        }

        /// <summary>
        /// Errors related to user update operations
        /// </summary>
        public static class Update
        {
            public static DomainException Failed(int userId, Exception? e = null) => 
                new(
                "USER_UPDATE_FAILED",
                $"Failed to update user {userId}",
                e
            );

            public static DomainException EmailUpdateFailed(int userId, string email, Exception? e = null) => 
                new(
                "USER_EMAIL_UPDATE_FAILED",
                $"Cannot update email for user {userId}",
                e
            );
        }

        /// <summary>
        /// Errors related to user authentication
        /// </summary>
        public static class Authentication
        {
            public static DomainException InvalidPassword(Exception? e = null) => 
                new(
                "USER_INVALID_PASSWORD",
                "The provided password is incorrect",
                e
            );
        }

        /// <summary>
        /// Errors related to user validation
        /// </summary>
        public static class Validation
        {
            public static DomainException PasswordTooWeak(Exception? e = null) => 
                new(
                "USER_WEAK_PASSWORD",
                "The provided password does not meet security requirements",
                e
            );
        }
        
        /// <summary>
        /// Errors related to user deletion
        /// </summary>
        public static class Deletion
        {
            public static DomainException Failed(int userId, Exception? e = null) => 
                new(
                "USER_DELETION_FAILED",
                $"Failed to delete user {userId}",
                e
            );
        }
}