# KingUtils

A collection of miscellaneous extension methods and utility classes.

## Features

Here's a quick overview of what the library adds:

```csharp
// Base16 encoding/decoding:
var base16String = Base16.Encode(byteArrayOrByteSpan);
var base16Bytes = Base16.Decode(base16String);

// Base32 encoding/decoding (Crockford, WordSafe, Rfc4648):
var base32String = Base32.Crockford.Encode(byteArrayOrByteSpan);
var base32Bytes = Base32.Crockford.Decode(base32String);

// String (and string-related) extensions:
var truncatedString = longString.Truncate(80);
var truncatedStringWithSuffix = longString.Truncate(80, "...");
var joinedString = (new[] { "one", "two", "three" }).ToJoinedString(coordinatingConjunction: "and", serialComma: true); // "one, and two, and three"

// Simple string encryption and decryption:
var encryptedValue = CryptoHelper.EncryptString("string to encrypt", "password", 16, 100_000);
var decryptedValue = CryptoHelper.DecryptString(encryptedValue, "password"); // Salt and iterations are encapsulated in the encrypted value, so don't need to provide them.

// Date and time extensions:
var humanReadableDateTime = dateTime.ToRelativeHumanReadableString(); // "4 hours and 15 minutes ago"
var humanReadableTimeSpan = timeSpan.ToHumanReadableString(); // "8 minutes and 20 seconds"

// Byte array extensions:
var hexString = byteArray.ToHexString();

// Dictionary extensions:
var value = dictionary.GetValueOrDefault("key", defaultValue);

// Task extensions:
task.FireAndForget();

// Enumerable extension polyfills for NET5 and earlier.
var chunks = enumerable.Chunk(4);
```

## Installation

Just grab it from [NuGet](https://www.nuget.org/packages/KingUtils/)

```
PM> Install-Package KingUtils
```

```
$ dotnet add package KingUtils
```

## License and copyright

Copyright Matthew King.

Distributed under the [MIT License](http://opensource.org/licenses/MIT).

Refer to license.txt for more information.
