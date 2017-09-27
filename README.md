# shoddy-steganographer
Hacky project to encode/decode messages to/from vowel triplets.

## Build
https://www.microsoft.com/net/download/core
On root, "dotnet build"

## Usage
Only try to encrypt chars from 65-190 in the UTF-32 table.

### Encrypt
dotnet run "E" "Hello_world" 

This will output something like IEAEIEOOEOOEEUEAEEUAIEUEUUEOOEAIE.
The challenge (which I'm terrible at, is to fill a phrase/paragraph with other non-vowel characters to create a decent sentence).
*There might be a nice way to do this automatically?!*

### Decrypt
dotnet run "D" "I went and their explooded on others estudes as elephua in get use uthue otto ealinge"

This will output Hello_world
(When parsing, it will strip out all but the vowels, case insensitive)
