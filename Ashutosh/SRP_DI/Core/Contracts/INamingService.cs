using System;

namespace Core
{
    public interface INamingService
    {
        bool IsValid(string name);
    }

    public class NamingService : INamingService
    {
        public bool IsValid(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            return true;
        }
    }
}
