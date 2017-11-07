﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SessionModule.DomainModels;

namespace SessionModule
{
    public class SessionServices
    {
        readonly ITokenGenerator tokenGenerator;

        readonly ConcurrentDictionary<string, UserSession> sessions =
            new ConcurrentDictionary<string, UserSession>();

        static readonly Dictionary<Credential, string> users =
            new Dictionary<Credential, string>
            {
                {new Credential("nancy", "1111aaaa"), "Nancy Gilbert"},
                {new Credential("kayla", "1111aaaa"), "Kayla Logan"}
            };

        public SessionServices(ITokenGenerator tokenGenerator)
        {
            this.tokenGenerator = tokenGenerator;
        }

        public string Create(Credential credential)
        {
            #region Please implement the method
            if(!users.ContainsKey(credential)) return null;
            string name = users[credential];
            string token = tokenGenerator.GenerateToken();
            return !sessions.TryAdd(token, new UserSession(name)) ? null : token;
            // This class will try validating the credential. If it is valid, it
            // should store the user session using a token, then you should be able to
            // find the user session via the token.
            //
            // The generated token will also be used as the returned value. Please note
            // that if the credential does not exist, it should return null.

            #endregion
        }

        public UserSession Get(string token)
        {
            UserSession session;
            bool getSessionSuccess = sessions.TryGetValue(token, out session);
            if (!getSessionSuccess) { return null; }
            return session;
        }

        public bool Delete(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;
            UserSession session;
            return sessions.TryRemove(token, out session);
        }
    }
}