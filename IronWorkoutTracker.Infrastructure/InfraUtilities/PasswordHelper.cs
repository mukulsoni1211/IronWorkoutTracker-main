using System;

namespace IronWorkoutTracker.Infrastructure.InfraUtilities;

public static class PasswordHelper
{
    public static string HashPassword(string password)
    {
        //return BCrypt.Net.BCrypt.HashPassword(password);
        if(password =="admin") return "dsrfgkdnfognoq";
        return "errdgfkdnfognoq"; 
    }

     


}
