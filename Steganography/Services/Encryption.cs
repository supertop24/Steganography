using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Steganography.Services
{
    public class Encryption
    {
        //Public and Private Keys for User
        private static readonly string userPublicKey= @"
<RSAKeyValue>
<Modulus>6J4DB1+gchk0saU/84LbxowZPyvqXM2d8TQE5jSB4vPpCaxvZnWSmCO2esfp6MHhZBoH1Q0gbmkhDdeaLB/ghFCKqzFXIpZRqoa0Smmck+tpwMmtu3caOfLktUkwj2BXQYwYC4YfIzhpbyB0+xBYyu+SXdmJg1C/SbWOjaSJPPutb6IkzfrqPyfZLYOv73LHR1lUILKSA2nG4M77c70lv/wyECiYoPBdzaYqi6aNfZC7GakHasrnssukSBKkrfk6DgEMqurg5Z24yZD9SVX4VIA49pxdt5HyHGVg8a6rGWgMGpS7WXFJv57/jgcxI2RB1Efy++sO3xr9SuZshB2PMQ==</Modulus>
<Exponent>AQAB</Exponent>
</RSAKeyValue>";
        private static readonly string userPrivateKey = @"
<RSAKeyValue>
<Modulus>6J4DB1+gchk0saU/84LbxowZPyvqXM2d8TQE5jSB4vPpCaxvZnWSmCO2esfp6MHhZBoH1Q0gbmkhDdeaLB/ghFCKqzFXIpZRqoa0Smmck+tpwMmtu3caOfLktUkwj2BXQYwYC4YfIzhpbyB0+xBYyu+SXdmJg1C/SbWOjaSJPPutb6IkzfrqPyfZLYOv73LHR1lUILKSA2nG4M77c70lv/wyECiYoPBdzaYqi6aNfZC7GakHasrnssukSBKkrfk6DgEMqurg5Z24yZD9SVX4VIA49pxdt5HyHGVg8a6rGWgMGpS7WXFJv57/jgcxI2RB1Efy++sO3xr9SuZshB2PMQ==</Modulus>
<Exponent>AQAB</Exponent>
<P>79xRamv8XSMWGgBd8qIKrRLLjrQPZn+oC7B0ACayEUAGAIb8FIakKUQNim6V6ewI+TJZFRJe72lEmb2M9LCWVqE95jhVvDBujdpC16Ta6QShGgBuCOQe9QeaJ9CY6VisAu6dP5afRFD5tKTXfofJvdo6hOvfwpuPcBfaovvfcRc=</P>
<Q>+ETsjGWcmnLhyKzBTonTOsBCZjpug0CdFeOqdRqkuMSxgjcE2hCCsTrmacBQKfxnRwl6VTsImDh1luF+/dEQ+b0JhBazz1qAtBv7cxB6dmkdNS3DIHhZFKX+ITuZjdsbU68ZdTORvV4SS6iLkr5ejU+zWEUGHWfzfvBI2YKnXvc=</Q>
<DP>bN1RMn0aiGcxU9HQ0hwLvmvmabl9t+a+rbeDnsHxVfT6BGk/pk1J2tThVHBGqGoR1JCub+rrnbvof+LRRAmV4nHCd2ggOY69zrI+XkObe+E3AWcqieyHxGyT4fhCPr6ZK6eDRWYmmUorW3rpt6sHvJx8rPGlmF0kaBrbh9fLnTc=</DP>
<DQ>sNWnB3l4yin52E30glR5N+epY9dK0AI94VGAIFkR6ulu5ij6M7h+3m3toHVyo/U8OsTtdbfOr13Ho+iJ44/+X2PeW08wVAlKkv87YwHhuGW2gCJQUdhm2uZA1Lr27KucE0ctAuXwcMmIjotGnwcpoc2bGMxRRrC8JzZPLV56iJE=</DQ>
<InverseQ>NUxzwnr7zsOKHBbBkEwHa/BSWG26hd+BNxTZkOHgTpvPEIdmZEn+DLyUvok1WH2NiDZJN/E8FOv5uVcP9fo0caopSfeggV5qcC/J+OKAst1eWmSR25EOrc6KbquqDuGxexRnspOGppCI0qIerVM0zC2o6PWTg2dz0WR+ju87yns=</InverseQ>
<D>fyLVjvMh8xp5pVonEMWFrwelSNSMu/oZh9ohj1xFXTBoPsRDAqHh5EsokgSvgGpu8NIVU8lf31iQG0uqVNF7WXXXxGcsM5gqvGPxJ/Z/AI2a+CaCUMJw0BlEDR0acFJ8fjO+dJXycMKFRZpctVei4A9+VNLviJjaY8PaTOoPpAbzYrv4MkeqY/P16wh5TWYRlzMWrY8AI+pkefzE68jr+/c3R1SsZ7r+jEV01s7P5MSiwpnczFIjEFEUgN5LBVQjv8paxZLrnqtaAHfjaGbHKt/zEXoeXGhVlDF+DQwMJUXny35h4WuFt6FP5xmM3NLIJyUjYhnMGXMK4ExA+R0CDQ==</D>
</RSAKeyValue>";
        //Public and Private Keys for Image
        private static readonly string imagePublicKey = @"
<RSAKeyValue>
<Modulus>mwvfdh7L5kxDsfogBiMehMoDUnT9IpCDpfkf4R8aB6AYDRKl+SjDE8GZY//s94lwYOMB1+6w1JA8lE6pfDJomMnMSHFlvY7YTZCtzZFvrQhthqc3GfgXe9gcTG81nl6JuDrvFybatTpoF+6BQFm5yLPDDxsu4xyB2EcMJRl3opORMgLV/rPyI5AmrFggwjZ07h21L6U8aAYaeQgdtaN4cZA6lkxPiiY2EZIevrYNF8SvIBa0bAdElpxY1O95P85U40Wjffs1SQ6zctlzEpoK7vtSEdt4u/GHp51yAJ/FTPRBZVGMMWn+TLYx81SccfVW+KwEhmy72fVBgNhA96dZYQ==</Modulus>
<Exponent>AQAB</Exponent>
</RSAKeyValue>";
        private static readonly string imagePrivateKey = @"
<RSAKeyValue>
<Modulus>mwvfdh7L5kxDsfogBiMehMoDUnT9IpCDpfkf4R8aB6AYDRKl+SjDE8GZY//s94lwYOMB1+6w1JA8lE6pfDJomMnMSHFlvY7YTZCtzZFvrQhthqc3GfgXe9gcTG81nl6JuDrvFybatTpoF+6BQFm5yLPDDxsu4xyB2EcMJRl3opORMgLV/rPyI5AmrFggwjZ07h21L6U8aAYaeQgdtaN4cZA6lkxPiiY2EZIevrYNF8SvIBa0bAdElpxY1O95P85U40Wjffs1SQ6zctlzEpoK7vtSEdt4u/GHp51yAJ/FTPRBZVGMMWn+TLYx81SccfVW+KwEhmy72fVBgNhA96dZYQ==</Modulus>
<Exponent>AQAB</Exponent>
<P>wiatWmeC9g+doFWNKaNsksMIiF7HU43RNUNOdcQAOd+q6OjN83SiWQkaE0GxJ1zIJUOxS4Skj166/nShxCwPMBNFZ78n7ZzRmwKH9fZn7SSATu6ytVRVFIezPJzCres9fqvH300RgU0GCv60slSoMUJJk52FtA74g7h+tOOZv98=</P>
<Q>zHAlcj/spVSZIZ9M2uYCHCdwFaZ0q9/kKEtHDKmIGztpl+YWF6RjAsSpjDj7s6CL4WEj8yt8DSpwdMrAVcr1piCzG+C+9ECtYrVrdMYTAa+kI7U/jBKVZ/Gcv7nhMUlk+Ixdi43Q2D27hDByMnYnlmcPtAAOdK4uvBG8yaLtDr8=</Q>
<DP>YwkDqHfgr89rYlwBc9nvCjX+ZkGif5Z5vyekICJo0xyqu7/1Pliib7Ra3rPFwARt+8Q57gGtR4zU6fNoiP7IiIdsAe3aWjSCHwX9gJO+k5Lo+Fp+QkzQUXafES4NiFXUuoa5n8haQ5CcooMmfUj9upLLoUba6uwwDFDSIUdwKBU=</DP>
<DQ>uD1hIeOGK0Fgd7KOmr+qqyDHnFtUzgiKH94ne7vVE2WpteD/i/Lz3+zIN7yq6akIJudadK19sIfMrRSD3U15sGvLvpX+wTUAjPKekrBnNJ/LtvqC+INO7kwKTacA0WAphO1K6Je33QlVUr4uTFe7OYpL1pYucO0A5TC/CKCyLxU=</DQ>
<InverseQ>OmPSTbhLy/9s18f6pHYxp4sI65TbToGP+8hQjMWZOlsF7AJna6oR19P6dHVSda3AGXglEUXFvx5fiLtvTtmJD5ARUvBAl9brv1q78p3ya5oTUYJJhTJ5QBpK3b5tF7w+ch6JlPtHk15wLDYrO4gJfdMM7R+Ci8TDakNyHaND6bw=</InverseQ>
<D>hKnoDp3TQb7HvaWTeSzblCt2JDvNzKFyw7UiPfHqx9OIQMdQFJ14LZNduF7nV/bVnVEf5q9pxreT3iJgitBnBzTEAshkkYmEMvo0fMyQjbHEZY72atYI4gLMU+pkKpohpO1oXpr0UFUldDSzi2g6uyx1HpvXxqRBxdkk3ymnYAcnC7pSg8UJpLjMoOTbKQpmt/+ujq91aYlj3AcsLspptjovgBwLU/EUUSiDZKepo036G1yrbJez+6w6BXjtqs4eA8kuF0u1BTf1+3GQeIAuABkTHGyM18grHPdSUpwjCXQSVgyIig2OQBcenlWv6ukMcocM9P87AU6T0LjY9wstMQ==</D>
</RSAKeyValue>";
        public string encryptUser(string password)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(password);
            RSA publicKey = RSA.Create();
            publicKey.FromXmlString(userPublicKey);
            string encryptedPass = Convert.ToBase64String(publicKey.Encrypt(data, RSAEncryptionPadding.Pkcs1));
            return encryptedPass;
        }
        public bool decryptUser(string password, string encryption)
        {
            byte[] encryptedData = Convert.FromBase64String(encryption);
            RSA privateKey = RSA.Create();
            privateKey.FromXmlString(userPrivateKey);
            byte[] decryptedData = privateKey.Decrypt(encryptedData, RSAEncryptionPadding.Pkcs1);
            string decryptedPass = System.Text.Encoding.UTF8.GetString(decryptedData);
            if(decryptedPass==password)
            {
                return true;
            }
            return false;
        }
        public string encryptMessage(string message)
        {
            byte[] data = Convert.FromBase64String(message);
            RSA publicKey = RSA.Create();
            publicKey.FromXmlString(imagePublicKey);
            byte[] encryptedData = publicKey.Encrypt(data, RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(encryptedData);
        }
        public string decryptMessage(string message)
        {
            byte[] data = Convert.FromBase64String(message);
            RSA privateKey = RSA.Create();
            privateKey.FromXmlString(imagePrivateKey);
            byte[] decryptedData = privateKey.Decrypt(data, RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(decryptedData);
        }
    }
}
