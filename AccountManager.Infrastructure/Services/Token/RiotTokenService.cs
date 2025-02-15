﻿using AccountManager.Core.Interfaces;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;

namespace AccountManager.Infrastructure.Services.Token
{
    public sealed class RiotTokenService : ITokenService
    {
        private readonly IGeneralFileSystemService _iOService;

        public RiotTokenService(IGeneralFileSystemService iOService)
        {
            _iOService = iOService;
        }

        public bool TryGetPortAndToken(out string token, out string port)
        {
            port = "";
            token = "";
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var fileName = $@"{appDataPath}\Riot Games\Riot Client\Config\lockfile";
            if (!_iOService.IsFileLocked(fileName))
                return false;

            using (FileStream fileStream = new(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader fileReader = new(fileStream))
            {
                if (!fileReader.EndOfStream)
                {
                    var lockfileContents = fileReader.ReadLine();
                    if (lockfileContents == null)
                        return false;

                    var riotParams = lockfileContents.Split(":");
                    token = riotParams[3];
                    port = riotParams[2];
                    return true;
                }
            }

            return false;
        }
    }
}
