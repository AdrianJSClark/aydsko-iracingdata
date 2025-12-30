using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Aydsko.iRacingData.TestCli;

internal static class TokenStorageService
{
    public static async Task<TokenSaveData?> ReadTokenSaveDataAsync(CancellationToken cancellationToken = default)
    {
        var testCliAppDataFolder = EnsureFolderExists();
        var tokenSaveFile = new FileInfo(Path.Combine(testCliAppDataFolder.FullName, "token.json.dat"));

        if (!tokenSaveFile.Exists)
        {
            return null;
        }

        var encryptedData = await File.ReadAllBytesAsync(tokenSaveFile.FullName, cancellationToken);
        var utf8Bytes = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
        var data = JsonSerializer.Deserialize<TokenSaveData>(utf8Bytes);

        return data;
    }

    public static async Task WriteTokenSaveDataAsync(TokenSaveData tokenSaveData, CancellationToken cancellationToken = default)
    {
        var testCliAppDataFolder = EnsureFolderExists();
        var tokenSaveFilePath = Path.Combine(testCliAppDataFolder.FullName, "token.json.dat");

        var dataToSave = JsonSerializer.Serialize(tokenSaveData);
        var utf8Bytes = Encoding.UTF8.GetBytes(dataToSave);
        var protectedBytes = ProtectedData.Protect(utf8Bytes, null, DataProtectionScope.CurrentUser);

        using var tokenSaveFileStream = new FileStream(tokenSaveFilePath, FileMode.OpenOrCreate);
        await tokenSaveFileStream.WriteAsync(protectedBytes, cancellationToken);
        await tokenSaveFileStream.FlushAsync(cancellationToken);
    }

    private static DirectoryInfo EnsureFolderExists()
    {
        var localAppData = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
        var testCliAppDataFolder = new DirectoryInfo(Path.Combine(localAppData.FullName, typeof(Program).Assembly.GetName()!.Name!));

        if (!testCliAppDataFolder.Exists)
        {
            testCliAppDataFolder.Create();
        }

        return testCliAppDataFolder;
    }
}
