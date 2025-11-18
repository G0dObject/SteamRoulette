using Microsoft.AspNetCore.Identity;

namespace SteamRoulette.Domain
{
    public class SteamUser : IdentityUser<int>
    {
        public string SteamUserId { get; set; } = string.Empty;

        public string? ImgUrl { get; set; }
        public string? Name { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastSeens { get; set; }

        public class Builder
        {
            private readonly SteamUser _user;

            public Builder()
            {
                _user = new SteamUser();
            }

            public Builder WithSteamUserId(string steamUserId)
            {
                _user.SteamUserId = steamUserId;
                return this;
            }

            public Builder WithImgUrl(string imgUrl)
            {
                _user.ImgUrl = imgUrl;
                return this;
            }

            public Builder WithName(string name)
            {
                _user.Name = name;
                return this;
            }

            public Builder WithCreatedAt(DateTime createdAt)
            {
                _user.CreatedAt = createdAt;
                return this;
            }

            public Builder WithLastSeen(DateTime lastSeen)
            {
                _user.LastSeens = lastSeen;
                return this;
            }

            public Builder WithUsername(string username)
            {
                _user.UserName = username;
                return this;
            }

            public Builder WithEmail(string email)
            {
                _user.Email = email;
                return this;
            }

            public Builder WithSteamId(string steamId)
            {
                _user.SteamUserId = steamId;
                return this;
            }

            public SteamUser Build()
            {
                // Validate the user before returning
                if (string.IsNullOrEmpty(_user.UserName) || string.IsNullOrEmpty(_user.SteamUserId))
                {
                    throw new InvalidOperationException("Username and SteamId are required.");
                }

                return _user;
            }
        }
    }
}