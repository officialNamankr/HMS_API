﻿using HMS_API.Models;

namespace HMS_API.Services.TokenGenerators
{
    public class RefreshTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        private readonly TokenGenerator _tokenGenerator;
        public RefreshTokenGenerator(AuthenticationConfiguration configuration, TokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }
        public string GenerateToken()
        {
            return _tokenGenerator.GenerateToken(
                _configuration.RefreshTokenSecret,
                _configuration.Issuer,
                _configuration.Audience,
                _configuration.RefreshTokenExpirationMinutes
                );
        }
    }
}
